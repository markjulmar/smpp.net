using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// This identifies the units of time for a broadcast frequency
    /// </summary>
    public enum BroadcastFrequencyUnits
    {
        /// <summary>
        /// As frequently as possible
        /// </summary>
        ASAP = 0x0,   
        /// <summary>
        /// The value is represented in seconds
        /// </summary>
        Seconds = 0x8,
        /// <summary>
        /// The value is represented in minutes
        /// </summary>
        Minutes = 0x9,
        /// <summary>
        /// The value is represented in hours
        /// </summary>
        Hours = 0xA,
        /// <summary>
        /// The value is represented in days
        /// </summary>
        Days = 0xB,
        /// <summary>
        /// The value is represented in weeks
        /// </summary>
        Weeks = 0xC,
        /// <summary>
        /// The value is represented in months
        /// </summary>
        Months = 0xD,
        /// <summary>
        /// The value is represented in years
        /// </summary>
        Years = 0xE
    }

    /// <summary>
    /// The broadcast_frequency_interval parameter defines frequency
    /// interval at which broadcasts of a message should be repeated.
    /// </summary>
    public class broadcast_frequency_interval : TlvParameter
    {
        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_BROADCAST_FREQUENCY_INTERVAL;

        /// <summary>
        /// Default constructor
        /// </summary>
        public broadcast_frequency_interval() : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="units">The units of frequency</param>
        /// <param name="value">The frequency</param>
        public broadcast_frequency_interval(BroadcastFrequencyUnits units, short value) : base(TlvTag)
        {
            SmppWriter writer = new SmppWriter(Data, true);
            writer.Add((byte)units);
            writer.Add(value);
        }

        /// <summary>
        /// Returns the units for the frequency
        /// </summary>
        public BroadcastFrequencyUnits FrequencyUnits
        {
            get 
            { 
                return (HasValue) ? 
                    (BroadcastFrequencyUnits)Data.GetBuffer()[0] : 
                        BroadcastFrequencyUnits.ASAP; 
            }

            set
            {
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Add((byte)value);
            }
        }

        /// <summary>
        /// This returns the frequency
        /// </summary>
        public short Frequency
        {
            get
            {
                if (HasValue && Data.Length > 1)
                {
                    SmppReader reader = new SmppReader(Data, true);
                    reader.Skip(1);
                    return reader.ReadShort();
                }
                return 0;
            }
            set
            {
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Skip(1);
                writer.Add((short)value);
            }
        }
    }
}
