using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The user_message_reference is the id assigned by the originating SME to the short message.
	/// </summary>
	public class user_message_reference : TlvShort
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_USER_MESSAGE_REF;

		/// <summary>
		/// Default constructor
		/// </summary>
		public user_message_reference() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public user_message_reference(int val) : base(TlvTag)
		{
			this.Value = val;
		}
	}
}
