using System;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Esme
{
    internal class EsmeBoundSessionState : SmppSessionState
    {
        protected EsmeSession session;

        public EsmeBoundSessionState(EsmeSession session)
        {
            this.session = session;
        }

        /// <summary>
        /// This processes the enquire_link pdu
        /// </summary>
        /// <param name="pdu"></param>
        public override void Process(enquire_link pdu)
        {
            this.session.SendPdu(new enquire_link_resp(pdu.SequenceNumber));
        }

        /// <summary>
        /// This processes the enquire_link_resp pdu
        /// </summary>
        /// <param name="pdu"></param>
        public override void Process(enquire_link_resp pdu)
        {
        }

        /// <summary>
        /// This method processes the unbind pdu if SMSC unbinds from ESME.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(unbind pdu)
        {
            this.session.SendPdu(new unbind_resp(pdu.SequenceNumber));
            this.session.CurrentState = new EsmeConnectedSessionState(this.session);
            this.session.FireEvent(EventType.UnBound, new SmppEventArgs(this.session));
        }

        /// <summary>
        /// This method processes the unbind_resp pdu
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(unbind_resp pdu)
        {
            if (pdu.Status == StatusCodes.ESME_ROK)
            {
                this.session.CurrentState = new EsmeConnectedSessionState(this.session);
                this.session.FireEvent(EventType.UnBound, new SmppEventArgs(this.session, pdu.OriginalRequest, pdu));
            }
        }
    }
}
