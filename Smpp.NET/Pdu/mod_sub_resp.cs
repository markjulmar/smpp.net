using System;
using JulMar.Smpp;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The mod_sub_resp PDU is used to acknowledge the receipt of an 
	/// mod_sub request by the SMSC.
	/// </summary>
	public class mod_sub_resp : SmppResponse {
		/// <summary>
		/// Default constructor
		/// </summary>
		public mod_sub_resp()
			: base(Commands.MOD_SUB_RESP) {
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="sequenceNumber">Sequence number</param>
		public mod_sub_resp(int sequenceNumber)
			:
			 base(Commands.MOD_SUB_RESP, StatusCodes.ESME_ROK, sequenceNumber) {
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("mod_sub_resp: {0}", base.ToString());
		}
	}
}