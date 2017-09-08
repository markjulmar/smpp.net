using System;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Esme
{
    /// <summary>
    /// Summary description for BOUND_TX state.  In this state, the bound ESME may receive Mobile Originated short 
    /// messages from the SMSC for processing.
    /// </summary>
    internal class EsmeBoundRXSessionState : EsmeBoundSessionState
    {
        protected EsmeSession session_;

        /// <summary>
        /// Constructor for the open session state
        /// </summary>
        /// <param name="session"></param>
        public EsmeBoundRXSessionState(EsmeSession session)
            : base(session)
        {
            session_ = session;
        }

        /// <summary>
        /// This returns whether the type of session binding
        /// </summary>
        /// <value>Current bind status</value>
        public override SmppBindStatus BindStatus
        {
            get { return SmppBindStatus.BoundRx; }
        }

        /// <summary>
        /// This processes the deliver_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(deliver_sm pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new deliver_sm_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.DeliverSm, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RSUBMITFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the data_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(data_sm pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new data_sm_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.DeliverSm, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RSUBMITFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the alert_notification PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(alert_notification pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, null);
            if (!session_.FireEvent(EventType.AlertNotification, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RSUBMITFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }
    }
}
