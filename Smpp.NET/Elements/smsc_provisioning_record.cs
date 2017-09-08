#region Using directives

using System;
using System.Text;
using JulMar.Smpp.Utility;

#endregion

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// This defines the service level types for a subscriber record
    /// </summary>
    public enum ServiceLevelType
    {
        /// <summary>
        /// Subscriber is enabled and able to use the SMSC
        /// </summary>
        Enabled = 1,
        /// <summary>
        /// Subscriber is disabled; no SMS services available
        /// </summary>
        Disabled = 0
        // > 1 Reserved for future use
    }

    /// <summary>
    /// This class describes the SMSC provisioning record used to add/delete/query/modify
    /// subscribers in the SMSC database.  This is part of the SMPPP 1.1 specification
    /// [4.1.7]
    /// </summary>
    public class smsc_provisioning_record : ISupportSmppByteStream
    {
        private SmppCOctetString customerId_ = new SmppCOctetString();
        private SmppCOctetString name_ = new SmppCOctetString();
        private SmppCOctetString address_ = new SmppCOctetString();
        private address sourceAddress_ = new address();
        private SmppInteger svcLevel_ = new SmppInteger((int)ServiceLevelType.Enabled);
        private SmppBool barStatus_ = new SmppBool(true);
        private SmppInteger ocos_ = new SmppInteger(1);
        private SmppInteger tcos_ = new SmppInteger(1);
        private SmppCOctetString password_ = new SmppCOctetString();

        private const int MAX_CUSTOMER_ID = 21;
        private const int MAX_CUSTOMER_NAME = 21;
        private const int MAX_CUSTOMER_ADDRESS = 31;

        /// <summary>
        /// Default constructor
        /// </summary>
        public smsc_provisioning_record()
        {
        }

        /// <summary>
        /// This constructs a provisioning record for the SMSC
        /// </summary>
        /// <param name="customerId">Unique identifier used to identify the customer in the database</param>
        /// <param name="smeAddress">Address of the subscriber's SME</param>
        public smsc_provisioning_record(string customerId, address smeAddress)
        {
            customerId_.Value = customerId;
            sourceAddress_ = smeAddress;
        }

        /// <summary>
        /// Unique identifier used to identify the customer in the database
        /// </summary>
        public string ID
        {
            get { return customerId_.Value; }
            set 
            {
                if (value == null)
                    throw new ArgumentNullException("ID");
                if (value.Length == 0 || value.Length > MAX_CUSTOMER_ID)
                    throw new ArgumentOutOfRangeException("ID length must be between 1 and " + MAX_CUSTOMER_ID.ToString() + " characters.");
                customerId_.Value = value; 
            }
        }

        /// <summary>
        /// Optional customer name
        /// </summary>
        public string Name
        {
            get { return name_.Value; }
            set 
            {
                if (value == null)
                    throw new ArgumentNullException("Name");
                if (value.Length == 0 || value.Length > MAX_CUSTOMER_NAME)
                    throw new ArgumentOutOfRangeException("Name length must be between 1 and " + MAX_CUSTOMER_NAME.ToString() + " characters.");

                name_.Value = value; 
            }
        }

        /// <summary>
        /// Optional street address for the customer
        /// </summary>
        public string Address
        {
            get { return address_.Value; }
            set 
            {
                if (value == null)
                    throw new ArgumentNullException("Address");
                if (value.Length == 0 || value.Length > MAX_CUSTOMER_ADDRESS)
                    throw new ArgumentOutOfRangeException("Address length must be between 1 and " + MAX_CUSTOMER_ADDRESS.ToString() + " characters.");

                address_.Value = value;
            }
        }

        /// <summary>
        /// Address of the customers ESME
        /// </summary>
        public address SMEAddress
        {
            get { return sourceAddress_; }
            set { sourceAddress_ = value; }
        }

        /// <summary>
        /// The different types of service that the subscriber's ESME is allowed
        /// </summary>
        public ServiceLevelType ServiceLevel
        {
            get { return (ServiceLevelType) svcLevel_.Value; }
            set { svcLevel_.Value = (int)value; }
        }

        /// <summary>
        /// This indicates whether the SMS facility is available to the customer
        /// </summary>
        public bool SMSAllowed
        {
            get { return barStatus_.Value; }
            set { barStatus_.Value = value; }
        }

        /// <summary>
        /// The originating class of service for the customer
        /// </summary>
        public int OriginatingClassOfService
        {
            get { return ocos_.Value; }
            set { ocos_.Value = value; }
        }

        /// <summary>
        /// The terminating class of service for the customer
        /// </summary>
        public int TerminatingClassOfService
        {
            get { return tcos_.Value; }
            set { tcos_.Value = value; }
        }

        /// <summary>
        /// The optional PIN assigned to the customer for authentication purposes.
        /// </summary>
        /// <value></value>
        public string Password
        {
            get { return password_.Value; }
            set { password_.Value = value; }
        }

        /// <summary>
        /// This method retrieves the provisioning record from the byte stream
        /// </summary>
        /// <param name="reader">Byte stream</param>
        public void GetFromStream(SmppReader reader)
        {
            customerId_.GetFromStream(reader);
            name_.GetFromStream(reader);
            address_.GetFromStream(reader);
            sourceAddress_.GetFromStream(reader);
            svcLevel_.GetFromStream(reader);
            barStatus_.GetFromStream(reader);
            ocos_.GetFromStream(reader);
            tcos_.GetFromStream(reader);
            password_.GetFromStream(reader);
        }

        /// <summary>
        /// This method adds our information to the byte stream.
        /// </summary>
        /// <param name="writer"></param>
        public void AddToStream(SmppWriter writer)
        {
            writer.Add(customerId_);
            writer.Add(name_);
            writer.Add(address_);
            writer.Add(sourceAddress_);
            writer.Add(svcLevel_);
            writer.Add(barStatus_);
            writer.Add(ocos_);
            writer.Add(tcos_);
            writer.Add(password_);
        }

        /// <summary>
        /// Override of the object.ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("smsc_provisioning_record=id:{0},name:{1},addr:{2},src:{3},svcLevel_:{4},status:{5},ocos_:{6},tcos_:{7},pin:{8}",
                    customerId_, name_, address_, sourceAddress_, svcLevel_, barStatus_,
                    ocos_, tcos_, password_);
        }
    }
}
