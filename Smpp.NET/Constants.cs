using System;
using System.Collections.Generic;
using System.Text;

namespace JulMar.Smpp {
	/// <summary>
	/// Supported versions of the SMPP protocol
	/// </summary>
	public struct SmppVersion {
		/// <summary>
		/// SMPP Version 3.3
		/// </summary>
		public const byte SMPP_V33 = 0x33;
		/// <summary>
		/// SMPP Version 3.4
		/// </summary>
		public const byte SMPP_V34 = 0x34;
		/// <summary>
		/// SMPP Version 5.0
		/// </summary>
		public const byte SMPP_V50 = 0x50;
	}

	/// <summary>
	/// The SMPP commands
	/// </summary>
	internal struct Commands {
		internal const int BIND_RECEIVER = 0x00000001;
		internal const int BIND_TRANSMITTER = 0x00000002;
		internal const int QUERY_SM = 0x00000003;
		internal const int SUBMIT_SM = 0x00000004;
		internal const int DELIVER_SM = 0x00000005;
		internal const int UNBIND = 0x00000006;
		internal const int REPLACE_SM = 0x00000007;
		internal const int CANCEL_SM = 0x00000008;
		internal const int BIND_TRANSCEIVER = 0x00000009;
		internal const int OUTBIND = 0x0000000B;
		internal const int ADD_SUB = 0x00000011;
		internal const int DEL_SUB = 0x00000012;
		internal const int MOD_SUB = 0x00000013;
		internal const int QUERY_SUB = 0x00000014;
		internal const int ENQUIRE_LINK = 0x00000015;
		internal const int ADD_DL = 0x00000016;
		internal const int MOD_DL = 0x00000017;
		internal const int DEL_DL = 0x00000018;
		internal const int VIEW_DL = 0x00000019;
		internal const int LIST_DLS = 0x00000020;
		internal const int SUBMIT_MULTI = 0x00000021;
		internal const int ALERT_NOTIFICATION = 0x00000102;
		internal const int DATA_SM = 0x00000103;
		internal const int BROADCAST_SM = 0x00000111;
		internal const int QUERY_BROADCAST_SM = 0x00000112;
		internal const int CANCEL_BROADCAST_SM = 0x00000113;
		internal const int GENERIC_NACK = unchecked((int)0x80000000);
		internal const int BIND_RECEIVER_RESP = unchecked((int)0x80000001);
		internal const int BIND_TRANSMITTER_RESP = unchecked((int)0x80000002);
		internal const int QUERY_SM_RESP = unchecked((int)0x80000003);
		internal const int SUBMIT_SM_RESP = unchecked((int)0x80000004);
		internal const int DELIVER_SM_RESP = unchecked((int)0x80000005);
		internal const int UNBIND_RESP = unchecked((int)0x80000006);
		internal const int REPLACE_SM_RESP = unchecked((int)0x80000007);
		internal const int CANCEL_SM_RESP = unchecked((int)0x80000008);
		internal const int BIND_TRANSCEIVER_RESP = unchecked((int)0x80000009);
		internal const int ADD_SUB_RESP = unchecked((int)0x80000011);
		internal const int DEL_SUB_RESP = unchecked((int)0x80000012);
		internal const int MOD_SUB_RESP = unchecked((int)0x80000013);
		internal const int QUERY_SUB_RESP = unchecked((int)0x80000014);
		internal const int ENQUIRE_LINK_RESP = unchecked((int)0x80000015);
		internal const int ADD_DL_RESP = unchecked((int)0x80000016);
		internal const int MOD_DL_RESP = unchecked((int)0x80000017);
		internal const int DEL_DL_RESP = unchecked((int)0x80000018);
		internal const int VIEW_DL_RESP = unchecked((int)0x80000019);
		internal const int LIST_DLS_RESP = unchecked((int)0x80000020);
		internal const int SUBMIT_MULTI_RESP = unchecked((int)0x80000021);
		internal const int DATA_SM_RESP = unchecked((int)0x80000103);
		internal const int BROADCAST_SM_RESP = unchecked((int)0x80000111);
		internal const int QUERY_BROADCAST_SM_RESP = unchecked((int)0x80000112);
		internal const int CANCEL_BROADCAST_SM_RESP = unchecked((int)0x80000113);
	}

	/// <summary>
	/// SMPP result codes
	/// </summary>
	public struct StatusCodes {
		/// <summary>
		/// Success - no error.
		/// </summary>
		public const int ESME_ROK = 0x00000000;
		/// <summary>
		/// Message Length is invalid.
		/// </summary>
		public const int ESME_RINVMSGLEN = 0x00000001;
		/// <summary>
		/// Invalid command length
		/// </summary>
		public const int ESME_RINVCMDLEN = 0x00000002;
		/// <summary>
		/// Invalid command id.
		/// </summary>
		public const int ESME_RINVCMDID = 0x00000003;
		/// <summary>
		/// Incorrect BIND status for a given command.
		/// </summary>
		public const int ESME_RINVBNDSTS = 0x00000004;
		/// <summary>
		/// ESME Already in bound state.
		/// </summary>
		public const int ESME_RALYBND = 0x00000005;
		/// <summary>
		/// Invalid priority flag
		/// </summary>
		public const int ESME_RINVPRTFLG = 0x00000006;
		/// <summary>
		/// Invalid registered delivery flag.
		/// </summary>
		public const int ESME_RINVREGDLVFLG = 0x00000007;
		/// <summary>
		/// System error
		/// </summary>
		public const int ESME_RSYSERR = 0x00000008;
		/// <summary>
		/// Invalid source address.
		/// </summary>
		public const int ESME_RINVSRCADR = 0x0000000A;
		/// <summary>
		/// Invalid Destination address.
		/// </summary>
		public const int ESME_RINVDSTADR = 0x0000000B;
		/// <summary>
		/// Invalid message id.
		/// </summary>
		public const int ESME_RINVMSGID = 0x0000000C;
		/// <summary>
		/// Bind failed.
		/// </summary>
		public const int ESME_RBINDFAIL = 0x0000000D;
		/// <summary>
		/// Invalid Password.
		/// </summary>
		public const int ESME_RINVPASWD = 0x0000000E;
		/// <summary>
		/// Invalid System ID.
		/// </summary>
		public const int ESME_RINVSYSID = 0x0000000F;
		/// <summary>
		/// Cancel SM failed.
		/// </summary>
		public const int ESME_RCANCELFAIL = 0x00000011;
		/// <summary>
		/// Replace SM failed.
		/// </summary>
		public const int ESME_RREPLACEFAIL = 0x00000013;
		/// <summary>
		/// Message Queue full.
		/// </summary>
		public const int ESME_RMSGQFUL = 0x00000014;
		/// <summary>
		/// Invalid service type.
		/// </summary>
		public const int ESME_RINVSERTYP = 0x00000015;
		/// <summary>
		/// Failed to Add Customer
		/// </summary>
		public const int ESME_RADDCUSTFAIL = 0x00000019;
		/// <summary>
		///  Failed to delete Customer
		/// </summary>
		public const int ESME_RDELCUSTFAIL = 0x0000001A;
		/// <summary>
		/// Failed to modify customer
		/// </summary>
		public const int ESME_RMODCUSTFAIL = 0x0000001B;
		/// <summary>
		/// Failed to Enquire Customer
		/// </summary>
		public const int ESME_RENQCUSTFAIL = 0x0000001C;
		/// <summary>
		/// Invalid Customer ID
		/// </summary>
		public const int ESME_RINVCUSTID = 0x0000001D;
		/// <summary>
		/// Invalid Customer Name
		/// </summary>
		public const int ESME_RINVCUSTNAME = 0x0000001F;
		/// <summary>
		/// Invalid Customer Address
		/// </summary>
		public const int ESME_RINVCUSTADR = 0x00000021;
		/// <summary>
		/// Invalid Address
		/// </summary>
		public const int ESME_RINVADR = 0x00000022;
		/// <summary>
		/// Customer Exists
		/// </summary>
		public const int ESME_RCUSTEXIST = 0x00000023;
		/// <summary>
		/// Customer does not exist
		/// </summary>
		public const int ESME_RCUSTNOTEXIST = 0x00000024;
		/// <summary>
		/// Failed to Add Distribution List
		/// </summary>
		public const int ESME_RADDDLFAIL = 0x00000026;
		/// <summary>
		/// Failed to modify Distribution List
		/// </summary>
		public const int ESME_RMODDLFAIL = 0x00000027;
		/// <summary>
		/// Failed to Delete Distribution List
		/// </summary>
		public const int ESME_RDELDLFAIL = 0x00000028;
		/// <summary>
		/// Failed to View Distribution List
		/// </summary>
		public const int ESME_RVIEWDLFAIL = 0x00000029;
		/// <summary>
		/// Failed to list Distribution Lists
		/// </summary>
		public const int ESME_RLISTDLSFAIL = 0x00000030;
		/// <summary>
		/// Param Retrieve Failed
		/// </summary>
		public const int ESME_RPARAMRETFAIL = 0x00000031;
		/// <summary>
		/// Invalid Param
		/// </summary>
		public const int ESME_RINVPARAM = 0x00000032;
		/// <summary>
		/// Invalid number of destinations
		/// </summary>
		public const int ESME_RINVNUMDESTS = 0x00000033;
		/// <summary>
		/// Invalid distribution list name
		/// </summary>
		public const int ESME_RINVDLNAME = 0x00000034;
		/// <summary>
		/// Invalid Distribution List Member Description
		/// </summary>
		public const int ESME_RINVDLMEMBDESC = 0x00000035;
		/// <summary>
		/// Invalid Distribution List Member Type
		/// </summary>
		public const int ESME_RINVDLMEMBTYP = 0x00000038;
		/// <summary>
		/// Invalid Distribution List Modify Option
		/// </summary>
		public const int ESME_RINVDLMODOPT = 0x00000039;
		/// <summary>
		/// Destination flag is invalid (submit_multi)
		/// </summary>
		public const int ESME_RINVDESTFLAG = 0x00000040;
		/// <summary>
		/// Invalid "Submit with replace" request
		/// </summary>
		public const int ESME_RINVSUBREP = 0x00000042;
		/// <summary>
		/// Invalid esm_class field data.
		/// </summary>
		public const int ESME_RINVESMCLASS = 0x00000043;
		/// <summary>
		/// Cannot submit to distribution list.
		/// </summary>
		public const int ESME_RCNTSUBDL = 0x00000044;
		/// <summary>
		/// Submit or Submit_multi failed.
		/// </summary>
		public const int ESME_RSUBMITFAIL = 0x00000045;
		/// <summary>
		/// Invalid source address TON
		/// </summary>
		public const int ESME_RINVSRCTON = 0x00000048;
		/// <summary>
		/// Invalid source address NPI
		/// </summary>
		public const int ESME_RINVSRCNPI = 0x00000049;
		/// <summary>
		/// Invalid destination address TON
		/// </summary>
		public const int ESME_RINVDSTTON = 0x00000050;
		/// <summary>
		/// Invalid destination address NPI
		/// </summary>
		public const int ESME_RINVDSTNPI = 0x00000051;
		/// <summary>
		/// Invalid system_type field
		/// </summary>
		public const int ESME_RINVSYSTYP = 0x00000053;
		/// <summary>
		/// Invalid replace_if_present flag
		/// </summary>
		public const int ESME_RINVREPFLAG = 0x00000054;
		/// <summary>
		/// Invalid number of messages
		/// </summary>
		public const int ESME_RINVNUMMSGS = 0x00000055;
		/// <summary>
		/// Throttling error (ESME has exceeded allowed message limits).
		/// </summary>
		public const int ESME_RTHROTTLED = 0x00000058;
		/// <summary>
		/// Provisioning Not Allowed
		/// </summary>
		public const int ESME_RPROVNOTALLWD = 0x00000059;
		/// <summary>
		/// Invalid scheduled delivery time
		/// </summary>
		public const int ESME_RINVSCHED = 0x00000061;
		/// <summary>
		/// Invalid message validity period
		/// </summary>
		public const int ESME_RINVEXPIRY = 0x00000062;
		/// <summary>
		/// Predefined message invalid or not found.
		/// </summary>
		public const int ESME_RINVDFTMSGID = 0x00000063;
		/// <summary>
		/// ESME receiver temporary app error code.
		/// </summary>
		public const int ESME_RX_T_APPN = 0x00000064;
		/// <summary>
		/// ESME receiver permenant app error code.
		/// </summary>
		public const int ESME_RX_P_APPN = 0x00000065;
		/// <summary>
		/// ESME receiver reject message error code.
		/// </summary>
		public const int ESME_RX_R_APPN = 0x00000066;
		/// <summary>
		/// query_sm request failed.
		/// </summary>
		public const int ESME_RQUERYFAIL = 0x00000067;
		/// <summary>
		/// Paging Customer ID Invalid No such subscriber
		/// </summary>
		public const int ESME_RINVPGCUSTID = 0x00000080;
		/// <summary>
		/// Paging Customer ID length Invalid
		/// </summary>
		public const int ESME_RINVPGCUSTIDLEN = 0x00000081;
		/// <summary>
		/// City Length Invalid
		/// </summary>
		public const int ESME_RINVCITYLEN = 0x00000082;
		/// <summary>
		/// State Length Invalid
		/// </summary>
		public const int ESME_RINVSTATELEN = 0x00000083;
		/// <summary>
		/// Zip Prefix Length Invalid
		/// </summary>
		public const int ESME_RINVZIPPREFIXLEN = 0x00000084;
		/// <summary>
		/// Zip Postfix Length Invalid
		/// </summary>
		public const int ESME_RINVZIPPOSTFIXLEN = 0x00000085;
		/// <summary>
		/// MIN Length Invalid
		/// </summary>
		public const int ESME_RINVMINLEN = 0x00000086;
		/// <summary>
		/// MIN Invalid (i.e. No such MIN)
		/// </summary>
		public const int ESME_RINVMIN = 0x00000087;
		/// <summary>
		/// PIN Length Invalid
		/// </summary>
		public const int ESME_RINVPINLEN = 0x00000088;
		/// <summary>
		/// Terminal Code Length Invalid
		/// </summary>
		public const int ESME_RINVTERMCODELEN = 0x00000089;
		/// <summary>
		/// Channel Length Invalid
		/// </summary>
		public const int ESME_RINVCHANNELLEN = 0x0000008A;
		/// <summary>
		/// Coverage Region Length Invalid
		/// </summary>
		public const int ESME_RINVCOVREGIONLEN = 0x0000008B;
		/// <summary>
		/// Cap Code Length Invalid
		/// </summary>
		public const int ESME_RINVCAPCODELEN = 0x0000008C;
		/// <summary>
		/// Message delivery time Length Invalid
		/// </summary>
		public const int ESME_RINVMDTLEN = 0x0000008D;
		/// <summary>
		/// Priority Message Length Invalid
		/// </summary>
		public const int ESME_RINVPRIORMSGLEN = 0x0000008E;
		/// <summary>
		/// Periodic Messages Length Invalid
		/// </summary>
		public const int ESME_RINVPERMSGLEN = 0x0000008F;
		/// <summary>
		/// Paging Alerts Length Invalid
		/// </summary>
		public const int ESME_RINVPGALERTLEN = 0x00000090;
		/// <summary>
		/// Short Message User Group Length Invalid
		/// </summary>
		public const int ESME_RINVSMUSERLEN = 0x00000091;
		/// <summary>
		/// Real Time Data broadcasts Length Invalid
		/// </summary>
		public const int ESME_RINVRTDBLEN = 0x00000092;
		/// <summary>
		/// Registered Delivery Lenght Invalid
		/// </summary>
		public const int ESME_RINVREGDELLEN = 0x00000093;
		/// <summary>
		/// Message Distribution Lenght Invalid
		/// </summary>
		public const int ESME_RINVMSGDISTLEN = 0x00000094;
		/// <summary>
		/// Priority Message Length Invalid
		/// </summary>
		public const int ESME_RINVPRIORMSG = 0x00000095;
		/// <summary>
		/// Message delivery time Invalid
		/// </summary>
		public const int ESME_RINVMDT = 0x00000096;
		/// <summary>
		/// Periodic Messages Invalid
		/// </summary>
		public const int ESME_RINVPERMSG = 0x00000097;
		/// <summary>
		/// Message Distribution Invalid
		/// </summary>
		public const int ESME_RINVMSGDIST = 0x00000098;
		/// <summary>
		/// Paging Alerts Invalid
		/// </summary>
		public const int ESME_RINVPGALERT = 0x00000099;
		/// <summary>
		/// Short Message User Group Invalid
		/// </summary>
		public const int ESME_RINVSMUSER = 0x0000009A;
		/// <summary>
		/// Real Time Data broadcasts Invalid
		/// </summary>
		public const int ESME_RINVRTDB = 0x0000009B;
		/// <summary>
		/// Registered Delivery Invalid
		/// </summary>
		public const int ESME_RINVREGDEL = 0x0000009C;
		/// <summary>
		/// KIF IW Field out of data
		/// </summary>
		public const int ESME_RINVOPTPARSTREAM = 0x0000009D;
		/// <summary>
		/// Optional Parameter not allowed
		/// </summary>
		public const int ESME_ROPTPARNOTALLWD = 0x0000009E;
		/// <summary>
		/// Invalid Optional Parameter Length
		/// </summary>
		public const int ESME_RINVOPTPARLEN = 0x0000009F;
		/// <summary>
		/// Error in the optional part of the PDU body.  Decoding of
		/// Tlvs (optional parameters) has resulted in either a corrupt
		/// PDU or an invalid length.
		/// </summary>
		public const int ESME_RINVTlvSTREAM = 0x000000C0;
		/// <summary>
		/// Tlv not allowed - a Tlv has been used in an invalid
		/// context, either inappropriate or deliberately rejected
		/// by the operator.
		/// </summary>
		public const int ESME_RTlvNOTALLOWED = 0x000000C1;
		/// <summary>
		/// Invalid parameter length.  A Tlv has specified a length
		/// which is considered invalid.
		/// </summary>
		public const int ESME_RINVTlvLEN = 0x000000C2;
		/// <summary>
		/// Expected optional parameter missing
		/// </summary>
		public const int ESME_RMISSINGOPTPARAM = 0x000000C3;
		/// <summary>
		/// Invalid optional parameter value
		/// </summary>
		public const int ESME_RINVOPTPARAMVAL = 0x000000C4;
		/// <summary>
		/// Delivery failure (used for data_sm_resp)
		/// </summary>
		public const int ESME_RDELIVERYFAILURE = 0x000000FE;
		/// <summary>
		/// Unknown error
		/// </summary>
		public const int ESME_RUNKNOWNERR = 0x000000FF;
		/// <summary>
		/// ESME not authorized to use specified operation.  The PDU
		/// was recognized by is denied to the ESME.
		/// </summary>
		public const int ESME_RSERTYPUNAUTH = 0x00000100;
		/// <summary>
		/// ESME prohibited from using specified operation.
		/// </summary>
		public const int ESME_RPROHIBITED = 0x00000101;
		/// <summary>
		/// Specified service_type is unavailable due to a service
		/// outage within the MC.
		/// </summary>
		public const int ESME_RSERTYPUNAVAIL = 0x00000102;
		/// <summary>
		/// Specified service_type is denied due to inappropriate
		/// message content wrt. the selected service_type.
		/// </summary>
		public const int ESME_RSERTYPDENIED = 0x00000103;
		/// <summary>
		/// Invalid Data Coding Scheme.  Specified DCS is invalid
		/// or not supported by the MC.
		/// </summary>
		public const int ESME_RINVDCS = 0x00000104;
		/// <summary>
		/// Source Address Sub unit is invalid
		/// </summary>
		public const int ESME_RINVSRVADDRSUBUNIT = 0x00000105;
		/// <summary>
		/// Destination Address Sub unit is invalid
		/// </summary>
		public const int ESME_RINVDSTADDRSUBUNIT = 0x00000106;
		/// <summary>
		/// Broadcast frequency interval is invalid or not supported.
		/// </summary>
		public const int ESME_RINVBCASTFREQINT = 0x00000107;
		/// <summary>
		/// Broadcast alias name is invalid - it has an invalid
		/// length or invalid/unsupported characters.
		/// </summary>
		public const int ESME_RINVBCASTALIAS_NAME = 0x00000108;
		/// <summary>
		/// Broadcast area format is invalid.  Specified value
		/// violates protocol or is unsupported.
		/// </summary>
		public const int ESME_RINVBCASTAREAFMT = 0x00000109;
		/// <summary>
		/// Number of Broadcast areas is invalid or unsupported.
		/// </summary>
		public const int ESME_RINVNUMBCAST_AREAS = 0x0000010A;
		/// <summary>
		/// Broadcast content type is invalid or unsupported.
		/// </summary>
		public const int ESME_RINVBCASTCNTTYPE = 0x0000010B;
		/// <summary>
		/// Broadcast message class is invalid or unsupported.
		/// </summary>
		public const int ESME_RINVBCASTMSGCLASS = 0x0000010C;
		/// <summary>
		/// broadcast_sm operation failed
		/// </summary>
		public const int ESME_RBCASTFAIL = 0x0000010D;
		/// <summary>
		/// query_broadcast_sm operation failed
		/// </summary>
		public const int ESME_RBCASTQUERYFAIL = 0x0000010E;
		/// <summary>
		/// cancel_broadcast_sm operation failed
		/// </summary>
		public const int ESME_RBCASTCANCELFAIL = 0x0000010F;
		/// <summary>
		/// Number of repeated broadcasts is invalid - violates
		/// protocol or is unsupported.
		/// </summary>
		public const int ESME_RINVBCAST_REP = 0x00000110;
		/// <summary>
		/// Broadcast Service Group is invalid.  Specified value
		/// violates protocol or is unsupported.
		/// </summary>
		public const int ESME_RINVBCASTSRVGROUP = 0x00000111;
		/// <summary>
		/// Broadcast channel indicator is invalid. Specified value
		/// violates protocol or is unsupported.
		/// </summary>
		public const int ESME_RINVBCASTCHANIND = 0x00000112;
		/// <summary>
		/// Start of the vendor specific errors
		/// </summary>
		public const int ESME_SMSC_VENDOR_START = 0x000000400;
		/// <summary>
		/// End of the vendor specific errors
		/// </summary>
		public const int ESME_SMSC_VENDOR_END = 0x0000004FF;
	}

	/// <summary>
	/// SMPP Optional Parameter Tag definitions (5.3.2)
	/// </summary>
	internal struct ParameterTags {
		internal const short TAG_DEST_ADDR_SUBUNIT = 0x0005;	// GSM
		internal const short TAG_DEST_NETWORK_TYPE = 0x0006;	// Generic
		internal const short TAG_DEST_BEARER_TYPE = 0x0007;	// Generic
		internal const short TAG_DEST_TELEMATICS_ID = 0x0008;	// GSM
		internal const short TAG_SOURCE_ADDR_SUBUNIT = 0x000D;// GSM
		internal const short TAG_SOURCE_NETWORK_TYPE = 0x000E;// Generic
		internal const short TAG_SOURCE_BEARER_TYPE = 0x000F;	// Generic
		internal const short TAG_SOURCE_TELEMATICS_ID = 0x0010;// GSM
		internal const short TAG_QOS_TIME_TO_LIVE = 0x0017;	// Generic
		internal const short TAG_PAYLOAD_TYPE = 0x0019;	    // Generic
		internal const short TAG_ADD_STATUS_INFO_TEXT = 0x001D;// Generic
		internal const short TAG_RECEIPTED_MESSAGE_ID = 0x001E;// Generic
		internal const short TAG_MS_MSG_WAIT_FACIL = 0x0030;	// GSM
		internal const short TAG_PRIVACY_INDICATOR = 0x0201;	// CDMA, TDMA
		internal const short TAG_SOURCE_SUBADDRESS = 0x0202;	// CDMA, TDMA
		internal const short TAG_DEST_SUBADDRESS = 0x0203;	// CDMA, TDMA
		internal const short TAG_USER_MESSAGE_REF = 0x0204;	// Generic
		internal const short TAG_USER_RESPONSE_CODE = 0x0205;	// CDMA, TDMA
		internal const short TAG_SOURCE_PORT = 0x020A;	    // Generic
		internal const short TAG_DESTINATION_PORT = 0x020B;	// Generic
		internal const short TAG_SAR_MSG_REF_NUM = 0x020C;	// Generic
		internal const short TAG_LANGUAGE_INDICATOR = 0x020D;	// CDMA, TDMA
		internal const short TAG_SAR_TOTAL_SEGMENTS = 0x020E;	// Generic
		internal const short TAG_SAR_SGEMENT_SEQNUM = 0x020F;	// Generic
		internal const short TAG_SC_INTERFACE_VERSION = 0x0210;// Generic
		internal const short TAG_CALLBACK_NUM_PRES_ID = 0x0302;// TDMA
		internal const short TAG_CALLBACK_NUM_ATAG = 0x0303;	// TDMA
		internal const short TAG_NUMBER_OF_MESSAGES = 0x0304;	// CDMA
		internal const short TAG_CALLBACK_NUM = 0x0381;	    // CDMA, TDMA, GSM, iDEN
		internal const short TAG_DPF_RESULT = 0x0420;	        // Generic
		internal const short TAG_SET_DPF = 0x0421;	        // Generic
		internal const short TAG_MS_AVAIL_STATUS = 0x0422;	// Generic
		internal const short TAG_NETWORK_ERROR_CODE = 0x0423;	// Generic
		internal const short TAG_MESSAGE_PAYLOAD = 0x0424;	// Generic
		internal const short TAG_DELIVERY_FAIL_REASON = 0x0425;// Generic
		internal const short TAG_MORE_MSGS_TO_SEND = 0x0426;	// GSM
		internal const short TAG_MESSAGE_STATE = 0x0427;	    // Generic
		internal const short TAG_CONGESTION_STATE = 0x0428;   // Generic
		internal const short TAG_USSD_SERVICE_OP = 0x0501;	// GSM (USSD)
		internal const short TAG_BROADCAST_CHANNEL_INDICATOR = 0x600; // GSM
		internal const short TAG_BROADCAST_CONTENT_TYPE = 0x601; // CDMA, TDMA, GSM
		internal const short TAG_BROADCAST_CONTENT_TYPE_INFO = 0x602; // CDMA, TDMA
		internal const short TAG_BROADCAST_MESSAGE_CLASS = 0x603; // GSM
		internal const short TAG_BROADCAST_REP_NUM = 0x604;   // GSM
		internal const short TAG_BROADCAST_FREQUENCY_INTERVAL = 0x605; // CDMA, TDMA, GSM
		internal const short TAG_BROADCAST_AREA_IDENTIFIER = 0x606; // CDMA, TDMA, GSM
		internal const short TAG_BROADCAST_ERROR_STATUS = 0x607; // CDMA, TDMA, GSM
		internal const short TAG_BROADCAST_AREA_SUCCESS = 0x608; // GSM
		internal const short TAG_BROADCAST_END_TIME = 0x609; // CDMA, TDMA, GSM
		internal const short TAG_BROADCAST_SERVICE_GROUP = 0x60A; // CDMA, TDMA
		internal const short TAG_BILLING_IDENTIFICATION = 0x60B;  // Generic
		internal const short TAG_SOURCE_NETWORK_ID = 0x60D;   // Generic
		internal const short TAG_DEST_NETWORK_ID = 0x60E;     // Generic
		internal const short TAG_SOURCE_NODE_ID = 0x60F;      // Generic
		internal const short TAG_DEST_NODE_ID = 0x610;        // Generic
		internal const short TAG_DEST_ADDR_NP_RESOLUTION = 0x611; // CDMA, TDMA (US Only)
		internal const short TAG_DEST_ADDR_NP_INFORMATION = 0x612; // CDMA, TDMA (US Only)
		internal const short TAG_DEST_ADDR_NP_COUNTRY = 0x613;    // CDMA, TDMA (US Only)
		internal const short TAG_DISPLAY_TIME = 0x1201;	    // CDMA, TDMA
		internal const short TAG_SMS_SIGNAL = 0x1203;	        // TDMA
		internal const short TAG_MS_VALIDITY = 0x1204;	    // CDMA, TDMA
		internal const short TAG_ALERT_ON_MSG_DELIVERY = 0x130C;// CDMA
		internal const short TAG_ITS_REPLY_TYPE = 0x1380;	    // CDMA
		internal const short TAG_ITS_SESSION_INFO = 0x1383;	// CDMA
	}
}
