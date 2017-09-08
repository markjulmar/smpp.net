using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The user_response_code is set in the User Acknowledgement/Reply message.  The response
	/// codes are application specific.
	/// </summary>
	public class user_response_code : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_USER_RESPONSE_CODE;

		/// <summary>
		/// Default constructor
		/// </summary>
		public user_response_code() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public user_response_code(byte val) : base(TlvTag)
		{
			this.Value = val;
		}
	}
}
