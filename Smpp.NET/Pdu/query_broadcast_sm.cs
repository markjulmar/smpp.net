using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The query_broadcast_sm operation is issued by the ESME to query the status 
	/// of a previously submitted broadcast message.  The message can be queried
	/// either on the basis of the Message Center assigned reference message_id
	/// reference number returned by the broadcast_sm_resp or by the ESME assigned
	/// user_message_reference element.
	/// </summary>
	public class query_broadcast_sm : SmppRequest {
		// Class data - required parameters
		private message_id msgid_ = new message_id();
		private address saddr_ = new address();
		private user_message_reference umsgid_ = new user_message_reference();

		/// <summary>
		/// Default constructor
		/// </summary>
		public query_broadcast_sm()
			: base(Commands.QUERY_BROADCAST_SM) {
			AddOptionalElements(umsgid_);
		}

		/// <summary>
		/// Primary constructor for the query_broadcast_sm PDU
		/// </summary>
		/// <param name="msgid">Message ID of the message whose state is being queried.</param>
		/// <param name="saddr">Source Address of the original request</param>
		public query_broadcast_sm(string msgid, address saddr)
			: this() {
			this.MessageID = msgid;
			this.SourceAddress = saddr;
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
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(msgid_);
			writer.Add(saddr_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(msgid_);
			reader.ReadObject(saddr_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("query_broadcast_sm: {0},msgid={1},src_{2}{3}",
				 base.ToString(), msgid_, saddr_, base.DumpOptionalParams());
		}
	}
}
