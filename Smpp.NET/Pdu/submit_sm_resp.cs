using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The submit_sm_resp is sent in response to the submit_sm PDU.
	/// </summary>
	public class submit_sm_resp : SmppResponse {
		// Class data
		private message_id mid_ = new message_id();

		/// <summary>
		/// Constructor for the submit_sm_resp PDU
		/// </summary>
		public submit_sm_resp()
			: base(Commands.SUBMIT_SM_RESP) {
		}

		/// <summary>
		/// Constructor for the submit_sm_resp PDU
		/// </summary>
		public submit_sm_resp(int seqNum)
			: base(Commands.SUBMIT_SM_RESP, seqNum) {
		}

		/// <summary>
		/// Constructor for the submit_sm_resp PDU
		/// </summary>
		public submit_sm_resp(int seqNum, int status)
			: base(Commands.SUBMIT_SM_RESP, status, seqNum) {
		}

		/// <summary>
		/// Constructor for the submit_sm_resp PDU
		/// </summary>
		public submit_sm_resp(int seqNum, string mid)
			: base(Commands.SUBMIT_SM_RESP, StatusCodes.ESME_ROK, seqNum) {
			mid_.Value = mid;
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
			return string.Format("submit_sm_resp: {0},mid={1}{2}", base.ToString(), mid_, base.DumpOptionalParams());
		}
	}
}