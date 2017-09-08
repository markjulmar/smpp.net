using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The broadcast_area_success parameter is a success rate indicator,
    /// defined as the ratio of the number of BTSs who accepted the message
    /// and the total number of BTSs who should accept the message, for a particular
    /// broadcast_area_identifier.
    /// </summary>
    public class broadcast_area_success : TlvByte
    {
        // Constants
        private const int MAX_VALUE = 100;

        /// <summary>
        /// This indicates that the broadcast area is not available
        /// </summary>
        public const byte INFORMATION_NOT_AVAILABLE = 255;

        /// <summary>
        /// Default constructor
        /// </summary>
        public broadcast_area_success()
            : base(ParameterTags.TAG_BROADCAST_AREA_SUCCESS)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public broadcast_area_success(byte totalBTS)
            : base(ParameterTags.TAG_BROADCAST_AREA_SUCCESS)
        {
            Value = totalBTS;
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Value > MAX_VALUE && Value != INFORMATION_NOT_AVAILABLE)
                throw new System.ArgumentOutOfRangeException("The broadcast_area_success cannot exceed " + MAX_VALUE.ToString() + ".");
        }
    }
}
