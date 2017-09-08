using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The SmppSubmitRequest base class defines all the optional message submission
	/// Tlvs which can be included by any of the submit_xxx functions.
	/// </summary>
	public class SmppSubmitRequest : SmppRequest {
		// Optional parameters follow
		private source_port srcPort_ = new source_port();
		private source_addr_subunit srcAddrSU_ = new source_addr_subunit();
		private source_network_type srcNetwork_ = new source_network_type();
		private source_bearer_type srcBearer_ = new source_bearer_type();
		private source_telematics_id srcTelID_ = new source_telematics_id();
		private destination_port destPort_ = new destination_port();
		private dest_addr_subunit destAddrSU_ = new dest_addr_subunit();
		private dest_network_type destNetwork_ = new dest_network_type();
		private dest_bearer_type destBearer_ = new dest_bearer_type();
		private dest_telematics_id destTelID_ = new dest_telematics_id();
		private sar_msg_ref_num msgrefNum_ = new sar_msg_ref_num();
		private sar_total_segments msgSegments_ = new sar_total_segments();
		private sar_segment_seqnum seqNum_ = new sar_segment_seqnum();
		private more_messages_to_send moreMsgs_ = new more_messages_to_send();
		private qos_time_to_live qos_ = new qos_time_to_live();
		private payload_type payloadType_ = new payload_type();
		private set_dpf dpf_ = new set_dpf();
		private receipted_message_id msgID_ = new receipted_message_id();
		private optional_message_state msgState_ = new optional_message_state();
		private network_error_code errCode_ = new network_error_code();
		private user_message_reference usrMsgRef_ = new user_message_reference();
		private privacy_indicator privInd_ = new privacy_indicator();
		private callback_num callbackNum_ = new callback_num();
		private callback_num_pres_ind callbackInd_ = new callback_num_pres_ind();
		private callback_num_atag callbackAtag_ = new callback_num_atag();
		private source_subaddress srcSubaddr_ = new source_subaddress();
		private dest_subaddress destSubaddr_ = new dest_subaddress();
		private user_response_code usrCode_ = new user_response_code();
		private display_time time_ = new display_time();
		private sms_signal smsSignal_ = new sms_signal();
		private ms_validity msValidity_ = new ms_validity();
		private ms_msg_wait_facilities msgWait_ = new ms_msg_wait_facilities();
		private number_of_messages numMsgs_ = new number_of_messages();
		private alert_on_message_delivery alert_ = new alert_on_message_delivery();
		private language_indicator language_ = new language_indicator();
		private its_reply_tone itsReply_ = new its_reply_tone();
		private its_session_info itsSession_ = new its_session_info();
		private billing_identification billId_ = new billing_identification();
		private dest_addr_np_country danpCountry_ = new dest_addr_np_country();
		private dest_addr_np_information danpInfo_ = new dest_addr_np_information();
		private dest_addr_np_resolution danpRes_ = new dest_addr_np_resolution();
		private dest_network_id destNetId_ = new dest_network_id();
		private dest_node_id destNodeId_ = new dest_node_id();
		private source_network_id srcNetId_ = new source_network_id();
		private source_node_id srcNodeId_ = new source_node_id();
		private ussd_service_op svcOp_ = new ussd_service_op();
		/// <summary>
		/// The message payload - exposed to derived classes directly
		/// </summary>
		protected message_payload msgPayload_ = new message_payload();

		/// <summary>
		/// Default constructor
		/// </summary>
		public SmppSubmitRequest(int commandId)
			: base(commandId) {
			// Add all the optional elements
			AddOptionalElements(srcPort_, srcAddrSU_, srcNetwork_,
					  srcBearer_, srcTelID_, destPort_, destAddrSU_,
					  destNetwork_, destBearer_, destTelID_,
					  msgrefNum_, msgSegments_, seqNum_, moreMsgs_,
					  qos_, payloadType_, msgPayload_, dpf_, msgID_,
					  msgState_, errCode_, usrMsgRef_, privInd_,
					  callbackNum_, callbackInd_, callbackAtag_,
					  srcSubaddr_, destSubaddr_, usrCode_, time_,
					  smsSignal_, msValidity_, msgWait_, numMsgs_, alert_,
					  language_, itsReply_, itsSession_, billId_,
					  danpCountry_, danpInfo_, danpRes_, destNetId_,
					  destNodeId_, srcNetId_, srcNodeId_, svcOp_);
		}

		/// <summary>
		/// Returns whether the source port exists.
		/// </summary>
		public bool HasSourcePort {
			get { return srcPort_.HasValue; }
		}

		/// <summary>
		/// Identifies the application port number associated with the source address
		/// of the message.  This parameter is required for WAP applications.
		/// </summary>
		public int SourcePort {
			get { return srcPort_.Value; }
			set { srcPort_.Value = value; }
		}

		/// <summary>
		/// Returns whether the source address subunit is in the packet.
		/// </summary>
		public bool HasSourceAddressSubunit {
			get { return srcAddrSU_.HasValue; }
		}

		/// <summary>
		/// The subcomponent in the destination device which created the user data.
		/// </summary>
		public SubunitType SourceAddressSubunit {
			get { return srcAddrSU_.Value; }
			set { srcAddrSU_.Value = value; }
		}

		/// <summary>
		/// Returns whether the source network type is present.
		/// </summary>
		public bool HasSourceNetworkType {
			get { return srcNetwork_.HasValue; }
		}

		/// <summary>
		/// The correct network associated with the originating device.
		/// </summary>
		public NetworkType SourceNetworkType {
			get { return srcNetwork_.Value; }
			set { srcNetwork_.Value = value; }
		}

		/// <summary>
		/// Returns whether the source bearer type is present.
		/// </summary>
		public bool HasSourceBearerType {
			get { return srcBearer_.HasValue; }
		}

		/// <summary>
		/// The correct bearer type for delivering the user data to the destination.
		/// </summary>
		public BearerType SourceBearerType {
			get { return srcBearer_.Value; }
			set { srcBearer_.Value = value; }
		}

		/// <summary>
		/// Returns whether the source telematics ID is present.
		/// </summary>
		public bool HasSourceTelematicsID {
			get { return srcTelID_.HasValue; }
		}

		/// <summary>
		/// The telematics identifier associated with the source.
		/// </summary>
		public int SourceTelematicsID {
			get { return srcTelID_.Value; }
			set { srcTelID_.Value = value; }
		}

		/// <summary>
		/// Returns whether the destination port exists.
		/// </summary>
		public bool HasDestinationPort {
			get { return destPort_.HasValue; }
		}

		/// <summary>
		/// Identifies the application port number associated with the destination address
		/// of the message.
		/// </summary>
		public int DestinationPort {
			get { return destPort_.Value; }
			set { destPort_.Value = value; }
		}

		/// <summary>
		/// Returns whether the destination address subunit is in the packet.
		/// </summary>
		public bool HasDestinationAddressSubunit {
			get { return destAddrSU_.HasValue; }
		}

		/// <summary>
		/// The subcomponent in the destination device for which the user data is intended.
		/// </summary>
		public SubunitType DestinationAddressSubunit {
			get { return destAddrSU_.Value; }
			set { destAddrSU_.Value = value; }
		}


		/// <summary>
		/// Returns whether the destination network type is present.
		/// </summary>
		public bool HasDestinationNetworkType {
			get { return destNetwork_.HasValue; }
		}

		/// <summary>
		/// The correct network associated with the destination device.
		/// </summary>
		public NetworkType DestinationNetworkType {
			get { return destNetwork_.Value; }
			set { destNetwork_.Value = value; }
		}

		/// <summary>
		/// Returns whether the destination bearer type is present.
		/// </summary>
		public bool HasDestinationBearerType {
			get { return destBearer_.HasValue; }
		}

		/// <summary>
		/// The correct bearer type for delivering the user data to the destination.
		/// </summary>
		public BearerType DestinationBearerType {
			get { return destBearer_.Value; }
			set { destBearer_.Value = value; }
		}

		/// <summary>
		/// Returns whether the destination telematics ID is present.
		/// </summary>
		public bool HasDestinationTelematicsID {
			get { return destTelID_.HasValue; }
		}

		/// <summary>
		/// The telematics identifier associated with the destination.
		/// </summary>
		public int DestinationTelematicsID {
			get { return destTelID_.Value; }
			set { destTelID_.Value = value; }
		}

		/// <summary>
		/// Returns whether a concatenated msg ref number is present.
		/// </summary>
		public bool HasConcatenatedMessageNumber {
			get { return msgrefNum_.HasValue; }
		}

		/// <summary>
		/// The reference number for a particular concatenated short message.
		/// </summary>
		public int ConcatenatedMessageNumber {
			get { return msgrefNum_.Value; }
			set { msgrefNum_.Value = value; }
		}

		/// <summary>
		/// Returns whether contatenated message segments exist.
		/// </summary>
		public bool HasConcatenatedMessageSegments {
			get { return msgSegments_.HasValue; }
		}

		/// <summary>
		/// Indicates the total number of short messages within the concatenated
		/// short message.
		/// </summary>
		public byte ConcatenatedMessageSegmentsCount {
			get { return msgSegments_.Value; }
			set { msgSegments_.Value = value; }
		}

		/// <summary>
		/// Returns whether the concatenated sequence number exists.
		/// </summary>
		public bool HasConcatenatedMessageSequence {
			get { return seqNum_.HasValue; }
		}

		/// <summary>
		/// Indicates the sequence number of a particular short message fragment within
		/// the concatenated short message.
		/// </summary>
		public byte ConcatenatedMessageSequence {
			get { return seqNum_.Value; }
			set { seqNum_.Value = value; }
		}

		/// <summary>
		/// Indicates there are more messages to follow for the destination SME
		/// </summary>
		public bool HasMoreMessages {
			get { return moreMsgs_.Value; }
			set { moreMsgs_.Value = value; }
		}

		/// <summary>
		/// Returns whether the QOS TTL is present
		/// </summary>
		public bool HasQualityOfServiceTTL {
			get { return qos_.HasValue; }
		}

		/// <summary>
		/// Time to live as a relative time in seconds from submission.
		/// </summary>
		public int QualityOfServiceTTL {
			get { return qos_.Value; }
			set { qos_.Value = value; }
		}

		/// <summary>
		/// Returns whether the payload type exists.
		/// </summary>
		public bool HasPayloadType {
			get { return payloadType_.HasValue; }
		}

		/// <summary>
		/// Defines the type of payload (e.g. WDP, WCMP, etc.)
		/// </summary>
		public PayloadTypes PayloadType {
			get { return payloadType_.Value; }
			set { payloadType_.Value = value; }
		}

		/// <summary>
		/// Returns whether the DPF is present.
		/// </summary>
		public bool DeliveryPendingFlagOnFailure {
			get { return (dpf_.HasValue && dpf_.Value == true); }
			set { dpf_.Value = value; }
		}

		/// <summary>
		/// Returns whether the message has a receipt id.
		/// </summary>
		public bool HasReceiptMessageID {
			get { return msgID_.HasValue; }
		}

		/// <summary>
		/// SMSC message ID of receipted message.
		/// </summary>
		public string ReceiptMessageID {
			get { return msgID_.Value; }
			set { msgID_.Value = value; }
		}


		/// <summary>
		/// Returns whether the network error code is present.
		/// </summary>
		public bool HasNetworkErrorCode {
			get { return errCode_.HasValue; }
		}

		/// <summary>
		/// Network Error Code may be available for intermediate notifications and delivery receipts.
		/// </summary>
		public network_error_code NetworkErrorCode {
			get { return errCode_; }
			set { errCode_ = value; }
		}

		/// <summary>
		/// Returns whether the message status exists
		/// </summary>
		public bool HasMessageStatus {
			get { return msgState_.HasValue; }
		}

		/// <summary>
		/// Should be present for SMSC delivery receipts and intermediate notifications.
		/// </summary>
		public MessageStatus MessageStatus {
			get { return msgState_.Value; }
			set { msgState_.Value = value; }
		}

		/// <summary>
		/// Returns whether the message reference ID exists.
		/// </summary>
		public bool HasMessageReferenceID {
			get { return this.usrMsgRef_.HasValue; }
		}

		/// <summary>
		/// ESME assigned message reference number
		/// </summary>
		public int MessageReferenceID {
			get { return this.usrMsgRef_.Value; }
			set { this.usrMsgRef_.Value = value; }
		}

		/// <summary>
		/// Returns whether the privacy indicator exists.
		/// </summary>
		public bool HasPrivacyIndicator {
			get { return privInd_.HasValue; }
		}

		/// <summary>
		/// Indicates the level of privacy associated with the message.
		/// </summary>
		public PrivacyTypes PrivacyIndicator {
			get { return privInd_.Value; }
			set { privInd_.Value = value; }
		}

		/// <summary>
		/// Returns whether the CallbackNumber is present.
		/// </summary>
		public bool HasCallbackNumber {
			get { return callbackNum_.HasValue; }
		}

		/// <summary>
		/// A callback number associated with the short message.  This parameter
		/// can be included a number of times for multiple callback addresses according
		/// to the spec, but we maintain only one here for now.  TODO: fix this.
		/// </summary>
		public callback_num CallbackNumber {
			get { return callbackNum_; }
		}

		/// <summary>
		/// Returns whether the CallbackNumber indicator is present.
		/// </summary>
		public bool HasCallbackNumberIndicator {
			get { return callbackInd_.HasValue; }
		}

		/// <summary>
		/// Defines the callback number presentation and screening. This parameter
		/// can be included a number of times for multiple callback addresses according
		/// to the spec, but we maintain only one here for now.  TODO: fix this.
		/// </summary>
		public callback_num_pres_ind CallbackNumberIndicator {
			get { return callbackInd_; }
		}

		/// <summary>
		/// Returns whether the CallbackNumberDisplay is present.
		/// </summary>
		public bool HasCallbackNumberDisplay {
			get { return callbackAtag_.HasValue; }
		}

		/// <summary>
		/// Associates a displayable alphanumeric tag with the callback number.  This parameter
		/// can be included a number of times for multiple callback addresses according
		/// to the spec, but we maintain only one here for now.  TODO: fix this.
		/// </summary>
		public callback_num_atag CallbackNumberDisplay {
			get { return callbackAtag_; }
		}

		/// <summary>
		/// Returns whether the source subaddress exists.
		/// </summary>
		public bool HasSourceSubaddress {
			get { return srcSubaddr_.HasValue; }
		}

		/// <summary>
		/// The subaddress of the message originator
		/// </summary>
		public source_subaddress SourceSubaddress {
			get { return srcSubaddr_; }
		}

		/// <summary>
		/// Returns whether the destination subaddress exists.
		/// </summary>
		public bool HasDestinationSubaddress {
			get { return destSubaddr_.HasValue; }
		}

		/// <summary>
		/// The subaddress of the message destination
		/// </summary>
		public dest_subaddress DestinationSubaddress {
			get { return destSubaddr_; }
		}

		/// <summary>
		/// Returns whether the user response code exists.
		/// </summary>
		public bool HasUserResponseCode {
			get { return usrCode_.HasValue; }
		}

		/// <summary>
		/// A user response code.  The actual codes are implementation specific.
		/// </summary>
		public byte UserResponseCode {
			get { return usrCode_.Value; }
			set { usrCode_.Value = value; }
		}

		/// <summary>
		/// Returns whether the display time exists
		/// </summary>
		public bool HasDisplayTime {
			get { return time_.HasValue; }
		}

		/// <summary>
		/// Provides the recieving MS with a display time associated with the message.
		/// </summary>
		public DisplayTimeType DisplayTime {
			get { return time_.Value; }
			set { time_.Value = value; }
		}

		/// <summary>
		/// Returns whether the SMS signal element exists.
		/// </summary>
		public bool HasSmsSignal {
			get { return smsSignal_.HasValue; }
		}

		/// <summary>
		/// Indicates the alerting mechanism when the message is received by an MS.
		/// </summary>
		public int SmsSignal {
			get { return smsSignal_.Value; }
			set { smsSignal_.Value = value; }
		}

		/// <summary>
		/// Returns whether the MS validity element exists.
		/// </summary>
		public bool HasMSValidity {
			get { return msValidity_.HasValue; }
		}

		/// <summary>
		/// Indicates validity information for this message to the recipient MS.
		/// </summary>
		public ValidityType MSValidity {
			get { return msValidity_.Value; }
			set { msValidity_.Value = value; }
		}

		/// <summary>
		/// Returns whether the message waiting type exists
		/// </summary>
		public bool HasMessageWaitingType {
			get { return msgWait_.HasValue; }
		}

		/// <summary>
		/// This controls the indication and type of message waiting indicator.
		/// </summary>
		public MWTypes MessageWaitingType {
			get { return msgWait_.Value; }
			set { msgWait_.Value = value; }
		}

		/// <summary>
		/// Returns whether the message waiting count exists.
		/// </summary>
		public bool HasMessageWaitingCount {
			get { return numMsgs_.HasValue; }
		}

		/// <summary>
		/// Indicates the number of messages stored in a mail box.
		/// </summary>
		public byte MessageWaitingCount {
			get { return numMsgs_.Value; }
			set { numMsgs_.Value = value; }
		}

		/// <summary>
		/// Requests an MS alert signal be invoked on message delivery.
		/// </summary>
		public bool AlertOnMessageDelivery {
			get { return alert_.Value; }
			set { alert_.Value = value; }
		}

		/// <summary>
		/// Returns whether the language indicator is present.
		/// </summary>
		public bool HasLanguageIndicator {
			get { return language_.HasValue; }
		}

		/// <summary>
		/// Indicates the language of the alphanumeric text message.
		/// </summary>
		public Language LanguageIndicator {
			get { return language_.Value; }
			set { language_.Value = value; }
		}

		/// <summary>
		/// Returns whether the ITSReplyType is present.
		/// </summary>
		public bool HasITSReplyType {
			get { return itsReply_.HasValue; }
		}

		/// <summary>
		/// The MS user's reply method to an SMS delivery message received
		/// from the network is indicated and controlled by this parameter.
		/// </summary>
		public ITSReplyTypes ITSReplyType {
			get { return itsReply_.Value; }
			set { itsReply_.Value = value; }
		}

		/// <summary>
		/// Returns whether the ITSSessionInfo is present.
		/// </summary>
		public bool HasITSSessionInfo {
			get { return itsSession_.HasValue; }
		}


		/// <summary>
		/// Session control information for Interactive Teleservice.
		/// </summary>
		public its_session_info ITSSessionInfo {
			get { return itsSession_; }
			set { itsSession_ = value; }
		}

		/// <summary>
		/// Returns whether a billing id was supplied for the message
		/// </summary>
		public bool HasBillingIdentification {
			get { return billId_.HasValue; }
		}

		/// <summary>
		/// Billing identification for message
		/// </summary>
		public billing_identification BillId {
			get { return billId_; }
			set { billId_ = value; }
		}

		/// <summary>
		/// Returns whether the destination country code is present
		/// </summary>
		public bool HasDestinationCountryCode {
			get { return danpCountry_.HasValue; }
		}

		/// <summary>
		/// This provides access to the destination country code (E.164)
		/// </summary>
		/// <value></value>
		public dest_addr_np_country DestinationCountryCode {
			get { return danpCountry_; }
			set { danpCountry_ = value; }
		}

		/// <summary>
		/// This returns whether the NPI is present
		/// </summary>
		public bool HasDestinationNumberPortabilityInfo {
			get { return danpInfo_.HasValue; }
		}

		/// <summary>
		/// This provides access to the number portability information
		/// </summary>
		public dest_addr_np_information DestinationNumberPortabilityInfo {
			get { return danpInfo_; }
			set { danpInfo_ = value; }
		}

		/// <summary>
		/// This returns whether the NPI resolution is present
		/// </summary>
		public bool HasDestinationNumberPortabilityResolution {
			get { return danpRes_.HasValue; }
		}

		/// <summary>
		/// This returns the destination number portability resolution type
		/// </summary>
		public dest_addr_np_resolution DestinationNumberPortabilityResolution {
			get { return danpRes_; }
			set { danpRes_ = value; }
		}

		/// <summary>
		/// This determines whether the network destination id is available
		/// </summary>
		public bool HasDestinationNetworkId {
			get { return destNetId_.HasValue; }
		}

		/// <summary>
		/// This returns the destination network id
		/// </summary>
		public string DestinationNetworkId {
			get { return destNetId_.Value; }
			set { destNetId_.Value = value; }
		}

		/// <summary>
		/// This returns whether the destination node id is present
		/// </summary>
		public bool HasDestinationNodeId {
			get { return destNodeId_.HasValue; }
		}

		/// <summary>
		/// This returns the destination node id
		/// </summary>
		public int DestinationNodeId {
			get { return destNodeId_.ID; }
			set { destNodeId_.ID = value; }
		}

		/// <summary>
		/// This returns whether the source network id is present
		/// </summary>
		public bool HasSourceNetworkId {
			get { return srcNetId_.HasValue; }
		}

		/// <summary>
		/// This returns the source network id
		/// </summary>
		public string SourceNetworkId {
			get { return srcNetId_.Value; }
			set { srcNetId_.Value = value; }
		}

		/// <summary>
		/// This returns whether the source node id is present
		/// </summary>
		public bool HasSourceNodeId {
			get { return srcNodeId_.HasValue; }
		}

		/// <summary>
		/// This returns the source node id
		/// </summary>
		public int SourceNodeId {
			get { return srcNodeId_.ID; }
			set { srcNodeId_.ID = value; }
		}

		/// <summary>
		/// This returns whether the USSD service type information is present
		/// </summary>
		public bool HasServiceTypeInfo {
			get { return svcOp_.HasValue; }
		}

		/// <summary>
		/// This returns the USSD service type information
		/// </summary>
		public ussd_service_op ServiceTypeInfo {
			get { return svcOp_; }
			set { svcOp_ = value; }
		}
	}
}
