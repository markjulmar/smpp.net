using System;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Esme
{
	/// <summary>
	/// This class represents a session from the ESME (client) side of the SMPP connection
	/// </summary>
	public class EsmeSession : SmppSession
	{
		private delegate bind_transmitter_resp BindTransmitterDelegate(bind_transmitter pdu);
		private delegate bind_receiver_resp BindReceiverDelegate(bind_receiver pdu);
		private delegate bind_transceiver_resp BindTransceiverDelegate(bind_transceiver pdu);
		private delegate unbind_resp UnBindDelegate(unbind pdu);
		private delegate submit_sm_resp SubmitSmDelegate(submit_sm pdu);
		private delegate enquire_link_resp EnquireLinkDelegate(enquire_link pdu);

		/// <summary>
		/// This event is fired when the session is successfully bound.
		/// </summary>
		public event EventHandler<SmppEventArgs> OnBound
		{
			add { eventMap_.AddHandler(EventType.Bound, value); }
			remove { eventMap_.RemoveHandler(EventType.Bound, value); }
		}

		/// <summary>
		/// This event is fired when a deliver_sm request is received.
		/// </summary>
		public event EventHandler<SmppEventArgs> OnDeliverSm
		{
			add { eventMap_.AddHandler(EventType.DeliverSm, value); }
			remove { eventMap_.RemoveHandler(EventType.DeliverSm, value); }
		}

		/// <summary>
		/// This event is fired when a data_sm request is received.
		/// </summary>
		public event EventHandler<SmppEventArgs> OnDataSm
		{
			add { eventMap_.AddHandler(EventType.DataSm, value); }
			remove { eventMap_.RemoveHandler(EventType.DataSm, value); }
		}

		/// <summary>
		/// This event is fired when a altert_notification request is received.
		/// NOTE: There is no altert_notification_resp response message
		/// </summary>
		public event EventHandler<SmppEventArgs> OnAlertNotification
		{
			add { eventMap_.AddHandler(EventType.AlertNotification, value); }
			remove { eventMap_.RemoveHandler(EventType.AlertNotification, value); }
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sid">System ID</param>
		public EsmeSession(string sid)
			: base(sid)
		{
			// Start out in a disconnected state
			base.CurrentState = new EsmeDisconnectedSessionState(this);
		}

		/// <summary>
		/// This method is called when the ESME connects to the SMSC.
		/// </summary>
		/// <param name="sender">EsmeSession</param>
		/// <param name="args">Socket event</param>
		protected override void OnStartSession(object sender, SocketEventArgs args)
		{
			base.CurrentState = new EsmeConnectedSessionState(this);
			base.OnStartSession(sender, args);
		}

		/// <summary>
		/// This method is called when the ESME disconnects from the SMSC (or vice-versa).
		/// </summary>
		/// <param name="sender">EsmeSession</param>
		/// <param name="args">Socket event</param>
		protected override void OnEndSession(object sender, SocketEventArgs args)
		{
			base.CurrentState = new EsmeDisconnectedSessionState(this);
			base.OnEndSession(sender, args);
		}

		/// <summary>
		/// This method is invoked when a failure occurs in the communication channel.
		/// </summary>
		/// <param name="sender">EsmeSession</param>
		/// <param name="args">Socket event</param>
		protected override void OnFailure(object sender, SocketEventArgs args)
		{
			base.CurrentState = new EsmeDisconnectedSessionState(this);
			base.OnFailure(sender, args);
		}

		/// <summary>
		/// This is used to connect to an SMSC
		/// </summary>
		/// <param name="address">IP address</param>
		/// <param name="port">Port number</param>
		public void Connect(string address, int port)
		{
			SocketClient sc = new SocketClient();
			base.SetSocket(sc);
			sc.Connect(address, port);
		}

		/// <summary>
		/// Asynch form of Connect
		/// </summary>
		/// <param name="address">IP address</param>
		/// <param name="port">Port number</param>
		public void BeginConnect(string address, int port)
		{
			SocketClient sc = new SocketClient();
			base.SetSocket(sc);
			sc.BeginConnect(address, port);
		}

		/// <summary>
		/// This method sends a bind_transmitter packet synchronously over to the peer.
		/// </summary>
		/// <param name="pdu">bind_transmitter packet</param>
		/// <returns>bind_transmitter_resp</returns>
		public bind_transmitter_resp BindTransmitter(bind_transmitter pdu)
		{
			bind_transmitter_resp response = null;
			PduSyncronizer sync = AddWaitingPdu(pdu);
			if (sync != null)
			{
				if (SendPdu(pdu))
				{
					if (sync.WaitForResponse())
					{
						response = sync.PduResponse as bind_transmitter_resp;
						if (response != null)
						{
							if (response.Status == StatusCodes.ESME_ROK)
								base.CurrentState = new EsmeBoundTXSessionState(this);
						}
						else
						{
							response = new bind_transmitter_resp(pdu.SequenceNumber, sync.PduResponse.Status);
						}
					}
					else
					{
						response = new bind_transmitter_resp(pdu.SequenceNumber, StatusCodes.ESME_RINVEXPIRY);
					}
				}
				else
				{
					response = new bind_transmitter_resp(pdu.SequenceNumber, StatusCodes.ESME_RBINDFAIL);
				}
				FindAndRemoveWaitingPdu(pdu.SequenceNumber);
			}
			else
			{
				response = new bind_transmitter_resp(pdu.SequenceNumber, StatusCodes.ESME_RMSGQFUL);
			}

			return response;
		}

		/// <summary>
		/// This method invokes the BindTransmitter method asynchronously
		/// </summary>
		/// <param name="pdu">bind_transmitter PDU to send</param>
		/// <param name="callback">Asynch callback</param>
		/// <returns>IAsyncResult interface for monitoring</returns>
		public IAsyncResult BeginBindTransmitter(bind_transmitter pdu, AsyncCallback callback)
		{
			AsynchCall acr = new AsynchCall(callback, this);
			return acr.BeginInvoke(new BindTransmitterDelegate(BindTransmitter), new object[] { pdu });
		}

		/// <summary>
		/// This method retrieves the result of an asynchronous BindTransmitter method call
		/// </summary>
		/// <param name="iar">Result from BeginBindTransmitter</param>
		/// <returns>Result packet</returns>
		public bind_transmitter_resp EndBindTransmitter(IAsyncResult iar)
		{
			return (bind_transmitter_resp)AsynchCall.ProcessEndInvoke(iar);
		}


		/// <summary>
		/// This method sends a bind_receiver packet synchronously over to the peer.
		/// </summary>
		/// <param name="pdu">bind_receiver packet</param>
		/// <returns>bind_receiver_resp</returns>
		public bind_receiver_resp BindReceiver(bind_receiver pdu)
		{
			bind_receiver_resp response = null;
			PduSyncronizer sync = AddWaitingPdu(pdu);
			if (sync != null)
			{
				if (SendPdu(pdu))
				{
					if (sync.WaitForResponse())
					{
						response = sync.PduResponse as bind_receiver_resp;
						if (response != null)
						{
							if (response.Status == StatusCodes.ESME_ROK)
								base.CurrentState = new EsmeBoundRXSessionState(this);
						}
						else
						{
							response = new bind_receiver_resp(pdu.SequenceNumber, sync.PduResponse.Status);
						}
					}
					else
					{
						response = new bind_receiver_resp(pdu.SequenceNumber, StatusCodes.ESME_RINVEXPIRY);
					}
				}
				else
				{
					response = new bind_receiver_resp(pdu.SequenceNumber, StatusCodes.ESME_RBINDFAIL);
				}
				FindAndRemoveWaitingPdu(pdu.SequenceNumber);
			}
			else
			{
				response = new bind_receiver_resp(pdu.SequenceNumber, StatusCodes.ESME_RMSGQFUL);
			}

			return response;
		}

		/// <summary>
		/// This method invokes the BindReceiver method asynchronously
		/// </summary>
		/// <param name="pdu">bind_receiver PDU to send</param>
		/// <param name="callback">Asynch callback</param>
		/// <returns>IAsyncResult interface for monitoring</returns>
		public IAsyncResult BeginBindReceiver(bind_receiver pdu, AsyncCallback callback)
		{
			AsynchCall acr = new AsynchCall(callback, this);
			return acr.BeginInvoke(new BindReceiverDelegate(BindReceiver), new object[] { pdu });
		}

		/// <summary>
		/// This method retrieves the result of an asynchronous BindReceiver method call
		/// </summary>
		/// <param name="iar">Result from BeginBindReceiver</param>
		/// <returns>Result packet</returns>
		public bind_receiver_resp EndBindReceiver(IAsyncResult iar)
		{
			return (bind_receiver_resp)AsynchCall.ProcessEndInvoke(iar);
		}

		/// <summary>
		/// This method sends a bind_transceiver packet synchronously over to the peer.
		/// </summary>
		/// <param name="pdu">bind_transceiver packet</param>
		/// <returns>bind_transceiver_resp</returns>
		public bind_transceiver_resp BindTransceiver(bind_transceiver pdu)
		{
			bind_transceiver_resp response = null;
			PduSyncronizer sync = AddWaitingPdu(pdu);
			if (sync != null)
			{
				if (SendPdu(pdu))
				{
					if (sync.WaitForResponse())
					{
						response = sync.PduResponse as bind_transceiver_resp;
						if (response != null)
						{
							if (response.Status == StatusCodes.ESME_ROK)
								base.CurrentState = new EsmeBoundTRXSessionState(this);
						}
						else
						{
							response = new bind_transceiver_resp(pdu.SequenceNumber, sync.PduResponse.Status);
						}
					}
					else
					{
						response = new bind_transceiver_resp(pdu.SequenceNumber, StatusCodes.ESME_RINVEXPIRY);
					}
				}
				else
				{
					response = new bind_transceiver_resp(pdu.SequenceNumber, StatusCodes.ESME_RBINDFAIL);
				}
				FindAndRemoveWaitingPdu(pdu.SequenceNumber);
			}
			else
			{
				response = new bind_transceiver_resp(pdu.SequenceNumber, StatusCodes.ESME_RMSGQFUL);
			}
			return response;
		}

		/// <summary>
		/// This method invokes the BindTransceiver method asynchronously
		/// </summary>
		/// <param name="pdu">bind_transceiver PDU to send</param>
		/// <param name="callback">Asynch callback</param>
		/// <returns>IAsyncResult interface for monitoring</returns>
		public IAsyncResult BeginBindTransceiver(bind_transceiver pdu, AsyncCallback callback)
		{
			AsynchCall acr = new AsynchCall(callback, this);
			return acr.BeginInvoke(new BindTransceiverDelegate(BindTransceiver), new object[] { pdu });
		}

		/// <summary>
		/// This method retrieves the result of an asynchronous BindTransceiver method call
		/// </summary>
		/// <param name="iar">Result from BeginBindTransceiver</param>
		/// <returns>Result packet</returns>
		public bind_transceiver_resp EndBindTransceiver(IAsyncResult iar)
		{
			return (bind_transceiver_resp)AsynchCall.ProcessEndInvoke(iar);
		}

		/// <summary>
		/// This method sends a submit_sm packet synchronously over to the peer.
		/// </summary>
		/// <param name="pdu">submit_sm packet</param>
		/// <returns>submit_sm response</returns>
		public submit_sm_resp SubmitSm(submit_sm pdu)
		{
			submit_sm_resp response = null;
			PduSyncronizer sync = AddWaitingPdu(pdu);
			if (sync != null)
			{
				if (IsBound && SendPdu(pdu))
				{
					if (sync.WaitForResponse())
					{
						response = sync.PduResponse as submit_sm_resp;
						if (response == null)
						{
							response = new submit_sm_resp(pdu.SequenceNumber, sync.PduResponse.Status);
						}
					}
					else
					{
						response = new submit_sm_resp(pdu.SequenceNumber, StatusCodes.ESME_RINVEXPIRY);
					}
				}
				else
				{
					response = new submit_sm_resp(pdu.SequenceNumber, StatusCodes.ESME_RSUBMITFAIL);
				}
				FindAndRemoveWaitingPdu(pdu.SequenceNumber);
			}
			else
			{
				response = new submit_sm_resp(pdu.SequenceNumber, StatusCodes.ESME_RMSGQFUL);
			}

			return response;
		}

		/// <summary>
		/// This method invokes the SubmitSm method asynchronously
		/// </summary>
		/// <param name="pdu">submit_sm PDU to send</param>
		/// <param name="callback">Asynch callback</param>
		/// <returns>IAsyncResult interface for monitoring</returns>
		public IAsyncResult BeginSubmitSm(submit_sm pdu, AsyncCallback callback)
		{
			AsynchCall acr = new AsynchCall(callback, this);
			return acr.BeginInvoke(new SubmitSmDelegate(SubmitSm), new object[] { pdu });
		}

		/// <summary>
		/// This method retrieves the result of an asynchronous SubmitSm method call
		/// </summary>
		/// <param name="iar">Result from BeginSubmitSm</param>
		/// <returns>Result packet</returns>
		public submit_sm_resp EndSubmitSm(IAsyncResult iar)
		{
			return (submit_sm_resp)AsynchCall.ProcessEndInvoke(iar);
		}

		/// <summary>
		/// This method sends a enquire_link packet synchronously over to the peer.
		/// </summary>
		/// <param name="pdu">enquire_link packet</param>
		/// <returns>enquire_link_resp response</returns>
		public enquire_link_resp EnquireLink(enquire_link pdu)
		{
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
					response = new enquire_link_resp(pdu.SequenceNumber, StatusCodes.ESME_RSUBMITFAIL);
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
		/// This method invokes the EnquireLink method asynchronously
		/// </summary>
		/// <param name="pdu">enquire_link PDU to send</param>
		/// <param name="callback">Asynch callback</param>
		/// <returns>IAsyncResult interface for monitoring</returns>
		public IAsyncResult BeginEnquireLink(enquire_link pdu, AsyncCallback callback)
		{
			AsynchCall acr = new AsynchCall(callback, this);
			return acr.BeginInvoke(new EnquireLinkDelegate(EnquireLink), new object[] { pdu });
		}
	}
}
