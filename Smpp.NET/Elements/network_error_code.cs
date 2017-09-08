using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Error types
	/// </summary>
	public enum NetworkErrorType : byte
	{
		/// <summary>
		/// ANSI 136
		/// </summary>
		ANSI_136	= 1,
		/// <summary>
		/// IS95
		/// </summary>
		IS_95		= 2,
		/// <summary>
		/// GSM
		/// </summary>
		GSM			= 3,
		/// <summary>
		/// Unknown/Reserved
		/// </summary>
		RESERVED	= 4
	}

	/// <summary>
	/// The network_error_code parameter used to indicate the actual network error code for
	/// a delivery failure.  The network error code is technology specific.
	/// </summary>
	public class network_error_code : TlvParameter
	{
		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_NETWORK_ERROR_CODE;

		private int REQUIRED_LENGTH = 3;

		/// <summary>
		/// Default constructor
		/// </summary>
		public network_error_code() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public network_error_code(NetworkErrorType type, short code) : base(TlvTag)
		{
			this.NetworkType = type;
			this.ErrorCode = code;
		}

		/// <summary>
		/// Set/Get the network type
		/// </summary>
		public NetworkErrorType NetworkType
		{
			get
			{
                return (HasValue) ?
                    (NetworkErrorType)Data.GetBuffer()[0] :
                        NetworkErrorType.RESERVED;
			}

			set
			{
                SmppWriter writer = new SmppWriter(Data, true);
				writer.Add((byte)value);
			}
		}

		/// <summary>
		/// This method retrieves the actual network error code.
		/// </summary>
		public short ErrorCode
		{
			get
			{
				if (Data.Length > 1)
				{
                    SmppReader reader = new SmppReader(Data, true);
                    reader.Skip(1);
                    return reader.ReadShort();
				}
				return 0;
			}

			set
			{
                SmppWriter writer = new SmppWriter(Data, true);
                if (!Data.EOS)
                    writer.Skip(1);
                else
                    writer.Add((byte)NetworkErrorType.RESERVED);
                writer.Add(value);
			}
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			long len = Data.Length;
			if (NetworkType != NetworkErrorType.ANSI_136 && 
                NetworkType != NetworkErrorType.IS_95 && 
                NetworkType != NetworkErrorType.GSM && 
                NetworkType != NetworkErrorType.RESERVED)
				throw new System.ArgumentOutOfRangeException("The network_error_code type is not valid.");
			else if (len != REQUIRED_LENGTH)
				throw new System.ArgumentOutOfRangeException("The network_error_code length must be " + REQUIRED_LENGTH.ToString() + " bytes.");
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
				switch (NetworkType)
				{
					case NetworkErrorType.ANSI_136:
						s = "ANSI-136";
						break;
					case NetworkErrorType.IS_95:
						s = "IS-95";
						break;
					case NetworkErrorType.GSM:
						s = "GSM";
						break;
					case NetworkErrorType.RESERVED:
						s = "Reserved";
						break;
					default:
						break;
				}
			}
			return string.Format("{0}{1} 0x{2:X4}", base.ToString(), s, ErrorCode);
		}
	}
}
