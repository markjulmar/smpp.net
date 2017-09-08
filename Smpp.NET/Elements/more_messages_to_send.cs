using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The more_messages_to_send parameter is used by the ESME in the submit_sm and data_sm
	/// operations to indicate the SMSC that there are further messages for the same destination
	/// SME.  The SMSC may use this setting for network resource optimization.
	/// </summary>
	public class more_messages_to_send : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_MORE_MSGS_TO_SEND;

		/// <summary>
		/// Default constructor
		/// </summary>
		public more_messages_to_send() : base(TlvTag)
		{
		}

			/// <summary>
			/// Parameterized constructor
			/// </summary>
		public more_messages_to_send(bool on) : base(TlvTag)
		{
			this.Value = on;
		}

		/// <summary>
		/// Returns the value as a boolean
		/// </summary>
		public new bool Value
		{
			get { return (base.Value == 1) ? true : false; }
			set { base.Value = (value == true) ? (byte)1 : (byte)0; }
		}
	}
}
