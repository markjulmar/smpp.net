using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The dest_addr_np_information parameter is used to carry E.164
    /// information related to number portability
    /// </summary>
    public class dest_addr_np_information : TlvCOctetString
    {
        // Constants
        private const int MAX_LENGTH = 10;

        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_DEST_ADDR_NP_INFORMATION;

        /// <summary>
        /// Default constructor
        /// </summary>
        public dest_addr_np_information() : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="npi">Number portability info (NXXX-XXXX)</param>
        public dest_addr_np_information(string npi) : base(TlvTag)
        {
            this.Value = npi;
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Value.Length > MAX_LENGTH)
                throw new System.ArgumentOutOfRangeException("The dest_addr_np_information cannot exceed " + MAX_LENGTH.ToString() + " characters.");
        }
    }
}
