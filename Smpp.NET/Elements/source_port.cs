using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The source_port parameter is used to indicate the application port number assigned with the
	/// source address of message.
	/// </summary>
	public class source_port : TlvShort
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_SOURCE_PORT;

		/// <summary>
		/// Default constructor
		/// </summary>
		public source_port() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public source_port(byte type) : base(TlvTag)
		{
			this.Value = type;
		}
	}
}
