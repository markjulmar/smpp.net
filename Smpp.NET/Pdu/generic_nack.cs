using System;
using JulMar.Smpp;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The generic_nack PDU is used to acknowledge the submission of an unrecognized
	/// or corrupt PDU.
	/// </summary>
	public class generic_nack : SmppResponse {
		/// <summary>
		/// Default constructor
		/// </summary>
		public generic_nack()
			: base(Commands.GENERIC_NACK) {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="status">Status</param>
		/// <param name="sequenceNumber">Sequence number</param>
		public generic_nack(int status, int sequenceNumber)
			:
			base(Commands.GENERIC_NACK, status, sequenceNumber) {
			Length = SmppPdu.REQUIRED_SIZE;
		}

		/// <summary>
		/// This propery returns whether a response is required for this PDU.
		/// </summary>
		/// <value>True/False</value>
		public override bool RequiresResponse {
			get { return false; }
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("generic_nak: {0}", base.ToString());
		}
	}
}