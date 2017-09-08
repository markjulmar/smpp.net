using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The address element identifies the SME which originated the message.  An ESME which is
    /// implemented as a single SME address may set this field to NULL to allow the SMSC to default the
    /// source address of the submitted message.
    /// An IP address is specified in "aaa.bbb.ccc.ddd" notation; IPV6 is not supported.
    /// </summary>
    public class address : ISupportSmppByteStream
    {
        // Class data
        private ton ton_ = new ton();
        private npi npi_ = new npi();
        private address_buffer addr_ = new address_buffer();

        /// <summary>
        /// Default constructor
        /// </summary>
        public address()
        {
        }

        /// <summary>
        /// Address constructor
        /// </summary>
        /// <param name="ton">Type Of Number</param>
        /// <param name="npi">Numeric plan</param>
        /// <param name="addr">Address</param>
        public address(TypeOfNumber ton, NumericPlanIndicator npi, string addr)
        {
            ton_.Value = ton;
            npi_.Value = npi;
            addr_.Value = addr;
        }

        /// <summary>
        /// Returns the type of number
        /// </summary>
        public TypeOfNumber TypeOfNumber
        {
            get { return ton_.Value; }
            set { ton_.Value = value; }
        }

        /// <summary>
        /// Returns the numeric plan indicator
        /// </summary>
        public NumericPlanIndicator NumericPlanIndicator
        {
            get { return npi_.Value; }
            set { npi_.Value = value; }
        }

        /// <summary>
        /// Textual address
        /// </summary>
        public string Address
        {
            get { return addr_.Value; }
            set { addr_.Value = value; }
        }

        /// <summary>
        /// This method retrieves the C-Octet string from the byte stream
        /// </summary>
        /// <param name="reader">Byte stream</param>
        public void GetFromStream(SmppReader reader)
        {
            ton_.GetFromStream(reader);
            npi_.GetFromStream(reader);
            addr_.GetFromStream(reader);
        }

        /// <summary>
        /// This method adds our information to the byte stream.
        /// </summary>
        /// <param name="writer"></param>
        public void AddToStream(SmppWriter writer)
        {
            ton_.AddToStream(writer);
            npi_.AddToStream(writer);
            addr_.AddToStream(writer);
        }

        /// <summary>
        /// Override of the object.ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("address={0},{1},\"{2}\"", ton_, npi_, addr_);
        }
    }
}