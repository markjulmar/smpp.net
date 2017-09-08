using System;
using Ninetech.Communication.Smpp;
using Ninetech.Communication.Smpp.Pdu;
using Ninetech.Communication.Smpp.Utility;

namespace Ninetech.Communication.Smpp.Esme {
	/// <summary>
	/// Summary description for OPEN state.
	/// </summary>
	internal class EsmeOpenSessionState : SmppSessionState {
		private EsmeSession session;

		/// <summary>
		/// Constructor for the open session state
		/// </summary>
		/// <param name="sess"></param>
		public EsmeOpenSessionState(EsmeSession session) {
			this.session = session;
		}

		/// <summary>
		/// This method processes the bind_transmitter PDU and performs the bind to the session.
		/// </summary>
		/// <param name="pdu">Protocol Data Unit being processed</param>
		public override void Process(bind_transmitter_resp pdu) {
			if (pdu.Status == StatusCodes.ESME_ROK) {
				this.session.CurrentState = new EsmeBoundTXSessionState(this.session);
				this.session.FireEvent(EventTypes.Bound, new SmppEventArgs(this.session, pdu.OriginalRequest, pdu));
			}
		}
	}
}
