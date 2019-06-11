using Giselle.Commons;
using MCProtocol.Test.BitNumbers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCSlaveConnection
    {
        private MCSlaveServer Parent = null;
        private TcpClient Client = null;
        private NetworkStream Stream = null;

        private readonly object StateLock = new object();

        private readonly object ConnectionLock = new object();
        private Thread ConnectionThread = null;
        private bool Closing = false;

        private MCLogger Logger = null;


        public MCSlaveConnection(MCSlaveServer parent, TcpClient client)
        {
            this.Parent = parent;
            this.Client = client;
            this.Stream = client.GetStream();
            this.Logger = new MCLogger(parent.Logger);
        }

        public string Name
        {
            get
            {
                return this.Logger.Name;
            }

            set
            {
                this.Logger.Name = value;
            }

        }

        private MCErrorCode ProcessPacket(MCFunction request, out MCFunction response)
        {
            if (request is MCFunctionBatchReadRequest)
            {
                var req = (MCFunctionBatchReadRequest)request;
                var res = new MCFunctionBatchReadResponse(req);

                res.Data = this.Parent.Slave.MemoryMap.Get(req.DeviceCode, req.Offset, req.Count);

                response = res;
                return MCErrorCode.None;
            }
            else if (request is MCFunctionBatchWriteRequest)
            {
                var req = (MCFunctionBatchWriteRequest)request;
                var res = new MCFunctionBatchWriteResponse(req);

                this.Parent.Slave.MemoryMap.Set(req.DeviceCode, req.Offset, req.Data);

                response = res;
                return MCErrorCode.None;
            }

            response = null;
            return MCErrorCode.UnkownRequest;
        }

        private MCQHeaderResponse CreateResponseQHeader(MCQHeader request, MCErrorCode errorCode)
        {
            var value = new MCQHeaderResponse();
            value.IONumber = request.IONumber;
            value.NetworkNumber = request.NetworkNumber;
            value.PLCNumber = request.PLCNumber;
            value.StationNumber = request.PLCNumber;
            value.ResultCode = (ushort)errorCode;

            return value;
        }

        private MCFunction GetResponseFunction(MCQHeader qHeader, MCErrorCode errorCode, MCFunction request, MCFunction response)
        {
            if (errorCode == MCErrorCode.None)
            {
                return response;
            }
            else
            {
                var f = new MCFunctionErrorResponse();
                f.IONumber = qHeader.IONumber;
                f.NetworkNumber = qHeader.NetworkNumber;
                f.PLCNumber = qHeader.PLCNumber;
                f.StationNumber = qHeader.PLCNumber;

                if (request != null)
                {
                    f.CommandCode = MCFunctionRegistry.Find(request.GetType()).Id;
                    f.SubCommandCode = request.GetSubCommandCode();
                }
                else
                {
                    f.CommandCode = 0;
                    f.SubCommandCode = 0;
                }

                return f;
            }

        }

        private void Connection()
        {
            try
            {
                while (true)
                {
                    using (var sw = new StreamWrapper(this.Stream))
                    {
                        try
                        {
                            MCPacket request = null;
                            MCErrorCode errorCode = MCErrorCode.None;
                            MCFunction responseFunction = null;

                            try
                            {
                                request = this.Parent.Slave.Protocol.ReadPacket(sw, null);
                                errorCode = this.ProcessPacket(request.Function, out responseFunction);
                            }
                            catch (MCProtocolException e)
                            {
                                errorCode = e.ErrorCode;
                                responseFunction = null;
                            }
                            catch (IOException)
                            {
                                throw;
                            }
                            catch (ThreadInterruptedException)
                            {
                                throw;
                            }
                            catch (Exception)
                            {
                                errorCode = MCErrorCode.UnkownRequest;
                                responseFunction = null;
                            }

                            var response = new MCPacket();
                            response.SubHeader = request.SubHeader;
                            response.QHeader = this.CreateResponseQHeader(request.QHeader, errorCode);
                            response.Function = this.GetResponseFunction(request.QHeader, errorCode, request.Function, responseFunction);
                            this.Parent.Slave.Protocol.WritePacket(sw, response);
                        }
                        finally
                        {
                            var mco = this.Parent.Slave;
                            var datas = mco.MemoryMap.Get(DeviceCode.D, 500, 32);
                            Console.Clear();

                            Console.WriteLine("status : " + datas[3]);

                            var position = new BitNumber2Words(new BitShort(), new BitShort());
                            position.UpperWord.Raw32 = datas[4];
                            position.LowerWord.Raw32 = datas[5];
                            Console.WriteLine("Position : " + position.Value);

                            var force = new BitNumber2Words(new BitShort(), new BitShort());
                            force.UpperWord.Raw32 = datas[6];
                            force.LowerWord.Raw32 = datas[7];
                            Console.WriteLine("Force : " + force.Value);

                            for (int i = 0; i < datas.Length; i ++)
                            {
                                //Console.WriteLine(i.ToString("D2") + " : " + datas[i].ToString("X4"));
                            }

                            //this.Logger.OnCommunicationLog(sw.Log);
                        }

                    }

                }

            }
            catch (Exception e)
            {
                this.Logger.OnMessageLog(e.ToString());
            }

            this.Close();
        }

        public void Open()
        {
            try
            {
                this.Logger.OnMessageLog("Opening");

                Thread thread = null;

                lock (this.ConnectionLock)
                {
                    this.ConnectionThread = thread = new Thread(this.Connection);
                    thread.Start();
                }

                this.Logger.OnMessageLog("Opened");
            }
            catch (Exception e)
            {
                this.Logger.OnMessageLog(e.ToString());

                this.Close();
            }

        }

        public void Close()
        {
            if (this.Closing == true)
            {
                return;
            }

            lock (this.ConnectionLock)
            {
                this.Logger.OnMessageLog("Closing");

                try
                {
                    this.Closing = true;

                    ObjectUtils.DisposeQuietly(this.Stream);
                    this.Stream = null;

                    ObjectUtils.DisposeQuietly(this.Client);
                    this.Client = null;

                    ThreadUtils.InterruptAndJoin(this.ConnectionThread);
                    this.ConnectionThread = null;

                }
                finally
                {
                    this.Closing = false;
                }

                this.Logger.OnMessageLog("Closed");
            }

        }

    }

}
