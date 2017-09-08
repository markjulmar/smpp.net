using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace JulMar.Smpp.Utility
{
    /// <summary>
    /// This internal structure holds listen information for sockets which are running in
    /// server-side mode.
    /// </summary>
    internal class ListenInfo
    {
        /// <summary>
        /// Maximum nunmber of connections which can be accepted.
        /// </summary>
        public int MaxConnections;

        /// <summary>
        /// Initial read size attempted of any connected socket.
        /// </summary>
        public int ReadSize;

        /// <summary>
        /// Current run state of the listening thread.
        /// </summary>
        public bool IsRunning = false;

        /// <summary>
        /// The thread associated with listening.
        /// </summary>
        public Thread ListenThread = null;

        /// <summary>
        /// An event used to accept sockets.
        /// </summary>
        public ManualResetEvent acceptingEvent_ = new ManualResetEvent(false);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maxConnections">Maximum number of inbound connections allowed</param>
        /// <param name="initialRead">Initial size of the block to read from an accepted socket</param>
        public ListenInfo(int maxConnections, int initialRead)
        {
            MaxConnections = maxConnections;
            ReadSize = initialRead;
        }

        /// <summary>
        /// This starts the listening thread
        /// </summary>
        /// <param name="ts">Starting function for new thread</param>
        public void Start(ThreadStart ts)
        {
            ListenThread = new Thread(ts);
            ListenThread.Start();
        }
    }

    /// <summary>
    /// This class is the concrete data which is passed through the SocketEventArgs
    /// structure for handler notifications.
    /// </summary>
    public class SocketClient : IDisposable
    {
        /// <summary>
        /// This is the default packet size which is queued against sockets.
        /// </summary>
        public const int DefaultReceiveSize = 512;

        /// <summary>
        /// This is the default MaxConnections for listening sockets.
        /// </summary>
        public const int DefaultMaxConnections = 30;

        // Class data
        private ListenInfo listenInfo_ = null;
        private byte[] data_ = null;
        private int usedSize_ = 0;
        private Socket sock_ = null;
        private IPAddress ipAddressPeer_ = null;
        private IPAddress ipAddress_ = null;
        private int port_ = 0;
        private object itemData_ = null;
        private bool isClosed_ = false;

        /// <summary>
        /// This event is fired when the socket is initially connected.
        /// </summary>
        public event EventHandler<SocketEventArgs> OnStartSession;

        /// <summary>
        /// This event is fired when the peer side closes the socket.
        /// </summary>
        public event EventHandler<SocketEventArgs> OnEndSession;

        /// <summary>
        /// This event is fired when a Send() completes and the peer receives the data.
        /// </summary>
        public event EventHandler<SocketEventArgs> OnSendDataComplete;

        /// <summary>
        /// This event is fired when data is received on the socket.
        /// </summary>
        public event EventHandler<SocketEventArgs> OnReceiveData;

        /// <summary>
        /// This event is fired when an error occurs.
        /// </summary>
        public event EventHandler<SocketEventArgs> OnFailure;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SocketClient()
        {
            sock_ = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            InitializeSocket();
        }

        /// <summary>
        /// Internal constructor utilized by the socket manager.
        /// </summary>
        /// <param name="ipAddress">IPAddress to listen on</param>
        /// <param name="port">TCP/IP port</param>
        /// <param name="maxConnections">Max connection count</param>
        /// <param name="initialRead">Initial packet read size</param>
        internal SocketClient(IPAddress ipAddress, int port, int maxConnections, int initialRead)
            : this()
        {
            ipAddress_ = ipAddress;
            port_ = port;
            listenInfo_ = new ListenInfo(maxConnections, initialRead);
            listenInfo_.Start(new ThreadStart(this.ListenThread));
        }

        /// <summary>
        /// Internal constructor utilized by socket manager.
        /// </summary>
        /// <param name="ipAddress">TCP/IP address</param>
        /// <param name="port">TCP/IP port</param>
        /// <param name="buffer">Optional buffer to initially send</param>
        internal SocketClient(string ipAddress, int port, byte[] buffer) : this()
        {
            // Create our new socket.
            port_ = port;
            if (buffer != null && buffer.Length > 0)
            {
                data_ = new byte[buffer.Length];
                Array.Copy(buffer, 0, data_, 0, buffer.Length);
            }
            else
                data_ = null;

            IPAddress ip = ConvertStringToIPAddress(ipAddress);

            // Establish a connection
            sock_.BeginConnect(new IPEndPoint(ip, port), new AsyncCallback(ConnectCallbackHandler), this);
        }

        /// <summary>
        /// Internal constructor when accepting inbound sockets.
        /// </summary>
        /// <param name="sock">New accepted socket</param>
        private SocketClient(Socket sock)
        {
            sock_ = sock;
            InitializeSocket();
        }

        /// <summary>
        /// This method takes an input string and converts it to an IP address or host name
        /// </summary>
        /// <param name="ipAddress">String with correctly formed URL or IP address</param>
        /// <returns>IPAddress</returns>
        private static IPAddress ConvertStringToIPAddress(string ipAddress)
        {
            // Convert the IP address from a string.
            IPAddress ip = null;
            if (ipAddress.Length > 0 && Char.IsDigit(ipAddress, 0))
            {
                ip = IPAddress.Parse(ipAddress);
            }
            else
            {
                IPHostEntry ipe = Dns.GetHostEntry(ipAddress);
                ip = ipe.AddressList[0];
            }
            return ip;
        }

        /// <summary>
        /// This method initializes each socket with specific characteristics for
        /// performance.
        /// </summary>
        private void InitializeSocket()
        {
            if (sock_ != null)
            {
                // Disable the NAGLE send coalescing delay and set the socket to 
                // linger 1 second after close.
                sock_.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, 1);
                sock_.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger,
                    new LingerOption(true, 1));
            }
        }

        /// <summary>
        /// This is used to connect to the peer end.
        /// </summary>
        /// <param name="address">Socket address</param>
        /// <param name="port">Port</param>
        public void Connect(string address, int port)
        {
            if (isClosed_)
                throw new InvalidOperationException("Cannot call Connect on open socket.");

            IPAddress addr = ConvertStringToIPAddress(address);
            port_ = port;

            // Establish a connection
            IAsyncResult ar = sock_.BeginConnect(new IPEndPoint(addr, port), new AsyncCallback(ConnectCallbackHandler), this);
/*
            try
            {
                sock_.EndConnect(ar);
            }
            catch (SocketException ex)
            {
                try
                {
                    if (OnFailure != null)
                        OnFailure(this, new ErrorEventArgs(this, ErrorEventArgs.SocketOperation.Connect, ex));
                }
                catch
                {
                }
            }
 */ 
        }

        /// <summary>
        /// This is used to asynchronously connect to the peer end.
        /// </summary>
        /// <param name="address">Socket address</param>
        /// <param name="port">Port</param>
        public void BeginConnect(string address, int port)
        {
            if (isClosed_)
                throw new InvalidOperationException("Cannot call Connect on open socket.");

            IPAddress addr = ConvertStringToIPAddress(address);
            port_ = port;

            // Establish a connection
            sock_.BeginConnect(new IPEndPoint(addr, port), new AsyncCallback(ConnectCallbackHandler), this);
        }

        /// <summary>
        /// This returns whether the socket is connected.
        /// </summary>
        public bool IsConnected
        {
            get { return sock_.Connected; }
        }

        /// <summary>
        /// Public IDisposable.Dispose implementation
        /// </summary>
        public void Dispose()
        {
            if (sock_ != null)
                ((IDisposable)sock_).Dispose();
        }

        /// <summary>
        /// This returns the address of the socket
        /// </summary>
        public IPAddress Address
        {
            get
            {
                if (ipAddressPeer_ == null)
                {
                    if (sock_ != null)
                    {
                        try
                        {
                            IPEndPoint ep = (IPEndPoint)sock_.RemoteEndPoint;
                            ipAddressPeer_ = ep.Address;
                        }
                        catch (ObjectDisposedException)
                        {
                            return new IPAddress(0);
                        }
                    }
                    else
                        return new IPAddress(0);
                }
                return ipAddressPeer_;
            }
        }

        /// <summary>
        /// This returns the port of the socket
        /// </summary>
        public int Port
        {
            get
            {
                if (port_ == 0 && sock_ != null)
                {
                    try
                    {
                        IPEndPoint ep = (IPEndPoint)sock_.RemoteEndPoint;
                        port_ = ep.Port;
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                }
                return port_;
            }
        }

        /// <summary>
        /// Optional itemdata tag
        /// </summary>
        public object Tag
        {
            get { return itemData_; }
            set { itemData_ = value; }
        }

        /// <summary>
        /// Override of the object.ToString method
        /// </summary>
        /// <returns>Textual representation of socket</returns>
        public override string ToString()
        {
            return string.Format("{0}:{1} Tag={2}", Address, Port, Tag);
        }

        /// <summary>
        /// This method sends data through the socket.
        /// </summary>
        /// <param name="message">String to send</param>
        /// <param name="formatData">Optional format parameters</param>
        public void Send(string message, params object[] formatData)
        {
            if (!isClosed_ && sock_.Connected)
            {
                this.Send(Encoding.ASCII.GetBytes(String.Format(message, formatData)));
            }
        }

        /// <summary>
        /// This method sends a binary block through the socket.
        /// </summary>
        /// <param name="dataBuffer">Buffer to send</param>
        public void Send(byte[] dataBuffer)
        {
            if (!isClosed_ && sock_.Connected)
            {
                sock_.BeginSend(dataBuffer, 0, dataBuffer.Length,
                    SocketFlags.None, new AsyncCallback(SendCallbackHandler), this);
            }
        }

        /// <summary>
        /// This closes the socket
        /// </summary>
        public void Close()
        {
            this.Close(false);
        }

        /// <summary>
        /// This closes the socket
        /// </summary>
        /// <param name="abort">True for abortive close</param>
        public void Close(bool abort)
        {
            // Close the socket.
            if (!isClosed_ && sock_ != null)
            {
                try
                {
                    sock_.Shutdown(SocketShutdown.Both);
                }
                catch (SocketException)
                {
                    abort = true;
                }

                if (abort)
                {
                    try
                    {
                        sock_.Close();
                    }
                    catch (SocketException)
                    {
                    }
                }
            }

            // Shutdown the listening thread if necessary
            if (listenInfo_ != null)
                listenInfo_.acceptingEvent_.Set();

            isClosed_ = true;
        }

        /// <summary>
        /// This method is used to post receive buffers using the memory buffer associated
        /// with this class.
        /// </summary>
        /// <param name="readSize">Read size</param>
        /// <param name="clearBuffer">True/False whether to reset the buffer</param>
        internal void PostReceive(int readSize, bool clearBuffer)
        {
            // Post a read if we have not closed the socket, we want to 
            // read data, and the socket itself is connected to a peer.
            if (!isClosed_ && readSize > 0 && sock_.Connected)
            {
                int pos = ResizeBuffer(readSize, clearBuffer);
                sock_.BeginReceive(data_, pos, readSize, SocketFlags.None, new AsyncCallback(this.ReadCallbackHandler), this);
            }
        }

        /// <summary>
        /// This method is used to resize the existing buffer to a new size.
        /// </summary>
        /// <param name="newSize">New size in bytes</param>
        /// <param name="clearBuffer">True to reset used buffer</param>
        /// <returns>Position to insert new data to</returns>
        private int ResizeBuffer(int newSize, bool clearBuffer)
        {
            // Get the new required size
            int reqSize = newSize;
            if (clearBuffer == false)
                reqSize += usedSize_;
            else
                usedSize_ = 0;

            // Realloc the buffer if necessary.
            if (data_ == null || reqSize > data_.Length)
            {
                newSize = reqSize + 1;
                if (data_ != null && reqSize < (data_.Length * 2))
                    newSize = (data_.Length * 2);

                byte[] buff = new byte[newSize];
                if (!clearBuffer)
                    Array.Copy(data_, 0, buff, 0, data_.Length);
                data_ = buff;
            }
            return usedSize_;
        }

        /// <summary>
        /// Listening thread handler
        /// </summary>
        private void ListenThread()
        {
            // Set our thread information
            listenInfo_.ListenThread.IsBackground = true;
            listenInfo_.ListenThread.Name = string.Format("ListenerThread:{0}", port_);

            // Establish the local endpoint for the socket - the DNS name of the computer
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress_, port_);

            // Create a TCP/IP socket.
            sock_ = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Mark us as running
            listenInfo_.IsRunning = true;

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                sock_.Bind(localEndPoint);
                sock_.Listen(listenInfo_.MaxConnections);

                while (listenInfo_.IsRunning == true)
                {
                    // Set the event to nonsignaled state.
                    listenInfo_.acceptingEvent_.Reset();

                    try
                    {
                        // Start an asynchronous socket to listen for connections.
                        sock_.BeginAccept(new AsyncCallback(this.AcceptCallbackHandler), this);

                        // Wait until a connection is made before continuing.
                        listenInfo_.acceptingEvent_.WaitOne();
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                }
            }
            catch (SocketException ex)
            {
                if (OnFailure != null)
                {
                    ErrorEventArgs errEv = new ErrorEventArgs(this, ErrorEventArgs.SocketOperation.Listen, ex);
                    try
                    {
                        OnFailure(this, errEv);
                    }
                    catch { }
                }
            }
            finally
            {
                // Mark the thread as stopped.
                listenInfo_.IsRunning = false;

                // Remove the socket.
                SocketManager.StopServer(port_);
            }
        }

        /// <summary>
        /// Handler for the connect operation
        /// </summary>
        /// <param name="ar">Result information</param>
        private void AcceptCallbackHandler(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            listenInfo_.acceptingEvent_.Set();

            try
            {
                // Get the socket that handles the client request.
                Socket handler = sock_.EndAccept(ar);

                // Create the state object.
                SocketClient scNew = new SocketClient(handler);

                // Notify the client application
                ConnectEventArgs connEv = new ConnectEventArgs(scNew, listenInfo_.ReadSize);
                if (OnStartSession != null)
                {
                    try
                    {
                        OnStartSession(this, connEv);
                    }
                    catch { }
                }

                if (scNew.IsConnected)
                {
                    // If we are auto-wireup events then do so.
                    if (connEv.AutoConnect == true)
                    {
                        if (this.OnEndSession != null)
                        {
                            foreach (Delegate d in this.OnEndSession.GetInvocationList())
                                scNew.OnEndSession += (EventHandler<SocketEventArgs>)d;
                        }
                        if (this.OnFailure != null)
                        {
                            foreach (Delegate d in this.OnFailure.GetInvocationList())
                                scNew.OnFailure += (EventHandler<SocketEventArgs>)d;
                        }
                        if (this.OnReceiveData != null)
                        {
                            foreach (Delegate d in this.OnReceiveData.GetInvocationList())
                                scNew.OnReceiveData += (EventHandler<SocketEventArgs>)d;
                        }
                        if (this.OnSendDataComplete != null)
                        {
                            foreach (Delegate d in this.OnSendDataComplete.GetInvocationList())
                                scNew.OnSendDataComplete += (EventHandler<SocketEventArgs>)d;
                        }
                    }

                    if (connEv.NextReadSize > 0)
                        scNew.PostReceive(connEv.NextReadSize, true);
                }
            }
            catch (ObjectDisposedException)
            {
                // Ignore and exit.
            }
            catch (SocketException ex)
            {
                if (OnFailure != null)
                {
                    ErrorEventArgs errEv = new ErrorEventArgs(this, ErrorEventArgs.SocketOperation.Accept, ex);
                    try
                    {
                        OnFailure(this, errEv);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Handler for the connect operation
        /// </summary>
        /// <param name="ar">Result information</param>
        private void ConnectCallbackHandler(IAsyncResult ar)
        {
            try
            {
                // Finish the connect
                sock_.EndConnect(ar);

                // Notify the client application
                ConnectEventArgs connEv = new ConnectEventArgs(this, DefaultReceiveSize);
                if (OnStartSession != null)
                {
                    try
                    {
                        OnStartSession(this, connEv);
                    }
                    catch { }
                }

                // Send any data
                if (data_ != null)
                {
                    this.Send(data_);
                    data_ = null;
                }

                // Post the read
                if (connEv.NextReadSize > 0)
                    this.PostReceive(connEv.NextReadSize, true);
            }
            catch (SocketException ex)
            {
                if (OnFailure != null)
                {
                    ErrorEventArgs errEv = new ErrorEventArgs(this, ErrorEventArgs.SocketOperation.Connect, ex);
                    try
                    {
                        OnFailure(this, errEv);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Handler for the read event
        /// </summary>
        /// <param name="ar">Result information</param>
        private void ReadCallbackHandler(IAsyncResult ar)
        {
            try
            {
                // Read data from the client socket. 
                int bytesRead = sock_.EndReceive(ar);
                if (bytesRead > 0)
                {
                    // Bump used size.
                    usedSize_ += bytesRead;

                    // Notify the handlers
                    ReadEventArgs readEv = new ReadEventArgs(this, data_, usedSize_, DefaultReceiveSize);
                    if (OnReceiveData != null)
                    {
                        try
                        {
                            OnReceiveData(this, readEv);
                        }
                        catch { }
                    }
                    if (readEv.NextReadSize > 0)
                        this.PostReceive(readEv.NextReadSize, !readEv.AppendToBuffer);
                }
                else // socket closed
                {
                    this.Close(true);
                    if (OnEndSession != null)
                    {
                        DisconnectEventArgs discEv = new DisconnectEventArgs(this, null);
                        try
                        {
                            OnEndSession(this, discEv);
                        }
                        catch { }
                    }
                }
            }
            catch (SocketException ex)
            {
                if (sock_.Connected == false && !isClosed_)
                {
                    this.Close(true);
                    if (OnEndSession != null)
                    {
                        DisconnectEventArgs discEv = new DisconnectEventArgs(this, null);
                        try
                        {
                            OnEndSession(this, discEv);
                        }
                        catch { }
                    }
                }

                else if (isClosed_ == false && OnFailure != null)
                {
                    ErrorEventArgs errEv = new ErrorEventArgs(this, ErrorEventArgs.SocketOperation.Receive, ex);
                    try
                    {
                        OnFailure(this, errEv);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Internal handler function
        /// </summary>
        /// <param name="ar">Result information</param>
        private void SendCallbackHandler(IAsyncResult ar)
        {
            try
            {
                // Complete sending the data to the remote device.
                int bytesSent = sock_.EndSend(ar);
                if (bytesSent > 0)
                {
                    if (OnSendDataComplete != null)
                    {
                        WriteEventArgs writeEv = new WriteEventArgs(this, bytesSent);
                        try
                        {
                            OnSendDataComplete(this, writeEv);
                        }
                        catch { }
                    }
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (SocketException ex)
            {
                if (OnFailure != null)
                {
                    ErrorEventArgs errEv = new ErrorEventArgs(this, ErrorEventArgs.SocketOperation.Send, ex);
                    try
                    {
                        OnFailure(this, errEv);
                    }
                    catch { }
                }
            }
        }
    }

    /// <summary>
    /// This class represents the base event argument.
    /// </summary>
    public class SocketEventArgs : EventArgs
    {
        private SocketClient sc_ = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sc">SocketClient object</param>
        internal SocketEventArgs(SocketClient sc)
            : base()
        {
            sc_ = sc;
        }

        /// <summary>
        /// This property returns the SocketClient associated with the connection.
        /// In most cases, this will be the same as the "sender" of any event.  The exception
        /// is the Accept() event which has a different sender.
        /// </summary>
        public SocketClient Client
        {
            get { return sc_; }
        }
    }

    /// <summary>
    /// This event object represents the type passed to the Connect event
    /// </summary>
    public class ConnectEventArgs : SocketEventArgs
    {
        private int readSize_ = 0;
        private bool autoConnect_ = true;

        internal ConnectEventArgs(SocketClient sc, int readSize)
            : base(sc)
        {
            this.readSize_ = readSize;
        }

        /// <summary>
        /// Set/Return the next read size
        /// </summary>
        public int NextReadSize
        {
            get { return readSize_; }
            set { readSize_ = value; }
        }

        /// <summary>
        /// True to auto-wireup events to listening socket
        /// </summary>
        public bool AutoConnect
        {
            get { return autoConnect_; }
            set { autoConnect_ = value; }
        }
    }

    /// <summary>
    /// This event is passed to the read handler
    /// </summary>
    public class ReadEventArgs : ConnectEventArgs
    {
        private bool append_ = false;
        private byte[] data_ = null;

        internal ReadEventArgs(SocketClient sc, byte[] buffer, int bytesRead, int nextReadSize)
            : base(sc, nextReadSize)
        {
            if (bytesRead > 0)
            {
                data_ = new byte[bytesRead];
                Array.Copy(buffer, 0, data_, 0, bytesRead);
            }
        }

        /// <summary>
        /// Returns the buffer length
        /// </summary>
        public int Length
        {
            get { return (data_ != null) ? data_.Length : 0; }
        }

        /// <summary>
        /// Returns the data buffer.
        /// </summary>
        public byte[] Buffer
        {
            get { return data_; }
        }

        /// <summary>
        /// Returns the buffer as a string
        /// </summary>
        public string TextBuffer
        {
            get { return (data_ != null) ? Encoding.ASCII.GetString(data_) : ""; }
        }

        /// <summary>
        /// True to append to existing buffer.
        /// </summary>
        public bool AppendToBuffer
        {
            get { return append_; }
            set { append_ = value; }
        }
    }

    /// <summary>
    /// This is passed to the disconnect handler
    /// </summary>
    public class DisconnectEventArgs : SocketEventArgs
    {
        private Exception ex_ = null;
        internal DisconnectEventArgs(SocketClient sc, Exception ex)
            : base(sc)
        {
            ex_ = ex;
        }

        /// <summary>
        /// Returns the error exception (if any)
        /// </summary>
        public Exception Exception
        {
            get { return ex_; }
        }
    }

    /// <summary>
    /// This is passed to the OnSendComplete handler
    /// </summary>
    public class WriteEventArgs : SocketEventArgs
    {
        int length_;
        internal WriteEventArgs(SocketClient sc, int length)
            : base(sc)
        {
            length_ = length;
        }

        /// <summary>
        /// This returns the length of the data
        /// </summary>
        public int Length
        {
            get { return length_; }
        }
    }

    /// <summary>
    /// The ErrorEventArgs class encapsulates a socket error associated with a specific
    /// operation type.
    /// </summary>
    public class ErrorEventArgs : DisconnectEventArgs
    {
        /// <summary>
        /// Socket operation types
        /// </summary>
        public enum SocketOperation
        {
            /// <summary>
            /// Listen() operation failed
            /// </summary>
            Listen,
            /// <summary>
            /// Accept() operation failed
            /// </summary>
            Accept,
            /// <summary>
            /// Connect() operation failed
            /// </summary>
            Connect,
            /// <summary>
            /// Receive() operation failed
            /// </summary>
            Receive,
            /// <summary>
            /// Send() operation failed
            /// </summary>
            Send,
            /// <summary>
            /// Close() operation failed
            /// </summary>
            Close
        }
        private SocketOperation opType_;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sc">Socket connection reporting the error (may be closed or empty).</param>
        /// <param name="op">Operation which failed</param>
        /// <param name="ex">Exception information</param>
        internal ErrorEventArgs(SocketClient sc, SocketOperation op, Exception ex)
            : base(sc, ex)
        {
            opType_ = op;
        }

        /// <summary>
        /// Returns the operation type
        /// </summary>
        public SocketOperation Operation
        {
            get { return opType_; }
        }
    }

    /// <summary>
    /// This class represents the socket manager for server-side connections.
    /// </summary>
    public sealed class SocketManager
    {
        // Class data
        private static Dictionary<int, SocketClient> listenThreads_ = new Dictionary<int, SocketClient>();

        /// <summary>
        /// Empty private constructor to ensure that SocketManager cannot be
        /// instantiated (all methods are static)
        /// </summary>
        private SocketManager()
        {
        }

        /// <summary>
        /// This method is the mechanism to start a new listening server.
        /// </summary>
        /// <param name="port">Port number to start</param>
        public static SocketClient StartServer(int port)
        {
            return SocketManager.StartServer(port, SocketClient.DefaultMaxConnections, SocketClient.DefaultReceiveSize);
        }

        /// <summary>
        /// This method is the mechanism to start a new listening server.
        /// </summary>
        /// <param name="port">Port number to start</param>
        /// <param name="maxConnections">Maximum number of inbound connections to accept simultaneously</param>
        /// <param name="initialRead">Initial read to post to new accepted (inbound) sockets.</param>
        public static SocketClient StartServer(int port, int maxConnections, int initialRead)
        {
            return SocketManager.StartServer(IPAddress.Any, port, maxConnections, initialRead);
        }

        /// <summary>
        /// This method is the mechanism to start a new listening server.
        /// </summary>
        /// <param name="ipAddress">IPAddress to listen on (for multi-homed systems).</param>
        /// <param name="port">Port number to start</param>
        /// <param name="maxConnections">Maximum number of inbound connections to accept simultaneously</param>
        /// <param name="initialRead">Initial read to post to new accepted (inbound) sockets.</param>
        public static SocketClient StartServer(IPAddress ipAddress, int port, int maxConnections, int initialRead)
        {
            // See if the thread already exists.
            lock (listenThreads_)
            {
                if (listenThreads_.ContainsKey(port))
                    throw new ArgumentException("Port is already listening.");
            }

            // Return our socket client
            SocketClient sc = new SocketClient(ipAddress, port, maxConnections, initialRead);
            lock (listenThreads_)
            {
                listenThreads_.Add(port, sc);
            }
            return sc;
        }


        /// <summary>
        /// Connect ability
        /// </summary>
        /// <param name="ipAddress">IP address to connect to.</param>
        /// <param name="port">Port to connect to</param>
        /// <returns>New socket</returns>
        public static SocketClient Connect(string ipAddress, int port)
        {
            return new SocketClient(ipAddress, port, null);
        }

        /// <summary>
        /// Function to connect to a peer
        /// </summary>
        /// <param name="ipAddress">IP address to connect to.</param>
        /// <param name="port">Port to connect to</param>
        /// <param name="data">Data to send</param>
        /// <returns>New socket</returns>
        public static SocketClient ConnectAndSend(string ipAddress, int port, string data)
        {
            return new SocketClient(ipAddress, port, Encoding.ASCII.GetBytes(data));
        }

        /// <summary>
        /// Function to connect to a peer
        /// </summary>
        /// <param name="ipAddress">IP address to connect to.</param>
        /// <param name="port">Port to connect to</param>
        /// <param name="data">Data to send</param>
        /// <returns>New socket</returns>
        public static SocketClient ConnectAndSend(string ipAddress, int port, byte[] data)
        {
            return new SocketClient(ipAddress, port, data);
        }

        /// <summary>
        /// This is the primary method to stop a listening server.
        /// </summary>
        /// <param name="port">Port number to stop</param>
        public static void StopServer(int port)
        {
            if (port == 0)
                StopAllServers();

            SocketClient listenSock = null;

            // See if the thread already exists.
            lock (listenThreads_)
            {
                if (listenThreads_.ContainsKey(port))
                    listenSock = (SocketClient)listenThreads_[port];
            }

            if (listenSock != null)
            {
                listenSock.Close();
            }
        }

        /// <summary>
        /// This method shuts down all listening sockets
        /// </summary>
        public static void StopAllServers()
        {
            SocketClient[] arr;
            lock (listenThreads_)
            {
                arr = new SocketClient[listenThreads_.Count];
                listenThreads_.Values.CopyTo(arr, 0);
            }
            foreach (SocketClient listenThread in arr)
                listenThread.Close();
        }
    }
}