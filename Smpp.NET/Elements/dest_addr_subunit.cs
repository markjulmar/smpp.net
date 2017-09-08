using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Subunit types used to identify external devices connected to a mobile station - such as
	/// a smart card, display, etc.
	/// </summary>
	public enum SubunitType
	{
		/// <summary>
		/// Unknown sub-unit type
		/// </summary>
		UNKNOWN				= 0,
		/// <summary>
		/// A display unit
		/// </summary>
		MS_DISPLAY			= 1,
		/// <summary>
		/// A mobile equipment unit (phone)
		/// </summary>
		MOBILE_EQUIPMENT	= 2,
		/// <summary>
		/// A smart card
		/// </summary>
		SMART_CARD			= 3,
		/// <summary>
		/// Some external unit type.
		/// </summary>
		EXTERNAL_UNIT		= 4
	}

	/// <summary>
	/// The dest_addr_subunit parameter is used to route messages when received by a mobile
	/// station; for example to a smart card in the mobile station or to an external device connection
	/// to the mobile station.
	/// </summary>
	public class dest_addr_subunit : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_DEST_ADDR_SUBUNIT;

		/// <summary>
		/// Default constructor
		/// </summary>
		public dest_addr_subunit() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public dest_addr_subunit(SubunitType type) : base(TlvTag)
		{
			this.Value = type;
		}

		/// <summary>
		/// Override of the property Value
		/// </summary>
		public new SubunitType Value
		{
			get { return (SubunitType) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value > SubunitType.EXTERNAL_UNIT)
				throw new System.ArgumentOutOfRangeException("The dest_addr_subunit is not valid.");
		}

		/// <summary>
		/// Override of the object.ToString method
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string s = "";
			if (HasValue)
			{
				switch (Value)
				{
					case SubunitType.UNKNOWN:
						s = "Unknown";
						break;
					case SubunitType.MS_DISPLAY:
						s = "MS Display";
						break;
					case SubunitType.MOBILE_EQUIPMENT:
						s = "Mobile Equipment";
						break;
					case SubunitType.SMART_CARD:
						s = "Smart Card I";
						break;
					case SubunitType.EXTERNAL_UNIT:
						s = "External Unit I";
						break;
					default:
						break;
				}
			}
			return string.Format("{0}{1}", base.ToString(), s);
		}
	}
}
