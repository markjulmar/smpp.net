using System;

namespace JulMar.Smpp.Pdu {
	/// <summary> 
	/// All classes which are used as SMPP requests are derived from this base class.
	/// </summary>
	public abstract class SmppRequest : SmppPdu {
		/// <summary>
		/// Create a request PDU with default parameters. 
		/// </summary>
		/// <param name="commandId">Command Id tag</param>
		public SmppRequest(int commandId)
			: base(commandId) {
		}

		/// <summary>
		/// Create request PDU with given command id.
		/// </summary>
		/// <param name="commandId">Command Id</param>
		/// <param name="status">Status</param>
		/// <param name="seqNum">Sequence number</param>
		/// <param name="length">Length</param>
		public SmppRequest(int commandId, int status, int seqNum, int length)
			:
			base(commandId, status, seqNum, length) {
		}
	}
}