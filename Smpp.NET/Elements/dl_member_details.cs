#region Using directives

using System;
using System.Text;
using JulMar.Smpp.Utility;

#endregion

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// This class describes the record passed with the mod_dl PDU when the modify
    /// option is set to "add". This is part of the SMPPP 1.1 specification [5.2.2.1]
    /// </summary>
    public class dl_member_details : ISupportSmppByteStream
    {
        // Class data
        private address address_ = new address();
        private SmppCOctetString description_ = new SmppCOctetString();
        private SmppByte type_ = new SmppByte(0);

        // Max size value
        private const int MAX_MEMBER_DESCRIPTION = 21;

        /// <summary>
        /// Default constructor
        /// </summary>
        public dl_member_details()
        {
        }

        /// <summary>
        /// Constructor for the member_details structure
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="description"></param>
        public dl_member_details(address addr, string description)
        {
            this.address_ = addr;
            this.description_.Value = description;
        }

        /// <summary>
        /// The source address of the distribution list member
        /// </summary>
        public address Address
        {
            get { return address_; }
            set { address_ = value; }
        }

        /// <summary>
        /// Description of the member for the distribution list.
        /// </summary>
        public string Description
        {
            get { return description_.Value; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Description");
                if (value.Length == 0 || value.Length > MAX_MEMBER_DESCRIPTION)
                    throw new ArgumentOutOfRangeException("Description length must be between 1 and " + MAX_MEMBER_DESCRIPTION.ToString() + " characters.");
                description_.Value = value;
            }
        }

        /// <summary>
        /// This method retrieves the provisioning record from the byte stream
        /// </summary>
        /// <param name="reader">Byte stream</param>
        public void GetFromStream(SmppReader reader)
        {
            address_.GetFromStream(reader);
            description_.GetFromStream(reader);
        }

        /// <summary>
        /// This method adds our information to the byte stream.
        /// </summary>
        /// <param name="writer">StreamWriter</param>
        public void AddToStream(SmppWriter writer)
        {
            writer.Add(address_);
            writer.Add(description_);
        }

        /// <summary>
        /// Override of the object.ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("dl_member_details={0},desc={1}", 
                address_.ToString(), description_.ToString());
        }
    }
}
