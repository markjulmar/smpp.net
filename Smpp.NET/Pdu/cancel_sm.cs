using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The cancel_sm operation is sent by the ESME to cancel one or more previously submitted
	/// short messages that are still pending delivery.  The command may specify a particular message
	/// to cancel, or all messages for a particular source, destination, and service_type.
	/// </summary>
	public class cancel_sm : SmppRequest {
		// Class data - required parameters
		private service_type stype_ = new service_type();
		private message_id msgid_ = new message_id();
		private address saddr_ = new address();
		private address daddr_ = new address();

		/// <summary>
		/// Default constructor
		/// </summary>
		public cancel_sm()
			: base(Commands.CANCEL_SM) {
		}

		/// <summary>
		/// Primary constructor for the cancel_sm PDU
		/// </summary>
		/// <param name="stype">Service Type</param>
		/// <param name="msgID">Message ID to cancel</param>
		/// <param name="saddr">Source Address to cancel pending messages for.</param>
		/// <param name="daddr">Destination Address to cancel pending messages for.</param>
		public cancel_sm(string stype, string msgID, address saddr, address daddr)
			: this() {
			this.MessageID = msgID;
			this.ServiceType = stype;
			this.SourceAddress = saddr;
			this.DestinationAddress = daddr;
		}

		/// <summary>
		/// Ths service_type parameter can be used to indicate the SMS application service
		/// associated with the message.
		/// </summary>
		public string ServiceType {
			get { return stype_.Value; }
			set { stype_.Value = value; }
		}


		/// <summary>
		/// Ths message ID of the specific message to be canceled.  This must be the SMSC
		/// assigned message ID allocated to the original short message when submitted to the 
		/// SMSC by the submit_sm, data_sm, or submit_multi command, and returned in the response
		/// PDU by the SMSC.  If canceling multiple messages, this should be NULL (0).
		/// </summary>
		public string MessageID {
			get { return msgid_.Value; }
			set { msgid_.Value = value; }
		}

		/// <summary>
		/// Address of SME which originated this message. If canceling only one message, 
		/// this should be null.
		/// </summary>
		public address SourceAddress {
			get { return saddr_; }
			set { saddr_ = value; }
		}

		/// <summary>
		/// Address of SME identified in the message. If canceling only one message, 
		/// this should be null.
		/// </summary>
		public address DestinationAddress {
			get { return daddr_; }
			set { daddr_ = value; }
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
			writer.Add(daddr_);
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
			reader.ReadObject(daddr_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("cancel_sm: {0},svc_type={1},msgid={2},src_{3},dst_{4}{5}",
				base.ToString(), stype_, msgid_, saddr_, daddr_, base.DumpOptionalParams());
		}
	}
}
