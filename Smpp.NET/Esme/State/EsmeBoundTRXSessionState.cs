using System;
using System.Collections.Generic;
using System.Text;
using JulMar.Smpp.Pdu;

namespace JulMar.Smpp.Esme
{
    /// <summary>
    /// Summary description for BOUND_TRX state.  In this state, the ESME may send and receive short messages from
    /// the SMSC.
    /// </summary>
    internal class EsmeBoundTRXSessionState : EsmeBoundSessionState
    {
        private EsmeBoundRXSessionState rx_ = null;
        private EsmeBoundTXSessionState tx_ = null;

        /// <summary>
        /// This returns whether the type of session binding
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
        public EsmeBoundTRXSessionState(EsmeSession sess)
            : base(sess)
        {
            rx_ = new EsmeBoundRXSessionState(sess);
            tx_ = new EsmeBoundTXSessionState(sess);
        }

        /// <summary>
        /// This processes the deliver_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(deliver_sm pdu)
        {
            rx_.Process(pdu);
        }
    }
}
