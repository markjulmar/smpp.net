using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The number_of_messages parameter is used to indicate the number of messages stored in a mailbox.
	/// </summary>
	public class number_of_messages : TlvByte
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_NUMBER_OF_MESSAGES;

		/// <summary>
		/// Default constructor
		/// </summary>
		public number_of_messages() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public number_of_messages(byte count) : base(TlvTag)
		{
			this.Value = count;
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if (Value > 99)
				throw new System.ArgumentOutOfRangeException("The number_of_messages is not valid.");
		}
	}
}
