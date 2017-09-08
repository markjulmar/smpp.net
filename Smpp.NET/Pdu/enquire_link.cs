using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The enquire_link PDU can be sent by either the ESME or SMSC and is used to provide
	/// a confidence check of the communication path between the ESME and SMSC.  On receipt
	/// of this request, the receiving party should respond with an enquire_link_resp.
	/// </summary>
	public class enquire_link : SmppRequest {
		/// <summary>
		/// Default constructor
		/// </summary>
		public enquire_link()
			: base(Commands.ENQUIRE_LINK) {
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("enquire_link: {0}", base.ToString());
		}
	}
}
