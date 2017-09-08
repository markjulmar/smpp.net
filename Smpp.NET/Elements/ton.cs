using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// These fields defined the Type Of Number (TON) to be used in the SME address
	/// parameters.
	/// </summary>
	public enum TypeOfNumber : byte
	{
		/// <summary>
		/// Unknown number type
		/// </summary>
		UNKNOWN			= 0,
		/// <summary>
		/// International number
		/// </summary>
		INTERNATIONAL	= 1,
		/// <summary>
		/// National number
		/// </summary>
		NATIONAL		= 2,
		/// <summary>
		/// Network specific
		/// </summary>
		NETWORK			= 3,
		/// <summary>
		/// Subscriber Number
		/// </summary>
		SUBSCRIBER		= 4,
		/// <summary>
		/// Alphanumeric
		/// </summary>
		ALPHANUMERIC	= 5,
		/// <summary>
		/// Abbreviated number.
		/// </summary>
		ABBREVIATED		= 6
	}

	/// <summary>
	/// The Type of number (TON) is used in the SME addres parameters.
	/// (addr_ton, source_addr_ton, dest_addr_ton, esme_addr_ton)
	/// </summary>
	public class ton : SmppByte
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ton() : base((byte)TypeOfNumber.UNKNOWN)
		{
		}

		/// <summary>
		/// Specific constructor
		/// </summary>
		/// <param name="ton">Type</param>
		public ton(TypeOfNumber ton) : base((byte)ton)
		{
		}

		/// <summary>
		/// Override of property value.
		/// </summary>
		public new TypeOfNumber Value
		{
			get { return (TypeOfNumber) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Override the object.ToString method
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
