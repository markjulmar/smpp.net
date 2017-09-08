using System;
using JulMar.Smpp;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The unbind_resp PDU is used to acknowledge the receipt of an unbind request by the SMSC.
	/// </summary>
	public class unbind_resp : SmppResponse {
		/// <summary>
		/// Default constructor
		/// </summary>
		public unbind_resp()
			: base(Commands.UNBIND_RESP) {
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="sequenceNumber">Sequence number</param>
		public unbind_resp(int sequenceNumber)
			:
			 base(Commands.UNBIND_RESP, StatusCodes.ESME_ROK, sequenceNumber) {
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="sequenceNumber">Sequence number</param>
		/// <param name="status">Status</param>
		public unbind_resp(int sequenceNumber, int status)
			:
			 base(Commands.UNBIND_RESP, status, sequenceNumber) {
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("unbind_resp: {0}", base.ToString());
		}
	}
}