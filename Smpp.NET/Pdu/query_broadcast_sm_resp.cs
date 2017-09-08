using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The query_broadcast_sm_resp is sent in response to the query_broadcast_sm PDU.
	/// </summary>
	public class query_broadcast_sm_resp : SmppResponse {
		// Class data
		private message_id mid_ = new message_id();
		private optional_message_state msgState_ = new optional_message_state();
		private broadcast_area_identifier areaId_ = new broadcast_area_identifier();
		private broadcast_area_success successRate_ = new broadcast_area_success();
		private broadcast_end_time endTime_ = new broadcast_end_time();
		private user_message_reference umsgId_ = new user_message_reference();

		/// <summary>
		/// Constructor for the query_broadcast_sm_resp PDU
		/// </summary>
		public query_broadcast_sm_resp()
			: base(Commands.QUERY_BROADCAST_SM_RESP) {
			AddOptionalElements(endTime_, umsgId_);
		}

		/// <summary>
		/// Constructor for the query_broadcast_sm_resp PDU
		/// </summary>
		/// <param name="seqNum">Sequence number for the PDU</param>
		public query_broadcast_sm_resp(int seqNum)
			: base(Commands.QUERY_BROADCAST_SM_RESP, seqNum) {
			AddOptionalElements(endTime_, umsgId_);
		}

		/// <summary>
		/// Constructor for the query_broadcast_sm_resp PDU
		/// </summary>
		/// <param name="seqNum">Sequence number for the PDU</param>
		/// <param name="mid">MC assigned message ID</param>
		/// <param name="msgState">Current status of the broadcast message</param>
		public query_broadcast_sm_resp(int seqNum, string mid, MessageStatus msgState)
			: base(Commands.QUERY_BROADCAST_SM_RESP, seqNum) {
			mid_.Value = mid;
			msgState_.Value = msgState;
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
		/// Identifies one or more broadcast areas for the message
		/// </summary>
		public broadcast_area_identifier AreaID {
			get { return areaId_; }
			set { areaId_ = value; }
		}

		/// <summary>
		/// The success rate indicator is the ratio of BTSs that accepted the
		/// message vs. the number that should have accepted.
		/// </summary>
		/// <value></value>
		public broadcast_area_success SuccessRate {
			get { return successRate_; }
			set { successRate_ = value; }
		}

		/// <summary>
		/// This returns whether the PDU has a broadcast end time.
		/// </summary>
		public bool HasBroadcastEndTime {
			get { return endTime_.HasValue; }
		}

		/// <summary>
		/// The date and time at which the broadcasting state of the message was set
		/// to terminated by the MC.
		/// </summary>
		/// <value></value>
		public SmppTime BroadcastEndTime {
			get { return endTime_.Value; }
			set { endTime_.Value = value; }
		}

		/// <summary>
		/// This returns whether the PDU has a user message ID
		/// </summary>
		public bool HasUserMessageID {
			get { return umsgId_.HasValue; }
		}

		/// <summary>
		/// ESME assigned message reference number
		/// </summary>
		public int UserMessageID {
			get { return umsgId_.Value; }
			set { umsgId_.Value = value; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(mid_);
			writer.Add(msgState_);
			writer.Add(areaId_);
			writer.Add(successRate_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(mid_);
			reader.ReadObject(msgState_);
			reader.ReadObject(areaId_);
			reader.ReadObject(successRate_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("query_broadcast_sm_resp: {0},mid={1},{2},{3},{4}{5}",
					  base.ToString(), mid_, msgState_, areaId_, successRate_,
					  base.DumpOptionalParams());
		}
	}
}