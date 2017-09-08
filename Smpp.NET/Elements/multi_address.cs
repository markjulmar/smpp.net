using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The multi_address element is an internal element which is used to hold either
	/// a distribution list item or an address
	/// </summary>
	public class multi_address : ISupportSmppByteStream
	{
		// Class data
		dest_flag flag_ = new dest_flag();
		object elem_ = null;

		/// <summary>
		/// Default constructor
		/// </summary>
		public multi_address()
		{
			flag_.Value = DestinationType.NONE;
		}

		/// <summary>
		/// Address constructor
		/// </summary>
		/// <param name="ton">Type Of Number</param>
		/// <param name="npi">Numeric plan</param>
		/// <param name="addr">Address</param>
		public multi_address(TypeOfNumber ton, NumericPlanIndicator npi, string addr)
		{
			flag_.Value = DestinationType.SME_ADDRESS;
			elem_ = new address(ton, npi, addr);
		}

		/// <summary>
		/// Address constructor
		/// </summary>
		/// <param name="addr">Existing address</param>
		public multi_address(address addr)
		{
			flag_.Value = DestinationType.SME_ADDRESS;
			elem_ = addr;
		}

		/// <summary>
		/// Address constructor
		/// </summary>
		/// <param name="name">Distribution List Name</param>
		public multi_address(string name)
		{
			flag_.Value = DestinationType.DL_NAME;
			elem_ = new dl_name(name);
		}

		/// <summary>
		/// Returns whether this multi address contains an SME address
		/// </summary>
		public bool IsAddress
		{
			get { return (flag_.Value == DestinationType.SME_ADDRESS); }
		}

		/// <summary>
		/// Returns whether this multi address contains a distribution list
		/// </summary>
		public bool IsDistributionList
		{
			get { return (flag_.Value == DestinationType.DL_NAME); }
		}

		/// <summary>
		/// Returns the address
		/// </summary>
		public address Address
		{
			get 
			{ 
				return (flag_.Value == DestinationType.SME_ADDRESS) ?
					(address) elem_ : null;
			}

			set 
			{ 
				flag_.Value = DestinationType.SME_ADDRESS;
				elem_ = value;
			}
		}

		/// <summary>
		/// Returns the distribution list
		/// </summary>
		public string DistibutionList
		{
			get
			{
				return (flag_.Value == DestinationType.DL_NAME) ?
					((dl_name)elem_).Value : null;
			}

			set
			{
				flag_.Value = DestinationType.DL_NAME;
				elem_ = new dl_name(value);
			}
		}

		/// <summary>
		/// This method retrieves the C-Octet string from the byte stream
		/// </summary>
		/// <param name="reader">Byte stream</param>
		public void GetFromStream(SmppReader reader)
		{
			flag_.GetFromStream(reader);
			switch (flag_.Value)
			{
				case DestinationType.DL_NAME:
					elem_ = new dl_name();
					break;
				case DestinationType.SME_ADDRESS:
					elem_ = new address();
					break;
				default:
					elem_ = null;
					break;
			}

			if (elem_ != null)
			{
				ISupportSmppByteStream isb = (ISupportSmppByteStream) elem_;
				isb.GetFromStream(reader);
			}
		}

		/// <summary>
		/// This method adds our information to the byte stream.
		/// </summary>
		/// <param name="writer"></param>
		public void AddToStream(SmppWriter writer)
		{
			if (elem_ != null)
			{
				flag_.AddToStream(writer);
				ISupportSmppByteStream isb = (ISupportSmppByteStream) elem_;
				isb.AddToStream(writer);
			}
		}

		/// <summary>
		/// Override of the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("multi_address={0},{1}", flag_, elem_);
		}
	}
}