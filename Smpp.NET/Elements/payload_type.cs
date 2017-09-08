using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Payload types
	/// </summary>
	public enum PayloadTypes : byte
	{
		/// <summary>
		/// Default (WAP WDP message)
		/// </summary>
		DEFAULT		= 0x0,
		/// <summary>
		/// Wireless Data Protocol formatted data.
		/// </summary>
		WDP_MSG		= 0x0,
		/// <summary>
		/// Wireless Control Message Protocol formatted data.
		/// </summary>
		WCMP_MSG	= 0x1
	}

	/// <summary>
	/// The payload_type parameter defines the higher layer PDU type contained in the message
	/// payload.
	/// </summary>
	public class payload_type : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_PAYLOAD_TYPE;

		/// <summary>
		/// Default constructor
		/// </summary>
		public payload_type() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public payload_type(PayloadTypes type) : base(TlvTag)
		{
			this.Value = type;
		}

		/// <summary>
		/// Override of property value
		/// </summary>
		public new PayloadTypes Value
		{
			get { return (PayloadTypes) base.Value; }
			set { base.Value = (byte) value; }
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value > PayloadTypes.WCMP_MSG)
				throw new System.ArgumentOutOfRangeException("The payload_type is not valid.");
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
					case PayloadTypes.DEFAULT:
						s = "Default";
						break;
					case PayloadTypes.WCMP_MSG:
						s = "WCMP Message";
						break;
					default:
						break;
				}
			}
			return string.Format("{0}{1}", base.ToString(), s);
		}
	}
}
