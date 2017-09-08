using System;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Smsc
{
    /// <summary>
    /// Summary description for BOUND_TRX state.  In this state, the bound ESME may receive short messages from
    /// the SMSC which may be originated by a mobile station, by another ESME or by the SMSC itself (delivery receipt).
    /// </summary>
    internal class SmscBoundTRXSessionState : SmscBoundSessionState
    {
        private SmscBoundRXSessionState rx_ = null;
        private SmscBoundTXSessionState tx_ = null;

        /// <summary>
        /// This returns whether the session is bound
        /// </summary>
        /// <value>Current bind status</value>
        public override SmppBindStatus BindStatus
        {
            get { return SmppBindStatus.BoundTrx; }
        }

        /// <summary>
        /// Constructor for the bound receiver session state
        /// </summary>
        /// <param name="sess"></param>
        public SmscBoundTRXSessionState(SmscSession sess)
            : base(sess)
        {
            rx_ = new SmscBoundRXSessionState(sess);
            tx_ = new SmscBoundTXSessionState(sess);
        }

        /// <summary>
        /// This processes the cancel_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(cancel_sm pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the query_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(query_sm pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the replace_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(replace_sm pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the submit_multi PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(submit_multi pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the submit_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(submit_sm pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the broadcast_sm event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(broadcast_sm pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the cancel_broadcast_sm event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(cancel_broadcast_sm pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the query_broadcast_sm event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(query_broadcast_sm pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the add_sub event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(add_sub pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the mod_sub event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(mod_sub pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the del_sub event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(del_sub pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the enquire_sub event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(enquire_sub pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the add_dl event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(add_dl pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the mod_dl event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(mod_dl pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the del_dl event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(del_dl pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the view_dl event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(view_dl pdu)
        {
            tx_.Process(pdu);
        }

        /// <summary>
        /// This processes the list_dls event.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit</param>
        public override void Process(list_dls pdu)
        {
            tx_.Process(pdu);
        }

    }
}
