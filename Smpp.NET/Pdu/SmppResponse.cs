using System;

namespace JulMar.Smpp.Pdu {
	/// <summary> 
	/// All classes which are used as SMPP response are derived from this base class.
	/// </summary>
	public abstract class SmppResponse : SmppPdu {
		// Class data
		private SmppRequest originalRequest_ = null;

		/// <summary>
		/// This property returns the original request associated with this
		/// response object.
		/// </summary>
		public SmppRequest OriginalRequest {
			get { return originalRequest_; }
			set { this.originalRequest_ = value; }
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="commandId">Command Id tag</param>
		public SmppResponse(int commandId)
			: base(commandId) {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="commandId">Command Id tag</param>
		/// <param name="seqNum">Sequence number</param>
		public SmppResponse(int commandId, int seqNum)
			:
		 base(commandId, StatusCodes.ESME_ROK, seqNum, 0) {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="commandId">Command Id tag</param>
		/// <param name="status">Status code</param>
		/// <param name="seqNum">Sequence number</param>
		public SmppResponse(int commandId, int status, int seqNum)
			:
			base(commandId, status, seqNum, 0) {
		}

		/// <summary>
		/// This propery returns whether a response is required for this PDU.
		/// </summary>
		/// <value>True/False</value>
		public override bool RequiresResponse {
			get { return false; }
		}

		/// <summary>
		/// Quick check for success codes
		/// </summary>
		/// <value>True/False</value>
		public bool Succeeded {
			get { return Status == StatusCodes.ESME_ROK; }
		}
	}
}