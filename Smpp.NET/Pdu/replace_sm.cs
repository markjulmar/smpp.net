using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The replace_sm operation is issued by the ESME to replace a previously submitted short
	/// message that is still pending delivery.  The matching mechanism is based on the message_id
	/// and source address of the original message.
	/// </summary>
	public class replace_sm : SmppRequest {
		// Class data - required parameters
		private message_id msgid_ = new message_id();
		private service_type stype_ = new service_type();
		private address saddr_ = new address();
		private schedule_delivery_time deliveryTime_ = new schedule_delivery_time();
		private validity_period validPeriod_ = new validity_period();
		private registered_delivery regDelivery_ = new registered_delivery();
		private sm_default_msg_id defMsgId_ = new sm_default_msg_id();
		private short_message msg_ = new short_message();

		/// <summary>
		/// Default constructor
		/// </summary>
		public replace_sm()
			: base(Commands.REPLACE_SM) {
		}

		/// <summary>
		/// Primary constructor for the replace_sm PDU
		/// </summary>
		/// <param name="msgid">Message ID of the message to be replaced.</param>
		/// <param name="stype">Service Type</param>
		/// <param name="saddr">Source Address</param>
		/// <param name="delTime">Delivery Time</param>
		/// <param name="valPeriod">Validation Period</param>
		/// <param name="regDelivery">Registered Delivery</param>
		/// <param name="defMsgId">Default Msg ID</param>
		/// <param name="msg">Message</param>
		public replace_sm(string msgid, string stype, address saddr, SmppTime delTime, SmppTime valPeriod,
			registered_delivery regDelivery, byte defMsgId, string msg)
			: this() {
			this.MessageID = msgid;
			this.ServiceType = stype;
			this.SourceAddress = saddr;
			this.DeliveryTime = delTime;
			this.ValidityPeriod = valPeriod;
			this.RegisteredDelivery = regDelivery;
			this.SmDefaultMessageID = defMsgId;
			this.Message = msg;
		}

		/// <summary>
		/// Ths message ID of the specific message to replace.  This must be the SMSC
		/// assigned message ID allocated to the original short message when submitted to the 
		/// SMSC by the submit_sm, data_sm, or submit_multi command, and returned in the response
		/// PDU by the SMSC. 
		/// </summary>
		public string MessageID {
			get { return msgid_.Value; }
			set { msgid_.Value = value; }
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
		/// Address of SME which originated this message. If not known, set to null.
		/// </summary>
		public address SourceAddress {
			get { return saddr_; }
			set { saddr_ = value; }
		}

		/// <summary>
		/// The short message is to be scheduled by the SMSC for delivery.
		/// Set to default schedule_delivery_time for immediate delivery.
		/// </summary>
		public SmppTime DeliveryTime {
			get { return (SmppTime)deliveryTime_; }
			set { deliveryTime_ = new schedule_delivery_time(value); }
		}

		/// <summary>
		/// The validity period of this message.  Set to default validity_period
		/// for default behavior.
		/// </summary>
		public SmppTime ValidityPeriod {
			get { return (SmppTime)validPeriod_; }
			set { validPeriod_ = new validity_period(value); }
		}

		/// <summary>
		/// Indicator to signify if an SMSC delivery receipt or SME acknowledgement is required.
		/// </summary>
		public registered_delivery RegisteredDelivery {
			get { return regDelivery_; }
			set { regDelivery_ = value; }
		}

		/// <summary>
		/// Indicates the short message to send from a list of predefined "canned" messages
		/// stored on the SMSC.  If not using, set to zero.
		/// </summary>
		public byte SmDefaultMessageID {
			get { return defMsgId_.Value; }
			set { defMsgId_.Value = value; }
		}

		/// <summary>
		/// Up to 254 bytes of text message to send.
		/// </summary>
		public string Message {
			get { return msg_.TextValue; }
			set { msg_.TextValue = value; }
		}

		/// <summary>
		/// This retrieves the short message in a byte array
		/// </summary>
		public byte[] BinaryMessage {
			get { return msg_.BinaryValue; }
			set { msg_.BinaryValue = value; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(msgid_);
			writer.Add(stype_);
			writer.Add(saddr_);
			writer.Add(deliveryTime_);
			writer.Add(validPeriod_);
			writer.Add(regDelivery_);
			writer.Add(defMsgId_);
			writer.Add(msg_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(msgid_);
			reader.ReadObject(stype_);
			reader.ReadObject(saddr_);
			reader.ReadObject(deliveryTime_);
			reader.ReadObject(validPeriod_);
			reader.ReadObject(regDelivery_);
			reader.ReadObject(defMsgId_);
			reader.ReadObject(msg_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("replace_sm: {0},msgid={1},svc_type={2},src_{3}," +
				"{4},{5},{6},{7},{8}{9}",
				base.ToString(),
				msgid_, stype_, saddr_, deliveryTime_, validPeriod_, regDelivery_,
				defMsgId_, msg_, base.DumpOptionalParams());
		}
	}
}
