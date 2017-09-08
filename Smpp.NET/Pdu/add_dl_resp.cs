using System;
using JulMar.Smpp;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The add_dl_resp PDU is used to acknowledge the receipt of an 
	/// add_dl request by the SMSC.
	/// </summary>
	public class add_dl_resp : SmppResponse {
		/// <summary>
		/// Default constructor
		/// </summary>
		public add_dl_resp()
			: base(Commands.ADD_DL_RESP) {
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="sequenceNumber">Sequence number</param>
		public add_dl_resp(int sequenceNumber)
			:
			 base(Commands.ADD_DL_RESP, StatusCodes.ESME_ROK, sequenceNumber) {
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("add_dl_resp: {0}", base.ToString());
		}
	}
}