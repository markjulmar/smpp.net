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
    public class address_buffer : SmppCOctetString
    {
        // Constants
        private const int MAX_LENGTH = 21;

        /// <summary>
        /// Default constructor
        /// </summary>
        public address_buffer()
            : base("")
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="addr">Address</param>
        public address_buffer(string addr)
            : base(addr)
        {
        }

        /// <summary>
        /// This method validate the address range value.
        /// </summary>
        protected override void ValidateData()
        {
            if (Value != null && Value.Length > MAX_LENGTH)
                throw new System.ArgumentOutOfRangeException("The address must be between 1 and " + MAX_LENGTH.ToString() + " characters (found " + Value.Length.ToString() + " chars).");
        }

        /// <summary>
        /// Override of the object.ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("address=\"{0}\"", Value);
        }
    }
}