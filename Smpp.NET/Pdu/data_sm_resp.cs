using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The data_sm_resp is sent in response to the data_sm PDU.
	/// </summary>
	public class data_sm_resp : SmppResponse {
		// Class data
		private message_id mid_ = new message_id();
		// Optional parameters
		private delivery_failure_reason failCode_ = new delivery_failure_reason();
		private network_error_code errCode_ = new network_error_code();
		private additional_status_info_text infoTxt_ = new additional_status_info_text();
		private dpf_result dpf_ = new dpf_result();

		/// <summary>
		/// Constructor for the data_sm_resp PDU
		/// </summary>
		public data_sm_resp()
			: this(0, "") {
		}

		/// <summary>
		/// Constructor for the data_sm_resp PDU
		/// </summary>
		public data_sm_resp(int seqNum)
			: this(seqNum, "") {
		}

		/// <summary>
		/// Constructor for the data_sm_resp PDU
		/// </summary>
		public data_sm_resp(int seqNum, int status)
			: base(Commands.DATA_SM_RESP, status, seqNum) {
		}

		/// <summary>
		/// Constructor for the data_sm_resp PDU
		/// </summary>
		public data_sm_resp(int seqNum, string mid)
			: base(Commands.DATA_SM_RESP, seqNum) {
			mid_.Value = mid;
			AddOptionalElements(failCode_, errCode_, infoTxt_, dpf_);
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
		/// Returns whether the delivery failure code is present.
		/// </summary>
		public bool HasDeliveryFailureCode {
			get { return failCode_.HasValue; }
		}

		/// <summary>
		/// Include to indicate reason for delivery failure.
		/// </summary>
		public DeliveryFailure DeliveryFailureCode {
			get { return failCode_.Value; }
			set { failCode_.Value = value; }
		}

		/// <summary>
		/// Returns whether the network error code is present.
		/// </summary>
		public bool HasNetworkErrorCode {
			get { return errCode_.HasValue; }
		}

		/// <summary>
		/// Network Error Code may be available for intermediate notifications and delivery receipts.
		/// </summary>
		public network_error_code NetworkErrorCode {
			get { return errCode_; }
			set { errCode_ = value; }
		}

		/// <summary>
		/// Returns whether status information text is present.
		/// </summary>
		public bool HasStatusInfoText {
			get { return infoTxt_.HasValue; }
		}

		/// <summary>
		/// ASCII text giving a description of the meaning of the response.
		/// </summary>
		public string StatusInfoText {
			get { return infoTxt_.Value; }
			set { infoTxt_.Value = value; }
		}

		/// <summary>
		/// Returns whether the delivery pending flag was set.
		/// </summary>
		public bool DPFResult {
			get { return (dpf_.HasValue && dpf_.Value == true); }
			set { dpf_.Value = value; }
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
			return string.Format("data_sm_resp: {0},mid={1}{2}", base.ToString(), mid_, base.DumpOptionalParams());
		}
	}
}