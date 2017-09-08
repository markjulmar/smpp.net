using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The source_subaddress parameter specifies the subaddress associated with the originator 
	/// of the message.
	/// </summary>
	public class source_subaddress : TlvParameter
	{
		// Constants
		private const int MIN_LENGTH = 2;
		private const int MAX_LENGTH = 23;

		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_SOURCE_SUBADDRESS;

		/// <summary>
		/// Default constructor
		/// </summary>
		public source_subaddress() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public source_subaddress(SubAddressFormat type, params byte[] addr) : base(TlvTag)
		{
			this.AddressType = type;
			this.Address = addr;
		}

		/// <summary>
		/// Set/Get the Type of Subaddress.  This indicates the type of subaddressing
		/// information which is included and implies the type/length of the Address.
		/// </summary>
		public SubAddressFormat AddressType
		{
			get
			{
				return (HasValue) ? 
					(SubAddressFormat) Data.GetBuffer()[0] :
					SubAddressFormat.USER_SPECIFIED;
			}

			set
			{
                SmppWriter writer = new SmppWriter(Data, true);
				writer.Add((byte)value);
			}
		}

		/// <summary>
		/// This method retrieves the address.  It is encoded based on the 
		/// address type property.
		/// </summary>
		public byte[] Address
		{
			get
			{
				if (Data.Length > 1)
				{
                    SmppReader reader = new SmppReader(Data, true);
                    reader.Skip(1);
                    return reader.ReadBytes((int)Data.Length-1);
				}
				return null;
			}

			set
			{
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Skip(1);
                writer.Add(value);
			}
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			int len = (int) Data.Length;
			if (AddressType != SubAddressFormat.NSAP_EVEN && 
				AddressType != SubAddressFormat.NSAP_ODD && 
				AddressType != SubAddressFormat.USER_SPECIFIED)
				throw new System.ArgumentOutOfRangeException("The source_subaddress type is not valid.");
			else if (AddressType != SubAddressFormat.USER_SPECIFIED && len < MIN_LENGTH)
				throw new System.ArgumentOutOfRangeException("The source_subaddress length must be between " + MIN_LENGTH.ToString() + " and " + MAX_LENGTH.ToString() + " bytes.");
			else if (len > MAX_LENGTH)
				throw new System.ArgumentOutOfRangeException("The source_subaddress length must be between " + MIN_LENGTH.ToString() + " and " + MAX_LENGTH.ToString() + " bytes.");
		}
	}
}
