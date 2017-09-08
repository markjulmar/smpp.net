using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// This determines the number portability indicator
    /// </summary>
    public enum NumberPortabilityType
    {
        /// <summary>
        /// The NPI query has not been performed
        /// </summary>
        QUERY_NOT_PERFORMED = 0x0,
        /// <summary>
        /// The NPI query has been performed and the number has not
        /// been ported.
        /// </summary>
        NUMBER_NOT_PORTED = 0x1,
        /// <summary>
        /// The NPI query has been performed and the number has been
        /// ported.
        /// </summary>
        NUMBER_PORTED = 0x2
    }

    /// <summary>
    /// The dest_addr_np_resolution parameter is used to pass an indicator
    /// relating to a number portability query.  If this Tlv is ommitted,
    /// the default value is assumed.
    /// </summary>
    public class dest_addr_np_resolution : TlvByte
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public dest_addr_np_resolution() : base(ParameterTags.TAG_DEST_ADDR_NP_RESOLUTION)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="type">Number portability type</param>
        public dest_addr_np_resolution(NumberPortabilityType type) : base(ParameterTags.TAG_DEST_ADDR_NP_RESOLUTION)
        {
            Value = (byte)type;
        }

        /// <summary>
        /// This returns the number portability information.
        /// </summary>
        public NumberPortabilityType Type
        {
            get { return (NumberPortabilityType)Value; }
            set { Value = (byte)value; }
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Value > (byte)NumberPortabilityType.NUMBER_PORTED)
                throw new System.ArgumentOutOfRangeException("The dest_addr_np_resolution is invalid.");
        }
    }
}
