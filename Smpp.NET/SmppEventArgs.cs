using System;
using System.Net;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Elements;

namespace JulMar.Smpp
{
    /// <summary>
    /// This class represents a reported event from the SMPP library.
    /// </summary>
    public class SmppEventArgs : EventArgs
    {
        private SmppPdu pduIn_ = null;
        private SmppSession session_ = null;
        private SmppPdu pduOut_ = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        internal SmppEventArgs()
        {
        }

        /// <summary>
        /// Constructor for the Smpp EventHandler class.
        /// </summary>
        /// <param name="session">The SMPP session this event corresponds to.</param>
        internal SmppEventArgs(SmppSession session)
        {
            session_ = session;
        }

        /// <summary>
        /// Constructor for the Smpp EventHandler class.
        /// </summary>
        /// <param name="session">The SMPP session this event corresponds to.</param>
        /// <param name="pdu">PDU this event will represent.</param>
        internal SmppEventArgs(SmppSession session, SmppPdu pdu)
        {
            session_ = session;
            pduIn_ = pdu;
        }

        /// <summary>
        /// Constructor for the Smpp EventHandler class.
        /// </summary>
        /// <param name="session">The SMPP session this event corresponds to.</param>
        /// <param name="pdu">PDU this event will represent.</param>
        /// <param name="pduOut">PDU this event will return.</param>
        internal SmppEventArgs(SmppSession session, SmppPdu pdu, SmppPdu pduOut)
        {
            session_ = session;
            pduIn_ = pdu;
            pduOut_ = pduOut;
        }

        /// <summary>
        /// The SMPP session for this event.
        /// </summary>
        public SmppSession Session
        {
            get { return session_; }
        }

        /// <summary>
        /// The PDU which this event represents.
        /// </summary>
        public SmppPdu PDU
        {
            get { return pduIn_; }
        }

        /// <summary>
        /// The PDU which will be returned for this event.
        /// </summary>
        /// <value></value>
        public SmppPdu ResponsePDU
        {
            get { return pduOut_; }
        }
    }

    /// <summary>
    /// This is passed to the Connect event
    /// </summary>
    public class SmppConnectEventArgs : SmppEventArgs
    {
        private IPAddress remoteAddress;
        private bool allowConnection = true;

        /// <summary>
        /// Constructor for the event arguments
        /// </summary>
        /// <param name="remoteAddress">Peer address</param>
        /// <param name="session">Session this event is associated with</param>
        internal SmppConnectEventArgs(IPAddress remoteAddress, SmppSession session)
            : base(session)
        {
            this.remoteAddress = remoteAddress;
        }

        /// <summary>
        /// This is used to notify the event sender as to whether to allow the connection
        /// to be processed.
        /// </summary>
        public bool AllowConnection
        {
            get { return this.allowConnection; }
            set { this.allowConnection = value; }
        }

        /// <summary>
        /// This returns the end-point address for this connection.
        /// </summary>
        public IPAddress RemoteAddress
        {
            get { return this.remoteAddress; }
        }
    }

    /// <summary>
    /// This event arguments class is passed to the session end event.
    /// </summary>
    public class SmppDisconnectEventArgs : SmppEventArgs
    {
        private Exception ex_ = null;

        internal SmppDisconnectEventArgs(SmppSession session)
            : base(session)
        {
        }

        internal SmppDisconnectEventArgs(SmppSession session, Exception ex)
            : base(session)
        {
            ex_ = ex;
        }

        /// <summary>
        /// Returns the error exception (if any)
        /// </summary>
        public Exception Exception
        {
            get { return ex_; }
        }
    }

    /// <summary>
    /// This event arguments class is passed when an error occurs.
    /// </summary>
    public class SmppErrorEventArgs : SmppEventArgs
    {
        private Exception ex_ = null;

        internal SmppErrorEventArgs(SmppSession session, Exception ex)
            : base(session)
        {
            ex_ = ex;
        }

        internal SmppErrorEventArgs(SmppSession session, Exception ex, SmppPdu pdu)
            : base(session, pdu)
        {
            ex_ = ex;
        }

        /// <summary>
        /// Returns the error exception (if any)
        /// </summary>
        public Exception Exception
        {
            get { return ex_; }
        }
    }

    /// <summary>
    /// This class is sent out as the argument buffer for the PduReceived event.
    /// </summary>
    public class SmppPduReceivedEventArgs : SmppEventArgs
    {
        private bool processed_ = false;
        internal SmppPduReceivedEventArgs(SmppSession session, SmppPdu pdu)
            : base(session, pdu)
        {
        }

        /// <summary>
        /// This method allows the receiver of the event to mark the event as "processed".
        /// </summary>
        public bool Handled
        {
            get { return processed_; }
            set { processed_ = value; }
        }
    }
}
