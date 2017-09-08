using System;
using JulMar.Smpp.Utility;
using System.Collections;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// These constants define the known broadcast area types
    /// which are included in the broadcast_area_identifier.
    /// </summary>
    public enum BroadcastArea
    {
        /// <summary>
        /// This allows specification of an area by name
        /// </summary>
        ALIAS_OR_NAME = 0x0,
        /// <summary>
        /// This allows specification of an area as an ellipsoid arc
        /// See 3GPP TS 23.032 section 5.7,7.3.7
        /// </summary>
        ELLIPSOID_ARC = 0x1,
        /// <summary>
        /// This allows specification of an area as a polygon
        /// See 3GPP TS 23.032 section 5.4,7.3.4
        /// </summary>
        POLYGON = 0x2
    }

    /// <summary>
    /// The broadcast_area_identifier parameter defines the broadcast
    /// area in terms of a geographical descriptor.
    /// </summary>
    public class broadcast_area_identifier : TlvParameter
    {
        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_BROADCAST_AREA_IDENTIFIER;
        private const int MAX_LENGTH = 100;

        /// <summary>
        /// Default constructor
        /// </summary>
        public broadcast_area_identifier()
            : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="formatType">The broadcast format type</param>
        /// <param name="text">The specific area details based on the type</param>
        public broadcast_area_identifier(BroadcastArea formatType, string text)
            : base(TlvTag)
        {
            SmppWriter writer = new SmppWriter(Data, true);
            writer.Add((byte)formatType);
            if (text.Length > (MAX_LENGTH - 1))
                text = text.Substring(0, MAX_LENGTH - 1);
            writer.Add(text, false);
        }

        /// <summary>
        /// Returns the area type.
        /// </summary>
        public BroadcastArea AreaType
        {
            get
            {
                return (!HasValue) ?
                    BroadcastArea.ALIAS_OR_NAME :
                    (BroadcastArea)Data.GetBuffer()[0];
            }

            set
            {
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Add((byte)value);
            }
        }

        /// <summary>
        /// This returns the area information data which is vendor specific.
        /// </summary>
        /// <value>Returning text</value>
        public string AreaInfo
        {
            get
            {
                SmppReader reader = new SmppReader(Data);
                if (Data.Length > 1)
                {
                    reader.Skip(1); // skip byte
                    return reader.ReadString();
                }
                return "";
            }
            set
            {
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Skip(1);

                // Add our text
                string text = value;
                if (text.Length > (MAX_LENGTH - 1))
                    text = text.Substring(0, MAX_LENGTH - 1);
                writer.Add(text, false);
            }
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Data.Length > MAX_LENGTH)
                throw new System.ArgumentOutOfRangeException("The broadcast_area_identifier cannot exceed " + MAX_LENGTH.ToString() + " characters.");
        }
    }
}
