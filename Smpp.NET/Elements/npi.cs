using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// These constants define the Numeric Plan Indicator (NPI) to be used in the SME
	/// address parameters.
	/// </summary>
	public enum NumericPlanIndicator
	{
		/// <summary>
		/// Unknown
		/// </summary>
		UNKNOWN		= 0x00,
		/// <summary>
		/// ISDN (E163/E164)
		/// </summary>
		E164		= 0x01,
		/// <summary>
		/// Data (X.121)
		/// </summary>
		X121		= 0x03,
		/// <summary>
		/// Telex (F.69)
		/// </summary>
		TELEX		= 0x04,
		/// <summary>
		/// Land Mobile (E.212)
		/// </summary>
		LANDMOBILE	= 0x06,
		/// <summary>
		/// National
		/// </summary>
		NATIONAL	= 0x08,
		/// <summary>
		/// Private
		/// </summary>
		PRIVATE		= 0x09,
		/// <summary>
		/// ERMES
		/// </summary>
		ERMES		= 0x0A,
		/// <summary>
		/// Internet (IP)
		/// </summary>
		INTERNET	= 0x0E,
		/// <summary>
		/// WAP Client Id (to be defined by WAP forum).
		/// </summary>
		WAP_CLIENTID= 0x12
	}

	/// <summary>
	/// Summary description for npi.
	/// </summary>
	public class npi : SmppByte
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public npi() : base((byte)NumericPlanIndicator.UNKNOWN)
		{
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public npi(NumericPlanIndicator npi) : base((byte)npi)
		{
		}

		/// <summary>
		/// Override the value property
		/// </summary>
		public new NumericPlanIndicator Value
		{
			get { return (NumericPlanIndicator) base.Value; }
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
