using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The broadcast_sm_resp is sent in response to the broadcast_sm PDU.
	/// </summary>
	public class broadcast_sm_resp : SmppResponse {
		// Class data
		private message_id mid_ = new message_id();
		private broadcast_error_status errStat_ = new broadcast_error_status();
		private broadcast_area_identifier failedAreaId_ = new broadcast_area_identifier();

		/// <summary>
		/// Constructor for the broadcast_sm_resp PDU
		/// </summary>
		public broadcast_sm_resp()
			: base(Commands.BROADCAST_SM_RESP) {
			AddOptionalElements(errStat_, failedAreaId_);
		}

		/// <summary>
		/// Constructor for the broadcast_sm_resp PDU
		/// </summary>
		public broadcast_sm_resp(int seqNum)
			: base(Commands.BROADCAST_SM_RESP, seqNum) {
			AddOptionalElements(errStat_, failedAreaId_);
		}

		/// <summary>
		/// Constructor for the broadcast_sm_resp PDU
		/// </summary>
		public broadcast_sm_resp(int seqNum, string mid)
			: base(Commands.BROADCAST_SM_RESP, StatusCodes.ESME_ROK, seqNum) {
			mid_.Value = mid;
			AddOptionalElements(errStat_, failedAreaId_);
		}

		/// <summary>
		/// The MessageID contains the SMSC message ID of the submitted message.  It may be
		/// used at a later stage to query the status of a message, cancel a message or replace
		/// a message.
		/// </summary>
		public string MessageID {
			get { return mid_.Value; }
			set { mid_.Value = value; }
		}


		/// <summary>
		/// Determines whether broadcast error status information 
		/// is available
		/// </summary>
		public bool HasErrorStatus {
			get { return errStat_.HasValue; }
		}

		/// <summary>
		/// Returns the broadcast error status
		/// </summary>
		/// <value>A StatusCode response</value>
		public int ErrorStatus {
			get { return errStat_.Value; }
			set { errStat_.Value = value; }
		}

		/// <summary>
		/// Determines whether failed geographical
		/// information is available
		/// </summary>
		public bool HasFailedArea {
			get { return failedAreaId_.HasValue; }
		}

		/// <summary>
		/// Failed broadcast area information
		/// </summary>
		/// <value></value>
		public broadcast_area_identifier FailedArea {
			get { return failedAreaId_; }
			set { failedAreaId_ = value; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(mid_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(mid_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("broadcast_sm_resp: {0},mid={1}{2}", base.ToString(), mid_, base.DumpOptionalParams());
		}
	}
}