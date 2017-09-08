using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Common service types (not a definitive list).
	/// </summary>
	public struct ServiceTypes
	{
		/// <summary>
		/// Default
		/// </summary>
		public const string DEFAULT = "";
		/// <summary>
		/// Cellular messaging
		/// </summary>
		public const string CELLULAR_MESSAGING = "CMT";
		/// <summary>
		/// Cellular paging
		/// </summary>
		public const string CELLULAR_PAGING = "CPT";
		/// <summary>
		/// Voicemail notification
		/// </summary>
		public const string VOICEMAIL_NOTIFICATION = "VMN";
		/// <summary>
		/// Voicemail alerting
		/// </summary>
		public const string VOICEMAIL_ALERTING = "VMA";
		/// <summary>
		/// WAP
		/// </summary>
		public const string WIRELESS_APPLICATION_PROTOCOL = "WAP";
		/// <summary>
		/// USSD
		/// </summary>
		public const string UNSTRUCTURED_SUPPLIMENTARY_SERVICES_DATA = "USSD";
        /// <summary>
        /// Cell Broadcast Service
        /// </summary>
        public const string CELL_BROADCAST_SERVICE = "CBS";
        /// <summary>
        /// Generic UDP Transport Service
        /// </summary>
        public const string GENERIC_UDP_TRANSPORT_SERVICE = "GUTS";
    }

	/// <summary>
	/// The service_type parameter can be used to indicate the SMS application service associated
	/// with the message.  Specifying the service_type allows the ESME to support "replace_if_present"
	/// and control the air interface. (5.2.11)
	/// </summary>
	public class service_type : SmppCOctetString
	{
		// Constants
		private const int MAX_LENGTH = 6;

		/// <summary>
		/// Constructor
		/// </summary>
		public service_type() : base(ServiceTypes.DEFAULT)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="s"></param>
		public service_type(string s) : base(s)
		{
		}

		/// <summary>
		/// This method validate the address range value.
		/// </summary>
		protected override void ValidateData()
		{
			if (Value != null && Value.Length > MAX_LENGTH)
				throw new System.ArgumentOutOfRangeException("The service_type must be between 0 and " + MAX_LENGTH.ToString() + " characters (found " + Value.Length.ToString() + " chars).");
		}
	}
}
