using System;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Esme
{
    /// <summary>
    /// Summary description for BOUND_TX state.  In this state, the bound ESME may send short messages to
    /// the SMSC for onward delivery to a Mobile Station or to another EMSE.  The ESME may also replace, query
    /// or cancel a previously submitted short message.
    /// </summary>
    internal class EsmeBoundTXSessionState : EsmeBoundSessionState
    {
		protected EsmeSession session_;

		/// <summary>
		/// Constructor for the open session state
		/// </summary>
		/// <param name="session"></param>
		public EsmeBoundTXSessionState(EsmeSession session)
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
            get { return SmppBindStatus.BoundTx; }
        }

        /// <summary>
        /// This processes the submit_sm_resp PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(submit_sm_resp pdu)
        {
        }
    }
}
