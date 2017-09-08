using System;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Smsc
{
    /// <summary>
    /// Summary description for BOUND_RX state.  In this state, the bound ESME may receive short messages from
    /// the SMSC which may be originated by a mobile station, by another ESME or by the SMSC itself (delivery receipt).
    /// </summary>
    internal class SmscBoundSessionState : SmppSessionState
    {
        protected SmscSession session_;

        /// <summary>
        /// Constructor for the bound receiver session state
        /// </summary>
        /// <param name="sess"></param>
        public SmscBoundSessionState(SmscSession sess)
        {
            session_ = sess;
        }

        /// <summary>
        /// This returns a specific error for the bind_receiver event; we are already bound!
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(bind_receiver pdu)
        {
            session_.SendPdu(new bind_receiver_resp(pdu.SequenceNumber, StatusCodes.ESME_RALYBND));
        }

        /// <summary>
        /// This returns a specific error for the bind_transmitter event; we are already bound!
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(bind_transmitter pdu)
        {
            session_.SendPdu(new bind_transmitter_resp(pdu.SequenceNumber, StatusCodes.ESME_RALYBND));
        }

        /// <summary>
        /// This returns a specific error for the bind_transceiver event; we are already bound!
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(bind_transceiver pdu)
        {
            session_.SendPdu(new bind_transceiver_resp(pdu.SequenceNumber, StatusCodes.ESME_RALYBND));
        }

        /// <summary>
        /// This processes the unbind event; we always allow this event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(unbind pdu)
        {
            unbind_resp pduOut = new unbind_resp(pdu.SequenceNumber);
            session_.FireEvent(EventType.UnBind, new SmppEventArgs(session_, pdu, pduOut));
            session_.PeerSystemID = "";
            session_.SmppVersion = 0;
            session_.CurrentState = new SmscOpenSessionState(session_);
            session_.SendPdu(pduOut);
            session_.Close();
        }

        /// <summary>
        /// This processes the data_sm event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(data_sm pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new data_sm_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.DataSm, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RDELIVERYFAILURE;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the enquire_link PDU
        /// </summary>
        /// <param name="pdu"></param>
        public override void Process(enquire_link pdu)
        {
            session_.SendPdu(new enquire_link_resp(pdu.SequenceNumber));
        }
    }
}
