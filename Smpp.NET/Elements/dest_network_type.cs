using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Network types associated with network points.
	/// </summary>
	public enum NetworkType
	{
		/// <summary>
		/// Unknown network type
		/// </summary>
		UNKNOWN	= 0x0,
		/// <summary>
		/// GSM
		/// </summary>
		GSM		= 0x1,
		/// <summary>
		/// ANSI-136/TDMA
		/// </summary>
		TDMA	= 0x2,
		/// <summary>
		/// IS-95/CDMA
		/// </summary>
		CDMA	= 0x3,
		/// <summary>
		/// PDC
		/// </summary>
		PDC		= 0x4,
		/// <summary>
		/// PHS
		/// </summary>
		PHS		= 0x5,
		/// <summary>
		/// iDEN
		/// </summary>
		iDEN	= 0x6,
		/// <summary>
		/// AMPS
		/// </summary>
		AMPS	= 0x7,
		/// <summary>
		/// Paging network
		/// </summary>
		PAGING_NW = 0x8
	}

	/// <summary>
	/// The dest_network_type parameter is used to indicate a network type associated with the
	/// destination address of a message.  In the case that the receiving system (e.g. SMSC) does not
	/// support the indicated network type, it may treat this as a failure and return a response PDU
	/// reporting a failure.
	/// </summary>
	public class dest_network_type : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_DEST_NETWORK_TYPE;

		/// <summary>
		/// Default constructor
		/// </summary>
		public dest_network_type() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public dest_network_type(NetworkType type) : base(TlvTag)
		{
			this.Value = type;
		}

		/// <summary>
		/// Override of property value
		/// </summary>
		public new NetworkType Value
		{
			get { return (NetworkType) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value > NetworkType.PAGING_NW)
				throw new System.ArgumentOutOfRangeException("The dest_network_type is not valid.");
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
