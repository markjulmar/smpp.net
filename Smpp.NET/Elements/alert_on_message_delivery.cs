using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The alert_on_message_delivery parameter is set to instruct an MS to alert the user (in an 
    /// MS implementation specific manner) when the short message arrives at the MS.
    /// </summary>
    public class alert_on_message_delivery : TlvParameter
    {
        // Class data
        private bool hasValue_ = false;

        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_ALERT_ON_MSG_DELIVERY;

        /// <summary>
        /// Default constructor
        /// </summary>
        public alert_on_message_delivery()
            : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public alert_on_message_delivery(bool b)
            : base(TlvTag)
        {
            hasValue_ = b;
        }

        /// <summary>
        /// This method inserts the Tlv into a byte stream object.
        /// </summary>
        /// <param name="stm">Byte stream</param>
        public override void AddToStream(SmppWriter stm)
        {
            if (HasValue)
            {
                stm.Add(Tag);
                stm.Add((short)0);
            }
        }

        /// <summary>
        /// This overrides the HasValue property
        /// </summary>
        public new bool HasValue
        {
            get { return hasValue_; }
        }

        /// <summary>
        /// This gets/sets the value of the message
        /// </summary>
        public bool Value
        {
            get { return hasValue_; }
            set { hasValue_ = value; }
        }

        /// <summary>
        /// This method retrieves the Tlv from a byte stream object.
        /// </summary>
        /// <param name="stm">Byte stream</param>
        public override void GetFromStream(SmppReader stm)
        {
            hasValue_ = true;
            Tag = stm.ReadShort();
            int length = stm.ReadShort();
            if (length != 0)
                throw new TlvException("Invalid length encountered for alert_on_message_delivery (was " + length.ToString() + ", should be zero.)");
        }

        /// <summary> 
        /// Override to provide debug information
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}:tag={1},len={2},Value={3}", GetType().Name, Tag, Length, Value);
        }
    }
}
