using System;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The enquire_link_resp is sent in response to the enquire_link PDU.
	/// </summary>
	public class enquire_link_resp : SmppResponse {
		/// <summary>
		/// Constructor for the enquire_link_resp PDU
		/// </summary>
		public enquire_link_resp()
			: base(Commands.ENQUIRE_LINK_RESP) {
		}

		/// <summary>
		/// Constructor for the enquire_link_resp PDU
		/// </summary>
		public enquire_link_resp(int seqNum)
			: base(Commands.ENQUIRE_LINK_RESP, seqNum) {
		}

		/// <summary>
		/// Constructor for the enquire_link_resp PDU
		/// </summary>
		public enquire_link_resp(int seqNum, int status)
			: base(Commands.ENQUIRE_LINK_RESP, status, seqNum) {
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("enquire_link_resp: {0}", base.ToString());
		}
	}
}