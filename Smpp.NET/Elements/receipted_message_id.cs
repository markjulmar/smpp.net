using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The receipted_message_id parameter indicates the ID of the message being receipted in an
	/// SMSC Delivery Receipt.  This is the opaque SMSC message identifier that was returned in the
	/// message_id parameter of the SMPP response PDU that acknowledged the submission of the original
	/// message.
	/// </summary>
	public class receipted_message_id : TlvCOctetString
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_RECEIPTED_MESSAGE_ID;
		private const int MAX_LENGTH = 65;

		/// <summary>
		/// Default constructor
		/// </summary>
		public receipted_message_id() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public receipted_message_id(string text) : base(TlvTag)
		{
			this.Value = text;
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value.Length > MAX_LENGTH)
				throw new System.ArgumentOutOfRangeException("The receipted_message_id cannot exceed " + MAX_LENGTH.ToString() + " characters.");
		}
	}
}
