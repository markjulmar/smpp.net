using System;
using JulMar.Smpp;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The mod_dl_resp PDU is used to acknowledge the receipt of an 
	/// mod_dl request by the SMSC.
	/// </summary>
	public class mod_dl_resp : SmppResponse {
		/// <summary>
		/// Default constructor
		/// </summary>
		public mod_dl_resp()
			: base(Commands.MOD_DL_RESP) {
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="sequenceNumber">Sequence number</param>
		public mod_dl_resp(int sequenceNumber)
			:
			 base(Commands.MOD_DL_RESP, StatusCodes.ESME_ROK, sequenceNumber) {
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("mod_dl_resp: {0}", base.ToString());
		}
	}
}