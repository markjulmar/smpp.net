using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The congestion_state parameter is a success rate indicator,
    /// defined as the ratio of the number of BTSs who accepted the message
    /// and the total number of BTSs who should accept the message, for a particular
    /// broadcast_area_identifier.
    /// </summary>
    public class congestion_state : TlvByte
    {
        // Constants
        private const int MAX_VALUE = 100;

        /// <summary>
        /// Default constructor
        /// </summary>
        public congestion_state() : base(ParameterTags.TAG_CONGESTION_STATE)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="congPercentage">Congestion percentage value</param>
        public congestion_state(byte congPercentage) : base(ParameterTags.TAG_CONGESTION_STATE)
        {
            Value = congPercentage;
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Value > MAX_VALUE)
                throw new System.ArgumentOutOfRangeException("The congestion_state cannot exceed " + MAX_VALUE.ToString() + "%.");
        }
    }
}
