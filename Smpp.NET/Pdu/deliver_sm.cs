using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <remarks>
	/// This marks the delivery status used to notify about delivery acknowledgement.
	/// </remarks>
	public enum DeliveryStatus {
		/// <summary>
		/// Message has been delivered
		/// </summary>
		DELIVERED,
		/// <summary>
		/// Message delivery/validity time has expired
		/// </summary>
		EXPIRED,
		/// <summary>
		/// Message has been deleted by the MC
		/// </summary>
		DELETED,
		/// <summary>
		/// Message was undeliverable by the MC
		/// </summary>
		UNDELIVERABLE,
		/// <summary>
		/// Message has been accepted by the MC but requires some
		/// intervention for delivery.
		/// </summary>
		ACCEPTED,
		/// <summary>
		/// Unknown message state
		/// </summary>
		UNKNOWN,
		/// <summary>
		/// Message has been rejected by the MS.
		/// </summary>
		REJECTED
	}

	/// <summary>
	/// The deliver_sm operation is issued by the SMSC to send a message to an ESME.  Using this
	/// command, the SMSC may route a short message to the ESME for delivery.
	/// </summary>
	public class deliver_sm : SmppSubmitRequest {
		// Class data - required parameters
		private service_type stype_ = new service_type();
		private address saddr_ = new address();
		private address daddr_ = new address();
		private esm_class esmclass_ = new esm_class();
		private protocol_id protid_ = new protocol_id();
		private priority_flag pflag_ = new priority_flag();
		private schedule_delivery_time deliveryTime_ = new schedule_delivery_time();
		private validity_period validPeriod_ = new validity_period();
		private registered_delivery regDelivery_ = new registered_delivery();
		private replace_if_present repPresent_ = new replace_if_present();
		private data_coding dataCoding_ = new data_coding();
		private sm_default_msg_id defMsgId_ = new sm_default_msg_id();
		private short_message msg_ = new short_message();

		/// <summary>
		/// Default constructor
		/// </summary>
		public deliver_sm()
			: base(Commands.DELIVER_SM) {
		}

		/// <summary>
		/// Primary constructor for the deliver_sm PDU
		/// </summary>
		/// <param name="stype">Service Type</param>
		/// <param name="saddr">Source Address</param>
		/// <param name="daddr">Destination Address</param>
		/// <param name="esm">ESM</param>
		/// <param name="protid">Protocol ID</param>
		/// <param name="priority">Priority</param>
		/// <param name="delTime">Delivery Time</param>
		/// <param name="valPeriod">Validation Period</param>
		/// <param name="regDelivery">Registered Delivery</param>
		/// <param name="replace">Replace If present</param>
		/// <param name="dataCoding">Data Coding type</param>
		/// <param name="defMsgId">Default Msg ID</param>
		/// <param name="msg">Message</param>
		public deliver_sm(string stype, address saddr, address daddr, esm_class esm, byte protid,
			MessagePriority priority, SmppTime delTime, SmppTime valPeriod,
			registered_delivery regDelivery, bool replace, DataEncoding dataCoding, byte defMsgId,
			string msg)
			: this() {
			this.ServiceType = stype;
			this.SourceAddress = saddr;
			this.DestinationAddress = daddr;
			this.EsmClass = esm;
			this.ProtocolID = protid;
			this.PriorityFlag = priority;
			this.DeliveryTime = delTime;
			this.ValidityPeriod = valPeriod;
			this.RegisteredDelivery = regDelivery;
			this.ReplaceExisting = replace;
			this.DataCoding = dataCoding;
			this.SmDefaultMessageID = defMsgId;
			this.Message = msg;
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
		/// Address of SME which will receive the message.
		/// </summary>
		public address DestinationAddress {
			get { return daddr_; }
			set { daddr_ = value; }
		}

		/// <summary>
		/// Indicates the message mode and type.
		/// </summary>
		public esm_class EsmClass {
			get { return esmclass_; }
			set { esmclass_ = value; }
		}

		/// <summary>
		/// Protocol identifier - network specific field.
		/// </summary>
		public byte ProtocolID {
			get { return protid_.Value; }
			set { protid_.Value = value; }
		}

		/// <summary>
		/// Designates the priority level of the message.
		/// </summary>
		public MessagePriority PriorityFlag {
			get { return pflag_.Value; }
			set { pflag_.Value = value; }
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
		/// Flag indicating if submitted message should replace any existing message.
		/// </summary>
		public bool ReplaceExisting {
			get { return repPresent_.Value; }
			set { repPresent_.Value = value; }
		}

		/// <summary>
		/// Defines the encoding scheme of the short message user data.
		/// </summary>
		public DataEncoding DataCoding {
			get { return dataCoding_.Value; }
			set { dataCoding_.Value = value; }
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
			get {
				return (msg_.Length > 0) ?
					msg_.TextValue :
					msgPayload_.TextValue;
			}

			set {
				if (value.Length > short_message.MAX_LENGTH) {
					msg_.TextValue = "";
					msgPayload_.TextValue = value;
				} else {
					msg_.TextValue = value;
					msgPayload_.TextValue = "";
				}
			}
		}

		/// <summary>
		/// This retrieves the short message in a byte array
		/// </summary>
		public byte[] BinaryMessage {
			get {
				return (msg_.Length > 0) ?
					msg_.BinaryValue :
					msgPayload_.BinaryValue;
			}

			set {
				if (value.Length > short_message.MAX_LENGTH) {
					msg_.BinaryValue = null;
					msgPayload_.BinaryValue = value;
				} else {
					msg_.BinaryValue = value;
					msgPayload_.TextValue = "";
				}
			}
		}

		/// <summary>
		/// This function builds a delivery receipt message for a previous submit.  It is inserted into the
		/// short_message parameter of the deliver_sm operation.  The format of the Delivery Receipt message
		/// is SMSC vendor specific, but this function builds the common form which is included in Appendix B
		/// of the SMPP 3.4 specification.
		/// </summary>
		/// <param name="messageId">The Message ID allocated to the message by the SMSC when originally submitted.</param>
		/// <param name="sub">Number of short messages originally submitted.  This is only used when the original message was submitted to a distribution list</param>
		/// <param name="dlvrd">Number of short messages delivered.  This is only used when the original message was submitted to a distribution list</param>
		/// <param name="submitDate">The time and date of submission</param>
		/// <param name="doneDate">The time and date at which the short message reached its final state.</param>
		/// <param name="status">The final status of the message</param>
		/// <param name="errCode">A network error code specific to the SMSC.</param>
		/// <param name="msg">The first 20 characters of the short message</param>
		/// <returns>A built string</returns>
		public static string BuildAckMessage(string messageId, int sub, int dlvrd,
			 DateTime submitDate, DateTime doneDate, DeliveryStatus status, int errCode, string msg) {
			string[] delStats = { "DELIVRD", "EXPIRED", "DELETED", "UNDELIV", "ACCEPTED", "UNKNOWN", "REJECTD" };
			if (status < DeliveryStatus.DELIVERED || status > DeliveryStatus.REJECTED)
				status = DeliveryStatus.UNKNOWN;

			return String.Format(@"id:{0} sub:{1} dlvrd:{2} submit date:{3} done date:{4} stat:{5} err:{6} text:{3}",
					  messageId.PadRight(10).Substring(0, 10),
					  sub.ToString().PadLeft(3, '0').Substring(0, 3),
					  dlvrd.ToString().PadLeft(3, '0').Substring(0, 3),
					  submitDate.ToString("yyMMddhhmm"),
					  doneDate.ToString("yyMMddhhmm"),
					  delStats[(int)status],
					  errCode.ToString().PadRight(3).Substring(0, 3),
					  msg.PadRight(20).Substring(0, 20));
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(stype_);
			writer.Add(saddr_);
			writer.Add(daddr_);
			writer.Add(esmclass_);
			writer.Add(protid_);
			writer.Add(pflag_);
			writer.Add(deliveryTime_);
			writer.Add(validPeriod_);
			writer.Add(regDelivery_);
			writer.Add(repPresent_);
			writer.Add(dataCoding_);
			writer.Add(defMsgId_);
			writer.Add(msg_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(stype_);
			reader.ReadObject(saddr_);
			reader.ReadObject(daddr_);
			reader.ReadObject(esmclass_);
			reader.ReadObject(protid_);
			reader.ReadObject(pflag_);
			reader.ReadObject(deliveryTime_);
			reader.ReadObject(validPeriod_);
			reader.ReadObject(regDelivery_);
			reader.ReadObject(repPresent_);
			reader.ReadObject(dataCoding_);
			reader.ReadObject(defMsgId_);
			reader.ReadObject(msg_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("deliver_sm: {0},svc_type={1},src_{2},dst_{3}," +
				"{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}{14}",
				base.ToString(),
				stype_, saddr_, daddr_,
				esmclass_, protid_, pflag_, deliveryTime_, validPeriod_, regDelivery_,
				repPresent_, dataCoding_, defMsgId_, msg_, base.DumpOptionalParams());
		}
	}
}
