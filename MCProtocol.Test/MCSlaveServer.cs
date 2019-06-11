using Giselle.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCSlaveServer
    {
        private MCObjectSlave _Slave = null;
        public MCObjectSlave Slave { get { return this._Slave; } }

        private int _Port = 0;
        public int Port { get { return this._Port; } set { this._Port = value; } }

        private readonly object StateLock = new object();
        private bool Started = false;
        private Thread AccepterThread = null;

        private List<MCSlaveConnection> Connections = null;
        private bool Closing = false;

        private readonly object ConnectingLock = new object();
        private TcpListener Listener = null;

        private MCLogger _Logger = null;
        public MCLogger Logger { get { return this._Logger; } }

        private int NextId = 0;

        public MCSlaveServer(MCObjectSlave parent)
        {
            this._Slave = parent;
            this.Connections = new List<MCSlaveConnection>();
            this._Logger = new MCLogger(parent.Logger);
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

        private void Accepter()
        {
            TcpListener listener = null;

            try
            {
                listener = new TcpListener(IPAddress.Any, this.Port);
                this.Listener = listener;
                listener.Start();

                this.Logger.OnMessageLog("Accepting");

                while (true)
                {
                    var client = listener.AcceptTcpClient();
                    this.Logger.OnMessageLog("Accpeted");

                    lock (this.Connections)
                    {
                        var c = new MCSlaveConnection(this, client);
                        c.Name = "CS" + (this.NextId ++);
                        c.Open();

                        this.Connections.Add(c);
                    }

                }

            }
            catch (Exception e)
            {
                this.Logger.OnMessageLog(e.ToString());
            }

        }

        public void Start()
        {
            lock (this.StateLock)
            {
                this.Stop();

                this.Started = true;

                this.Logger.OnMessageLog("Starting");

                this.AccepterThread = new Thread(this.Accepter);
                this.AccepterThread.Start();

                this.Logger.OnMessageLog("Started");
            }

        }

        public void Stop()
        {
            lock (this.StateLock)
            {
                if (this.Started == true)
                {
                    this.Logger.OnMessageLog("Stopping");

                    var listener = this.Listener;

                    if (listener != null)
                    {
                        listener.Stop();
                    }

                    ThreadUtils.InterruptAndJoin(this.AccepterThread);
                    this.AccepterThread = null;
                    
                    lock ( this.Connections)
                    {
                        foreach (var c in this.Connections)
                        {
                            c.Close();
                        }

                    }

                    this.Logger.OnMessageLog("Stopped");

                    this.Started = false;
                }

            }

        }

    }

}
