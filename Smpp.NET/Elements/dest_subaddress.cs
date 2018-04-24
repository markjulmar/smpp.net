using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// Subaddress formatting
	/// </summary>
	public enum SubAddressFormat : byte
	{
		/// <summary>
		/// NSAP (Even) - Binary encoding specified in ITUT X.213. 
		/// </summary>
		NSAP_EVEN		= 0x80,
		/// <summary>
		/// NSAP (Odd) - Binary encoding specified in ITUT X.213. 
		/// </summary>
		NSAP_ODD		= 0x88,
		/// <summary>
		/// User Specified - encoded according to user specifications, subject
		/// to a maximum of 22 octets.
		/// </summary>
		USER_SPECIFIED  = 0xA0
	}

	/// <summary>
	/// The dest_subaddress parameter specifies the subaddress associated with the destination
	/// of the message.
	/// </summary>
	public class dest_subaddress : TlvParameter
	{
		// Constants
		private const int MIN_LENGTH = 2;
		private const int MAX_LENGTH = 23;

		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_DEST_SUBADDRESS;

		/// <summary>
		/// Default constructor
		/// </summary>
		public dest_subaddress() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public dest_subaddress(SubAddressFormat type, params byte[] addr) : base(TlvTag)
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
				if (HasValue)
				{
                    SmppReader reader = new SmppReader(Data, true);
                    reader.Skip(1);
                    return reader.ReadBytes(Data.Length - 1);
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
			long len = Data.Length;
			if (AddressType != SubAddressFormat.NSAP_EVEN && 
				AddressType != SubAddressFormat.NSAP_ODD && 
				AddressType != SubAddressFormat.USER_SPECIFIED)
				throw new System.ArgumentOutOfRangeException("The dest_subaddress type is not valid.");
			else if (AddressType != SubAddressFormat.USER_SPECIFIED && len < MIN_LENGTH)
				throw new System.ArgumentOutOfRangeException("The dest_subaddress length must be between " + MIN_LENGTH.ToString() + " and " + MAX_LENGTH.ToString() + " bytes.");
			else if (len > MAX_LENGTH)
				throw new System.ArgumentOutOfRangeException("The dest_subaddress length must be between " + MIN_LENGTH.ToString() + " and " + MAX_LENGTH.ToString() + " bytes.");
		}
	}
}
