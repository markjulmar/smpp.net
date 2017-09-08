using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The broadcast_end_time parameter specifies the date/time at which the broadcasting
    /// state of this message was set to terminated by the messaging center (MC).
    /// </summary>
    public class broadcast_end_time : TlvParameter
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public broadcast_end_time() : base(ParameterTags.TAG_BROADCAST_END_TIME)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="tm">Time value</param>
        public broadcast_end_time(SmppTime tm) : base(ParameterTags.TAG_BROADCAST_END_TIME)
        {
            this.Value = tm;
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public broadcast_end_time(DateTime dt) : base(ParameterTags.TAG_BROADCAST_END_TIME)
        {
            this.Value = new SmppTime(dt, true);
        }

        /// <summary>
        /// This returns the SmppTime for this element
        /// </summary>
        /// <value>SmppTime</value>
        public SmppTime Value
        {
            get
            {
                SmppTime timeValue = new SmppTime();
                if (HasValue)
                    timeValue.GetFromStream(new SmppReader(Data, true));
                return timeValue;
            }

            set
            {
                value.AddToStream(new SmppWriter(Data,true));
            }
        }

        /// <summary>
        /// Override of the object.ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            SmppTime timeValue = this.Value;
            sb.Append("broadcast_end_time=");
            sb.Append(timeValue.AbsoluteTime);
            sb.AppendFormat(" [{0}]", timeValue.Value);
            return sb.ToString();
        }
    }
}
