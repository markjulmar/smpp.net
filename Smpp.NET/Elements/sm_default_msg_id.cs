using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The sm_default_msg_id parameter is used to specify the SMSC index of a pre-defined ("canned")
	/// message.
	/// </summary>
	public class sm_default_msg_id : SmppByte
	{
		/// <summary>
		/// Minimum index for a canned message.
		/// </summary>
		public const byte MIN_VALUE = 1;
		/// <summary>
		/// Maximum index for a message.
		/// </summary>
		public const byte MAX_VALUE = 254;

		/// <summary>
		/// Default constructor
		/// </summary>
		public sm_default_msg_id() : base(MIN_VALUE)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public sm_default_msg_id(byte id) : base(id)
		{
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value != 0 && Value < MIN_VALUE || Value > MAX_VALUE)
				throw new System.ArgumentOutOfRangeException("The sm_default_msg_id is not valid.");
		}

		/// <summary>
		/// Override object.ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("sm_default_msg_id: {0:X}", Value);
		}

	}
}
