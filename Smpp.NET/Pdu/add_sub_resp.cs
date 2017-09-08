using System;
using JulMar.Smpp;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The add_sub_resp PDU is used to acknowledge the receipt of an 
	/// add_sub request by the SMSC.
	/// </summary>
	public class add_sub_resp : SmppResponse {
		/// <summary>
		/// Default constructor
		/// </summary>
		public add_sub_resp()
			: base(Commands.ADD_SUB_RESP) {
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="sequenceNumber">Sequence number</param>
		public add_sub_resp(int sequenceNumber)
			:
			 base(Commands.ADD_SUB_RESP, StatusCodes.ESME_ROK, sequenceNumber) {
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("add_sub_resp: {0}", base.ToString());
		}
	}
}