using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The deliver_sm_resp is sent in response to the deliver_sm PDU.
	/// </summary>
	public class deliver_sm_resp : SmppResponse {
		// Class data
		private message_id mid_ = new message_id();

		/// <summary>
		/// Constructor for the deliver_sm_resp PDU
		/// </summary>
		public deliver_sm_resp()
			: base(Commands.DELIVER_SM_RESP) {
		}

		/// <summary>
		/// Constructor for the deliver_sm_resp PDU
		/// </summary>
		public deliver_sm_resp(int seqNum)
			: base(Commands.DELIVER_SM_RESP, seqNum) {
		}

		/// <summary>
		/// The MessageID is not used in the deliver_sm PDU.
		/// </summary>
		public string MessageID {
			get { return string.Empty; }
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
			return string.Format("deliver_sm_resp: {0},mid={1}{2}", base.ToString(), mid_, base.DumpOptionalParams());
		}
	}
}