using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The cancel_broadcast_sm operation is issued by the ESME to cancel a
	/// previously submitted broadcast message which is still pending delivery.  
	/// If the message_id is set to the ID of a previously submitted message, then
	/// provided that the source address matches that of the stored message, that message
	/// will be canceled.  If the message_id is null, then all outstanding undelivered
	/// messages with matching source/destination addresses are canceled.
	/// </summary>
	public class cancel_broadcast_sm : SmppRequest {
		// Class data - required parameters
		private service_type stype_ = new service_type();
		private message_id msgid_ = new message_id();
		private address saddr_ = new address();
		private broadcast_content_type contentType_ = new broadcast_content_type();
		private user_message_reference umsgid_ = new user_message_reference();

		/// <summary>
		/// Default constructor
		/// </summary>
		public cancel_broadcast_sm()
			: base(Commands.CANCEL_BROADCAST_SM) {
			AddOptionalElements(contentType_, umsgid_);
		}

		/// <summary>
		/// Primary constructor for the cancel_broadcast_sm PDU
		/// </summary>
		/// <param name="stype">Service Type</param>
		/// <param name="msgid">Message ID of the message whose state is being queried.</param>
		/// <param name="saddr">Source Address of the original request</param>
		public cancel_broadcast_sm(string stype, address saddr, string msgid)
			: this() {
			this.ServiceType = stype;
			this.MessageID = msgid;
			this.SourceAddress = saddr;
		}

		/// <summary>
		/// Ths service_type parameter cna be used to indicate the SMS application service
		/// associated with the message.
		/// </summary>
		public string ServiceType {
			get { return stype_.Value; }
			set { stype_.Value = value; }
		}

		/// <summary>
		/// Ths message ID of the message whose state is to be queried.  This must be the SMSC
		/// assigned message ID allocated to the original short message when submitted to the 
		/// SMSC by the submit_sm, data_sm, or submit_multi command, and returned in the response
		/// PDU by the SMSC.
		/// </summary>
		public string MessageID {
			get { return msgid_.Value; }
			set { msgid_.Value = value; }
		}

		/// <summary>
		/// Address of SME which originated this message. This must be set to the same
		/// source address of the original submit request PDU which generated the message.
		/// </summary>
		public address SourceAddress {
			get { return saddr_; }
			set { saddr_ = value; }
		}

		/// <summary>
		/// This returns whether the user message ID is filled in.
		/// </summary>
		public bool HasUserMesssageID {
			get { return umsgid_.HasValue; }
		}

		/// <summary>
		/// This allows the get/set of the user message ID
		/// </summary>
		public int UserMessageID {
			get { return umsgid_.Value; }
			set { umsgid_.Value = value; }
		}

		/// <summary>
		/// Returns whether the content type is valid.
		/// </summary>
		public bool HasContentType {
			get { return contentType_.HasValue; }
		}

		/// <summary>
		/// Specifies the content type of the message
		/// </summary>
		public broadcast_content_type Content {
			get {
				return contentType_;
			}
			set {
				contentType_ = value;
			}
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(stype_);
			writer.Add(msgid_);
			writer.Add(saddr_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(stype_);
			reader.ReadObject(msgid_);
			reader.ReadObject(saddr_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("cancel_broadcast_sm: {0},svc_type={1},msgid={2},src={3}{4}",
				 base.ToString(), stype_, msgid_, saddr_, base.DumpOptionalParams());
		}
	}
}
