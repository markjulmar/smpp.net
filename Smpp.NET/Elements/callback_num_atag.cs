using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The callback_num_atag parameter associates an alphanumeric display with the callback number.
	/// </summary>
	public class callback_num_atag : TlvParameter
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_CALLBACK_NUM_ATAG;

		/// <summary>
		/// Default constructor
		/// </summary>
		public callback_num_atag() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public callback_num_atag(DataEncoding dcod, string display) : base(TlvTag)
		{
            SmppWriter writer = new SmppWriter(Data, true);
            writer.Add((byte)dcod);
			writer.Add(display, false);
		}

		/// <summary>
		/// Returns the data coding type
		/// </summary>
		public DataEncoding DataCoding
		{
			get
			{
				if (HasValue)
					return (DataEncoding) Data.GetBuffer()[0];
				return DataEncoding.SMSC_DEFAULT;
			}
			
			set 
			{
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Add((byte)value);
			}
		}

		/// <summary>
		/// Returns the type of number
		/// </summary>
		public string Display
		{
			get
			{
                if (HasValue && Data.Length > 1)
				{
                    SmppReader reader = new SmppReader(Data, true);
                    reader.Skip(1);
                    return reader.ReadString();
				}
				return "";
			}

			set
			{
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Skip(1);
				writer.Add(value, false);
			}
		}

		/// <summary>
		/// Override for object.ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(base.ToString());
			sb.AppendFormat("data_coding={0},",this.DataCoding);
			sb.AppendFormat("Display=\"{0}\"", this.Display);
			return sb.ToString();
		}
	}
}
