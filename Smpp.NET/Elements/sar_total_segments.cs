using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The sar_total_segments parameter is used to indicate the total number of short messages
	/// within the concatenated short message.
	/// </summary>
	public class sar_total_segments : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_SAR_TOTAL_SEGMENTS;

		/// <summary>
		/// Default constructor
		/// </summary>
		public sar_total_segments() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="count">Segment Count</param>
		public sar_total_segments(byte count) : base(TlvTag)
		{
			this.Value = count;
		}

		/// <summary>
		/// This validates the data.
		/// </summary>
		protected override void ValidateData()
		{
			if (Value < 1)
				throw new ArgumentOutOfRangeException("The sar_total_segments value must be at least 1.");
		}
	}
}
