using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// The source_node_id parameter is a unique number assigned within
    /// a single ESME or MC network and must uniquely identify a
    /// source node wihtin the context of the MC or ESME.  The
    /// content of a source_node_id is comprised of decimal digits and
    /// is at the discretion of the owning EMSE or MC.
    /// </summary>
    public class source_node_id : TlvCOctetString
    {
        // Constants
        private const int REQUIRED_LENGTH = 6;

        /// <summary>
        /// SMPP element tag
        /// </summary>
        public const short TlvTag = ParameterTags.TAG_SOURCE_NODE_ID;

        /// <summary>
        /// Default constructor
        /// </summary>
        public source_node_id() : base(TlvTag)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="id">Numeric identifier</param>
        public source_node_id(int id) : base(TlvTag)
        {
            string text = id.ToString();
            text = text.PadLeft(REQUIRED_LENGTH, '0');
            this.Value = text;
        }

        /// <summary>
        /// This returns the ID of the node
        /// </summary>
        public int ID
        {
            get
            {
                int id = 0;
                Int32.TryParse(base.Value, out id);
                return id;
            }
            set
            {
                string text = value.ToString();
                text = text.PadLeft(REQUIRED_LENGTH, '0');
                this.Value = text;
            }
        }

        /// <summary>
        /// Data validation support
        /// </summary>
        protected override void ValidateData()
        {
            if (Value.Length != REQUIRED_LENGTH)
                throw new System.ArgumentOutOfRangeException("The source_node_id must be " + REQUIRED_LENGTH.ToString() + " characters.");
            else
            {
                int value;
                if (Int32.TryParse(Value, out value) == false)
                    throw new System.ArgumentOutOfRangeException("The source_node_id must be numeric.");
            }
        }
    }
}
