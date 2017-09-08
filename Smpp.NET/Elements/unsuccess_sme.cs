using System;
using System.Text;
using System.Collections;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// This object contains a multi_address and a status code indicating whether delivery
	/// of an SMS message to the given address was successful or not.
	/// </summary>
	public class unsuccess_sme : ISupportSmppByteStream
	{
		// Class data
		private multi_address addr_ = new multi_address();
		int status_ = StatusCodes.ESME_ROK;

		/// <summary>
		/// Default constructor
		/// </summary>
		public unsuccess_sme()
		{
		}

		/// <summary>
		/// Address constructor
		/// </summary>
		/// <param name="status">Status code</param>
		/// <param name="ton">Type Of Number</param>
		/// <param name="npi">Numeric plan</param>
		/// <param name="addr">Address</param>
		public unsuccess_sme(int status, TypeOfNumber ton, NumericPlanIndicator npi, string addr) :
				this(status, new address(ton, npi, addr))
		{
		}

		/// <summary>
		/// Address constructor
		/// </summary>
		/// <param name="status">Status code</param>
		/// <param name="name">Distribution List Name</param>
		public unsuccess_sme(int status, string name)
		{
			status_ = status;
			addr_ = new multi_address(name);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="status">Status code</param>
		/// <param name="addr">Address</param>
		public unsuccess_sme(int status, address addr)
		{
			status_ = status;
			addr_ = new multi_address(addr);
		}

		/// <summary>
		/// Returns the status code associated with the SME address/DL
		/// </summary>
		public int Status
		{
			get { return status_; }
			set { status_ = value; }
		}

		/// <summary>
		/// Returns whether this multi address contains an SME address
		/// </summary>
		public bool IsAddress
		{
			get { return addr_.IsAddress; }
		}

		/// <summary>
		/// Returns whether this multi address contains a distribution list
		/// </summary>
		public bool IsDistributionList
		{
			get { return addr_.IsDistributionList; }
		}

		/// <summary>
		/// Returns the address
		/// </summary>
		public address Address
		{
			get { return addr_.Address; }
			set { addr_.Address = value; }
		}

		/// <summary>
		/// Returns the distribution list
		/// </summary>
		public string DistibutionList
		{
			get { return addr_.DistibutionList; }
			set { addr_.DistibutionList = value; }
		}

		/// <summary>
		/// This method retrieves the C-Octet string from the byte stream
		/// </summary>
		/// <param name="reader">Byte stream</param>
		public void GetFromStream(SmppReader reader)
		{
			addr_.GetFromStream(reader);
			status_ = reader.ReadInt32();
		}

		/// <summary>
		/// This method adds our information to the byte stream.
		/// </summary>
		/// <param name="writer"></param>
		public void AddToStream(SmppWriter writer)
		{
			addr_.AddToStream(writer);
			writer.Add(status_);
		}

		/// <summary>
		/// Override of the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("unsuccess_sme={0},rc=0x{1:X}", addr_, status_);
		}
	}

	/// <summary>
	/// The unsuccess_sme_collection element holds addresses in a counted array.  A concrete example of this 
	/// parameter is the "number_of_dests" element which indicates the number of dest_address structures 
	/// that are to follow in the submit_multi operation.
	/// </summary>
	public class unsuccess_sme_collection : CollectionBase
	{
		// Constants
		private const int MAX_COUNT = 255;

		/// <summary>
		/// ICollection.IsSynchronized
		/// </summary>
		public virtual bool IsSynchronized 
		{
			get { return false; } 
		}

		/// <summary>
		/// ICollection.SyncRoot
		/// </summary>
		public virtual unsuccess_sme_collection SyncRoot 
		{ 
			get { return this; } 
		}

		/// <summary>
		/// ICollection.CopyTo
		/// </summary>
		public virtual void CopyTo (unsuccess_sme[] addr, int index)
		{
			InnerList.CopyTo(addr, index);
		}

		/// <summary>
		/// IList.IsFixedSize property
		/// </summary>
		public virtual bool IsFixedSize
		{
			get { return InnerList.IsFixedSize; }
		}

		/// <summary>
		/// IList.IsReadOnly property
		/// </summary>
		public virtual bool IsReadOnly
		{
			get { return InnerList.IsReadOnly; }
		}

		/// <summary>
		/// IList.Item property
		/// </summary>
		public virtual unsuccess_sme this[int index]
		{
			get { return (unsuccess_sme)(InnerList[index]); }
			set { InnerList[index] = value; }
		}

		/// <summary>
		/// IList.Add
		/// </summary>
		/// <param name="addr"></param>
		/// <returns></returns>
		public virtual int Add (unsuccess_sme addr)
		{
			if (this.Count > MAX_COUNT)
				throw new ArgumentException("Too many unsuccess_sme objects in collection, max count allowed is " + MAX_COUNT.ToString());
			return InnerList.Add(addr);
		}

		/// <summary>
		/// IList.Contains
		/// </summary>
		/// <param name="addr"></param>
		/// <returns></returns>
		public virtual bool Contains (unsuccess_sme addr)
		{
			return InnerList.Contains (addr);
		}

		/// <summary>
		/// IList.IndexOf
		/// </summary>
		/// <param name="addr"></param>
		/// <returns></returns>
		public virtual int IndexOf (unsuccess_sme addr)
		{
			return InnerList.IndexOf (addr);
		}

		/// <summary>
		/// IList.Insert
		/// </summary>
		/// <param name="index"></param>
		/// <param name="addr"></param>
		public virtual void Insert (int index, unsuccess_sme addr)
		{
			if (this.Count > MAX_COUNT)
				throw new ArgumentException("Too many address objects in collection, max count allowed is " + MAX_COUNT.ToString());
			InnerList.Insert (index, addr);
		}

		/// <summary>
		/// IList.Remove
		/// </summary>
		/// <param name="addr"></param>
		public virtual void Remove (unsuccess_sme addr)
		{
			InnerList.Remove (addr);
		}

		/// <summary>
		/// This method retrieves the address array from the byte stream
		/// </summary>
		/// <param name="reader">Byte stream</param>
		public void GetFromStream(SmppReader reader)
		{
			int count = (int) reader.ReadByte();
			for (int i = 0; i < count; ++i)
			{
				unsuccess_sme addr = new unsuccess_sme();
				addr.GetFromStream(reader);
				Add(addr);
			}
			if (this.Count > MAX_COUNT)
				throw new ArgumentException("Too many address objects in collection, max count allowed is " + MAX_COUNT.ToString());
		}

		/// <summary>
		/// This method adds our information to the byte stream.
		/// </summary>
		/// <param name="writer"></param>
		public void AddToStream(SmppWriter writer)
		{
			writer.Add((byte)this.Count);
			for (int i = 0; i < this.Count; ++i)
			{
				unsuccess_sme addr = this[i];
				addr.AddToStream(writer);
			}
		}

		/// <summary>
		/// Overrides the debug ToString method
		/// </summary>
		/// <returns>String</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("Count={0}", this.Count);
			for (int i = 0; i < this.Count; ++i)
				sb.AppendFormat(",{0}:{1}", i, this[i]);
			return sb.ToString();
		}
	}
}
