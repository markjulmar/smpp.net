using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The cancel_broadcast_sm_resp is sent in response to the cancel_broadcast_sm PDU.
	/// </summary>
	public class cancel_broadcast_sm_resp : SmppResponse {
		/// <summary>
		/// Constructor for the cancel_broadcast_sm_resp PDU
		/// </summary>
		public cancel_broadcast_sm_resp()
			: base(Commands.CANCEL_BROADCAST_SM_RESP) {
		}

		/// <summary>
		/// Constructor for the cancel_broadcast_sm_resp PDU
		/// </summary>
		public cancel_broadcast_sm_resp(int seqNum)
			: base(Commands.CANCEL_BROADCAST_SM_RESP, seqNum) {
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("cancel_broadcast_sm_resp: {0}", base.ToString());
		}
	}
}