using System;
using System.Text;
using System.Collections;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The address_collection element holds addresses in a counted array.  A concrete example of this 
    /// parameter is the "number_of_dests" element which indicates the number of dest_address structures 
    /// that are to follow in the submit_multi operation.
    /// </summary>
    public class address_collection : CollectionBase
    {
        // Constants
        private const int MAX_COUNT = 254;

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
        public virtual address_collection SyncRoot
        {
            get { return this; }
        }

        /// <summary>
        /// ICollection.CopyTo
        /// </summary>
        public virtual void CopyTo(multi_address[] addr, int index)
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
        public virtual multi_address this[int index]
        {
            get { return (multi_address)(InnerList[index]); }
            set { InnerList[index] = value; }
        }

        /// <summary>
        /// IList.Add
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public virtual int Add(multi_address addr)
        {
            if (this.Count > MAX_COUNT)
                throw new ArgumentException("Too many address objects in collection, max count allowed is " + MAX_COUNT.ToString());
            return InnerList.Add(addr);
        }

        /// <summary>
        /// IList.Contains
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public virtual bool Contains(multi_address addr)
        {
            return InnerList.Contains(addr);
        }

        /// <summary>
        /// IList.IndexOf
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public virtual int IndexOf(multi_address addr)
        {
            return InnerList.IndexOf(addr);
        }

        /// <summary>
        /// IList.Insert
        /// </summary>
        /// <param name="index"></param>
        /// <param name="addr"></param>
        public virtual void Insert(int index, multi_address addr)
        {
            if (this.Count > MAX_COUNT)
                throw new ArgumentException("Too many address objects in collection, max count allowed is " + MAX_COUNT.ToString());
            InnerList.Insert(index, addr);
        }

        /// <summary>
        /// IList.Remove
        /// </summary>
        /// <param name="addr"></param>
        public virtual void Remove(multi_address addr)
        {
            InnerList.Remove(addr);
        }

        /// <summary>
        /// This method retrieves the address array from the byte stream
        /// </summary>
        /// <param name="reader">Byte stream</param>
        public void GetFromStream(SmppReader reader)
        {
            int count = (int)reader.ReadByte();
            for (int i = 0; i < count; ++i)
            {
                multi_address addr = new multi_address();
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
                multi_address addr = this[i];
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
