using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The broadcast_service_group parameter is used to specify
    /// special target groups for broadcast information
    /// </summary>
    public class broadcast_service_group : TlvCOctetString
    {
        // Constants
        private const int MAX_LENGTH = 255;

        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_BROADCAST_SERVICE_GROUP;

        /// <summary>
        /// Default constructor
        /// </summary>
        public broadcast_service_group() : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public broadcast_service_group(string text) : base(TlvTag)
        {
            this.Value = text;
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Value.Length > MAX_LENGTH)
                throw new System.ArgumentOutOfRangeException("The broadcast_service_group cannot exceed " + MAX_LENGTH.ToString() + " characters.");
        }
    }
}
