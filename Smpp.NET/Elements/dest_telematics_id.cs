using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The dest_telematics_id parameter defines the telematic interworking to be used by the delivering
	/// system for the destination address.  This is only useful when a specific dest_bearer_type parameter
	/// has also been specified as the value is bearer dependant.  In the case that the receiving system 
	/// (e.g. SMSC) does not support the indicated telematic interworking, it may treat this as a failure
	/// and return a response PDU indicating a failure.
	/// </summary>
	public class dest_telematics_id : TlvShort
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_DEST_TELEMATICS_ID;

		/// <summary>
		/// Default constructor
		/// </summary>
		public dest_telematics_id() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public dest_telematics_id(int type) : base(TlvTag)
		{
			this.Value = type;
		}
	}
}
