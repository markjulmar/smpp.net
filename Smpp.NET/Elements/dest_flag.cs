using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Destination types
	/// </summary>
	public enum DestinationType
	{
		/// <summary>
		/// None
		/// </summary>
		NONE		= 0,
		/// <summary>
		/// Specific SME address
		/// </summary>
		SME_ADDRESS	= 1,
		/// <summary>
		/// Distribution list name
		/// </summary>
		DL_NAME		= 2
	}

	/// <summary>
	/// The dest_flag parameter identifies whether a destination address is a distribution list (DL)
	/// name or SME address.
	/// </summary>
	public class dest_flag : SmppByte
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public dest_flag() : this(DestinationType.NONE)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public dest_flag(DestinationType type) : base((byte)type)
		{
		}

		/// <summary>
		/// Override of property value
		/// </summary>
		public new DestinationType Value
		{
			get { return (DestinationType) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value < DestinationType.NONE || Value > DestinationType.DL_NAME)
				throw new System.ArgumentOutOfRangeException("The dest_flag is not valid.");
		}

		/// <summary>
		/// Override of the object.ToString method
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string s;
			switch (Value)
			{
				case DestinationType.NONE:
					s = "None";
					break;
				case DestinationType.SME_ADDRESS:
					s = "SME Address";
					break;
				case DestinationType.DL_NAME:
					s = "Distribution List Name";
					break;
				default:
					s = Value.ToString("X");
					break;
			}
			return string.Format("dest_flag={0}", s);
		}
	}
}
