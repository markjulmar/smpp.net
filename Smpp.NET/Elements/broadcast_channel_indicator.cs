using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// This defines the types of Cell Broadcast channels which may
    /// be used to broadcast a message
    /// </summary>
    public enum BroadcastChannel
    {
        /// <summary>
        /// Basic broadcast channel (default)
        /// </summary>
        BASIC = 0x0,
        /// <summary>
        /// Extended broadcast channel
        /// </summary>
        EXTENDED = 0x1
    }

    /// <summary>
    /// The broadcast_channel_indicator parameter specifies the Cell
    /// Broadcast channel that should be used for broadcasting
    /// the message.
    /// </summary>
    public class broadcast_channel_indicator : TlvByte
    {
        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_BROADCAST_CHANNEL_INDICATOR;

        /// <summary>
        /// Default constructor
        /// </summary>
        public broadcast_channel_indicator()
            : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="channel">The specified channel to use</param>
        public broadcast_channel_indicator(BroadcastChannel channel)
            : base(TlvTag)
        {
            Value = (byte)channel;
        }

        /// <summary>
        /// Returns the specific channel
        /// </summary>
        public BroadcastChannel Channel
        {
            get { return (BroadcastChannel)Value; }
            set { Value = (byte)value; }
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Value != 0 && Value != 1)
                throw new System.ArgumentOutOfRangeException("The broadcast_channel_indicator must be either Basic or Extended.");
        }
    }
}
