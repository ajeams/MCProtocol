using Giselle.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCObjectMaster : MCObject
    {
        private string Hostname = null;
        private int Port = 0;

        private readonly object StateLock = new object();
        private bool Started = false;
        private Thread ConnectionStarter = null;

        private readonly object ConnectionLock = new object();
        private TcpClient Client = null;
        private NetworkStream Stream = null;
        private Thread ConnectionThread = null;
        private bool Connected = false;
        private bool Closing = false;

        private readonly object ConnectingLock = new object();
        private TcpClient ConnetingClient = null;

        private List<Command> Commands = null;
        private ManualResetEventSlim CommandsEvent = null;

        public MCObjectMaster()
        {
            this.Commands = new List<Command>();
            this.CommandsEvent = new ManualResetEventSlim(false);
        }

        public override void Bind(MCCommSettings settings)
        {
            var settings2 = (MCCommSettingsMaster)settings;
            this.Hostname = settings2.Hostname;
            this.Port = settings2.Port;
        }

        public override bool ExecuteCommand(Command command)
        {
            if (this.Connected == false)
            {
                return false;
            }

            lock (this.Commands)
            {
                this.Commands.Add(command);

                this.CommandsEvent.Set();
            }

            return true;
        }

        private void Connection()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        this.CommandsEvent.Wait();
                    }
                    catch (ThreadInterruptedException)
                    {
                        break;
                    }

                    Command[] commands = null;

                    lock (this.Commands)
                    {
                        commands = this.Commands.ToArray();
                        this.Commands.Clear();

                        this.CommandsEvent.Reset();
                    }

                    foreach (var command in commands)
                    {
                        command.Execute(this);
                    }

                }

            }
            catch (Exception e)
            {
                this.Logger.OnMessageLog(e.ToString());
            }

            this.Close();
        }

        private void Open()
        {
            TcpClient client = null;

            try
            {
                this.Logger.OnMessageLog("Opening");

                lock (this.ConnectingLock)
                {
                    client = new TcpClient();
                    this.ConnetingClient = client;
                }

                client.Connect(this.Hostname, this.Port);

                Thread thread = null;

                lock (this.ConnectionLock)
                {
                    this.Client = client;
                    this.Stream = client.GetStream();
                    this.ConnectionThread = thread = new Thread(this.Connection);
                    this.Connected = true;
                    thread.Start();
                }

                this.Logger.OnMessageLog("Opened");
            }
            catch (Exception e)
            {
                this.Logger.OnMessageLog(e.ToString());

                ObjectUtils.DisposeQuietly(client);
                this.Close();
            }


        }

        private void Close()
        {
            lock (this.ConnectingLock)
            {
                ObjectUtils.DisposeQuietly(this.ConnetingClient);
                this.ConnetingClient = null;
            }

            if (this.Closing == true)
            {
                return;
            }

            lock (this.ConnectionLock)
            {
                this.Logger.OnMessageLog("Closing");

                try
                {
                    this.Connected = false;
                    this.Closing = true;

                    ThreadUtils.InterruptAndJoin(this.ConnectionThread);
                    this.ConnectionThread = null;

                    ObjectUtils.DisposeQuietly(this.Stream);
                    this.Stream = null;

                    ObjectUtils.DisposeQuietly(this.Client);
                    this.Client = null;
                }
                finally
                {
                    this.Closing = false;
                }

                this.Logger.OnMessageLog("Closed");
            }

        }

        private void StartConnection()
        {
            this.Open();
        }

        public override void Start()
        {
            lock (this.StateLock)
            {
                this.Stop();

                this.Started = true;

                this.Logger.OnMessageLog("Starting");

                this.ConnectionStarter = new Thread(this.StartConnection);
                this.ConnectionStarter.Start();

                this.Logger.OnMessageLog("Started");
            }

        }

        public override void Stop()
        {
            lock (this.StateLock)
            {
                if (this.Started == true)
                {
                    this.Logger.OnMessageLog("Stopping");

                    this.Close();

                    ThreadUtils.InterruptAndJoin(this.ConnectionStarter);
                    this.ConnectionStarter = null;

                    this.Logger.OnMessageLog("Stopped");

                    this.Started = true;
                }

            }

        }


        protected void SetupQHeader(MCQHeader qHeader)
        {
            qHeader.PLCNumber = 0xFF;
            qHeader.IONumber = 0x03FF;
            qHeader.StationNumber = 0;
            qHeader.NetworkNumber = 0;
        }

        private MCPacket CreatePacket()
        {
            var packet = new MCPacket();

            var subHeader = new MCSubHeader3E();
            packet.SubHeader = subHeader;

            var qHeader = new MCQHeaderRequest();
            this.SetupQHeader(qHeader);
            qHeader.Watchdog = 0;
            packet.QHeader = qHeader;

            return packet;
        }

        public override void SetMemoryRequest(DeviceCode code, ushort offset, ushort[] values)
        {
            var request = this.CreatePacket();
            var requestFunction = new MCFunctionBatchWriteRequest();
            requestFunction.DeviceCode = code;
            requestFunction.Offset = offset;
            requestFunction.Data = values;
            request.Function = requestFunction;

            using (var sw = new StreamWrapper(this.Stream))
            {
                try
                {
                    this.Protocol.WritePacket(sw, request);
                    var response = this.Protocol.ReadPacket(sw, request.Function);

                    if (response.Function is MCFunctionBatchWriteResponse responseFunction)
                    {
                        this.Logger.OnMessageLog("Set : " + code.ToString() + offset + ".." + values.Length + "=" + ObjectUtils.ToString(values));
                    }
                    else
                    {
                        this.Logger.OnMessageLog(((MCQHeaderResponse)response.QHeader).ResultCode.ToString("X4"));
                    }

                }
                finally
                {
                    this.Logger.OnCommunicationLog(sw.Log);
                }

            }

        }

        public override void GetMemoryRequest(DeviceCode code, ushort offset, ushort count)
        {
            var request = this.CreatePacket();
            var requestFunction = new MCFunctionBatchReadRequest();
            requestFunction.DeviceCode = code;
            requestFunction.Offset = offset;
            requestFunction.Count = count;
            request.Function = requestFunction;

            using (var sw = new StreamWrapper(this.Stream))
            {
                try
                {
                    this.Protocol.WritePacket(sw, request);
                    var response = this.Protocol.ReadPacket(sw, request.Function);

                    if (response.Function is MCFunctionBatchReadResponse responseFunction)
                    {
                        this.Logger.OnMessageLog("Get : " + code.ToString() + offset + ".." + count + "=" + ObjectUtils.ToString(responseFunction.Data));
                    }
                    else
                    {
                        this.Logger.OnMessageLog("Err : " + ((MCQHeaderResponse)response.QHeader).ResultCode.ToString("X4"));
                    }

                }
                finally
                {
                    this.Logger.OnCommunicationLog(sw.Log);
                }

            }

        }

    }

}
