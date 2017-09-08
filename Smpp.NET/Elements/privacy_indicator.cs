using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Privacy types
	/// </summary>
	public enum PrivacyTypes
	{
		/// <summary>
		/// Level 0 - Not restricted (default)
		/// </summary>
		NOT_RESTRICTED	= 0,
		/// <summary>
		/// Level 1 - Restricted
		/// </summary>
		RESTRICTED		= 1,
		/// <summary>
		/// Level 2 - Confidential
		/// </summary>
		CONFIDENTIAL	= 2,
		/// <summary>
		/// Level 3 - Secret
		/// </summary>
		SECRET			= 3,
	}

	/// <summary>
	/// The privacy_indicator parameter indicates the privacy level of the message.
	/// </summary>
	public class privacy_indicator : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_PRIVACY_INDICATOR;

		/// <summary>
		/// Default constructor
		/// </summary>
		public privacy_indicator() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public privacy_indicator(PrivacyTypes type) : base(TlvTag)
		{
			this.Value = type;
		}

		/// <summary>
		/// Override of property value
		/// </summary>
		public new PrivacyTypes Value
		{
			get { return (PrivacyTypes) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value > PrivacyTypes.SECRET)
				throw new System.ArgumentOutOfRangeException("The privacy_indicator is not valid.");
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
