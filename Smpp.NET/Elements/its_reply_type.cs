using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// ITS reply types
	/// </summary>
	public enum ITSReplyTypes : byte
	{
		/// <summary>
		/// Digit
		/// </summary>
		DIGIT	= 0x0,
		/// <summary>
		/// Number
		/// </summary>
		NUMBER	= 0x1,
		/// <summary>
		/// Telephone Number
		/// </summary>
		TELNUM	= 0x2,
		/// <summary>
		/// Password
		/// </summary>
		PASSWORD= 0x3,
		/// <summary>
		/// Character Line
		/// </summary>
		CHARLINE= 0x4,
		/// <summary>
		/// Menu
		/// </summary>
		MENU	= 0x5,
		/// <summary>
		/// Date
		/// </summary>
		DATE	= 0x6,
		/// <summary>
		/// Time
		/// </summary>
		TIME	= 0x7,
		/// <summary>
		/// Continue
		/// </summary>
		CONTINUE= 0x8
	}

	/// <summary>
	/// The its_reply_tone parameter is used to indicate the version of the SMPP
	/// protocol supported by the SMSC.  It is returned by the bind_response PDU.
	/// </summary>
	public class its_reply_tone : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_ITS_REPLY_TYPE;

		/// <summary>
		/// Default constructor
		/// </summary>
		public its_reply_tone() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public its_reply_tone(ITSReplyTypes type) : base(TlvTag)
		{
			this.Value = type;
		}

		/// <summary>
		/// Override property value
		/// </summary>
		public new ITSReplyTypes Value
		{
			get { return (ITSReplyTypes) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value < ITSReplyTypes.DIGIT || Value > ITSReplyTypes.CONTINUE)
				throw new System.ArgumentOutOfRangeException("The its_reply_tone is not valid.");
		}

		/// <summary>
		/// Override of the object.ToString method
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string s = (HasValue) ? Value.ToString() : "";
			return string.Format("{0}{1}", base.ToString(), s);
		}
	}
}
