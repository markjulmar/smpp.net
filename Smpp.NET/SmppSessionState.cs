using System;
using JulMar.Smpp.Pdu;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp
{
    /// <summary>
    /// Summary description for SessionState.
    /// </summary>
    internal abstract class SmppSessionState
    {
        /// <summary>
        /// This returns whether the session is bound
        /// </summary>
        /// <value>Current bind status</value>
        public virtual SmppBindStatus BindStatus
        {
            get { return SmppBindStatus.Unknown; }
        }

        /// <summary>
        /// This processes the alert_notification PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(alert_notification pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for an alert_notification operation.");
        }


        /// <summary>
        /// This processes the bind_transmitter PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(bind_transmitter pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a bind operation.");
        }

		/// <summary>
		/// This processes the bind_transmitter PDU
		/// </summary>
		/// <param name="pdu">Protocol Data Unit being processed</param>
		public virtual void Process(bind_transmitter_resp pdu)
		{
			throw new InvalidSmppStateException("Session is not in the proper state for a bind operation.");
		}

		/// <summary>
		/// This processes the bind_receiver PDU
		/// </summary>
		/// <param name="pdu">Protocol Data Unit being processed</param>
		public virtual void Process(bind_receiver pdu)
		{
			throw new InvalidSmppStateException("Session is not in the proper state for a bind operation.");
		}

		/// <summary>
		/// This processes the bind_receiver_resp PDU
		/// </summary>
		/// <param name="pdu">Protocol Data Unit being processed</param>
		public virtual void Process(bind_receiver_resp pdu)
		{
			throw new InvalidSmppStateException("Session is not in the proper state for a bind operation.");
		}

        /// <summary>
        /// This processes the bind_transceiver PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(bind_transceiver pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a bind operation.");
        }

		/// <summary>
		/// This processes the bind_transceiver_resp PDU
		/// </summary>
		/// <param name="pdu">Protocol Data Unit being processed</param>
		public virtual void Process(bind_transceiver_resp pdu)
		{
			throw new InvalidSmppStateException("Session is not in the proper state for a bind operation.");
		}

        /// <summary>
        /// This processes the unbind PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(unbind pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for an unbind operation.");
        }

        /// <summary>
        /// This processes the unbind_resp PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(unbind_resp pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for an unbind operation.");
        }

        /// <summary>
        /// This processes the cancel_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(cancel_sm pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a cancel_sm operation.");
        }

        /// <summary>
        /// This processes the data_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(data_sm pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a data_sm operation.");
        }

        /// <summary>
        /// This processes the deliver_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(deliver_sm pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a deliver_sm operation.");
        }

		/// <summary>
		/// This processes the deliver_sm_resp PDU
		/// </summary>
		/// <param name="pdu">Protocol Data Unit being processed</param>
		public virtual void Process(deliver_sm_resp pdu)
		{
			throw new InvalidSmppStateException("Session is not in the proper state for a deliver_sm operation.");
		}

        /// <summary>
        /// This processes the enquire_link PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(enquire_link pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a enquire_link operation.");
        }

        /// <summary>
        /// This processes the enquire_link_resp PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(enquire_link_resp pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a enquire_link operation.");
        }

        /// <summary>
        /// This processes the generic_nak PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(generic_nack pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a generic_nack operation.");
        }

        /// <summary>
        /// This processes the outbind PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(outbind pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a outbind operation.");
        }

        /// <summary>
        /// This processes the query_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(query_sm pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a query_sm operation.");
        }

        /// <summary>
        /// This processes the replace_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(replace_sm pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a replace_sm operation.");
        }

        /// <summary>
        /// This processes the submit_multi PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(submit_multi pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a submit_multi operation.");
        }

        /// <summary>
        /// This processes the submit_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(submit_sm pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a submit_sm operation.");
        }

        /// <summary>
        /// This processes the submit_sm_resp PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(submit_sm_resp pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a submit_sm operation.");
        }

        /// <summary>
        /// This processes the broadcast_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(broadcast_sm pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a broadcast_sm operation.");
        }

        /// <summary>
        /// This processes the query_broadcast_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(query_broadcast_sm pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a query_broadcast_sm operation.");
        }

        /// <summary>
        /// This processes the cancel_broadcast_sm PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(cancel_broadcast_sm pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a cancel_broadcast_sm operation.");
        }

        /// <summary>
        /// This processes the add_sub PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(add_sub pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a add_sub operation.");
        }

        /// <summary>
        /// This processes the del_sub PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(del_sub pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a del_sub operation.");
        }

        /// <summary>
        /// This processes the mod_sub PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(mod_sub pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a mod_sub operation.");
        }

        /// <summary>
        /// This processes the enquire_sub PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(enquire_sub pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a enquire_sub operation.");
        }

        /// <summary>
        /// This processes the add_dl PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(add_dl pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a add_dl operation.");
        }

        /// <summary>
        /// This processes the del_dl PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(del_dl pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a del_dl operation.");
        }

        /// <summary>
        /// This processes the mod_dl PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(mod_dl pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a mod_dl operation.");
        }

        /// <summary>
        /// This processes the view_dl PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(view_dl pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a view_dl operation.");
        }

        /// <summary>
        /// This processes the list_dls PDU
        /// </summary>
        /// <param name="pdu">Protocol Data Unit being processed</param>
        public virtual void Process(list_dls pdu)
        {
            throw new InvalidSmppStateException("Session is not in the proper state for a list_dls operation.");
        }
    }
}
