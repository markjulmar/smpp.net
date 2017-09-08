using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The broadcast_rep_num parameter indicates the number of repeated
    /// broadcasts requested by the submitter.
    /// </summary>
    public class broadcast_rep_num : TlvShort
    {
        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_BROADCAST_REP_NUM;

        /// <summary>
        /// Default constructor
        /// </summary>
        public broadcast_rep_num() : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public broadcast_rep_num(short value) : base(TlvTag)
        {
            Value = value;
        }
    }
}
