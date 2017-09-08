using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// The broadcast_sm operation is used to submit a short message to the SMSC for onward
	/// transmission broadcasted over a given geographical area.
	/// </summary>
	public class broadcast_sm : SmppRequest {
		// Class data - required parameters
		private service_type stype_ = new service_type();
		private address saddr_ = new address();
		private message_id mid_ = new message_id();
		private priority_flag pflag_ = new priority_flag();
		private schedule_delivery_time deliveryTime_ = new schedule_delivery_time();
		private validity_period validPeriod_ = new validity_period();
		private replace_if_present repPresent_ = new replace_if_present();
		private data_coding dataCoding_ = new data_coding();
		private sm_default_msg_id defMsgId_ = new sm_default_msg_id();
		private broadcast_area_identifier areaId_ = new broadcast_area_identifier();
		private broadcast_content_type contentType_ = new broadcast_content_type();
		private broadcast_rep_num repNum_ = new broadcast_rep_num();
		private broadcast_frequency_interval freqInt_ = new broadcast_frequency_interval();
		// Optional parameters follow
		private alert_on_message_delivery alert_ = new alert_on_message_delivery();
		private broadcast_channel_indicator bcChan_ = new broadcast_channel_indicator();
		private broadcast_content_type_info contentTypeInfo_ = new broadcast_content_type_info();
		private broadcast_message_class bcmsgClass_ = new broadcast_message_class();
		private broadcast_service_group bgServiceGrp_ = new broadcast_service_group();
		private callback_num callbackNum_ = new callback_num();
		private callback_num_pres_ind callbackInd_ = new callback_num_pres_ind();
		private callback_num_atag callbackAtag_ = new callback_num_atag();
		private dest_addr_subunit destAddrSU_ = new dest_addr_subunit();
		private dest_subaddress destSubaddr_ = new dest_subaddress();
		private destination_port destPort_ = new destination_port();
		private display_time time_ = new display_time();
		private language_indicator language_ = new language_indicator();
		private message_payload msgPayload_ = new message_payload();
		private ms_validity msValidity_ = new ms_validity();
		private payload_type payloadType_ = new payload_type();
		private privacy_indicator privInd_ = new privacy_indicator();
		private sms_signal smsSignal_ = new sms_signal();
		private source_addr_subunit srcAddrSU_ = new source_addr_subunit();
		private source_port srcPort_ = new source_port();
		private source_subaddress srcSubaddr_ = new source_subaddress();
		private user_message_reference usrMsgRef_ = new user_message_reference();
		private broadcast_error_status errStat_ = new broadcast_error_status();
		private broadcast_area_identifier failedAreaId_ = new broadcast_area_identifier();

		/// <summary>
		/// Default constructor
		/// </summary>
		public broadcast_sm()
			: base(Commands.BROADCAST_SM) {
			AddOptionalElements(alert_, bcChan_, contentTypeInfo_,
				 bcmsgClass_, bgServiceGrp_, callbackNum_,
				 callbackInd_, callbackAtag_, destAddrSU_,
				 destSubaddr_, destPort_, time_, language_, msgPayload_,
				 msValidity_, payloadType_, privInd_, smsSignal_,
				 srcAddrSU_, srcPort_, srcSubaddr_, usrMsgRef_,
				 errStat_, failedAreaId_);
		}

		/// <summary>
		/// Primary constructor for the broadcast_sm PDU
		/// </summary>
		/// <param name="stype">Service Type</param>
		/// <param name="saddr">Source Address</param>
		/// <param name="messageId">Message Id</param>
		/// <param name="priority">Priority</param>
		/// <param name="delTime">Delivery Time</param>
		/// <param name="valPeriod">Validation Period</param>
		/// <param name="replace">Replace If present</param>
		/// <param name="dataCoding">Data Coding type</param>
		/// <param name="defMsgId">Default Msg ID</param>
		/// <param name="areaId">Geographical area ID</param>
		/// <param name="contentType">Content type</param>
		/// <param name="repeatCount">Repeat count</param>
		/// <param name="frequency">Frequency interval</param>
		public broadcast_sm(string stype, address saddr, string messageId,
							  MessagePriority priority, SmppTime delTime, SmppTime valPeriod,
							  bool replace, DataEncoding dataCoding, byte defMsgId,
							  broadcast_area_identifier areaId, broadcast_content_type contentType,
							  int repeatCount, broadcast_frequency_interval frequency)
			: this() {
			this.ServiceType = stype;
			this.SourceAddress = saddr;
			this.PriorityFlag = priority;
			this.DeliveryTime = delTime;
			this.ValidityPeriod = valPeriod;
			this.ReplaceExisting = replace;
			this.DataCoding = dataCoding;
			this.SmDefaultMessageID = defMsgId;
			this.areaId_ = areaId;
			this.contentType_ = contentType;
			this.repNum_.Value = repeatCount;
			this.freqInt_ = frequency;
		}

		/// <summary>
		/// Ths service_type parameter cna be used to indicate the SMS application service
		/// associated with the message.
		/// </summary>
		public string ServiceType {
			get { return stype_.Value; }
			set { stype_.Value = value; }
		}

		/// <summary>
		/// Address of SME which originated this message. If not known, set to null.
		/// </summary>
		public address SourceAddress {
			get { return saddr_; }
			set { saddr_ = value; }
		}

		/// <summary>
		/// Designates the priority level of the message.
		/// </summary>
		public MessagePriority PriorityFlag {
			get { return pflag_.Value; }
			set { pflag_.Value = value; }
		}

		/// <summary>
		/// The short message is to be scheduled by the SMSC for delivery.
		/// Set to default schedule_delivery_time for immediate delivery.
		/// </summary>
		public SmppTime DeliveryTime {
			get { return (SmppTime)deliveryTime_; }
			set { deliveryTime_ = new schedule_delivery_time(value); }
		}

		/// <summary>
		/// The validity period of this message.  Set to default validity_period
		/// for default behavior.
		/// </summary>
		public SmppTime ValidityPeriod {
			get { return (SmppTime)validPeriod_; }
			set { validPeriod_ = new validity_period(value); }
		}

		/// <summary>
		/// Flag indicating if submitted message should replace any existing message.
		/// </summary>
		public bool ReplaceExisting {
			get { return repPresent_.Value; }
			set { repPresent_.Value = value; }
		}

		/// <summary>
		/// Defines the encoding scheme of the short message user data.
		/// </summary>
		public DataEncoding DataCoding {
			get { return dataCoding_.Value; }
			set { dataCoding_.Value = value; }
		}

		/// <summary>
		/// Indicates the short message to send from a list of predefined "canned" messages
		/// stored on the SMSC.  If not using, set to zero.
		/// </summary>
		public byte SmDefaultMessageID {
			get { return defMsgId_.Value; }
			set { defMsgId_.Value = value; }
		}

		/// <summary>
		/// Up to 254 bytes of text message to send.
		/// </summary>
		public string Message {
			get {
				return msgPayload_.TextValue;
			}

			set {
				msgPayload_.TextValue = value;
			}
		}

		/// <summary>
		/// This retrieves the short message in a byte array
		/// </summary>
		public byte[] BinaryMessage {
			get {
				return msgPayload_.BinaryValue;
			}

			set {
				msgPayload_.BinaryValue = value;
			}
		}

		/// <summary>
		/// Identifies the target broadcast area(s) for the
		/// requested message broadcast.
		/// </summary>
		public broadcast_area_identifier BroadcastArea {
			get {
				return areaId_;
			}
			set {
				areaId_ = value;
			}
		}

		/// <summary>
		/// Specifies the content type of the message
		/// </summary>
		public broadcast_content_type Content {
			get {
				return contentType_;
			}
			set {
				contentType_ = value;
			}
		}

		/// <summary>
		/// Represents the number of repeated broadcasts of a message
		/// </summary>
		public int RepeatCount {
			get {
				return repNum_.Value;
			}
			set {
				repNum_.Value = value;
			}
		}

		/// <summary>
		/// Represents the frequency interval at which the
		/// broadcasts of a message should be repeated.
		/// </summary>
		public broadcast_frequency_interval FrequencyInterval {
			get {
				return freqInt_;
			}
			set {
				freqInt_ = value;
			}
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
		/// Indicates whether the broadcast channel indicator is available
		/// </summary>
		public bool HasBroadcastChannelIndicator {
			get { return bcChan_.HasValue; }
		}

		/// <summary>
		/// Specifies the cell broadcast channel that should be used
		/// for broadcasting messages
		/// </summary>
		public BroadcastChannel BroadcastChannel {
			get { return bcChan_.Channel; }
			set { bcChan_.Channel = value; }
		}

		/// <summary>
		/// Returns whether content information is available
		/// </summary>
		public bool HasContentTypeInfo {
			get { return contentTypeInfo_.HasValue; }
		}

		/// <summary>
		/// Contains free format content information
		/// </summary>
		public string ContentTypeInfo {
			get { return contentTypeInfo_.Value; }
			set { contentTypeInfo_.Value = value; }
		}

		/// <summary>
		/// Determines whether message class information is available
		/// </summary>
		public bool HasMessageClass {
			get { return bcmsgClass_.HasValue; }
		}

		/// <summary>
		/// Returns the broadcast message class
		/// </summary>
		public BroadcastMessageClass MessageClass {
			get { return bcmsgClass_.Class; }
			set { bcmsgClass_.Class = value; }
		}

		/// <summary>
		/// Determines whether broadcast service group information 
		/// is available
		/// </summary>
		public bool HasServiceGroup {
			get { return bgServiceGrp_.HasValue; }
		}

		/// <summary>
		/// Returns the broadcast service group
		/// </summary>
		public string ServiceGroup {
			get { return bgServiceGrp_.Value; }
			set { bgServiceGrp_.Value = value; }
		}

		/// <summary>
		/// Determines whether broadcast error status information 
		/// is available
		/// </summary>
		public bool HasErrorStatus {
			get { return errStat_.HasValue; }
		}

		/// <summary>
		/// Returns the broadcast error status
		/// </summary>
		/// <value>A StatusCode response</value>
		public int ErrorStatus {
			get { return errStat_.Value; }
			set { errStat_.Value = value; }
		}

		/// <summary>
		/// Determines whether failed geographical
		/// information is available
		/// </summary>
		public bool HasFailedArea {
			get { return failedAreaId_.HasValue; }
		}

		/// <summary>
		/// Failed broadcast area information
		/// </summary>
		/// <value></value>
		public broadcast_area_identifier FailedArea {
			get { return failedAreaId_; }
			set { failedAreaId_ = value; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(stype_);
			writer.Add(saddr_);
			writer.Add(mid_);
			writer.Add(pflag_);
			writer.Add(deliveryTime_);
			writer.Add(validPeriod_);
			writer.Add(repPresent_);
			writer.Add(dataCoding_);
			writer.Add(defMsgId_);
			writer.Add(areaId_);
			writer.Add(contentType_);
			writer.Add(repNum_);
			writer.Add(freqInt_);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(stype_);
			reader.ReadObject(saddr_);
			reader.ReadObject(mid_);
			reader.ReadObject(pflag_);
			reader.ReadObject(deliveryTime_);
			reader.ReadObject(validPeriod_);
			reader.ReadObject(repPresent_);
			reader.ReadObject(dataCoding_);
			reader.ReadObject(defMsgId_);
			reader.ReadObject(areaId_);
			reader.ReadObject(contentType_);
			reader.ReadObject(repNum_);
			reader.ReadObject(freqInt_);
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("broadcast_sm: {0},svc_type={1},src={2},msgid={3}" +
										"{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}{14}",
							base.ToString(),
							stype_, saddr_, mid_, pflag_, deliveryTime_, validPeriod_,
							repPresent_, dataCoding_, defMsgId_, areaId_,
							contentType_, repNum_, freqInt_,
							base.DumpOptionalParams());
		}
	}
}
