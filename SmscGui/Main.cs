#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using Smpp;
using Smpp.Pdu;
using Smpp.Elements;

#endregion

namespace SmscGui
{
    enum MessageResponse
    {
        Random = 0,
        Always_Succeed = 1,
        Always_Fail = 2,
        Manual_Response = 3
    }

    partial class MainForm : Form
    {
        SmppServer server_ = null;
        Random randGen_ = new Random();
        int nxtMid_ = 0;
        string password_ = "";
        byte supportedVersion_;
        MessageResponse respType_ = MessageResponse.Random;
        List<SmsMessage> arrMessages = new List<SmsMessage>();

        // Delegates
        delegate void SafeAddLogText(StatusCode code, string sMsg);

        public MainForm()
        {
            InitializeComponent();

            // Set the initial setting for our combo box
            tscbMessageResponseType.SelectedIndex = 0;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            InitForm form = new InitForm();
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                this.Close();
            }
            else
            {
                this.SetBounds(this.Left, this.Top, this.Width + 1, this.Height + 1);

                // Pull the data from the init form.
                password_ = form.Password;
                supportedVersion_ = (byte)form.Version;
                nxtMid_ = randGen_.Next(32000);

                // Start the SMPP server
                server_ = new SmppServer(form.SystemID);
                server_.OnNewSession += OnNewSession;
                server_.StartServer(form.Port);
                timer1.Enabled = true;
            }
        }

        private void AddLogText(StatusCode code, string sMsg)
        {
            // If we are not currently on the UI thread, then pass control to it.
            if (InvokeRequired)
            {
                SafeAddLogText del = this.AddLogText;
                Invoke(del, new object[] { code, sMsg });
                return;
            }

            switch (code)
            {
                case StatusCode.Normal:
                    richTextBox1.SelectionColor = Color.Black;
                    break;
                case StatusCode.Pdu:
                    richTextBox1.SelectionColor = Color.Blue;
                    break;
                case StatusCode.Session:
                    richTextBox1.SelectionColor = Color.Green;
                    break;
                case StatusCode.Error:
                    richTextBox1.SelectionColor = Color.Red;
                    break;
                default:
                    break;
            }
            richTextBox1.AppendText(sMsg + "\r\n");
        }

        private void OnPreprocessPdu(object sender, SmppEventArgs ea)
        {
            AddLogText(StatusCode.Pdu, string.Format("RCV: {0}\r\n", ea.PDU.ToString()));
        }

        private void OnPostprocessPdu(object sender, SmppEventArgs ea)
        {
            AddLogText(StatusCode.Pdu, string.Format("SEND: {0}\r\n{1}\r\n", ea.PDU.ToString(), ea.PDU.ToString("b")));
        }

        private void OnNewSession(object sender, SmppEventArgs ea)
        {
            SmppConnectEventArgs ce = (SmppConnectEventArgs) ea;

            // Get the session information.
            IPAddress ipAddr = ce.Address;

            AddLogText(StatusCode.Session, string.Format("Connection from {0} accepted.", ipAddr.ToString()));

            // Get the session and establish our event handlers.
            SmscSession sess = (SmscSession) ce.Session;
            sess.OnPreprocessPdu += OnPreprocessPdu;
            sess.OnPostprocessPdu += OnPostprocessPdu;
            sess.OnBind += OnBind;
            sess.OnSubmitSm += OnSubmitSm;
            sess.OnQuerySm += OnQuerySm;
            sess.OnCancelSm += OnCancelSm;
            sess.OnUnbind += OnUnbind;
            sess.OnError += OnError;
            sess.OnSessionDisconnect += OnDisconnect;

            // Add the session to our listview
            new SessionListViewItem(lvSessions, sess);
            EnableGenerateSmsButton(true);
        }

        private delegate void EnableGenerateSmsButtonDelegate(bool enable);
        private void EnableGenerateSmsButton(bool enable)
        {
            if (InvokeRequired)
            {
                BeginInvoke((EnableGenerateSmsButtonDelegate)EnableGenerateSmsButton, enable);
                return;
            }
            btnGenerateSms.Enabled = enable;
        }

		private delegate void OnDisconnectHandler(object sender, SmppEventArgs ea);
		private void OnDisconnect(object sender, SmppEventArgs ea)
        {
			// Assure we are on the UI thread
            if (InvokeRequired)
            {
                BeginInvoke((OnDisconnectHandler)OnDisconnect, sender, ea);
                return;
            }

            AddLogText(StatusCode.Session, string.Format("Connection from {0} dropped.", ea.Session.IPAddress.ToString()));

            SessionListViewItem lvi = (SessionListViewItem)ea.Session.Tag;
            if (lvi != null)
                lvi.Remove();

            // Turn off the generate if no sessions.
			if (server_.CurrentSessions.Count == 1)
				btnGenerateSms.Enabled = false;

            SmppDisconnectEventArgs dea = (SmppDisconnectEventArgs)ea;
            if (dea.Exception != null)
                AddLogText(StatusCode.Error, dea.Exception.ToString());
        }

        private void OnBind(object sender, SmppEventArgs ea)
        {
            SmppPdu pdu = ea.PDU;
            if ((pdu as bind_receiver) != null || (pdu as bind_transmitter) != null)
                ea.ResponsePDU.Status = StatusCodes.ESME_RINVCMDID;
            else
            {
                bind_transceiver btpdu = (bind_transceiver)pdu;
                if (btpdu.Password != password_)
                {
                    ea.ResponsePDU.Status = StatusCodes.ESME_RINVPASWD;
                    return;
                }

                bind_transceiver_resp resp = (bind_transceiver_resp) ea.ResponsePDU;
                resp.InterfaceVersion = (byte) supportedVersion_;
            }

            // Update the listview.
            new System.Threading.Timer(new TimerCallback(UpdateListItem), ea.Session.Tag, 100, Timeout.Infinite);
        }

        /// <summary>
        /// This callback updates a list item; we do this a bit tardy to allow the underlying session to 
        /// update to its new status.
        /// </summary>
        /// <param name="state">List item to update</param>
        private void UpdateListItem(object state)
        {
            IListViewItem item = (IListViewItem)state;
            item.UpdateState();
        }

        private void OnUnbind(object sender, SmppEventArgs ea)
        {
            new System.Threading.Timer(new TimerCallback(UpdateListItem), ea.Session.Tag, 100, Timeout.Infinite);
        }

        private void OnError(object sender, SmppEventArgs ea)
        {
            SmppErrorEventArgs eea = (SmppErrorEventArgs) ea;
            SmppPdu pdu = eea.PDU;
            if (pdu != null)
                AddLogText(StatusCode.Error, string.Format("{0} PDU: {1}", eea.Session, pdu.CommandId));
            else
                AddLogText(StatusCode.Error, string.Format("{0}", eea.Session));
            Exception ex = eea.Exception;
            while (ex != null)
            {
                AddLogText(StatusCode.Error, ex.ToString());
                ex = ex.InnerException;
            }
        }

        private void OnSubmitSm(object sender, SmppEventArgs ea)
        {
            submit_sm req = (submit_sm) ea.PDU;
            AddLogText(StatusCode.Normal, string.Format("SendSms: {0}", req.Message));
            
            submit_sm_resp resp = (submit_sm_resp)ea.ResponsePDU;
            resp.MessageID = Interlocked.Increment(ref nxtMid_).ToString();

            // Create our tracking message and insert it into the list.
            SmsMessage message = new SmsMessage(ea.Session, req, resp.MessageID, req.RegisteredDelivery.DeliveryReceipt);
            new MessageListViewItem(lvMessages, message);
            arrMessages.Add(message);

            // If we need a delivery receipt, then generate a callback to provide it.
            if (respType_ != MessageResponse.Manual_Response)
                new System.Threading.Timer(new TimerCallback(SendAck), message, randGen_.Next(10000), Timeout.Infinite);
        }

        private void OnQuerySm(object sender, SmppEventArgs ea)
        {
            query_sm req = (query_sm)ea.PDU;
            query_sm_resp resp = (query_sm_resp) ea.ResponsePDU;
            foreach (SmsMessage msg in arrMessages)
            {
                if (msg.MessageId == req.MessageID)
                {
                    if (msg.deliveryTime != DateTime.MinValue)
                        resp.FinalDate = new SmppTime(msg.deliveryTime, true);

                    switch (msg.deliveryResult)
                    {
                        case DeliveryStatus.ACCEPTED:
                            resp.MessageStatus = MessageStatus.ACCEPTED;
                            break;
                        case DeliveryStatus.DELETED:
                            resp.MessageStatus = MessageStatus.DELETED;
                            break;
                        case DeliveryStatus.DELIVERED:
                            resp.MessageStatus = MessageStatus.DELIVERED;
                            break;
                        case DeliveryStatus.EXPIRED:
                            resp.MessageStatus = MessageStatus.EXPIRED;
                            break;
                        case DeliveryStatus.REJECTED:
                            resp.MessageStatus = MessageStatus.REJECTED;
                            break;
                        case DeliveryStatus.UNDELIVERABLE:
                            resp.MessageStatus = MessageStatus.UNDELIVERABLE;
                            break;
                        default:
                            resp.MessageStatus = MessageStatus.ENROUTE;
                            break;
                    }
                    return;
                }
            }

            // Could not find message
            resp.ErrorCode = StatusCodes.ESME_RINVMSGID;
        }

        private void OnCancelSm(object sender, SmppEventArgs ea)
        {
            query_sm req = (query_sm)ea.PDU;
            query_sm_resp resp = (query_sm_resp)ea.ResponsePDU;
            foreach (SmsMessage msg in arrMessages)
            {
                if (msg.MessageId == req.MessageID)
                {
                    if (msg.deliveryTime != DateTime.MinValue)
                    {
                        msg.deliveryResult = DeliveryStatus.DELETED;
                        msg.lvi.UpdateState();
                        return;
                    }
                    else
                        resp.ErrorCode = StatusCodes.ESME_RCANCELFAIL;
                }
            }

            // Could not find message
            resp.ErrorCode = StatusCodes.ESME_RINVMSGID;
        }

        private void CompleteMessageDelivery(SmsMessage message)
        {
            // Generate a random delivery status.
            message.deliveryTime = DateTime.Now;

            switch (respType_)
            {
                case MessageResponse.Always_Fail:
                    message.deliveryResult = DeliveryStatus.REJECTED;
                    break;
                case MessageResponse.Always_Succeed:
                    message.deliveryResult = DeliveryStatus.DELIVERED;
                    break;
                default:
                    message.deliveryResult = (DeliveryStatus)(randGen_.Next(Enum.GetValues(typeof(DeliveryStatus)).Length));
                    if (message.deliveryResult == DeliveryStatus.UNKNOWN)
                        message.deliveryResult = DeliveryStatus.DELIVERED;
                    break;
            }

            message.lvi.UpdateState();
        }

        private void SendAck(object o)
        {
            SmsMessage message = (SmsMessage)o;
            
            if (message.deliveryTime == DateTime.MinValue)
                CompleteMessageDelivery(message);

            // If we are to send a receipt then do so.
            if ((message.deliveryReceipt == DeliveryReceiptType.FINAL_DELIVERY_RECEIPT) ||
                (message.deliveryReceipt == DeliveryReceiptType.FINAL_DELIVERY_FAIL_RECEIPT &&
                        (message.deliveryResult != DeliveryStatus.DELIVERED &&
                         message.deliveryResult != DeliveryStatus.ACCEPTED)))
            {
                deliver_sm pdu = new deliver_sm("", message.PDU.SourceAddress,
                        message.PDU.DestinationAddress,
                        new esm_class(MessagingMode.DEFAULT_MODE, MessageType.SMSC_DELIVERY_RCPT),
                        0, MessagePriority.NORMAL, new SmppTime(), new SmppTime(),
                        new registered_delivery(DeliveryReceiptType.NONE, AcknowledgementType.NONE, false),
                        false, DataEncoding.SMSC_DEFAULT, 0,
                            deliver_sm.BuildAckMessage(message.MessageId, 0, 0, DateTime.Now, DateTime.Now,
                                message.deliveryResult, 0, message.PDU.Message));

                ((SmscSession)message.Session).BeginDeliverSm(pdu, new AsyncCallback(EndDeliverSm));
            }
        }

        private void OnSendEnquire(object sender, EventArgs e)
        {
            if (btnEnableEnquire.Checked)
            {
                foreach (SmppSession session in server_.CurrentSessions)
                {
                    if (session.IsBound)
                        session.BeginEnquireLink(new AsyncCallback(EnquireLinkCallback));
                }
            }
        }

        private void EnquireLinkCallback(IAsyncResult ar)
        {
            SmscSession session = (SmscSession) ar.AsyncState;
            enquire_link_resp resp = session.EndEnquireLink(ar);
            if (resp.Status != StatusCodes.ESME_ROK)
                AddLogText(StatusCode.Error, string.Format("Enquire link to {0} failed, status={1:X}", session, resp.Status));
        }

        private void EndDeliverSm(IAsyncResult ar)
        {
            SmscSession session = (SmscSession) ar.AsyncState;
            deliver_sm_resp dresp = session.EndDeliverSm(ar);
            if (!dresp.Succeeded)
                AddLogText(StatusCode.Error, string.Format("DeliverSm failed: {0}", dresp));
        }

        private void OnGenerateSms(object sender, EventArgs e)
        {
            GenerateSms dlg = new GenerateSms(server_);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                deliver_sm pdu = new deliver_sm(dlg.ServiceType,
                        new address(TypeOfNumber.NATIONAL, NumericPlanIndicator.E164, dlg.SourceAddress),
                        new address(TypeOfNumber.NATIONAL, NumericPlanIndicator.E164, dlg.DestinationAddress),
                        new esm_class(MessagingMode.DEFAULT_MODE, MessageType.DEFAULT_MSG_TYPE),
                        0, MessagePriority.NORMAL, new SmppTime(), new SmppTime(),
                        new registered_delivery(DeliveryReceiptType.NONE, AcknowledgementType.NONE, false),
                        false, DataEncoding.SMSC_DEFAULT, 0, dlg.ShortMessage);
                SmscSession session = dlg.Session as SmscSession;
                if (session != null && session.IsBound)
                    session.BeginDeliverSm(pdu, new AsyncCallback(EndDeliverSm));
            }
        }

        private void OnMessageResponeTypeChanged(object sender, EventArgs e)
        {
            respType_ = (MessageResponse) tscbMessageResponseType.SelectedIndex;
        }

        private void OnSendFailResponse(object sender, EventArgs e)
        {
            foreach (MessageListViewItem lvi in lvMessages.SelectedItems)
            {
                SmsMessage message = lvi.Message;
                if (message != null && message.deliveryTime == DateTime.MinValue)
                {
                    message.deliveryTime = DateTime.Now;
                    message.deliveryResult = DeliveryStatus.EXPIRED;
                    lvi.UpdateState();
                    SendAck(message);
                }
            }
        }

        private void OnSendRejectResponse(object sender, EventArgs e)
        {
            foreach (MessageListViewItem lvi in lvMessages.SelectedItems)
            {
                SmsMessage message = lvi.Message;
                if (message != null && message.deliveryTime == DateTime.MinValue)
                {
                    message.deliveryTime = DateTime.Now;
                    message.deliveryResult = DeliveryStatus.REJECTED;
                    lvi.UpdateState();
                    SendAck(message);
                }
            }
        }

        private void OnSendSuccessResponse(object sender, EventArgs e)
        {
            foreach (MessageListViewItem lvi in lvMessages.SelectedItems)
            {
                SmsMessage message = lvi.Message;
                if (message != null && message.deliveryTime == DateTime.MinValue)
                {
                    message.deliveryTime = DateTime.Now;
                    message.deliveryResult = DeliveryStatus.DELIVERED;
                    lvi.UpdateState();
                    SendAck(message);
                }
            }
        }
    }
}