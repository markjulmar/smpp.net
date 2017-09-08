using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The broadcast_content_type_info parameter contains additional
    /// information specific to the broadcast_content_type.
    /// </summary>
    public class broadcast_content_type_info : TlvCOctetString
    {
        // Constants
        private const int MAX_LENGTH = 255;

        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_BROADCAST_CONTENT_TYPE_INFO;

        /// <summary>
        /// Default constructor
        /// </summary>
        public broadcast_content_type_info() : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public broadcast_content_type_info(string text) : base(TlvTag)
        {
            this.Value = text;
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Value.Length > MAX_LENGTH)
                throw new System.ArgumentOutOfRangeException("The broadcast_content_type_info cannot exceed " + MAX_LENGTH.ToString() + " characters.");
        }
    }
}
