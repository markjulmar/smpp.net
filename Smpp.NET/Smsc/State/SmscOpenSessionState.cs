using System;
using JulMar.Smpp;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Smsc
{
    /// <summary>
    /// Summary description for OPEN state.
    /// </summary>
    internal class SmscOpenSessionState : SmppSessionState
    {
        private SmscSession session_;

        /// <summary>
        /// Constructor for the open session state
        /// </summary>
        /// <param name="sess"></param>
        public SmscOpenSessionState(SmscSession sess)
        {
            session_ = sess;
        }

        /// <summary>
        /// This method processes the bind_receiver PDU and performs the bind to the session.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(bind_receiver pdu)
        {
            // Build our response PDU
            bind_receiver_resp pduOut = new bind_receiver_resp(pdu.SequenceNumber, session_.LocalSystemID);

            // Assign the peer id and version
            session_.PeerSystemID = pdu.SystemID;
            session_.SmppVersion = pdu.InterfaceVersion;

            // Fire the bind event to the session owner
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, pduOut);
            if (session_.FireEvent(EventType.Bind, ea))
            {
                // If the session owner indicated it's ok to bind, then perform the binding.
                if (pduOut.Status == StatusCodes.ESME_ROK)
                    session_.CurrentState = new SmscBoundRXSessionState(session_);
                else
                {
                    session_.PeerSystemID = "";
                    session_.SmppVersion = 0;
                }
                session_.SendPdu(pduOut);
            }
        }

        /// <summary>
        /// This method processes the bind_transmitter PDU and performs the bind to the session.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(bind_transmitter pdu)
        {
            // Build our response PDU
            bind_transmitter_resp pduOut = new bind_transmitter_resp(pdu.SequenceNumber, session_.LocalSystemID);

            // Assign the peer id and version
            session_.PeerSystemID = pdu.SystemID;
            session_.SmppVersion = pdu.InterfaceVersion;

            // Fire the bind event to the session owner
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, pduOut);
            if (session_.FireEvent(EventType.Bind, ea))
            {
                // If the session owner indicated it's ok to bind, then perform the binding.
                if (pduOut.Status == StatusCodes.ESME_ROK)
                    session_.CurrentState = new SmscBoundTXSessionState(session_);
                else
                {
                    session_.PeerSystemID = "";
                    session_.SmppVersion = 0;
                }
                session_.SendPdu(pduOut);
            }
        }

        /// <summary>
        /// This method processes the bind_transceiver PDU and performs the bind to the session.
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public override void Process(bind_transceiver pdu)
        {
            // Build our response PDU
            bind_transceiver_resp pduOut = new bind_transceiver_resp(pdu.SequenceNumber, session_.LocalSystemID);

            // Assign the peer id and version
            session_.PeerSystemID = pdu.SystemID;
            session_.SmppVersion = pdu.InterfaceVersion;

            // Fire the bind event to the session owner
            SmppEventArgs ea = new SmppEventArgs(session_, pdu, pduOut);
            if (session_.FireEvent(EventType.Bind, ea))
            {
                // If the session owner indicated it's ok to bind, then perform the binding.
                if (pduOut.Status == StatusCodes.ESME_ROK)
                    session_.CurrentState = new SmscBoundTRXSessionState(session_);
                else
                {
                    session_.PeerSystemID = "";
                    session_.SmppVersion = 0;
                }
                session_.SendPdu(pduOut);
            }
        }
    }
}
