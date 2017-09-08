using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The sms_signal parameter is used to provide a TDMA MS with alert tone information
	/// associated with the received short message.
	/// </summary>
	public class sms_signal : TlvShort
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_SMS_SIGNAL;

		/// <summary>
		/// Default constructor
		/// </summary>
		public sms_signal() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public sms_signal(short type) : base(TlvTag)
		{
			this.Value = type;
		}
	}
}
