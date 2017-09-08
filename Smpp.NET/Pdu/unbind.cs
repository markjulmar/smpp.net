using System;
using JulMar.Smpp;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The purpose of the unbind operation is to deregister an instance of an ESME from the
	/// SMSC and inform the SMSC that the ESME no longer wishes to use this network connection for 
	/// the submission or deliver of messages.
	/// </summary>
	public class unbind : SmppRequest {
		/// <summary>
		/// Default constructor
		/// </summary>
		public unbind()
			: base(Commands.UNBIND) {
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("unbind: {0}", base.ToString());
		}
	}
}