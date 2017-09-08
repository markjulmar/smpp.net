using System;
using JulMar.Smpp;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Esme
{
    /// <summary>
    /// Summary description for disconnected state.
    /// </summary>
    internal class EsmeDisconnectedSessionState : SmppSessionState
    {
        private EsmeSession session;

        /// <summary>
        /// Constructor for the disconnected session state
        /// </summary>
        /// <param name="session"></param>
        public EsmeDisconnectedSessionState(EsmeSession session)
        {
            this.session = session;
        }
    }
}
