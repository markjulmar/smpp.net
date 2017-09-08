using System;
using JulMar.Smpp;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The del_dl_resp PDU is used to acknowledge the receipt of an 
	/// del_dl request by the SMSC.
	/// </summary>
	public class del_dl_resp : SmppResponse {
		/// <summary>
		/// Default constructor
		/// </summary>
		public del_dl_resp()
			: base(Commands.DEL_DL_RESP) {
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="sequenceNumber">Sequence number</param>
		public del_dl_resp(int sequenceNumber)
			:
			 base(Commands.DEL_DL_RESP, StatusCodes.ESME_ROK, sequenceNumber) {
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("del_dl_resp: {0}", base.ToString());
		}
	}
}