using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// This identifies the network type for a broadcast_content_type
    /// </summary>
    public enum TypeOfNetwork
    {
        /// <summary>
        /// Generic SMPP network
        /// </summary>
        GENERIC = 0x0,
        /// <summary>
        /// GSM - 23041
        /// </summary>
        GSM = 0x1,
        /// <summary>
        /// TDMA - IS824/ANSI-41
        /// </summary>
        TDMA = 0x2,
        /// <summary>
        /// CDMA - IS824 - IS637
        /// </summary>
        CDMA = 0x3
    }

    /// <summary>
    /// This identifies the content type being broadcast.
    /// These apply when the type of network is Generic.
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// Index (System Service)
        /// </summary>
        SYSTEM_INDEX = 0x0,
        /// <summary>
        /// Emergency broadcast (System Service)
        /// </summary>
        SYSTEM_EMERGENCY_BROADCAST = 0x1,
        /// <summary>
        /// IRDB download (System Service)
        /// </summary>
        SYSTEM_IRDB_DOWNLOAD = 0x2,
        /// <summary>
        /// News flash (System Service)
        /// </summary>
        NEWS_FLASH = 0x10,
        /// <summary>
        /// General news - local
        /// </summary>
        GENERAL_NEWS_LOCAL = 0x11,
        /// <summary>
        /// General news - regional
        /// </summary>
        GENERAL_NEWS_REGIONAL = 0x12,
        /// <summary>
        /// General news - national
        /// </summary>
        GENERAL_NEWS_NATIONAL = 0x13,
        /// <summary>
        /// General news - international
        /// </summary>
        GENERAL_NEWS_INTERNATIONAL = 0x14,
        /// <summary>
        /// Business/Financial news - local
        /// </summary>
        BUSINESS_FINANCIAL_NEWS_LOCAL = 0x15,
        /// <summary>
        /// Business/Financial news - regional
        /// </summary>
        BUSINESS_FINANCIAL_NEWS_REGIONAL = 0x16,
        /// <summary>
        /// Business/Financial news - national
        /// </summary>
        BUSINESS_FINANCIAL_NEWS_NATIONAL = 0x17,
        /// <summary>
        /// Business/Financial news - international
        /// </summary>
        BUSINESS_FINANCIAL_NEWS_INTERNATIONAL = 0x18,
        /// <summary>
        /// Sports news - local
        /// </summary>
        SPORTS_NEWS_LOCAL = 0x19,
        /// <summary>
        /// Sports news - regional
        /// </summary>
        SPORTS_NEWS_REGIONAL = 0x1A,
        /// <summary>
        /// Sports news - national
        /// </summary>
        SPORTS_NEWS_NATIONAL = 0x1B,
        /// <summary>
        /// Sports news - international
        /// </summary>
        SPORTS_NEWS_INTERNATIONAL = 0x1C,
        /// <summary>
        /// Entertainment news - local
        /// </summary>
        ENTERTAINMENT_NEWS_LOCAL = 0x1D,
        /// <summary>
        /// Entertainment news - regional
        /// </summary>
        ENTERTAINMENT_NEWS_REGIONAL = 0x1E,
        /// <summary>
        /// Entertainment news - national
        /// </summary>
        ENTERTAINMENT_NEWS_NATIONAL = 0x1F,
        /// <summary>
        /// Entertainment news - international
        /// </summary>
        ENTERTAINMENT_NEWS_INTERNATIONAL = 0x20,
        /// <summary>
        /// Medical health/hospitals (Subscriber information)
        /// </summary>
        SUBINFO_MEDICAL_HEALTH_HOSPITALS = 0x21,
        /// <summary>
        /// Doctors (Subscriber information)
        /// </summary>
        SUBINFO_DOCTORS = 0x22,
        /// <summary>
        /// Pharmacy (Subscriber information)
        /// </summary>
        SUBINFO_PHARMACY = 0x23,
        /// <summary>
        /// Local traffic reports (Subscriber information)
        /// </summary>
        SUBINFO_LOCAL_TRAFFIC_REPORT = 0x30,
        /// <summary>
        /// Long distance traffic reports (Subscriber information)
        /// </summary>
        SUBINFO_LD_TRAFFIC_REPORT = 0x31,
        /// <summary>
        /// Taxis (Subscriber information)
        /// </summary>
        SUBINFO_TAXIS = 0x32,
        /// <summary>
        /// Weather (Subscriber information)
        /// </summary>
        SUBINFO_WEATHER = 0x33,
        /// <summary>
        /// Local airport/flight schedules (Subscriber information)
        /// </summary>
        SUBINFO_LOCAL_AIRPORT_SCHEDULES = 0x34,
        /// <summary>
        /// Restaurant information (Subscriber information)
        /// </summary>
        SUBINFO_RESTAURANTS = 0x35,
        /// <summary>
        /// Lodging information (Subscriber information)
        /// </summary>
        SUBINFO_LODGINGS = 0x36,
        /// <summary>
        /// Retail directory (Subscriber information)
        /// </summary>
        SUBINFO_RETAIL_DIRECTORY = 0x37,
        /// <summary>
        /// Advertisements (Subscriber information)
        /// </summary>
        SUBINFO_ADVERTISEMENTS = 0x38,
        /// <summary>
        /// Stock Quotes (Subscriber information)
        /// </summary>
        SUBINFO_STOCK_QUOTES = 0x39,
        /// <summary>
        /// Employment opportunities (Subscriber information)
        /// </summary>
        SUBINFO_EMPLOYMENT_OPPORTUNITIES = 0x40,
        /// <summary>
        /// Technology/News (Subscriber information)
        /// </summary>
        SUBINFO_TECHNOLOGY_NEWS = 0x41,
        /// <summary>
        /// Carrier district information
        /// </summary>
        CARRIER_DISTRICT = 0x70,
        /// <summary>
        /// Carrier network information
        /// </summary>
        CARRIER_NETWORK_INFO = 0x71,
        /// <summary>
        /// Operator services (Subscriber care)
        /// </summary>
        SUBCARE_OPERATOR_SERVICES = 0x80,
        /// <summary>
        /// /// National Directory enquiry (Subscriber care)
        /// </summary>
        SUBCARE_DIRECTORY_ENQ_NATIONAL = 0x81,
        /// <summary>
        /// International Directory enquiry (Subscriber care)
        /// </summary>
        SUBCARE_DIRECTORY_ENQ_INTERNATIONAL = 0x82,
        /// <summary>
        /// Customer care (Subscriber care)
        /// </summary>
        SUBCARE_CUSTOMER_CARE_NATIONAL = 0x83,
        /// <summary>
        /// International customer care (Subscriber care)
        /// </summary>
        SUBCARE_CUSTOMER_CARE_INTERNATIONAL = 0x84,
        /// <summary>
        /// Local date/time (Subscriber care)
        /// </summary>
        SUBCARE_LOCAL_DATETIME = 0x85,
        /// <summary>
        /// Multi-category services
        /// </summary>
        MULTI_CATEGORY_SERVICES = 0x100
    }

    /// <summary>
    /// The broadcast_content_type parameter defines the broadcast
    /// area in terms of a geographical descriptor.
    /// </summary>
    public class broadcast_content_type : TlvParameter
    {
        // Constants
        private const int MAX_LENGTH = 100;

        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_BROADCAST_CONTENT_TYPE;

        /// <summary>
        /// Default constructor
        /// </summary>
        public broadcast_content_type()
            : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="netType">Type of network</param>
        /// <param name="content">Content type</param>
        public broadcast_content_type(TypeOfNetwork netType, ContentType content)
            : base(TlvTag)
        {
            SmppWriter writer = new SmppWriter(Data, true);
            writer.Add((byte)netType);
            writer.Add((short)content);
        }

        /// <summary>
        /// Returns the type of network
        /// </summary>
        public TypeOfNetwork Network
        {
            get
            {
                return (HasValue) ?
                    (TypeOfNetwork)Data.GetBuffer()[0] :
                    TypeOfNetwork.GENERIC;
            }

            set
            {
                SmppWriter writer = new SmppWriter(Data, true);
                writer.Add((byte)value);
            }
        }

        /// <summary>
        /// This returns the content type
        /// </summary>
        public ContentType Content
        {
            get
            {
                if (HasValue && Data.Length > 1)
                {
                    SmppReader reader = new SmppReader(Data, true);
                    reader.Skip(1);
                    return (ContentType)reader.ReadShort();
                }
                return ContentType.SYSTEM_INDEX;
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
