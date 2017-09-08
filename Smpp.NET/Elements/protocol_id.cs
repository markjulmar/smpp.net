using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The protocol_id parameter is used to indicate the type of protocol used.
	/// This field is not used in TDMA or CDMA networks and should be defaulted to NULL.
	/// </summary>
	public class protocol_id : SmppByte
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public protocol_id() : base(0)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public protocol_id(byte ver) : base(ver)
		{
		}

		/// <summary>
		/// Override of the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("protocol_id={0:X}", Value);
		}
	}
}
