using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The short_message parameter contains the user data.  A maximum of 254 octets can be
	/// sent.  ESME's should use the optional message_payload parameter in submit_sm, submit_multi,
	/// or deliver_sm to send larger data sizes.
	/// 
	/// NOTE: We deviate from the element definition slightly in that we include the "sm_length"
	/// element as part of this element.  They always go together in this instance.
	/// </summary>
	public class short_message : SmppOctetString
	{
		/// <summary>
		/// The max length of a short-message data component.
		/// </summary>
		public const int MAX_LENGTH = 254;

		/// <summary>
		/// Default constructor
		/// </summary>
		public short_message() : base()
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="s">Value</param>
		public short_message(string s) : base(s)
		{
		}

		/// <summary>
		/// Returns the length of the data
		/// </summary>
		public int Length
		{
			get { return Value.Length; }
		}

		/// <summary>
		/// Returns the base message value
		/// </summary>
		public string TextValue
		{
			get { return base.Value; }
			set { base.Value = value; }
		}

		/// <summary>
		/// Returns the message as an array of bytes
		/// </summary>
		public byte[] BinaryValue
		{
			get
			{
				return Encoding.ASCII.GetBytes(base.Value);
			}

			set
			{
				Value = Encoding.ASCII.GetString(value);
			}
		}

		/// <summary>
		/// This method validate the data in the element.
		/// </summary>
		protected override void ValidateData()
		{
			if (Value.Length > MAX_LENGTH)
				throw new ArgumentException("short_message too long - it must be <= " + MAX_LENGTH.ToString());
		}

		/// <summary>
		/// Override of the object.ToString method
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("short_message: len={0}, \"{1}\"", Value.Length, Value);
		}
	}
}
