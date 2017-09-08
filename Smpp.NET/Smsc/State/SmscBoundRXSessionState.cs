using System;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Smsc
{
    /// <summary>
    /// Summary description for BOUND_RX state.  In this state, the bound ESME may receive short messages from
    /// the SMSC which may be originated by a mobile station, by another ESME or by the SMSC itself (delivery receipt).
    /// </summary>
    ///
    internal class SmscBoundRXSessionState : SmscBoundSessionState
    {
        /// <summary>
        /// This returns whether the session is bound
        /// </summary>
        /// <value>Current bind status</value>
        public override SmppBindStatus BindStatus
        {
            get { return SmppBindStatus.BoundRx; }
        }

        /// <summary>
        /// Constructor for the bound receiver session state
        /// </summary>
        /// <param name="sess"></param>
        public SmscBoundRXSessionState(SmscSession sess)
            : base(sess)
        {
        }
    }
}
