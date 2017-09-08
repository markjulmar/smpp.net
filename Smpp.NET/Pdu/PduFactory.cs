using System;
using System.Collections;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The PduFactory is responsible for maintaining the "Chain of Responsibility" and creating
	/// new SmppPdu elements based on the existing defitions.
	/// </summary>
	public class PduFactory {
		/// <summary> 
		/// This vector contains instances of all possible PDUs whic can be
		/// received and sent. It is used to create new instance of
		/// class based only on command id using a factory pattern.
		/// </summary>
		private static ArrayList pduFactory_ = null;

		/// <summary>
		/// Constructor
		/// </summary>
		public PduFactory() {
		}

		/// <summary> 
		/// Creates a list of instances of classes which can represent a PDU.
		/// </summary>
		static PduFactory() {
			pduFactory_ = ArrayList.Synchronized(new ArrayList());

			// Create each PDU here for the chain.
			pduFactory_.Add(new generic_nack());
			pduFactory_.Add(new bind_transmitter());
			pduFactory_.Add(new bind_transmitter_resp());
			pduFactory_.Add(new bind_receiver());
			pduFactory_.Add(new bind_receiver_resp());
			pduFactory_.Add(new bind_transceiver());
			pduFactory_.Add(new bind_transceiver_resp());
			pduFactory_.Add(new outbind());
			pduFactory_.Add(new unbind());
			pduFactory_.Add(new unbind_resp());
			pduFactory_.Add(new submit_sm());
			pduFactory_.Add(new submit_sm_resp());
			pduFactory_.Add(new submit_multi());
			pduFactory_.Add(new submit_multi_resp());
			pduFactory_.Add(new deliver_sm());
			pduFactory_.Add(new deliver_sm_resp());
			pduFactory_.Add(new data_sm());
			pduFactory_.Add(new data_sm_resp());
			pduFactory_.Add(new query_sm());
			pduFactory_.Add(new query_sm_resp());
			pduFactory_.Add(new enquire_link());
			pduFactory_.Add(new enquire_link_resp());
			pduFactory_.Add(new broadcast_sm());
			pduFactory_.Add(new broadcast_sm_resp());
			pduFactory_.Add(new query_broadcast_sm());
			pduFactory_.Add(new query_broadcast_sm_resp());
			pduFactory_.Add(new cancel_broadcast_sm());
			pduFactory_.Add(new cancel_broadcast_sm_resp());
			pduFactory_.Add(new add_sub());
			pduFactory_.Add(new add_sub_resp());
			pduFactory_.Add(new del_sub());
			pduFactory_.Add(new del_sub_resp());
			pduFactory_.Add(new mod_sub());
			pduFactory_.Add(new mod_sub_resp());
			pduFactory_.Add(new enquire_sub());
			pduFactory_.Add(new enquire_sub_resp());
			pduFactory_.Add(new add_dl());
			pduFactory_.Add(new add_dl_resp());
			pduFactory_.Add(new mod_dl());
			pduFactory_.Add(new mod_dl_resp());
			pduFactory_.Add(new del_dl());
			pduFactory_.Add(new del_dl_resp());
			pduFactory_.Add(new list_dls());
			pduFactory_.Add(new list_dls_resp());
			pduFactory_.Add(new view_dl());
			pduFactory_.Add(new view_dl_resp());
		}

		/// <summary>
		/// This method walks the Pdu list and creates new Pdu object instances
		/// using the existing one already created.
		/// </summary>
		/// <param name="commandId">Command Id to create</param>
		/// <returns>New Pdu object</returns>
		public static SmppPdu CreatePdu(int commandId) {
			int size = pduFactory_.Count;
			for (int i = 0; i < size; i++) {
				SmppPdu pdu = (SmppPdu)pduFactory_[i];
				if (pdu != null && pdu.CommandId == commandId) {
					try {
						return (SmppPdu)Activator.CreateInstance(pdu.GetType());
					} catch (Exception) {
					}
				}
			}
			return null;
		}
	}
}
