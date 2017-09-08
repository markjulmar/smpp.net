using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The sc_interface_version parameter is used to indicate the version of the SMPP
	/// protocol supported by the SMSC.  It is returned by the bind_response PDU.
	/// </summary>
	public class sc_interface_version : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_SC_INTERFACE_VERSION;

		/// <summary>
		/// Default constructor
		/// </summary>
		public sc_interface_version() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public sc_interface_version(byte ver) : base(TlvTag)
		{
			this.Value = ver;
		}
	}
}
