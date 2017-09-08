using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The replace_sm_resp is sent in response to the replace_sm PDU.
	/// </summary>
	public class replace_sm_resp : SmppResponse {
		/// <summary>
		/// Constructor for the replace_sm_resp PDU
		/// </summary>
		public replace_sm_resp()
			: base(Commands.REPLACE_SM_RESP) {
		}

		/// <summary>
		/// Constructor for the replace_sm_resp PDU
		/// </summary>
		public replace_sm_resp(int seqNum)
			: base(Commands.REPLACE_SM_RESP, seqNum) {
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("replace_sm_resp: {0}", base.ToString());
		}
	}
}