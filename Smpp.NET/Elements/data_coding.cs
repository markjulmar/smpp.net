using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// This defines the encoding of data within the SMPP packet.
	/// </summary>
	public enum DataEncoding
	{
		/// <summary>
		/// Default alphabet
		/// </summary>
		SMSC_DEFAULT		= 0,
		/// <summary>
		/// IA5 (CCITT T.50)/ASCII (ANSI X3.4)
		/// </summary>
		IA5					= 1,
		/// <summary>
		/// Unspecified (8 bit binary)
		/// </summary>
		OCTET2				= 2,
		/// <summary>
		/// (ISO-8859-1)
		/// </summary>
		LATIN				= 3,
		/// <summary>
		/// Unspecified (8 bit binary)
		/// </summary>
		OCTET8				= 4,
		/// <summary>
		/// (X 0208-1990)
		/// </summary>
		JIS					= 5,
		/// <summary>
		/// (ISO-8859-5)
		/// </summary>
		CYRLLIC				= 6,
		/// <summary>
		/// (ISO-8859-8)
		/// </summary>
		LATINHEBREW			= 7,
		/// <summary>
		/// (ISO/IEC-10646)
		/// </summary>
		UCS2				= 8,
		/// <summary>
		/// Pictogram encoding
		/// </summary>
		PICTOGRAM_ENCODING	= 9,
		/// <summary>
		/// Music codes
		/// </summary>
		ISO2022_JP			= 10,
		/// <summary>
		/// (X 0212-1990)
		/// </summary>
		EXTENDEDKANJIJIS	= 13,
		/// <summary>
		/// KSC5601
		/// </summary>
		KS_C_5601			= 14
		// 1 1 0 0 x x x x GSM MWI control - see [GSM 03.38] d
		// 1 1 0 1 x x x x GSM MWI control - see [GSM 03.38] d
		// 1 1 1 0 x x x x reserved
		// 1 1 1 1 x x x x GSM message class control - see [GSM 03.38]
	}

	/// <summary>
	/// The data_coding parameter is used to indicate the data encoding used for SMS data.
	/// </summary>
	public class data_coding : SmppByte
	{
		// Constants
		private const int GSM_MESSAGE_CLASS_MASK = 0xf0;

		/// <summary>
		/// Default constructor
		/// </summary>
		public data_coding() : base((byte)DataEncoding.SMSC_DEFAULT)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public data_coding(DataEncoding type) : base((byte)((int)type&~GSM_MESSAGE_CLASS_MASK))
		{
		}

		/// <summary>
		/// Returns the DataEncoding value for this object.
		/// </summary>
		public new DataEncoding Value
		{
			get { return (DataEncoding) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Override of object.ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("data_coding: {0:X} {1} ", this.Value, this.Value.ToString());
		}
	}
}
