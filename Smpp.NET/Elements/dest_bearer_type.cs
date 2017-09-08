using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Bearer types
	/// </summary>
	public enum BearerType
	{
		/// <summary>
		/// Unknown bearer type
		/// </summary>
		UNKNOWN		= 0x0,
		/// <summary>
		/// Short message
		/// </summary>
		SMS			= 0x1,
		/// <summary>
		/// Circuit Switched Data
		/// </summary>
		CSD			= 0x2,
		/// <summary>
		/// Packet data
		/// </summary>
		PACKET_DATA	= 0x3,
		/// <summary>
		/// USSD
		/// </summary>
		USSD		= 0x4,
		/// <summary>
		/// CDPD
		/// </summary>
		CDPD		= 0x5,
		/// <summary>
		/// DataTAC
		/// </summary>
		DATA_TAC	= 0x6,
		/// <summary>
		/// FLEX/ReFLEX
		/// </summary>
		FLEX		= 0x7,
		/// <summary>
		/// Cell Broadcast
		/// </summary>
		CELLCAST	= 0x8
	}

	/// <summary>
	/// The dest_bearer_type parameter is used to request the desired bearer for delivery of the
	/// message to the destination address.  In the case that the receiving system (e.g. SMSC) does not
	/// support the indicated bearer type, it may treat this as a failure and return a response PDU
	/// indicating a failure.
	/// </summary>
	public class dest_bearer_type : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_DEST_BEARER_TYPE;

		/// <summary>
		/// Default constructor
		/// </summary>
		public dest_bearer_type() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public dest_bearer_type(BearerType type) : base(TlvTag)
		{
			this.Value = type;
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value > BearerType.CELLCAST)
				throw new System.ArgumentOutOfRangeException("The dest_bearer_type is not valid.");
		}

		/// <summary>
		/// Override of the property value
		/// </summary>
		public new BearerType Value
		{
			get { return (BearerType) base.Value; }
			set { base.Value = (byte) value; }
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
					case BearerType.UNKNOWN:
						s = "Unknown";
						break;
					case BearerType.SMS:
						s = "SMS";
						break;
					case BearerType.CSD:
						s = "CSD";
						break;
					case BearerType.PACKET_DATA:
						s = "Packet Data";
						break;
					case BearerType.USSD:
						s = "USSD";
						break;
					case BearerType.CDPD:
						s = "CDPD";
						break;
					case BearerType.DATA_TAC:
						s = "DataTAC";
						break;
					case BearerType.FLEX:
						s = "FLEX/ReFLEX";
						break;
					case BearerType.CELLCAST:
						s = "Cell Broadcast";
						break;
					default:
						break;
				}
			}
			return string.Format("{0}{1}", base.ToString(), s);
		}
	}
}
