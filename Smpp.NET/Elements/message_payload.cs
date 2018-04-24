using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The message_payload parameter contains the user data.
	/// </summary>
	public class message_payload : TlvParameter
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_MESSAGE_PAYLOAD;

		/// <summary>
		/// Constructor
		/// </summary>
		public message_payload() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public message_payload(params byte[] data) : base(TlvTag)
		{
			BinaryValue = data;
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public message_payload(string data) : base(TlvTag)
		{
			TextValue = data;
		}

		/// <summary>
		/// Returns the length of the data
		/// </summary>
		public new int Length
		{
			get { return (int) Data.Length; }
		}

		/// <summary>
		/// This attempts to return the data in string form.
		/// </summary>
		public string TextValue
		{
			get
			{
                SmppReader reader = new SmppReader(Data, true);
                return reader.ReadString();
			}

			set
			{
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Add(value, false);
			}
		}

		/// <summary>
		/// This returns the buffer as a binary block.
		/// </summary>
		public byte[] BinaryValue
		{
			get
			{
                SmppReader reader = new SmppReader(Data, true);
                return reader.ReadBytes(Data.Length);
            }

			set
			{
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Add(value);
            }
		}
	}
}
