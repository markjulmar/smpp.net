using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// MS validity types
	/// </summary>
	public enum ValidityType : byte
	{
		/// <summary>
		/// Store indefinitely (default)
		/// </summary>
		STORE_INDEFINITELY	= 0,
		/// <summary>
		/// Power Down
		/// </summary>
		POWER_DOWN			= 1,
		/// <summary>
		/// SID based registration area
		/// </summary>
		SID_BASED_REG		= 2,
		/// <summary>
		/// Display Only
		/// </summary>
		DISPLAY_ONLY		= 3
	}

	/// <summary>
	/// The ms_validity parameter is used to provide an MS with validity information associated
	/// with the received short message.
	/// </summary>
	public class ms_validity : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_MS_VALIDITY;

		/// <summary>
		/// Default constructor
		/// </summary>
		public ms_validity() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public ms_validity(ValidityType type) : base(TlvTag)
		{
			this.Value = type;
		}

		/// <summary>
		/// Override property value
		/// </summary>
		public new ValidityType Value
		{
			get { return (ValidityType) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// This validates the data.
		/// </summary>
		protected override void ValidateData()
		{
			if (Value < ValidityType.STORE_INDEFINITELY || Value > ValidityType.DISPLAY_ONLY)
				throw new ArgumentOutOfRangeException("The ms_validity parameter is not valid.");
		}

		/// <summary>
		/// Override the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string s = (HasValue) ? Value.ToString() : "";
			return string.Format("{0}{1}", base.ToString(), s);
		}
	}
}
