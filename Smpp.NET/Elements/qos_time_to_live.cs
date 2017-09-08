using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The qos_time_to_live parameter defines the number of seconds which the sender requests the
	/// SMSC to keep the message if undelivered before it is deemed expired and not worth delivering.
	/// If the parameter is not present, the SMSC may apply a default value.
	/// </summary>
	public class qos_time_to_live : TlvInt
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_QOS_TIME_TO_LIVE;

		/// <summary>
		/// Default constructor
		/// </summary>
		public qos_time_to_live() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public qos_time_to_live(int secs) : base(TlvTag)
		{
			this.Value = secs;
		}
	}
}
