using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The source_bearer_type parameter is used to indicate the bearer type over which the 
	/// message originated.
	/// </summary>
	public class source_bearer_type : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_SOURCE_BEARER_TYPE;

		/// <summary>
		/// Default constructor
		/// </summary>
		public source_bearer_type() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public source_bearer_type(BearerType type) : base(TlvTag)
		{
			this.Value = type;
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value > BearerType.CELLCAST)
				throw new System.ArgumentOutOfRangeException("The source_bearer_type is not valid.");
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
