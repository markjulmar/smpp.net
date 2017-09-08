using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The sar_segment_seqnum parameter is used to indicate the sequence number of a particular
	/// short message within a concatenated short message.
	/// </summary>
	public class sar_segment_seqnum : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_SAR_SGEMENT_SEQNUM;

		/// <summary>
		/// Default constructor
		/// </summary>
		public sar_segment_seqnum() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="count">Segment Count</param>
		public sar_segment_seqnum(byte count) : base(TlvTag)
		{
			this.Value = count;
		}

		/// <summary>
		/// This validates the data.
		/// </summary>
		protected override void ValidateData()
		{
			if (Value < 1)
				throw new ArgumentOutOfRangeException("The sar_segment_seqnum value must be at least 1.");
		}
	}
}
