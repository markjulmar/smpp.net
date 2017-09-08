#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Smpp;
using Smpp.Pdu;
using Smpp.Elements;

#endregion

namespace SmscGui
{
    /// <summary>
    /// These status codes are used to color the listbox
    /// </summary>
    enum StatusCode
    {
        Normal,
        Pdu,
        Session,
        Error
    }

    /// <summary>
    /// This is the interface our "smart" listview items are derived from
    /// </summary>
    public interface IListViewItem
    {
        void Remove();
        void UpdateState();
    }

    /// <summary>
    /// This class overrides the ListViewItem object in order to provide a "thread-safe" version
    /// which is bound to a specific SmppSession
    /// </summary>
    public class SessionListViewItem : ListViewItem, IListViewItem
    {
        private SmppSession sess_ = null;
        private ListView lvOwner_ = null;

        public SessionListViewItem(ListView lv, SmppSession sess)
        {
            sess.Tag = this;
            sess_ = sess;
            lvOwner_ = lv;

            this.SubItems.AddRange(new string[]{"","",""});

            // Update the item
            UpdateState();
        }

        /// <summary>
        /// This method removes our association with the listview and session.
        /// </summary>
        public new void Remove()
        {
            if (lvOwner_ == null)
                return;

            if (lvOwner_.InvokeRequired)
            {
                lvOwner_.Invoke(new MethodInvoker(Remove), null);
                return;
            }

            lvOwner_.Items.Remove(this);
            lvOwner_ = null;
            sess_.Tag = null;
            sess_ = null;
        }

        /// <summary>
        /// This method is used to trigger an update to the object.
        /// </summary>
        public void UpdateState()
        {
            if (lvOwner_ == null)
                return;

            if (lvOwner_.InvokeRequired)
            {
                lvOwner_.Invoke(new MethodInvoker(UpdateState), null);
                return;
            }

            // Set our text elements
            this.Text = (sess_.PeerSystemID != "") ? sess_.PeerSystemID : "n/a";
            this.SubItems[1].Text = sess_.IPAddress.ToString();

            if (sess_.IsBound)
            {
                string version = sess_.BindStatus.ToString() + " (";
                switch (sess_.SmppVersion)
                {
                    case SmppVersion.SMPP_V33: version += "3.3"; break;
                    case SmppVersion.SMPP_V34: version += "3.4"; break;
                    case SmppVersion.SMPP_V50: version += "5.0"; break;
                    default: version += "n/a"; break;
                }
                version += ")";
                this.SubItems[2].Text = version;
            }
            else
                this.SubItems[2].Text = "n/a";

            // Add the item if necessary
            if (!lvOwner_.Items.Contains(this))
                lvOwner_.Items.Add(this);
        }
    }

    /// <summary>
    /// This class represents a single Sms message
    /// </summary>
    public class SmsMessage
    {
        public SmppSession Session;
        public submit_sm PDU;
        public MessageListViewItem lvi = null;
        public string MessageId;
        
        public DateTime submitTime = DateTime.Now;
        public DateTime deliveryTime = DateTime.MinValue;
        public DeliveryStatus deliveryResult = DeliveryStatus.UNKNOWN;
        public DeliveryReceiptType deliveryReceipt;

        public SmsMessage(SmppSession sess, submit_sm pdu, string msgId, DeliveryReceiptType responseRequested)
        {
            Session = sess;
            PDU = pdu;
            MessageId = msgId;
            deliveryReceipt = responseRequested;
        }
    }

    /// <summary>
    /// This is our custom ListViewItem for displaying Sms Messages.
    /// </summary>
    public class MessageListViewItem : ListViewItem, IListViewItem
    {
        private SmsMessage msg_ = null;
        private ListView lvOwner_;

        public MessageListViewItem(ListView lv, SmsMessage msg)
        {
            msg_ = msg;
            lvOwner_ = lv;
            msg_.lvi = this;

            SubItems.AddRange(new string[]{"","","","","",""});
            UpdateState();
        }

        public SmsMessage Message
        {
            get { return msg_; }
        }

        /// <summary>
        /// This method removes our association with the listview and session.
        /// </summary>
        public new void Remove()
        {
            if (lvOwner_.InvokeRequired)
            {
                lvOwner_.Invoke(new MethodInvoker(Remove), null);
                return;
            }

            lvOwner_.Items.Remove(this);
            lvOwner_ = null;
            msg_ = null;
        }

        public void UpdateState()
        {
            if (lvOwner_.InvokeRequired)
            {
                lvOwner_.Invoke(new MethodInvoker(UpdateState), null);
                return;
            }

            // Add the item if necessary
            if (!lvOwner_.Items.Contains(this))
                lvOwner_.Items.Add(this);

            this.Text = Message.MessageId;
            this.SubItems[1].Text = Message.PDU.SourceAddress.Address;
            this.SubItems[2].Text = Message.PDU.DestinationAddress.Address;
            this.SubItems[3].Text = Message.submitTime.ToString();
            if (Message.deliveryTime != DateTime.MinValue)
                this.SubItems[4].Text = Message.deliveryTime.ToString();
            else
                this.SubItems[4].Text = "n/a";
            this.SubItems[5].Text = Message.deliveryResult.ToString();

            // Add the item if necessary
            if (!lvOwner_.Items.Contains(this))
                lvOwner_.Items.Add(this);
        }

    }
}
