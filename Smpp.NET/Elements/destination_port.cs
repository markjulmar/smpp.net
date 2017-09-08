using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The destination_port parameter is used to indicate the application port number assigned with the
	/// destination address of the message.
	/// </summary>
	public class destination_port : TlvShort
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_DESTINATION_PORT;

		/// <summary>
		/// Default constructor
		/// </summary>
		public destination_port() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public destination_port(byte type) : base(TlvTag)
		{
			this.Value = type;
		}
	}
}
