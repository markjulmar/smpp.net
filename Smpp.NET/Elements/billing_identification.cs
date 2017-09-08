using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The billing_identification parameter represents the Billing
    /// Information passed from the ESME to the MC during message
    /// submission.  The format of the data is vendor-specific.
    /// </summary>
    public class billing_identification : TlvCOctetString
    {
        // Constants
        private const int MAX_LENGTH = 1024;

        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_BILLING_IDENTIFICATION;

        /// <summary>
        /// Default constructor
        /// </summary>
        public billing_identification()
            : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public billing_identification(string text)
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
                throw new System.ArgumentOutOfRangeException("The billing_identification cannot exceed " + MAX_LENGTH.ToString() + " characters.");
        }
    }
}
