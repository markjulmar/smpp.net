using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using JulMar.Smpp.Utility;
using JulMar.Smpp;
using JulMar.Smpp.Pdu;

namespace JulMar.Smpp
{
	/// <summary>
	/// This represents the current bound status of our session
	/// </summary>
	public enum SmppBindStatus
	{
		/// <summary>
		/// The bind status is unknown
		/// </summary>
		Unknown,
		/// <summary>
		/// The bind status is unbound
		/// </summary>
		Unbound,
		/// <summary>
		/// The bind status is bound-receiver
		/// </summary>
		BoundRx,
		/// <summary>
		/// The bind status is bound-transmitter
		/// </summary>
		BoundTx,
		/// <summary>
		/// The bind status is bound transciever
		/// </summary>
		BoundTrx
	}

	/// <remarks>
	/// This class holds the event types used to fire events.
	/// </remarks>
	internal class EventType
	{
		internal static readonly object Error = new object();
		internal static readonly object SessionConnected = new object();
		internal static readonly object SessionDisconnected = new object();
		internal static readonly object PreProcessPdu = new object();
		internal static readonly object PostProcessPdu = new object();
		internal static readonly object ReceivedUnknownPdu = new object();
		internal static readonly object UnBind = new object();
		internal static readonly object UnBound = new object();
		internal static readonly object Bind = new object();
		internal static readonly object Bound = new object();
		internal static readonly object SubmitSm = new object();
		internal static readonly object SubmitSmMulti = new object();
		internal static readonly object DataSm = new object();
		internal static readonly object DeliverSm = new object();
		internal static readonly object QuerySm = new object();
		internal static readonly object CancelSm = new object();
		internal static readonly object ReplaceSm = new object();
		internal static readonly object BroadcastSm = new object();
		internal static readonly object CancelBroadcastSm = new object();
		internal static readonly object QueryBroadcastSm = new object();
		internal static readonly object AddSub = new object();
		internal static readonly object DelSub = new object();
		internal static readonly object ModSub = new object();
		internal static readonly object EnquireSub = new object();
		internal static readonly object AddDl = new object();
		internal static readonly object DelDl = new object();
		internal static readonly object ModDl = new object();
		internal static readonly object ListDls = new object();
		internal static readonly object ViewDl = new object();
		internal static readonly object AlertNotification = new object();
	}

	/// <remarks>
	/// This class is used to map sequence numbers back to the PDU and
	/// waiting handle for processing.
	/// </remarks>
	internal class PduSyncronizer
	{
		private ManualResetEvent doneEvent_ = new ManualResetEvent(false);
		private SmppPdu pduRequest = null;
		private SmppPdu pduResponse = null;
		private TimeSpan responseTimeout;

		/// <summary>
		/// Constructor for the PduSyncronizer
		/// </summary>
		/// <param name="pdu">PDU to wait on</param>
		/// <param name="responseTimeout">Time before request is droped</param>
		public PduSyncronizer(SmppPdu pdu, TimeSpan responseTimeout)
		{
			this.pduRequest = pdu;
			this.responseTimeout = responseTimeout;
		}

		/// <summary>
		/// Pdu request.
		/// </summary>
		/// <value>PDU</value>
		public SmppPdu PduRequest
		{
			get { return pduRequest; }
		}

		/// <summary>
		/// Pdu response.
		/// </summary>
		/// <value>PDU</value>
		public SmppPdu PduResponse
		{
			get { return pduResponse; }
			set { pduResponse = value; doneEvent_.Set(); }
		}

		/// <summary>
		/// Waits for response to be set, or for timeout to expire
		/// </summary>
		/// <returns>True if response received, else false</returns>
		public bool WaitForResponse()
		{
			return doneEvent_.WaitOne(this.responseTimeout, false);
		}
	}

	/// <summary>
	/// This class represents a single P2P session between an ESME and an SMSC.  This class
	/// is shared between the server and client implementations; the server holds more than one session,
	/// the client is represented by a single session.
	/// </summary>
	public class SmppSession
	{
		private SocketClient sock_ = null;
		private SmppSessionState state_ = null;
		private string mysid_ = "";
		private string peersid_ = "";
		private byte version_ = 0;
		private object tag_ = null;
		private TimeSpan responseTimeout;
		private int pendingRequestLimit;
		private Dictionary<int, PduSyncronizer> pendingRequests = new Dictionary<int, PduSyncronizer>();
		private const int DefaultResponseTimeout = 30;
		private const int DefaultPendingRequestLimit = 10;

		private delegate unbind_resp UnbindDelegate();
		private delegate data_sm_resp SendDataDelegate(data_sm pdu);
		private delegate enquire_link_resp EnquireLinkDelegate();

		/// <summary>
		/// Event handler list exposed to derived types
		/// </summary>
		protected EventHandlerList eventMap_ = new EventHandlerList();

		#region Events

		/// <summary>
		/// This event is fired when the session connects to the SMSC.
		/// </summary>
		public event EventHandler<SmppEventArgs> OnSessionConnected
		{
			add { eventMap_.AddHandler(EventType.SessionConnected, value); }
			remove { eventMap_.RemoveHandler(EventType.SessionConnected, value); }
		}

		/// <summary>
		/// This event is fired when the session is disconnected.
		/// </summary>
		public event EventHandler<SmppEventArgs> OnSessionDisconnected
		{
			add { eventMap_.AddHandler(EventType.SessionDisconnected, value); }
			remove { eventMap_.RemoveHandler(EventType.SessionDisconnected, value); }
		}

		/// <summary>
		/// This event is fired when the Pdu is received prior to being processed.
		/// </summary>
		public event EventHandler<SmppEventArgs> OnPreProcessPdu
		{
			add { eventMap_.AddHandler(EventType.PreProcessPdu, value); }
			remove { eventMap_.RemoveHandler(EventType.PreProcessPdu, value); }
		}

		/// <summary>
		/// This event is fired for each sent Pdu prior to sending.
		/// </summary>
		public event EventHandler<SmppEventArgs> OnPostProcessPdu
		{
			add { eventMap_.AddHandler(EventType.PostProcessPdu, value); }
			remove { eventMap_.RemoveHandler(EventType.PostProcessPdu, value); }
		}

		/// <summary>
		/// This event is fired when a PDU exception occurs during processing.
		/// </summary>
		public event EventHandler<SmppEventArgs> OnError
		{
			add { eventMap_.AddHandler(EventType.Error, value); }
			remove { eventMap_.RemoveHandler(EventType.Error, value); }
		}

		/// <summary>
		/// This event is fired when the Pdu received is unknown.
		/// </summary>
		public event EventHandler<SmppEventArgs> OnReceivedUnknownPdu
		{
			add { eventMap_.AddHandler(EventType.ReceivedUnknownPdu, value); }
			remove { eventMap_.RemoveHandler(EventType.ReceivedUnknownPdu, value); }
		}

		/// <summary>
		/// This event is fired when an unbind() is received.
		/// </summary>
		public event EventHandler<SmppEventArgs> OnUnBind
		{
			add { eventMap_.AddHandler(EventType.UnBind, value); }
			remove { eventMap_.RemoveHandler(EventType.UnBind, value); }
		}

		/// <summary>
		/// This event is fired when the session has been unbound.
		/// </summary>
		public event EventHandler<SmppEventArgs> OnUnBound
		{
			add { eventMap_.AddHandler(EventType.UnBound, value); }
			remove { eventMap_.RemoveHandler(EventType.UnBound, value); }
		}

		#endregion

		#region Properties

		/// <summary>
		/// Specifies the time a request will wait for a response.
		/// </summary>
		public TimeSpan ResponseTimeout
		{
			get { return this.responseTimeout; }
			set { this.responseTimeout = value; }
		}

		/// <summary>
		/// Maximum number of pending requests
		/// </summary>
		public int PendingRequestLimit
		{
			get { return this.pendingRequestLimit; }
			set { this.pendingRequestLimit = value; }
		}

		/// <summary>
		/// The system_id returns a string identifing the ESME or SMSC system id associated
		/// with this session.  This is used by the peer end to uniquely identify the system.
		/// </summary>
		/// <value>System ID value</value>
		public string LocalSystemID
		{
			get { return mysid_; }
		}

		/// <summary>
		/// This property provides a user-item "tag" for the session.
		/// </summary>
		/// <value>System.Object</value>
		public object Tag
		{
			get { return tag_; }
			set { tag_ = value; }
		}

		/// <summary>
		/// The system_id returns a string identifing the ESME or SMSC system id associated
		/// with this session.  This is used by the peer end to uniquely identify the system.
		/// </summary>
		/// <value>System ID value</value>
		public string PeerSystemID
		{
			get { return peersid_; }
			set { peersid_ = value; }
		}

		/// <summary>
		/// This returns the version supported by this session
		/// </summary>
		/// <value>Smpp Version (SmppVersion value)</value>
		public byte SmppVersion
		{
			get { return version_; }
			set { version_ = value; }
		}

		/// <summary>
		/// This returns the peer IP address
		/// </summary>
		/// <value>IPAddress</value>
		public System.Net.IPAddress IPAddress
		{
			get
			{
				if (sock_ != null)
					return sock_.Address;
				return new System.Net.IPAddress(0);
			}
		}

		/// <summary>
		/// This returns the port of the connected peer
		/// </summary>
		/// <value>Integer port</value>
		public int Port
		{
			get
			{
				if (sock_ != null)
					return sock_.Port;
				return 0;
			}
		}

		/// <summary>
		/// This returns whether the session is bound up
		/// </summary>
		/// <value>True/False</value>
		public SmppBindStatus BindStatus
		{
			get { return CurrentState.BindStatus; }
		}

		/// <summary>
		/// This returns whether the session is bound
		/// </summary>
		/// <value>True/False</value>
		public bool IsBound
		{
			get
			{
				return (BindStatus != SmppBindStatus.Unbound &&
						  BindStatus != SmppBindStatus.Unknown);
			}
		}

		/// <summary>
		/// This returns whether the session socket is connected
		/// </summary>
		/// <value>True/False</value>
		public bool IsConnected
		{
			get
			{
				return this.sock_ != null ? this.sock_.IsConnected : false;
			}
		}

		/// <summary>
		/// This property provides access to the current session state which processes
		/// the commands for the session.
		/// </summary>
		/// <value></value>
		internal SmppSessionState CurrentState
		{
			get { return state_; }
			set { state_ = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// This constructor    creates a disconnected session.
		/// </summary>
		internal SmppSession(string sid)
		{
			this.mysid_ = sid;
			this.responseTimeout = TimeSpan.FromSeconds(DefaultResponseTimeout);
			this.pendingRequestLimit = DefaultPendingRequestLimit;
		}

		/// <summary>
		/// This constructor creates a session from an existing (inbound) socket.
		/// </summary>
		/// <param name="socket">Socket</param>
		/// <param name="sid">System ID</param>
		internal SmppSession(SocketClient socket, string sid)
		{
			this.mysid_ = sid;
			this.responseTimeout = TimeSpan.FromSeconds(DefaultResponseTimeout);
			this.pendingRequestLimit = DefaultPendingRequestLimit;
			SetSocket(socket);
		}

		#endregion

		#region EventHandlers

		/// <summary>
		/// This event handler processes an initial connection to a server (SMSC).
		/// It is not used for the server-side processing.
		/// </summary>
		/// <param name="sender">Socket</param>
		/// <param name="args">Arguments</param>
		protected virtual void OnStartSession(object sender, SocketEventArgs args)
		{
			FireEvent(EventType.SessionConnected, new SmppConnectEventArgs(((SocketEventArgs)args).Client.Address, this));
		}

		/// <summary>
		/// This event handler processes the disconnection of a connection.
		/// </summary>
		/// <param name="sender">Socket</param>
		/// <param name="args">Arguments</param>
		protected virtual void OnEndSession(object sender, SocketEventArgs args)
		{
			FireEvent(EventType.SessionDisconnected, new SmppDisconnectEventArgs(this, ((DisconnectEventArgs)args).Exception));
		}

		/// <summary>
		/// This method is called when a failure occurs on the socket layer.
		/// </summary>
		/// <param name="sender">Socket</param>
		/// <param name="args">Arguments</param>
		protected virtual void OnFailure(object sender, SocketEventArgs args)
		{
			FireEvent(EventType.SessionDisconnected, new SmppDisconnectEventArgs(this, ((DisconnectEventArgs)args).Exception));
			((SocketClient)sender).Close(true);
		}

		/// <summary>
		/// This method is called when data is received from our socket.
		/// </summary>
		/// <param name="sender">The socket which sent the data</param>
		/// <param name="args">Socket data</param>
		protected virtual void OnReceiveData(object sender, SocketEventArgs args)
		{
			SocketClient socket = (SocketClient)sender;
			ReadEventArgs re = (ReadEventArgs)args;
			int packetLength = SmppPdu.REQUIRED_SIZE;

			if (re.Length >= packetLength)
			{
				// Get the first DWORD from the buffer; this is the total size of the packet.
				packetLength = BitConverter.ToInt32(new byte[] { re.Buffer[3], re.Buffer[2], re.Buffer[1], re.Buffer[0] }, 0);
				if (re.Length >= packetLength)
				{
					try
					{
						// Have the entire buffer; parse it out.
						SmppPdu pdu = SmppPdu.Create(new SmppByteStream(re.Buffer));
						SmppPduReceivedEventArgs ea = new SmppPduReceivedEventArgs(this, pdu);
						FireEvent(EventType.PreProcessPdu, ea);

						PduSyncronizer sync = null;
						if (pdu is SmppResponse)
						{
							if ((sync = FindAndRemoveWaitingPdu(pdu.SequenceNumber)) != null)
							{
								((SmppResponse)pdu).OriginalRequest = (SmppRequest)sync.PduRequest;
							}
							else
							{
								throw new SmppException("Invalid pdu response received with no pending request: " + pdu.ToString());
							}
						}

						if (!ea.Handled)
						{
							try
							{
								ProcessPdu(pdu);
							}
							catch (InvalidSmppStateException)
							{
								if (pdu.RequiresResponse)
								{
									SendPdu(new generic_nack(StatusCodes.ESME_RINVCMDID, pdu.SequenceNumber));
								}
							}
						}

						if (sync != null)
						{
							sync.PduResponse = pdu;
						}

					}
					catch (PduException pduex)
					{
						SmppPdu pdu = (pduex.HasPDU) ? pduex.PDU : null;
						if (pdu != null && pdu.RequiresResponse)
						{
							SendPdu(new generic_nack(StatusCodes.ESME_RINVCMDID, pdu.SequenceNumber));
						}
						FireEvent(EventType.Error, new SmppErrorEventArgs(this, pduex, pdu));
					}
					catch (Exception ex)
					{
						socket.Close(true);
						FireEvent(EventType.SessionDisconnected, new SmppDisconnectEventArgs(this, ex));
					}

					// Reset the buffer
					re.AppendToBuffer = false;
					re.NextReadSize = SmppPdu.REQUIRED_SIZE;
					return;
				}
			}

			// Wait for more data to show up on the socket.
			re.AppendToBuffer = true;
			re.NextReadSize = packetLength - re.Length;
		}

		#endregion

		/// <summary>
		/// This method connects the session to the given socket instance.
		/// </summary>
		/// <param name="socket"></param>
		protected void SetSocket(SocketClient socket)
		{
			sock_ = socket;
			sock_.OnStartSession += OnStartSession;
			sock_.OnEndSession += OnEndSession;
			sock_.OnFailure += OnFailure;
			sock_.OnReceiveData += OnReceiveData;
		}

		/// <summary>
		/// This method is used to unbind our session synchronously.
		/// </summary>
		/// <returns>Unbind response PDU</returns>
		public unbind_resp UnBind()
		{
			unbind pdu = new unbind();
			unbind_resp response = null;
			PduSyncronizer sync = AddWaitingPdu(pdu);
			if (sync != null)
			{
				if (IsBound && SendPdu(pdu))
				{
					if (sync.WaitForResponse())
					{
						response = sync.PduResponse as unbind_resp;
						if (response == null)
						{
							response = new unbind_resp(pdu.SequenceNumber, sync.PduResponse.Status);
						}
					}
					else
					{
						response = new unbind_resp(pdu.SequenceNumber, StatusCodes.ESME_RINVEXPIRY);
					}
				}
				else
				{
					response = new unbind_resp(pdu.SequenceNumber, StatusCodes.ESME_RINVBNDSTS);
				}
				FindAndRemoveWaitingPdu(pdu.SequenceNumber);
			}
			else
			{
				response = new unbind_resp(pdu.SequenceNumber, StatusCodes.ESME_RMSGQFUL);
			}

			return response;
		}

		/// <summary>
		/// This method is used to unbind from the session asynchronously.
		/// </summary>
		/// <returns></returns>
		public IAsyncResult BeginUnBind(AsyncCallback ac)
		{
			AsynchCall acr = new AsynchCall(ac, this);
			return acr.BeginInvoke(new UnbindDelegate(UnBind), null);
		}

		/// <summary>
		/// This completes the unbind and returns the response.
		/// </summary>
		/// <param name="ar">Original IAsynchResult from BeginUnbind</param>
		/// <returns>Unbind response</returns>
		public unbind_resp EndUnBind(IAsyncResult ar)
		{
			return (unbind_resp)AsynchCall.ProcessEndInvoke(ar);
		}

		/// <summary>
		/// This method sends an enquire link
		/// </summary>
		/// <returns></returns>
		public enquire_link_resp EnquireLink()
		{
			SmppPdu pdu = new enquire_link();
			enquire_link_resp response = null;
			PduSyncronizer sync = AddWaitingPdu(pdu);
			if (sync != null)
			{
				if (IsBound && SendPdu(pdu))
				{
					if (sync.WaitForResponse())
					{
						response = sync.PduResponse as enquire_link_resp;
						if (response == null)
						{
							response = new enquire_link_resp(pdu.SequenceNumber, sync.PduResponse.Status);
						}
					}
					else
					{
						response = new enquire_link_resp(pdu.SequenceNumber, StatusCodes.ESME_RINVEXPIRY);
					}
				}
				else
				{
					response = new enquire_link_resp(pdu.SequenceNumber, StatusCodes.ESME_RINVCMDID);
				}
				FindAndRemoveWaitingPdu(pdu.SequenceNumber);
			}
			else
			{
				response = new enquire_link_resp(pdu.SequenceNumber, StatusCodes.ESME_RMSGQFUL);
			}

			return response;
		}

		/// <summary>
		/// This method is used to enquire the session asynchronously.
		/// </summary>
		/// <returns></returns>
		public IAsyncResult BeginEnquireLink(AsyncCallback ac)
		{
			AsynchCall acr = new AsynchCall(ac, this);
			return acr.BeginInvoke(new EnquireLinkDelegate(EnquireLink), null);
		}

		/// <summary>
		/// This completes the enquire and returns the response.
		/// </summary>
		/// <param name="ar">Original IAsynchResult from BeginUnbind</param>
		/// <returns>Enquire response</returns>
		public enquire_link_resp EndEnquireLink(IAsyncResult ar)
		{
			return (enquire_link_resp)AsynchCall.ProcessEndInvoke(ar);
		}

		/// <summary>
		/// This method sends a data_sm packet synchronously over to the peer.
		/// </summary>
		/// <param name="pdu">data_sm packet</param>
		/// <returns>data_sm response</returns>
		public data_sm_resp SendData(data_sm pdu)
		{
			data_sm_resp response = null;
			PduSyncronizer sync = AddWaitingPdu(pdu);
			if (sync != null)
			{
				if (IsBound && SendPdu(pdu))
				{
					if (sync.WaitForResponse())
					{
						response = sync.PduResponse as data_sm_resp;
						if (response == null)
						{
							response = new data_sm_resp(pdu.SequenceNumber, sync.PduResponse.Status);
						}
					}
					else
					{
						response = new data_sm_resp(pdu.SequenceNumber, StatusCodes.ESME_RINVEXPIRY);
					}
				}
				else
				{
					response = new data_sm_resp(pdu.SequenceNumber, StatusCodes.ESME_RDELIVERYFAILURE);
				}
				FindAndRemoveWaitingPdu(pdu.SequenceNumber);
			}
			else
			{
				response = new data_sm_resp(pdu.SequenceNumber, StatusCodes.ESME_RMSGQFUL);
			}

			return response;
		}

		/// <summary>
		/// This method invokes the SendData method asynchronously
		/// </summary>
		/// <param name="pdu">data_sm PDU to send</param>
		/// <param name="ac">Asynch callback</param>
		/// <returns>IAsyncResult interface for monitoring</returns>
		public IAsyncResult BeginSendData(data_sm pdu, AsyncCallback ac)
		{
			AsynchCall acr = new AsynchCall(ac, this);
			return acr.BeginInvoke(new SendDataDelegate(SendData), new object[] { pdu });
		}

		/// <summary>
		/// This method retrieves the result of an asynchronous SendData method call
		/// </summary>
		/// <param name="ar">Result from BeginSendData</param>
		/// <returns>Result packet</returns>
		public data_sm_resp EndSendData(IAsyncResult ar)
		{
			return (data_sm_resp)AsynchCall.ProcessEndInvoke(ar);
		}

		/// <summary>
		/// This method allows the user to send a generic_nak packet
		/// </summary>
		/// <param name="seq">Sequence number</param>
		/// <param name="status">Status code</param>
		/// <returns>True/False</returns>
		public bool SendNak(int seq, int status)
		{
			return SendPdu(new generic_nack(status, seq));
		}

		/// <summary>
		/// This method is used to process a PDU
		/// </summary>
		/// <param name="pdu">PDU</param>
		protected void ProcessPdu(SmppPdu pdu)
		{
			// Dispatch using the intermediate PduHandler class which will correctly
			// cast the base Pdu into a concrete type and invoke the proper method for
			// the passed state object.
			if (!PduHandler.Dispatch(state_, pdu))
			{
				// Unrecognized PDU received from partner; notify handler.
				FireEvent(EventType.ReceivedUnknownPdu, new SmppEventArgs(this, pdu));
			}
		}

		/// <summary>
		/// This method sends a PDU on the socket session
		/// </summary>
		/// <param name="pdu"></param>
		/// <returns></returns>
		internal bool SendPdu(SmppPdu pdu)
		{
			if (sock_.IsConnected)
			{
				try
				{
					FireEvent(EventType.PostProcessPdu, new SmppEventArgs(this, pdu));
				}
				catch
				{
				}

				try
				{
					SmppByteStream bs = new SmppByteStream();
					pdu.Serialize(new SmppWriter(bs, this.version_));
					sock_.Send(bs.GetBuffer());
					return true;
				}
				catch (System.Net.Sockets.SocketException ex)
				{
					sock_.Close(true);
					FireEvent(EventType.SessionDisconnected, new SmppDisconnectEventArgs(this, ex));
				}
			}
			return false;
		}

		/// <summary>
		/// This closes the connection and releases the session.
		/// </summary>
		public virtual void Close()
		{
			sock_.Close();
			FireEvent(EventType.SessionDisconnected, new SmppEventArgs(this));
		}

		/// <summary>
		/// This method fires the unbind event
		/// </summary>
		/// <param name="ea">Event parameters</param>
		/// <param name="eventType">Event arguments data structure</param>
		/// <returns>True/False whether an event was raised</returns>
		internal virtual bool FireEvent(object eventType, SmppEventArgs ea)
		{
			EventHandler<SmppEventArgs> evt = (EventHandler<SmppEventArgs>)eventMap_[eventType];
			if (evt != null)
			{
				try
				{
					evt(this, ea);
				}
				catch
				{
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// This adds a PDU to our waiting list.
		/// </summary>
		/// <param name="pdu">PDU</param>
		/// <returns>True/False</returns>
		internal PduSyncronizer AddWaitingPdu(SmppPdu pdu)
		{
			lock (this.pendingRequests)
			{
				if (this.pendingRequests.Count < DefaultPendingRequestLimit)
				{
					PduSyncronizer sync = new PduSyncronizer(pdu, ResponseTimeout);
					this.pendingRequests.Add(pdu.SequenceNumber, sync);
					return sync;
				}
			}
			return null;
		}

		/// <summary>
		/// This locates and removes a PduSyncronizer.
		/// </summary>
		/// <param name="seqNum">Sequence number to locate</param>
		/// <returns>PduSyncronizer in question</returns>
		internal PduSyncronizer FindAndRemoveWaitingPdu(int seqNum)
		{
			PduSyncronizer sync = null;
			lock (this.pendingRequests)
			{
				if (this.pendingRequests.ContainsKey(seqNum))
				{
					sync = this.pendingRequests[seqNum];
					this.pendingRequests.Remove(seqNum);
				}
			}
			return sync;
		}

		/// <summary>
		/// Overrides the base.ToString method
		/// </summary>
		/// <returns>String</returns>
		public override string ToString()
		{
			return string.Format("{0}: {1} running Smpp {2}",
				PeerSystemID, IPAddress, SmppVersion);
		}
	}
}
