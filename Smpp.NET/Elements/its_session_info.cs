using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Summary description for its_session_info.
	/// </summary>
	public class its_session_info : TlvParameter
	{
		// Constants
		private const int REQUIRED_LENGTH = 2;

		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const int TlvTag = ParameterTags.TAG_ITS_SESSION_INFO;

		/// <summary>
		/// Constructor
		/// </summary>
		public its_session_info() : base(TlvTag)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="snum">Session number</param>
		public its_session_info(byte snum) : this(snum,false)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="snum">Session number</param>
		/// <param name="eos">End of session indicator</param>
		public its_session_info(byte snum, bool eos) : base(TlvTag)
		{
            SmppWriter writer = new SmppWriter(Data, true);
            writer.Add(snum);
            writer.Add((eos == true) ? (byte)1 : (byte)0);
        }

		/// <summary>
		/// This returns the session number
		/// </summary>
		public byte SessionNumber
		{
			get
			{
                return (HasValue) ? Data.GetBuffer()[0] : (byte)0;
			}

			set 
			{
                new SmppWriter(Data, true).Add(value);
            }
		}

		/// <summary>
		/// Returns whether this is the end of the session
		/// </summary>
		public bool IsEndOfSession
		{
			get
			{
				if (HasValue && Data.Length > 1)
					return (Data.GetBuffer()[1] & 0x1) == 1 ? true : false;
				return false;
			}

			set 
			{
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Skip(1);
                writer.Add((value == true) ? (byte)1 : (byte)0);
            }
		}

		/// <summary>
		/// Override of the object.ToString method
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(base.ToString());
			if (HasValue)
				sb.AppendFormat("snum={0},eos={1}", this.SessionNumber, this.IsEndOfSession);
			return sb.ToString();
		}
	}
}
