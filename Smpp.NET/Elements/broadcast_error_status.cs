using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The broadcast_error_status parameter specifies the nature of the failure
    /// associated with a particular broadcast_area_identifier specified
    /// in a broadcast request.
    /// </summary>
    public class broadcast_error_status : TlvInt
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public broadcast_error_status() : base(ParameterTags.TAG_BROADCAST_ERROR_STATUS)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public broadcast_error_status(int error) : base(ParameterTags.TAG_BROADCAST_ERROR_STATUS)
        {
            Value = error;
        }
    }
}
