using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The source_network_id parameter is assigned to a wireless
    /// network operator or ESME operator and is a unique address
    /// which may be derived and assigned by the node owner without
    /// establishing a central assignment and management authority.
    /// When this Tlv is specified, it must be accompanied by a 
    /// source_node_id Tlv.
    /// </summary>
    public class source_network_id : TlvCOctetString
    {
        // Constants
        private const int MIN_LENGTH = 7;
        private const int MAX_LENGTH = 65;

        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_SOURCE_NETWORK_ID;

        /// <summary>
        /// Default constructor
        /// </summary>
        public source_network_id() : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public source_network_id(string text) : base(TlvTag)
        {
            this.Value = text;
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Value.Length < MIN_LENGTH || Value.Length > MAX_LENGTH)
                throw new System.ArgumentOutOfRangeException("The source_network_id must be between " + MIN_LENGTH.ToString() + " and " + MAX_LENGTH.ToString() + " characters.");
        }
    }
}
