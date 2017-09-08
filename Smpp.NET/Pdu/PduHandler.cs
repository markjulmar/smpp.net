using System;
using System.Collections.Generic;
using System.Text;

namespace JulMar.Smpp.Pdu {
	/// <remarks>
	/// This class is used to generically identify a particular PDU type
	/// when it is received from the SmppSession in a chain of responsibility
	/// style pattern.
	/// </remarks>
	internal class PduHandler {
		private static Dictionary<int, PduHandler> typeMap_ = new Dictionary<int, PduHandler>();

		static PduHandler() {
			typeMap_.Add(Commands.UNBIND, new unbind_handler());
			typeMap_.Add(Commands.UNBIND_RESP, new unbind_resp_handler());

			typeMap_.Add(Commands.GENERIC_NACK, new generic_nack_handler());

			typeMap_.Add(Commands.BIND_RECEIVER, new bind_receiver_handler());
			typeMap_.Add(Commands.BIND_RECEIVER_RESP, new bind_receiver_resp_handler());

			typeMap_.Add(Commands.BIND_TRANSMITTER, new bind_transmitter_handler());
			typeMap_.Add(Commands.BIND_TRANSMITTER_RESP, new bind_transmitter_resp_handler());

			typeMap_.Add(Commands.QUERY_SM, new query_sm_handler());

			typeMap_.Add(Commands.SUBMIT_SM, new submit_sm_handler());
			typeMap_.Add(Commands.SUBMIT_SM_RESP, new submit_sm_resp_handler());

			typeMap_.Add(Commands.DELIVER_SM, new deliver_sm_handler());
			typeMap_.Add(Commands.DELIVER_SM_RESP, new deliver_sm_resp_handler());

			typeMap_.Add(Commands.REPLACE_SM, new replace_sm_handler());

			typeMap_.Add(Commands.CANCEL_SM, new cancel_sm_handler());

			typeMap_.Add(Commands.BIND_TRANSCEIVER, new bind_transceiver_handler());
			typeMap_.Add(Commands.BIND_TRANSCEIVER_RESP, new bind_transceiver_resp_handler());

			typeMap_.Add(Commands.OUTBIND, new outbind_handler());

			typeMap_.Add(Commands.ENQUIRE_LINK, new enquire_link_handler());
			typeMap_.Add(Commands.ENQUIRE_LINK_RESP, new enquire_link_resp_handler());

			typeMap_.Add(Commands.SUBMIT_MULTI, new submit_multi_handler());
			typeMap_.Add(Commands.ALERT_NOTIFICATION, new alert_notification_handler());
			typeMap_.Add(Commands.DATA_SM, new data_sm_handler());
			typeMap_.Add(Commands.BROADCAST_SM, new broadcast_sm_handler());
			typeMap_.Add(Commands.QUERY_BROADCAST_SM, new query_broadcast_sm_handler());
			typeMap_.Add(Commands.CANCEL_BROADCAST_SM, new cancel_broadcast_sm_handler());
			typeMap_.Add(Commands.ADD_SUB, new add_sub_handler());
			typeMap_.Add(Commands.MOD_SUB, new mod_sub_handler());
			typeMap_.Add(Commands.DEL_SUB, new del_sub_handler());
			typeMap_.Add(Commands.QUERY_SUB, new query_sub_handler());
			typeMap_.Add(Commands.ADD_DL, new add_dl_handler());
			typeMap_.Add(Commands.MOD_DL, new mod_dl_handler());
			typeMap_.Add(Commands.DEL_DL, new del_dl_handler());
			typeMap_.Add(Commands.LIST_DLS, new list_dls_handler());
			typeMap_.Add(Commands.VIEW_DL, new view_dl_handler());
		}

		/// <summary>
		/// This is the entrypoint which is called by the session object to dispatch
		/// a given PDU to the handler.
		/// </summary>
		/// <param name="state"></param>
		/// <param name="pdu"></param>
		/// <returns></returns>
		public static bool Dispatch(SmppSessionState state, SmppPdu pdu) {
			// Quick validation of the received PDU; make sure the command id matches up
			// to the type of PDU received and that we have a handler.
			if (typeMap_.ContainsKey(pdu.CommandId)) {
				PduHandler handler = typeMap_[pdu.CommandId];
				try {
					handler.InternalDispatch(state, pdu);
					return true;
				} catch (InvalidCastException) {
				}
			}
			return false;
		}

		/// <summary>
		/// This method must be overridden in implementations to cast the PDU
		/// to a concrete type and call the appropriate session state method.
		/// </summary>
		/// <param name="state">Session state</param>
		/// <param name="pdu">PDU</param>
		/// <returns></returns>
		public virtual void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			throw new NotImplementedException();
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class alert_notification_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((alert_notification)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class unbind_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((unbind)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class unbind_resp_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((unbind_resp)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class generic_nack_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((generic_nack)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class bind_receiver_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((bind_receiver)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class bind_receiver_resp_handler : PduHandler
	{
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu)
		{
			state.Process((bind_receiver_resp)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class bind_transmitter_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((bind_transmitter)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class bind_transmitter_resp_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((bind_transmitter_resp)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class query_sm_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((query_sm)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class submit_sm_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((submit_sm)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class submit_sm_resp_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((submit_sm_resp)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class deliver_sm_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((deliver_sm)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class deliver_sm_resp_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu)
		{
			state.Process((deliver_sm_resp)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class replace_sm_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((replace_sm)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class cancel_sm_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((cancel_sm)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class bind_transceiver_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((bind_transceiver)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class bind_transceiver_resp_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu){
			state.Process((bind_transceiver_resp)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class outbind_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((outbind)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class enquire_link_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((enquire_link)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class enquire_link_resp_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((enquire_link_resp)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class submit_multi_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((submit_multi)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class data_sm_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((data_sm)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class broadcast_sm_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((broadcast_sm)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class query_broadcast_sm_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((query_broadcast_sm)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class cancel_broadcast_sm_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((cancel_broadcast_sm)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class add_sub_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((add_sub)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class mod_sub_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((mod_sub)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class del_sub_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((del_sub)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class query_sub_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((enquire_sub)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class add_dl_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((add_dl)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class del_dl_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((del_dl)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class mod_dl_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((mod_dl)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class view_dl_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((view_dl)pdu);
		}
	}

	/// <remarks>
	/// This class acts as the "glue" to call the appropriate session state
	/// handler for a given pdu
	/// </remarks>
	internal class list_dls_handler : PduHandler {
		public override void InternalDispatch(SmppSessionState state, SmppPdu pdu) {
			state.Process((list_dls)pdu);
		}
	}
}
