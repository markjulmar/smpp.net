using System;
using System.Collections.Generic;
using System.ComponentModel;
using JulMar.Smpp.Utility;
using JulMar.Smpp.Pdu;

namespace JulMar.Smpp.Smsc
{
    /// <summary>
    /// This class represents a single P2P session between an ESME and an SMSC.  This class
    /// is shared between the server and client implementations; the server holds more than one session,
    /// the client is represented by a single session.
    /// </summary>
    public class SmscSession : SmppSession
    {
        private SmppServer server_;
        private delegate deliver_sm_resp DeliverSmDelegate(deliver_sm pdu);

        #region Events

        /// <summary>
        /// This event is fired when a bind_xxx request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnBind
        {
            add { eventMap_.AddHandler(EventType.Bind, value); }
            remove { eventMap_.RemoveHandler(EventType.Bind, value); }
        }

        /// <summary>
        /// This event is fired when a submit_sm request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnSubmitSm
        {
            add { eventMap_.AddHandler(EventType.SubmitSm, value); }
            remove { eventMap_.RemoveHandler(EventType.SubmitSm, value); }
        }

        /// <summary>
        /// This event is fired when a submit_multi request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnSubmitSmMulti
        {
            add { eventMap_.AddHandler(EventType.SubmitSmMulti, value); }
            remove { eventMap_.RemoveHandler(EventType.SubmitSmMulti, value); }
        }

        /// <summary>
        /// This event is fired when a data_sm request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnDataSm
        {
            add { eventMap_.AddHandler(EventType.DataSm, value); }
            remove { eventMap_.RemoveHandler(EventType.DataSm, value); }
        }

        /// <summary>
        /// This event is fired when a query_sm request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnQuerySm
        {
            add { eventMap_.AddHandler(EventType.QuerySm, value); }
            remove { eventMap_.RemoveHandler(EventType.QuerySm, value); }
        }

        /// <summary>
        /// This event is fired when a cancel_sm request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnCancelSm
        {
            add { eventMap_.AddHandler(EventType.CancelSm, value); }
            remove { eventMap_.RemoveHandler(EventType.CancelSm, value); }
        }

        /// <summary>
        /// This event is fired when a replace_sm request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnReplaceSm
        {
            add { eventMap_.AddHandler(EventType.ReplaceSm, value); }
            remove { eventMap_.RemoveHandler(EventType.ReplaceSm, value); }
        }

        /// <summary>
        /// This event is fired when a broadcast_sm request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnBroadcastSm
        {
            add { eventMap_.AddHandler(EventType.BroadcastSm, value); }
            remove { eventMap_.RemoveHandler(EventType.BroadcastSm, value); }
        }

        /// <summary>
        /// This event is fired when a cancel_broadcast_sm request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnCancelBroadcastSm
        {
            add { eventMap_.AddHandler(EventType.CancelBroadcastSm, value); }
            remove { eventMap_.RemoveHandler(EventType.CancelBroadcastSm, value); }
        }

        /// <summary>
        /// This event is fired when a query_broadcast_sm request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnQueryBroadcastSm
        {
            add { eventMap_.AddHandler(EventType.QueryBroadcastSm, value); }
            remove { eventMap_.RemoveHandler(EventType.QueryBroadcastSm, value); }
        }

        /// <summary>
        /// This event is fired when an add_sub request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnAddSub
        {
            add { eventMap_.AddHandler(EventType.AddSub, value); }
            remove { eventMap_.RemoveHandler(EventType.AddSub, value); }
        }

        /// <summary>
        /// This event is fired when a mod_sub request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnModSub
        {
            add { eventMap_.AddHandler(EventType.ModSub, value); }
            remove { eventMap_.RemoveHandler(EventType.ModSub, value); }
        }

        /// <summary>
        /// This event is fired when a del_sub request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnDelSub
        {
            add { eventMap_.AddHandler(EventType.DelSub, value); }
            remove { eventMap_.RemoveHandler(EventType.DelSub, value); }
        }

        /// <summary>
        /// This event is fired when an enquire_sub request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnEnquireSub
        {
            add { eventMap_.AddHandler(EventType.EnquireSub, value); }
            remove { eventMap_.RemoveHandler(EventType.EnquireSub, value); }
        }

        /// <summary>
        /// This event is fired when an add_dl request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnAddDL
        {
            add { eventMap_.AddHandler(EventType.AddDl, value); }
            remove { eventMap_.RemoveHandler(EventType.AddDl, value); }
        }

        /// <summary>
        /// This event is fired when an mod_dl request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnModDL
        {
            add { eventMap_.AddHandler(EventType.ModDl, value); }
            remove { eventMap_.RemoveHandler(EventType.ModDl, value); }
        }

        /// <summary>
        /// This event is fired when an del_dl request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnDelDL
        {
            add { eventMap_.AddHandler(EventType.DelDl, value); }
            remove { eventMap_.RemoveHandler(EventType.DelDl, value); }
        }

        /// <summary>
        /// This event is fired when an view_dl request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnViewDL
        {
            add { eventMap_.AddHandler(EventType.ViewDl, value); }
            remove { eventMap_.RemoveHandler(EventType.ViewDl, value); }
        }

        /// <summary>
        /// This event is fired when an list_dls request is received.
        /// </summary>
        public event EventHandler<SmppEventArgs> OnListDLs
        {
            add { eventMap_.AddHandler(EventType.ListDls, value); }
            remove { eventMap_.RemoveHandler(EventType.ListDls, value); }
        }

        #endregion

        /// <summary>
        /// This constructor creates a session from an existing (inbound) socket.
        /// </summary>
        /// <param name="server">SmscServer object</param>
        /// <param name="sc">Socket</param>
        /// <param name="sid">System ID</param>
        internal SmscSession(SmppServer server, SocketClient sc, string sid)
            : base(sc, sid)
        {
            server_ = server;
            CurrentState = new SmscOpenSessionState(this);
        }

        /// <summary>
        /// This method sends a deliver_sm packet synchronously over to the peer.
        /// </summary>
        /// <param name="pdu">deliver_sm packet</param>
        /// <returns>deliver_sm response</returns>
        public deliver_sm_resp DeliverSm(deliver_sm pdu)
        {
            deliver_sm_resp rpdu = null;
            PduSyncronizer evt = AddWaitingPdu(pdu);
            if (IsBound && SendPdu(pdu))
            {
                SmppPdu pduR = evt.PduResponse;
                if ((pduR as deliver_sm_resp) != null)
                    rpdu = (deliver_sm_resp)pduR;
                else
                {
                    rpdu = new deliver_sm_resp();
                    rpdu.Status = pduR.Status;
                }
            }
            else
            {
                FindAndRemoveWaitingPdu(pdu.SequenceNumber);
                rpdu = new deliver_sm_resp();
                rpdu.Status = StatusCodes.ESME_RDELIVERYFAILURE;
            }
            return rpdu;
        }

        /// <summary>
        /// This method invokes the DeliverSm method asynchronously
        /// </summary>
        /// <param name="pdu">deliver_sm PDU to send</param>
        /// <param name="ac">Asynch callback</param>
        /// <returns>IAsyncResult interface for monitoring</returns>
        public IAsyncResult BeginDeliverSm(deliver_sm pdu, AsyncCallback ac)
        {
            AsynchCall acr = new AsynchCall(ac, this);
            return acr.BeginInvoke(new DeliverSmDelegate(DeliverSm), new object[] { pdu });
        }

        /// <summary>
        /// This method retrieves the result of an asynchronous DeliverSm method call
        /// </summary>
        /// <param name="ar">Result from BeginDeliverSm</param>
        /// <returns>Result packet</returns>
        public deliver_sm_resp EndDeliverSm(IAsyncResult ar)
        {
            return (deliver_sm_resp)AsynchCall.ProcessEndInvoke(ar);
        }

        /// <summary>
        /// This method sends an alert_notification packet synchronously over to the peer.
        /// </summary>
        /// <param name="pdu">alert_notification packet</param>
        /// <returns>True/False</returns>
        public bool AlertNotification(alert_notification pdu)
        {
            return (IsBound && SendPdu(pdu));
        }

        /// <summary>
        /// This closes the connection and releases the session.
        /// </summary>
        public override void Close()
        {
            base.Close();
            server_.EndSession(this);
        }

        /// <summary>
        /// This method fires an event to all listeners
        /// </summary>
        /// <param name="eventType">Event being fired</param>
        /// <param name="ea">Arguments for event</param>
        /// <returns></returns>
        internal override bool FireEvent(object eventType, SmppEventArgs ea)
        {
            bool rc = base.FireEvent(eventType, ea);
            if (eventType == EventType.SessionDisconnected)
                server_.EndSession(this);

            return rc;
        }
    }
}
