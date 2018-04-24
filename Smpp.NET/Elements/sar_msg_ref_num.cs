using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The sar_msg_ref_num parameter is used to indicate the reference number for a particular
	/// concatenated short message.
	/// </summary>
	public class sar_msg_ref_num : TlvShort
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_SAR_MSG_REF_NUM;

		/// <summary>
		/// Default constructor
		/// </summary>
		public sar_msg_ref_num() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="refno">Reference Number</param>
		public sar_msg_ref_num(byte refno) : base(TlvTag)
		{
			this.Value = refno;
		}
	}
}
