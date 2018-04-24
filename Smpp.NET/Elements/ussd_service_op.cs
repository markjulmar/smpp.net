using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// service operation types
	/// </summary>
	public enum USSDOperations : byte
	{
		/// <summary>
		/// PSSD indication
		/// </summary>
		PSSD_INDICATION = 0,
		/// <summary>
		/// PSSR indication
		/// </summary>
		PSSR_INDICATION = 1,
		/// <summary>
		///USSR request
		/// </summary>
		USSR_REQUEST = 2,
		/// <summary>
		/// USSN request
		/// </summary>
		USSN_REQUEST = 3,
		/// <summary>
		/// PSSD response
		/// </summary>
		PSSD_RESPONSE	= 16,
		/// <summary>
		/// PSSR response
		/// </summary>
		PSSR_RESPONSE	= 17,
		/// <summary>
		/// USSR confirm
		/// </summary>
		USSR_CONFIRM	= 18,
		/// <summary>
		/// USSN confirm
		/// </summary>
		USSN_CONFIRM	= 19
	}

	/// <summary>
	/// The ussd_service_op parameter is required to define the USSD service operation when
	/// SMPP is being used as an interface to a GSM USSD system.
	/// </summary>
	public class ussd_service_op : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_USSD_SERVICE_OP;

		/// <summary>
		/// Default constructor
		/// </summary>
		public ussd_service_op() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public ussd_service_op(USSDOperations type) : base(TlvTag)
		{
			this.Value = type;
		}

		/// <summary>
		/// Override of the property value
		/// </summary>
		public new USSDOperations Value
		{
			get { return (USSDOperations) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Override of object.ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string s = (HasValue) ? Value.ToString() : "";
			return string.Format("{0}{1}", base.ToString(), s);
		}

	}
}
