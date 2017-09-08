using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The query_sm_resp is sent in response to the query_sm PDU.
	/// </summary>
	public class query_sm_resp : SmppResponse {
		// Class data
		private message_id mid_ = new message_id();
		private final_date finalDate_ = new final_date();
		private message_state msgState_ = new message_state();
		private int errCode_ = 0;

		/// <summary>
		/// Constructor for the query_sm_resp PDU
		/// </summary>
		public query_sm_resp()
			: base(Commands.QUERY_SM_RESP) {
		}

		/// <summary>
		/// Constructor for the query_sm_resp PDU
		/// </summary>
		public query_sm_resp(int seqNum)
			: base(Commands.QUERY_SM_RESP, seqNum) {
		}

		/// <summary>
		/// Constructor for the query_sm_resp PDU
		/// </summary>
		public query_sm_resp(int seqNum, string mid, MessageStatus msgState, int errCode)
			: base(Commands.QUERY_SM_RESP, seqNum) {
			mid_.Value = mid;
			msgState_.Value = msgState;
			errCode_ = errCode;
		}

		/// <summary>
		/// The MessageID contains the SMSC message ID of the message being queried.
		/// </summary>
		public string MessageID {
			get { return mid_.Value; }
			set { mid_.Value = value; }
		}

		/// <summary>
		/// Should be present for SMSC delivery receipts and intermediate notifications.
		/// </summary>
		public MessageStatus MessageStatus {
			get { return msgState_.Value; }
			set { msgState_.Value = value; }
		}

		/// <summary>
		/// Where appropriate, this holds a network error code defining the
		/// reason for failure of message delivery.
		/// </summary>
		public int ErrorCode {
			get { return errCode_; }
			set { errCode_ = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public SmppTime FinalDate {
			get { return (SmppTime)finalDate_; }
			set { finalDate_ = new final_date(value); }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(mid_);
			writer.Add(finalDate_);
			writer.Add(msgState_);
			writer.Add(errCode_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(mid_);
			reader.ReadObject(finalDate_);
			reader.ReadObject(msgState_);
			errCode_ = reader.ReadInt32();
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("query_sm_resp: {0},mid={1},{2},{3},{4}{5}",
					base.ToString(), mid_, finalDate_, msgState_, errCode_,
					base.DumpOptionalParams());
		}
	}
}