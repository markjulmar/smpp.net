using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// This defines the specific allowed broadcast message classes
    /// </summary>
    public enum BroadcastMessageClass
    {
        /// <summary>
        /// None
        /// </summary>
        NONE = 0x0,
        /// <summary>
        /// Class 1
        /// </summary>
        CLASS1 = 0x1,
        /// <summary>
        ///  Class 2
        /// </summary>
        CLASS2 = 0x2,
        /// <summary>
        ///  Class 3 - Terminal equipment
        /// </summary>
        TERMINAL_EQUIPMENT = 0x3
    }

    /// <summary>
    /// The broadcast_message_class parameter is used to route messages when
    /// received by a mobile station to user defined destinations or
    /// Terminal Equipment.
    /// </summary>
    public class broadcast_message_class : TlvByte
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public broadcast_message_class() : base(ParameterTags.TAG_BROADCAST_MESSAGE_CLASS)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public broadcast_message_class(BroadcastMessageClass msgClass) : base(ParameterTags.TAG_BROADCAST_MESSAGE_CLASS)
        {
            Value = (byte) msgClass;
        }

        /// <summary>
        /// This returns the typed message class
        /// </summary>
        public BroadcastMessageClass Class
        {
            get { return (BroadcastMessageClass) Value; }
            set { Value = (byte)value; }
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Value > (byte) BroadcastMessageClass.TERMINAL_EQUIPMENT)
                throw new System.ArgumentOutOfRangeException("The broadcast_message_class is invalid.");
        }
    }
}
