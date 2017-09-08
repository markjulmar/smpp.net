using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The additional_status_info_text parameter gives the ASCII textual description of the 
    /// meaning of a response PDU.  It is to be used by an implementation to allow easy diagnosis
    /// of problems.
    /// </summary>
    public class additional_status_info_text : TlvCOctetString
    {
        // Constants
        private const int MAX_LENGTH = 65;

        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_ADD_STATUS_INFO_TEXT;

        /// <summary>
        /// Default constructor
        /// </summary>
        public additional_status_info_text()
            : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public additional_status_info_text(string text)
            : base(TlvTag)
        {
            this.Value = text;
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Value.Length > MAX_LENGTH)
                throw new System.ArgumentOutOfRangeException("The additional_status_info_text cannot exceed " + MAX_LENGTH.ToString() + " characters.");
        }
    }
}
