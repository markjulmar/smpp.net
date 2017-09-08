using System;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Smsc
{
    /// <summary>
    /// Summary description for BOUND_TX state.  In this state, the bound ESME may send short messages to
    /// the SMSC for onward delivery to a Mobile Station or to another EMSE.  The ESME may also replace, query
    /// or cancel a previously submitted short message.
    /// </summary>
    internal class SmscBoundTXSessionState : SmscBoundSessionState
    {
        /// <summary>
        /// This returns whether the session is bound
        /// </summary>
        /// <value>Current bind status</value>
        public override SmppBindStatus BindStatus
        {
            get { return SmppBindStatus.BoundTx; }
        }

        /// <summary>
        /// Constructor for the open session state
        /// </summary>
        /// <param name="sess"></param>
        public SmscBoundTXSessionState(SmscSession sess)
            : base(sess)
        {
        }

        /// <summary>
        /// This processes the submit_sm event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(submit_sm pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new submit_sm_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.SubmitSm, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RSUBMITFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the submit_multi event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(submit_multi pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new submit_multi_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.SubmitSmMulti, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RSUBMITFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the query_sm event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(query_sm pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new query_sm_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.QuerySm, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RQUERYFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the cancel_sm event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(cancel_sm pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new cancel_sm_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.CancelSm, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RCANCELFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the data_sm event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(replace_sm pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new replace_sm_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.ReplaceSm, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RREPLACEFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the broadcast_sm event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(broadcast_sm pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new broadcast_sm_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.BroadcastSm, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RBCASTFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the cancel_broadcast_sm event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(cancel_broadcast_sm pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new cancel_broadcast_sm_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.CancelBroadcastSm, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RBCASTCANCELFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the query_broadcast_sm event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(query_broadcast_sm pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new query_broadcast_sm_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.QueryBroadcastSm, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RBCASTQUERYFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the add_sub event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(add_sub pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new add_sub_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.AddSub, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RADDCUSTFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the mod_sub event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(mod_sub pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new mod_sub_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.ModSub, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RMODCUSTFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the del_sub event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(del_sub pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new del_sub_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.DelSub, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RDELCUSTFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the enquire_sub event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(enquire_sub pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new enquire_sub_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.EnquireSub, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RENQCUSTFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the add_dl event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(add_dl pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new add_dl_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.AddDl, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RADDDLFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the mod_dl event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(mod_dl pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new mod_dl_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.ModDl, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RMODDLFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the del_dl event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(del_dl pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new del_dl_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.DelDl, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RDELDLFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the view_dl event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(view_dl pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new view_dl_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.ViewDl, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RVIEWDLFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }

        /// <summary>
        /// This processes the list_dls event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(list_dls pdu)
        {
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, new list_dls_resp(pdu.SequenceNumber));
            if (!session_.FireEvent(EventType.ListDls, ea))
                ea.ResponsePDU.Status = StatusCodes.ESME_RLISTDLSFAIL;
            session_.SendPdu(ea.ResponsePDU);
        }
    }
}
