using System;
using System.Collections.Generic;
using System.Text;
using JulMar.Smpp.Esme;
using JulMar.Smpp;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace TestEsme
{
	class Program
	{
		public enum LocalSessionState
		{
			Disconnected = 0,
			Connected,
			Binding,
			Bound
		};

		public static LocalSessionState _localSessionState = LocalSessionState.Disconnected;

		public static EsmeSession _smppSession = new EsmeSession("Audiotel");

		static void Main(string[] args)
		{
			// Set the SMPP version for our SMPP session
			_smppSession.SmppVersion = SmppVersion.SMPP_V34;

			// Hook up our events (why are they are not defined)?
//			_smppSession.OnPostProcessPdu += OnPreprocessPdu;
//			_smppSession.OnPostProcessPdu += OnPostprocessPdu;
			_smppSession.OnSessionConnected += OnSessionConnected;
			_smppSession.OnSessionDisconnected += OnSessionDisconnected;
            _smppSession.OnBound += OnSessionBound;
			_smppSession.OnDeliverSm += OnDeliverSm;

			// Start ball rolling by attempting to connect to the SMSC
			ConnectToSmsc();

			Console.WriteLine("Press key to unbind session");
			Console.ReadKey();
			_smppSession.UnBind();
			Console.WriteLine("Session is bound: " + _smppSession.IsBound);
			Console.WriteLine("Press key to close session/socket");
			Console.ReadKey();
			_smppSession.Close();
			Console.WriteLine("Session is connected: " + _smppSession.IsConnected);
			Console.WriteLine("Press key to exit");
			Console.ReadKey();
		}

		public static void ConnectToSmsc()
		{
			Console.WriteLine("Connecting to SMSC...");
			try
			{
				_smppSession.Connect("127.0.0.1", 8080);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception trying to connect to Smsc: " + ex.ToString());
			}
		}

		private static void OnSessionConnected(object sender, SmppEventArgs args)
		{
			// Test bind status?
			// Send Bind PDU to SMSC asynchronously
			Console.WriteLine("Successfully connected to SMSC. Now binding...");
			bind_transceiver bindPdu = new bind_transceiver("RON-HAY-LAP", "test", "",
															new interface_version(),	// Default is version 3.4
															new address_range());
			_smppSession.BeginBindTransceiver(bindPdu, new AsyncCallback(BindTransceiverCallback));
		}

        private static void OnSessionBound(object sender, SmppEventArgs args)
        {
            // Session is now bound
            Console.WriteLine("Session is successfully bound to the SMSC");
            // Try sending a single message
            submit_sm submitPdu = new submit_sm();
            submitPdu.SourceAddress = new address(TypeOfNumber.NATIONAL, NumericPlanIndicator.E164, "9727321655");
            submitPdu.DestinationAddress = new address(TypeOfNumber.NATIONAL, NumericPlanIndicator.E164, "9724151634");
            submitPdu.RegisteredDelivery = new registered_delivery(DeliveryReceiptType.FINAL_DELIVERY_RECEIPT, AcknowledgementType.DELIVERY_USER_ACK_REQUEST, true);
            mBloxOperatorId operatorId = new mBloxOperatorId("12345");
            submitPdu.AddVendorSpecificElements(operatorId);
            submitPdu.Message = "This is a test";
//            TlvParameter test = submitPdu.GetOptionalElement(operatorId.Tag);
            _smppSession.BeginSubmitSm(submitPdu, new AsyncCallback(SubmitSmCallback));
        }

		private static void BindTransceiverCallback(IAsyncResult ar)
		{
			// Process the bind result
			EsmeSession session = (EsmeSession)ar.AsyncState;
			bind_transceiver_resp bindResp = session.EndBindTransceiver(ar);
			if (bindResp.Status != StatusCodes.ESME_ROK)
			{
				// Display binding error 
				Console.WriteLine("Error binding to SMSC: " + bindResp.Status.ToString());

				// Drop the session which will cause a reconnect
				_smppSession.Close();
			}
		}

		private static void SubmitSmCallback(IAsyncResult ar)
		{
            // Process the send/submit result
            EsmeSession session = (EsmeSession)ar.AsyncState;
			submit_sm_resp submitResp = session.EndSubmitSm(ar);
			Console.WriteLine(string.Format("Submit short message result: {0}, message id: {1}",
							  submitResp.Status, submitResp.MessageID));
		}

		// Used to get delivery ack.'s and MO messages from SMSC
		private static void OnDeliverSm(object sender, SmppEventArgs args)
		{
			deliver_sm req = (deliver_sm)args.PDU;
			deliver_sm_resp resp = (deliver_sm_resp)args.ResponsePDU;
			esm_class esm = req.EsmClass;
			switch (esm.MessageType)
			{
				case MessageType.SMSC_DELIVERY_RCPT:	// Delivery receipt for previously sent short message
					smsc_delivery_receipt receipt = new smsc_delivery_receipt(req.Message);
					Console.WriteLine(string.Format("Delivery receipt for message {0}: Type: {1}",
									  receipt.OriginalMessageId, receipt.FinalMessageStatus));								
					break;

				case MessageType.DEFAULT_MSG_TYPE:		// Mobile originated message
					Console.WriteLine(string.Format("Mobile originated message from {0}: {1}",
									  req.SourceAddress, req.Message));
					break;

				default:
					Console.WriteLine("Unexpected deliver_sm message type: " + esm.MessageType.ToString());
					break;
			}
		}

		private static void OnSessionDisconnected(object sender, SmppEventArgs args)
		{
			// If we are not shutting down then attempt to reconnect using a timer
			Console.WriteLine("Sesssion disconnected...");
		}

		public static void OnPreprocessPdu(object sender, SmppEventArgs ea)
		{
			Console.WriteLine(string.Format("RCV: {0}\r\n", ea.PDU.ToString()));
		}

		public static void OnPostprocessPdu(object sender, SmppEventArgs ea)
		{
			Console.WriteLine(string.Format("SEND: {0}\r\n{1}\r\n", ea.PDU.ToString(), ea.PDU.ToString("b")));
		}
	}

    public class mBloxOperatorId : TlvString
    {
        // Constants
        private const int MAX_LENGTH = 5;

        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = 0x1402;

        /// <summary>
        /// Default constructor
        /// </summary>
        public mBloxOperatorId()
            : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public mBloxOperatorId(string operatorId)
            : base(TlvTag)
        {
            this.Value = operatorId;
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Value.Length > MAX_LENGTH)
                throw new System.ArgumentOutOfRangeException("The OperatorId cannot exceed " + MAX_LENGTH.ToString() + " characters.");
        }
    }
}
