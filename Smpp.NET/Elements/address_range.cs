using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The address_range element is used to address a specific address of a known type.
    /// </summary>
    public class address_range : ISupportSmppByteStream
    {
        // Constants
        private const int MAX_LENGTH = 41;
        private ton ton_ = new ton();
        private npi npi_ = new npi();
        private SmppCOctetString addr_ = new SmppCOctetString("");

        /// <summary>
        /// Default constructor
        /// </summary>
        public address_range()
        {
        }

        /// <summary>
        /// Address constructor
        /// </summary>
        /// <param name="ton">Type Of Number</param>
        /// <param name="npi">Numeric plan</param>
        /// <param name="addr">Address</param>
        public address_range(TypeOfNumber ton, NumericPlanIndicator npi, string addr)
        {
            ton_.Value = ton;
            npi_.Value = npi;
            addr_.Value = addr;
            ValidateData();
        }

        /// <summary>
        /// This method validate the address range value.
        /// </summary>
        protected void ValidateData()
        {
            if (addr_.Value != null && addr_.Value.Length > MAX_LENGTH)
                throw new System.ArgumentOutOfRangeException("The address_range must be between 1 and " + MAX_LENGTH.ToString() + " characters (found " + addr_.Value.Length.ToString() + " chars).");
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
            set { addr_.Value = value; ValidateData(); }
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
            ValidateData();
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
            return string.Format("address_range={0},{1},\"{2}\"", ton_, npi_, addr_);
        }
    }
}