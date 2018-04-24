using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The submit_multi_resp is sent in response to the submit_multi PDU.
	/// </summary>
	public class submit_multi_resp : SmppResponse {
		// Class data
		private message_id mid_ = new message_id();
		private unsuccess_sme_collection smes_ = new unsuccess_sme_collection();

		/// <summary>
		/// Constructor for the submit_multi_resp PDU
		/// </summary>
		public submit_multi_resp()
			: this(0) {
		}

		/// <summary>
		/// Constructor for the submit_multi_resp PDU
		/// </summary>
		public submit_multi_resp(int seqNum)
			: base(Commands.SUBMIT_MULTI_RESP, seqNum) {
		}

		/// <summary>
		/// Constructor for the submit_multi_resp PDU
		/// </summary>
		public submit_multi_resp(int seqNum, string mid, params unsuccess_sme[] elems)
			:
			base(Commands.SUBMIT_MULTI_RESP, seqNum) {
			mid_.Value = mid;
			for (int i = 0; i < elems.Length; ++i)
				smes_.Add(elems[i]);
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
		/// This provides access to the error collection
		/// </summary>
		public unsuccess_sme_collection ErrorCollection {
			get { return smes_; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(mid_);
			smes_.AddToStream(writer);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(mid_);
			smes_.GetFromStream(reader);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("submit_multi_resp: {0},mid={1},errs={2}{3}", base.ToString(), mid_, smes_, base.DumpOptionalParams());
		}
	}
}