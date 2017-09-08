using System;
using System.Collections.Generic;
using System.Text;
using JulMar.Smpp.Utility;
using System.IO;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// Used to create/parse SMSC delivery receipts
    /// </summary>
    public class smsc_delivery_receipt
	{
		/// <summary>
		/// The final message delivery status
		/// </summary>
		public enum FinalDeliveryStatus
		{
			/// <summary>
			/// Message was delivered to destination
			/// </summary>
			DELIVERED = 0,
			/// <summary>
			/// Message validity period has expired
			/// </summary>
			EXPIRED,
			/// <summary>
			/// Message has been deleted
			/// </summary>
			DELETED,
			/// <summary>
			/// Message is undeliverable
			/// </summary>
			UNDELIVERABLE,
			/// <summary>
			/// Message is in accepted state (manually read by customer service)
			/// </summary>
			ACCEPTED,
			/// <summary>
			/// Message is in an invalid state
			/// </summary>
			UNKNOWN,
			/// <summary>
			/// Message is in a rejected state
			/// </summary>
			REJECTED
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public smsc_delivery_receipt()
		{
		}

		/// <summary>
		/// Parameterized constructor - Formatted SMSC delivery receipt
		/// </summary>
		public smsc_delivery_receipt(string msg)
		{
			// Parse out members from message
			// id: sub: dlvrd: submit date: done date: stat: err: text:
			int index = msg.IndexOf("id:");
			if (index != -1)
			{
				originalMessageId_ = msg.Substring(index + 3, 10).Trim();
			}
			index = msg.IndexOf("sub:");
			if (index != -1)
			{
				Int32.TryParse(msg.Substring(index + 4, 3), out numMessagesSubmitted_);
			}
			index = msg.IndexOf("dlvrd:");
			if (index != -1)
			{
				Int32.TryParse(msg.Substring(index + 6, 3), out numMessagesDelivered_);
			}
			index = msg.IndexOf("submit date:");
			if (index != -1)
			{
				MemoryStream memStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(msg.Substring(index + 12, 10)));
				SmppReader reader = new SmppReader((Stream)memStream);
				submitDateTime_ = new SmppTime();
				submitDateTime_.GetFromStream(reader);
			}
			index = msg.IndexOf("done date:");
			if (index != -1)
			{
				MemoryStream memStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(msg.Substring(index + 10, 10)));
				SmppReader reader = new SmppReader((Stream)memStream);
				deliveryDateTime_ = new SmppTime();
				deliveryDateTime_.GetFromStream(reader);
			}
			index = msg.IndexOf("stat:");
			if (index != -1)
			{
				string finalStatus = msg.Substring(index + 5, 7).ToUpper().Trim();
				switch (finalStatus)
				{
					case "DELIVRD":
						finalMessageStatus_ = FinalDeliveryStatus.DELIVERED;
						break;
					case "EXPIRED":
						finalMessageStatus_ = FinalDeliveryStatus.EXPIRED;
						break;
					case "DELETED":
						finalMessageStatus_ = FinalDeliveryStatus.DELETED;
						break;
					case "UNDELIV":
						finalMessageStatus_ = FinalDeliveryStatus.UNDELIVERABLE;
						break;
					case "ACCEPTD":
						break;
					case "REJECTD":
						finalMessageStatus_ = FinalDeliveryStatus.REJECTED;
						break;
					default:
					case "UNKNOWN":
						finalMessageStatus_ = FinalDeliveryStatus.UNKNOWN;
						break;
				}
			}
			index = msg.IndexOf("err:");
			if (index != -1)
			{
				errorCode_ = msg.Substring(index + 4, 3).Trim();
			}
			index = msg.IndexOf("text:");
			if (index != -1)
			{
				messageStart_ = msg.Substring(index + 6).Trim();
			}
		}

		/// <summary>
		/// This property returns the original message ID returned by the SMSC
		/// </summary>
		public string OriginalMessageId
		{
			get { return originalMessageId_; }
			set { originalMessageId_ = value; }
		}
        private string originalMessageId_ = "";

		/// <summary>
		/// This property returns the original number of messages submitted
		/// </summary>
		public int NumMessagesSubmitted
		{
			get{ return numMessagesSubmitted_; }
			set{ numMessagesSubmitted_ = value; }
		}
        private int numMessagesSubmitted_ = 0;

		/// <summary>
		/// This property returns the original number of messages delivered
		/// </summary>
		public int NumMessagesDelivered
		{
			get { return numMessagesDelivered_; }
			set { numMessagesDelivered_ = value; }
		}
        private int numMessagesDelivered_ = 0;

		/// <summary>
		/// This property returns the date and time the message(s) were submitted
		/// </summary>
		public SmppTime SubmitDateTime
		{
			get { return submitDateTime_; }
			set { submitDateTime_ = value; }
		}
		private SmppTime submitDateTime_ = new SmppTime();

		/// <summary>
		/// This property returns the date and time the message(s) were delivered
		/// </summary>
		public SmppTime DeliveryDateTime
		{
			get { return deliveryDateTime_; }
			set { deliveryDateTime_ = value; }
		}
		private SmppTime deliveryDateTime_ = new SmppTime();

		/// <summary>
		/// This property returns the final message status
		/// </summary>
		public FinalDeliveryStatus FinalMessageStatus
		{
			get { return finalMessageStatus_; }
			set {finalMessageStatus_ = value; }
		}
		private FinalDeliveryStatus finalMessageStatus_ = FinalDeliveryStatus.UNKNOWN;

		/// <summary>
		/// This property returns a network or SMSC specific error code
		/// </summary>
		public string ErrorCode
		{
			get { return errorCode_; }
			set { errorCode_ = value; }
		}
		private string errorCode_ = "";

		/// <summary>
		/// This property returns the first 20 charcaters of the message
		/// </summary>
		public string MessageStart
		{
			get { return messageStart_; }
			set { messageStart_ = value; }
		}
		private string messageStart_ = "";

        /// <summary>
        /// This property generates a SMSC delibery receipt using current class settings
        /// </summary>
        public string FormattedReceiptMessage
		{
			get
			{
				return String.Format(@"id:{0} sub:{1} dlvrd:{2} submit date:{3} done date:{4} stat:{5} err:{6} text:{3}",
						  originalMessageId_.ToString().PadRight(10).Substring(0, 10),
						  numMessagesSubmitted_.ToString().PadLeft(3, '0').Substring(0, 3),
						  numMessagesDelivered_.ToString().PadLeft(3, '0').Substring(0, 3),
						  submitDateTime_.Value,
						  deliveryDateTime_.Value,
						  finalMessageStatus_.ToString(),
						  errorCode_.ToString().PadRight(3).Substring(0, 3),
						  messageStart_.PadRight(20).Substring(0, 20));
			}
		}
	}
}
