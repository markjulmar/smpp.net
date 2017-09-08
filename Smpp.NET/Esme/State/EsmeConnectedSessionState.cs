using System;
using JulMar.Smpp;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Esme
{
    /// <summary>
    /// Summary description for connected state.
    /// </summary>
    internal class EsmeConnectedSessionState : SmppSessionState
    {
        private EsmeSession session;

        /// <summary>
        /// Constructor for the connected session state
        /// </summary>
        /// <param name="session"></param>
        public EsmeConnectedSessionState(EsmeSession session)
        {
            this.session = session;
        }

        /// <summary>
        /// This method processes the bind_transmitter_resp PDU and performs the bind to the session.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(bind_transmitter_resp pdu)
        {
            if (pdu.Status == StatusCodes.ESME_ROK)
            {
                this.session.CurrentState = new EsmeBoundTXSessionState(this.session);
                this.session.FireEvent(EventType.Bound, new SmppEventArgs(this.session, pdu.OriginalRequest, pdu));
            }
        }

		/// <summary>
		/// This method processes the bind_receiver_resp PDU and performs the bind to the session.
		/// </summary>
		/// <param name="pdu">Protocol Data Unit being processed</param>
		public override void Process(bind_receiver_resp pdu)
		{
			if (pdu.Status == StatusCodes.ESME_ROK)
			{
				this.session.CurrentState = new EsmeBoundTXSessionState(this.session);
				this.session.FireEvent(EventType.Bound, new SmppEventArgs(this.session, pdu.OriginalRequest, pdu));
			}
		}

		/// <summary>
		/// This method processes the bind_transceiver_resp PDU and performs the bind to the session.
		/// </summary>
		/// <param name="pdu">Protocol Data Unit being processed</param>
		public override void Process(bind_transceiver_resp pdu)
		{
			if (pdu.Status == StatusCodes.ESME_ROK)
			{
				this.session.CurrentState = new EsmeBoundTXSessionState(this.session);
				this.session.FireEvent(EventType.Bound, new SmppEventArgs(this.session, pdu.OriginalRequest, pdu));
			}
		}
	}
}
