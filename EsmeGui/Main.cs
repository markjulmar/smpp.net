using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JulMar.Smpp.Esme;
using JulMar.Smpp;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Elements;
using System.Configuration;
using System.Timers;
using JulMar.Smpp.Utility;

namespace EsmeGui
{
    public partial class mainForm : Form
    {
        public enum SmscConnectionStatus
        {
            Disconnected = 0,
            Connected,
            Binding,
            Bound
        };

        public EsmeSession smppSession_ = null;
        public SmscConnectionStatus smscConnectionStatus_ = SmscConnectionStatus.Disconnected;
        public string smscAddress_;
        public int smscPort_;
        public string smscSystemId_;
        public string smscPassword_;
        public string smscSystemType_;
		public string submitServiceType_;
        public int enquireLinkSeconds_;
		public string destinationAdrPrefix_;
		public int submitResponseMessageIdBase_ = 10;

        public System.Timers.Timer enquireLinkTimer;

        public mainForm()
        {
            InitializeComponent();

            // Get the default settings from our configuration file
            smscAddress_ = GetAppSetting("SmscAddress", "127.0.0.1");
            smscPort_ = GetAppSettingInt("SmscPort", 8080);
            smscSystemId_ = GetAppSetting("SystemId", Environment.MachineName);
            smscPassword_ = GetAppSetting("Password", "");
            smscSystemType_ = GetAppSetting("SystemType", "");
			submitServiceType_ = GetAppSetting("ServiceType", "");
            enquireLinkSeconds_ = GetAppSettingInt("EnquireLinkSeconds", 30);
			destinationAdrPrefix_ = GetAppSetting("DestinationAddressPrefix", "1");
			submitResponseMessageIdBase_ = GetAppSettingInt("SubmitResponseMessageIdBase", 10);

            // Create the ESME Smpp session - default version is 3.4
            smppSession_ = new EsmeSession(smscSystemId_);
            smppSession_.SmppVersion = SmppVersion.SMPP_V34;

            // Hook up all events we need
            smppSession_.OnSessionConnected += OnSessionConnected;
            smppSession_.OnSessionDisconnected += OnSessionDisconnected;
            smppSession_.OnBound += OnSessionBound;
            smppSession_.OnDeliverSm += OnDeliverSm;
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            // Display the SMSC Connection dialog
            SmscConnectionDialog dlgConnection = new SmscConnectionDialog();
            dlgConnection.SmscAddress = smscAddress_;
            dlgConnection.SmscPort = smscPort_;
            dlgConnection.SystemID = smscSystemId_;
            dlgConnection.Password = smscPassword_;
            dlgConnection.SystemType = smscSystemType_;
            dlgConnection.EnquireLinkSeconds = enquireLinkSeconds_;

            if (dlgConnection.ShowDialog() == DialogResult.Cancel)
                this.Close();

            // Get all connection settings
            smscAddress_ = dlgConnection.SmscAddress;
            smscPort_ = dlgConnection.SmscPort;
            smscSystemId_ = dlgConnection.SystemID;
            smscPassword_ = dlgConnection.Password;
            smscSystemType_ = dlgConnection.SystemType;
            smppSession_.SmppVersion = (byte)dlgConnection.SmppInterfaceVersion;
            enquireLinkSeconds_ = dlgConnection.EnquireLinkSeconds;

            // Start the ball rolling by attempting to connect to the SMSC asynchronously
            ConnectToSmsc();
        }

        private void ConnectToSmsc()
        {
            try
            {
                smppSession_.Connect(smscAddress_, smscPort_);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception trying to connect to Smsc: " + ex.ToString());
                Close();
            }
        }

        // Update the SMSC connection status and related controls
        private delegate void SetSmscConnectionStatusHandler(SmscConnectionStatus status);
        private void SetSmscConnectionStatus(SmscConnectionStatus status)
        {
            if (InvokeRequired)
            {
                BeginInvoke((SetSmscConnectionStatusHandler)SetSmscConnectionStatus, status);
                return;
            }
            lblSmscConnectionStatus.Text = status.ToString();
            smscConnectionStatus_ = status;
            bool isBound = (status == SmscConnectionStatus.Bound);
            btnSendMessage.Enabled = isBound;
        }

        // Called when the connection to SMSC is established
        private void OnSessionConnected(object sender, SmppEventArgs args)
        {
            // Update the SMSC connection status and related controls
            SetSmscConnectionStatus(SmscConnectionStatus.Connected);

            // Send Bind PDU to SMSC asynchronously
            bind_transceiver bindPdu = new bind_transceiver(smscSystemId_, smscPassword_, "",
                                                            new interface_version(),	// Default is version 3.4
                                                            new address_range());
            bindPdu.SystemType = smscSystemType_;
            smppSession_.BeginBindTransceiver(bindPdu, new AsyncCallback(BindTransceiverCallback));
            SetSmscConnectionStatus(SmscConnectionStatus.Bound);
        }

        // Used to catch and display binding issues
        private void BindTransceiverCallback(IAsyncResult ar)
        {
            // Process the bind result
            EsmeSession session = (EsmeSession)ar.AsyncState;
            bind_transceiver_resp bindResp = session.EndBindTransceiver(ar);
            if (bindResp.Status != StatusCodes.ESME_ROK)
            {
                MessageBox.Show("Error binding to SMSC: " + bindResp.Status.ToString());
//                Close();
            }
        }

        private void OnSessionBound(object sender, SmppEventArgs args)
        {
            // Set the session to bound
            // If the enquireLinkSeconds_ is non-zero then create a timer to send enquire link PDU's
            SetSmscConnectionStatus(SmscConnectionStatus.Bound);
            if (enquireLinkSeconds_ != 0)
            {
                enquireLinkTimer = new System.Timers.Timer((double)enquireLinkSeconds_ * 1000);
                enquireLinkTimer.Elapsed += new ElapsedEventHandler(OnEnquireLinkElapsedTimer);
                enquireLinkTimer.Enabled = true;
            }
        }

        // Used to send Enquire Link messages to SMSC every enquireLinkSeconds_ seconds
        private void OnEnquireLinkElapsedTimer(object source, ElapsedEventArgs e)
        {
            // Send if our session is still connected/bound
            if ((smppSession_.IsConnected) && (smppSession_.IsBound))
            {
                enquire_link enquirePdu = new enquire_link();
                smppSession_.BeginEnquireLink(enquirePdu, new AsyncCallback(EnquireLinkCallback));
            }
        }

        private void EnquireLinkCallback(IAsyncResult ar)
        {
            // Process the enquire link result
            EsmeSession session = (EsmeSession)ar.AsyncState;
            enquire_link_resp enquireResp = session.EndEnquireLink(ar);
            if (enquireResp.Status != StatusCodes.ESME_ROK)
            {
                MessageBox.Show("Error sending enquire link to SMSC: " + enquireResp.Status.ToString());
            }
        }

        // Used to get delivery ack.'s and MO messages from SMSC
        private void OnDeliverSm(object sender, SmppEventArgs args)
        {
            deliver_sm req = (deliver_sm)args.PDU;
            deliver_sm_resp resp = (deliver_sm_resp)args.ResponsePDU;
            esm_class esm = req.EsmClass;
            switch (esm.MessageType)
            {
                case MessageType.SMSC_DELIVERY_RCPT:	// Delivery receipt for previously sent short message
                    smsc_delivery_receipt receipt = new smsc_delivery_receipt(req.Message);
                    // Update the final deleivery status for the sent message using the message Id to locate
                    UpdateSentMessageFinalStatus(receipt);
                    break;

                case MessageType.DEFAULT_MSG_TYPE:		// Mobile originated message
                    AddNewMessage("Mobile Orig.", req.SourceAddress.Address, req.Message, "DELIVRD", DateTime.Now.ToString("d"), 0);
                    break;

                default:
					AddNewMessage("Error", req.SourceAddress.Address, "Unknown message type - " + esm.MessageType, "n/a", "n/a", 0);
					break;
            }
        }

        private void OnSessionDisconnected(object sender, SmppEventArgs args)
        {
            SmppDisconnectEventArgs dea = (SmppDisconnectEventArgs)args;
            if (dea.Exception != null)
                MessageBox.Show("Socket error: " + ((dea.Exception.Message != null) ? dea.Exception.Message : dea.Exception.ToString()));
            else
                MessageBox.Show("Smsc Session/Connection was dropped");

            SetSmscConnectionStatus(SmscConnectionStatus.Disconnected);
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            // Confirm all settings
            string sourceNumber = txtSourceNumber.Text.Trim();
            string rawTargetNumber = txtDestinationNumber.Text.Trim();
            string message = txtMessage.Text.Trim();
			if ((sourceNumber.Length < 5) || (rawTargetNumber.Length < 10) || (message.Length == 0))
            {
                MessageBox.Show("Either a number is invalid or the message is blank");
                return;
            }
			// Add target address prefix
			string targetNumber = destinationAdrPrefix_ + rawTargetNumber;

            // Disable send until we get a reply from the SMSC for the current nessage
            btnSendMessage.Enabled = false;

			submit_sm submitPdu = new submit_sm();
			if (!string.IsNullOrEmpty(submitServiceType_))
				submitPdu.ServiceType = submitServiceType_;
            submitPdu.SourceAddress = new address(TypeOfNumber.NATIONAL, NumericPlanIndicator.E164, sourceNumber);
            submitPdu.DestinationAddress = new address(TypeOfNumber.NATIONAL, NumericPlanIndicator.E164, targetNumber);
            submitPdu.RegisteredDelivery = new registered_delivery(DeliveryReceiptType.FINAL_DELIVERY_RECEIPT, AcknowledgementType.DELIVERY_USER_ACK_REQUEST, true);
            submitPdu.Message = message;
            smppSession_.BeginSubmitSm(submitPdu, new AsyncCallback(SubmitSmCallback));

            // Add the message to the sent listview using the message sequence # for tracking
			AddNewMessage("Pending", targetNumber, message, "n/a", "n/a", submitPdu.SequenceNumber);
        }

		private delegate void AddNewMesasgeHandler(string messageId, string phoneNumber, string message, string finalStatus, string deliveryDateTime, int tag);
		public void AddNewMessage(string messageId, string phoneNumber, string message, string finalStatus, string deliveryDateTime, int tag)
		{
			if (InvokeRequired)
			{
				BeginInvoke((AddNewMesasgeHandler)AddNewMessage, messageId, phoneNumber, message, finalStatus, deliveryDateTime, tag);
				return;
			}
			ListViewItem msgItem = new ListViewItem(messageId);
			msgItem.SubItems.Add(phoneNumber);
			if (message.Length >= 40)
				message = message.Substring(0, 40) + "...";
			msgItem.SubItems.Add(message);
			msgItem.SubItems.Add(finalStatus);
			msgItem.SubItems.Add(deliveryDateTime);
			msgItem.Tag = tag;
			lvSentMsgs.Items.Add(msgItem);
		}

        private delegate void SubmitSmCallbackHandler(IAsyncResult ar);
        private void SubmitSmCallback(IAsyncResult ar)
        {
            if (InvokeRequired)
            {
                BeginInvoke((SubmitSmCallbackHandler)SubmitSmCallback, ar);
                return;
            }
            // Process the send/submit result
            EsmeSession session = (EsmeSession)ar.AsyncState;										
            submit_sm_resp submitResp = session.EndSubmitSm(ar);

            // Update the message # in the message list using the sequence #
            string status = "Pending";
            if (submitResp.Status != StatusCodes.ESME_ROK)
            {
                MessageBox.Show("Error sending message: " + submitResp.Status);
				UpdateSentMessageId(submitResp.SequenceNumber, "******", "Error: " + submitResp.Status);
				return;
            }
			// Some carriers submit_sm message ID are in hex - convert to decimal since delivery receipt is in decimal
			string decimalMessageId = Convert.ToInt64(submitResp.MessageID, submitResponseMessageIdBase_).ToString();
			UpdateSentMessageId(submitResp.SequenceNumber, decimalMessageId, status);

            // Re-enable the Send button...
            btnSendMessage.Enabled = true;
        }

        private delegate void UpdateSentMessageIdHandler(int sequenceNumber, string messageID, string status);
        private void UpdateSentMessageId(int sequenceNumber, string messageID, string status)
        {
            if (InvokeRequired)
            {
                BeginInvoke((UpdateSentMessageIdHandler)UpdateSentMessageId, sequenceNumber, messageID, status);
                return;
            }

            // Locate the sent message using the sequence # (tag) starting w/last
            ListViewItem msgItem = null;
            int itemTag;
            for (int count = lvSentMsgs.Items.Count - 1; count >= 0; --count)
            {
                msgItem = lvSentMsgs.Items[count];
                itemTag = (int) msgItem.Tag;
                if (itemTag == sequenceNumber)
                {
                    msgItem.Text = messageID;
                    return;
                }
            }
        }

        private delegate void UpdateSentMessageFinalStatusHandler(smsc_delivery_receipt receipt);
        private void UpdateSentMessageFinalStatus(smsc_delivery_receipt receipt)
        {
            if (InvokeRequired)
            {
                BeginInvoke((UpdateSentMessageFinalStatusHandler)UpdateSentMessageFinalStatus, receipt);
                return;
            }

            // Locate the sent message using the message Id
            ListViewItem msgItem = lvSentMsgs.FindItemWithText(receipt.OriginalMessageId);
            if (msgItem != null)
            {
                msgItem.SubItems[3].Text = receipt.FinalMessageStatus.ToString();
                msgItem.SubItems[4].Text = receipt.DeliveryDateTime.ToString();
            }
//            MessageBox.Show(receipt.FormattedReceiptMessage);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lvSentMsgs.Items.Clear();
        }

        private string GetAppSetting(string setting, string defaultValue)
        {
            string value = ConfigurationManager.AppSettings.Get(setting);
            return String.IsNullOrEmpty(value) ? defaultValue : value;
        }

        private int GetAppSettingInt(string setting, int defaultValue)
        {
            int intValue = defaultValue;
            string value = ConfigurationManager.AppSettings.Get(setting);
            if (!String.IsNullOrEmpty(value))
                Int32.TryParse(value, out intValue);
            return intValue;
        }

        private bool GetAppSettingBool(string setting, bool defaultValue)
        {
            bool boolValue = defaultValue;
            string value = ConfigurationManager.AppSettings.Get(setting);
            if (!String.IsNullOrEmpty(value))
                Boolean.TryParse(value, out boolValue);
            return boolValue;
        }
    }
}
