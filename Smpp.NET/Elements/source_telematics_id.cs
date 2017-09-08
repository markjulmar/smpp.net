using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The source_telematics_id parameter indicates the telematic interworking over which the
	/// message originated.
	/// </summary>
	public class source_telematics_id : TlvShort
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_SOURCE_TELEMATICS_ID;

		/// <summary>
		/// Default constructor
		/// </summary>
		public source_telematics_id() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public source_telematics_id(int type) : base(TlvTag)
		{
			this.Value = type;
		}
	}
}
