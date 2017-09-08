using System;
using System.Net;
using System.Collections.Generic;
using System.Collections;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Pdu;

namespace JulMar.Smpp.Smsc
{
    /// <summary>
    /// Summary description for SmppServer.
    /// </summary>
    public class SmppServer
    {
        private SocketClient inSock_ = null;
        private List<SmscSession> clients_ = new List<SmscSession>();
        private string systemid_ = "";

        /// <summary>
        /// This event is fired when a new client connects to the server; this is prior
        /// to the BIND event being generated.  The receiver must then connect to any
        /// required events on the session itself in order to receive further events.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnNewSession;

        /// <summary>
        /// Constructor for the SMPP server
        /// </summary>
        public SmppServer()
        {
        }

        /// <summary>
        /// Constructor for the SMPP server
        /// </summary>
        /// <param name="sid">System ID which identifies the SMSC</param>
        public SmppServer(string sid)
        {
            systemid_ = sid;
        }

        /// <summary>
        /// The system id is used to identify this SMSC.
        /// </summary>
        public string SystemId
        {
            get { return systemid_; }
            set { systemid_ = value; }
        }

        /// <summary>
        /// This method is called to start the SMPP server
        /// </summary>
        /// <param name="port">Port number to listen on</param>
        /// <returns>True/False result code</returns>
        public void StartServer(int port)
        {
            StartServer(IPAddress.Any, port);
        }

        /// <summary>
        /// This method is called to start the SMPP server
        /// </summary>
        /// <param name="ipAddress">Specific local address to listen on (for multi-homed systems).</param>
        /// <param name="port">Port number to listen on</param>
        /// <returns>True/False result code</returns>
        public void StartServer(IPAddress ipAddress, int port)
        {
            StartServer(ipAddress, port, 50);
        }

        /// <summary>
        /// This method is called to start the SMPP server
        /// </summary>
        /// <param name="ipAddress">Specific local address to listen on (for multi-homed systems).</param>
        /// <param name="port">Port number to listen on</param>
        /// <param name="maxConnections">Maximum number of inbound connections</param>
        /// <returns>True/False result code</returns>
        public void StartServer(IPAddress ipAddress, int port, int maxConnections)
        {
            inSock_ = SocketManager.StartServer(ipAddress, port, maxConnections, Pdu.SmppPdu.REQUIRED_SIZE);
            inSock_.OnStartSession += OnStartSession;
        }

        /// <summary>
        /// This method stops the SMPP server; all existing connections are dropped.
        /// </summary>
        public void StopServer()
        {
            SocketManager.StopServer(inSock_.Port);
        }

        /// <summary>
        /// This property returns the current sessions.
        /// </summary>
        /// <value></value>
        [CLSCompliant(false)]
        public IList<SmscSession> CurrentSessions
        {
            get
            {
                return clients_.AsReadOnly();
            }
        }

        /// <summary>
        /// This method allows the SMSC to signal an ESME to originate a bind_receiver request to the SMSC.
        /// </summary>
        /// <param name="ipAddress">IP address of the ESME</param>
        /// <param name="port">Port number to connect to</param>
        /// <param name="password">Password (may be blank for none)</param>
        static public void Outbind(IPAddress ipAddress, int port, string password)
        {
        }

        /// <summary>
        /// This method is invoked when a new inbound socket hits our server.  We will create a new
        /// SMPP session object to manage the socket and begin communicating with the ESME session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void OnStartSession(object sender, SocketEventArgs args)
        {
            ConnectEventArgs connEv = (ConnectEventArgs)args;
            SocketClient scNew = connEv.Client;

            // Create a new SMPP session to manage this connection.
            SmscSession newSession = new SmscSession(this, scNew, systemid_);

            // Back-link the session
            scNew.Tag = newSession;

            // Notify anyone who wants to know
            SmppConnectEventArgs sce = new SmppConnectEventArgs(scNew.Address, newSession);
            if (OnNewSession != null)
                OnNewSession(this, sce);
            if (!sce.AllowConnection)
                scNew.Close(true);
            else
            {
                scNew.OnEndSession += OnEndClientSession;
                lock (clients_)
                {
                    clients_.Add(newSession);
                }
            }
        }

        /// <summary>
        /// This method is called when a client socket disconnects.  We need to remove the session from
        /// our socket list; it will remain around until it disconnects from the socket notify list.
        /// </summary>
        /// <param name="sender">Socket notify</param>
        /// <param name="args">Disconnect args</param>
        protected void OnEndClientSession(object sender, SocketEventArgs args)
        {
            SocketClient sock = (SocketClient)sender;
            DisconnectEventArgs de = (DisconnectEventArgs)args;
            SmscSession session = sock.Tag as SmscSession;
            sock.Tag = null;
            if (session != null)
                EndSession(session);
        }

        /// <summary>
        /// This is used to destroy a session during the unbind process.
        /// </summary>
        /// <param name="session">Session being ended</param>
        internal void EndSession(SmscSession session)
        {
            lock (clients_)
            {
                clients_.Remove(session);
            }
        }
    }
}
