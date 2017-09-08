using System;
using System.Text;
using JulMar.Smpp.Utility;
using System.Collections;

namespace JulMar.Smpp.Utility {
	/// <summary> 
	/// All optional parameters in an SMPP PDU are encoded in Tlv (Tag-Length-Value)
	/// format.  The tag and length are 2 byte integers with the value being
	/// a variable length entity.
	/// </summary>
	public class TlvParameter : ISupportSmppByteStream {
		/// <summary>
		/// Required size  of a Tlv in the SMPP specification is 4-bytes.
		/// </summary>
		public const int REQUIRED_SIZE = 4;

		// Class data
		private short tag_ = 0;
		private SmppByteStream data_ = new SmppByteStream();

		/// <summary>
		/// Default constructor
		/// </summary>
		public TlvParameter() {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="tag">The command tag</param>
		public TlvParameter(short tag) {
			tag_ = tag;
		}

		/// <summary>
		/// Performs a comparison against another object type.
		/// </summary>
		/// <param name="obj">Object</param>
		/// <returns>True/False</returns>
		public override bool Equals(Object obj) {
			if (obj != null && (obj is TlvParameter))
				return this.Tag == ((TlvParameter)obj).Tag;
			return false;
		}

		/// <summary>
		/// Typed version of Equals.
		/// </summary>
		/// <param name="obj">TlvParameter</param>
		/// <returns>True/False</returns>
		public bool Equals(TlvParameter obj) {
			return this.Tag == obj.Tag;
		}

		/// <summary>
		/// Override of the GetHashCode method
		/// </summary>
		/// <returns>Hash code</returns>
		public override int GetHashCode() {
			return tag_.GetHashCode();
		}

		/// <summary>
		/// The Tag field is used to uniquely identify the particular optional
		/// parameter in question.  It is always 2 octets in length.
		/// </summary>
		public short Tag {
			get { return tag_; }
			set { tag_ = value; }
		}

		/// <summary>
		/// This property returns whether a value is associated with the Tlv.
		/// </summary>
		public virtual bool HasValue {
			get { return data_.Length > 0; }
		}

		/// <summary>
		/// This property allows access to the variant data stored by the Tlv.
		/// </summary>
		internal virtual SmppByteStream Data {
			get { return data_; }
			set { data_ = value; }
		}

		/// <summary> 
		/// Returns the length of the binary data representing the
		/// value carried by this Tlv.  Note that the length does not
		/// include the Tag or Length fields.
		/// </summary>
		public virtual int Length {
			get { return (int)data_.Length; }
		}

		/// <summary>
		/// This method inserts the Tlv into a byte stream object.
		/// </summary>
		/// <param name="stm">Byte stream</param>
		public virtual void AddToStream(SmppWriter stm) {
			if (HasValue) {
				stm.Add(Tag);
				stm.Add((short)data_.Length);
				stm.Add(data_);
			}
		}

		/// <summary>
		/// This method retrieves the Tlv from a byte stream object.
		/// </summary>
		/// <param name="stm">Byte stream</param>
		public virtual void GetFromStream(SmppReader stm) {
			tag_ = stm.ReadShort();
			int length = stm.ReadShort();
			data_ = new SmppByteStream(stm.ReadBytes(length));
		}

		/// <summary> 
		/// Override to provide debug information
		/// </summary>
		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0}:tag={1},len={2},value=", GetType().Name, tag_, Length);
			if (HasValue)
				sb.Append(data_);
			else
				sb.Append("not present");
			return sb.ToString();
		}

		/// <summary>
		/// Virtual method to validate information in the Tlv
		/// </summary>
		protected virtual void ValidateData() {
			// Implement in derived class
		}
	}

	/// <summary>
	/// This class provides a Tlv which is oriented around a single byte.
	/// </summary>
	public class TlvByte : TlvParameter {
		/// <summary>
		/// Default constructor
		/// </summary>
		public TlvByte(short TlvTag)
			: base(TlvTag) {
		}

		/// <summary>
		/// This method returns the assigned value for the Tlv.
		/// </summary>
		public byte Value {
			get {
				return (HasValue) ? Data.GetBuffer()[0] : (byte)0;
			}

			set {
				Data.Clear();
				Data.WriteByte(value);
				ValidateData();
			}
		}
	}

	/// <summary>
	/// This class provides a Tlv which is oriented around a 2 byte value.
	/// </summary>
	public class TlvShort : TlvParameter {
		/// <summary>
		/// Default constructor
		/// </summary>
		public TlvShort(short TlvTag)
			: base(TlvTag) {
		}

		/// <summary>
		/// This method returns the assigned value for the Tlv.
		/// </summary>
		public int Value {
			get {
				return (HasValue) ? (int)new SmppReader(Data, true).ReadShort() : (int)0;
			}

			set {
				Data.Clear();
				new SmppWriter(Data, true).Add((short)value);
				ValidateData();
			}
		}
	}

	/// <summary>
	/// This class provides a Tlv which is oriented around a 4 byte value
	/// </summary>
	public class TlvInt : TlvParameter {
		/// <summary>
		/// Default constructor
		/// </summary>
		public TlvInt(short TlvTag)
			: base(TlvTag) {
		}

		/// <summary>
		/// This method returns the assigned value for the Tlv.
		/// </summary>
		public int Value {
			get {
				return (HasValue) ? new SmppReader(Data, true).ReadInt32() : (int)0;
			}

			set {
				Data.Clear();
				new SmppWriter(Data).Add(value);
				ValidateData();
			}
		}
	}

	/// <summary>
	/// This class provides a Tlv which is oriented around a null-terminated string value
	/// </summary>
	public class TlvCOctetString : TlvParameter {
		/// <summary>
		/// Default constructor
		/// </summary>
		public TlvCOctetString(short TlvTag)
			: base(TlvTag) {
		}

		/// <summary>
		/// This method returns the assigned value for the Tlv.
		/// </summary>
		public string Value {
			get {
				return (HasValue) ? new SmppReader(Data).ReadString() : "";
			}

			set {
				Data.Clear();
				new SmppWriter(Data).Add(value);
				ValidateData();
			}
		}

		/// <summary>
		/// Override of the object.ToString method
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0}:tag={1},len={2},value=", GetType().Name, Tag, Length);
			if (HasValue) {
				sb.Append("\"");
				sb.Append(Value);
				sb.Append("\"");
			} else
				sb.Append("not present");
			return sb.ToString();
		}
	}

    /// <summary>
    /// This class provides a Tlv which is oriented around a null-terminated string value
    /// </summary>
    public class TlvString : TlvParameter
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TlvString(short TlvTag)
            : base(TlvTag)
        {
        }

        /// <summary>
        /// This method returns the assigned value for the Tlv.
        /// </summary>
        public string Value
        {
            get
            {
                return (HasValue) ? new SmppReader(Data).ReadString(Length) : "";
            }

            set
            {
                Data.Clear();
                new SmppWriter(Data).Add(value, false);
                ValidateData();
            }
        }

        /// <summary>
        /// Override of the object.ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}:tag={1},len={2},value=", GetType().Name, Tag, Length);
            if (HasValue)
            {
                sb.Append("\"");
                sb.Append(Value);
                sb.Append("\"");
            }
            else
                sb.Append("not present");
            return sb.ToString();
        }
    }

    /// <summary>
    /// This class provides a conversion from TlvParameter to basic types
    /// </summary>
    public class TlvConverter
    {
        /// <summary>
        /// Convert a TlvParameter to a TlvByte
        /// </summary>
        /// <returns>TlvByte</returns>
        public static TlvByte ToTlvByte(TlvParameter parm)
        {
            TlvByte byteParm = new TlvByte(parm.Tag);
            byteParm.Data = parm.Data;
            return byteParm;
        }

        /// <summary>
        /// Convert a TlvParameter to a TlvShort
        /// </summary>
        /// <returns>TlvShort</returns>
        public static TlvShort ToTlvShort(TlvParameter parm)
        {
            TlvShort shortParm = new TlvShort(parm.Tag);
            shortParm.Data = parm.Data;
            return shortParm;
        }

        /// <summary>
        /// Convert a TlvParameter to a TlvInt
        /// </summary>
        /// <returns>TlvInt</returns>
        public static TlvInt ToTlvInt(TlvParameter parm)
        {
            TlvInt intParm = new TlvInt(parm.Tag);
            intParm.Data = parm.Data;
            return intParm;
        }

        /// <summary>
        /// Convert a TlvParameter to a TlvCOctetString
        /// </summary>
        /// <returns>TlvCOctetString</returns>
        public static TlvCOctetString ToTlvTlvCOctetString(TlvParameter parm)
        {
            TlvCOctetString stringParm = new TlvCOctetString(parm.Tag);
            stringParm.Data = parm.Data;
            return stringParm;
        }

        /// <summary>
        /// Convert a TlvParameter to a TlvString
        /// </summary>
        /// <returns>TlvString</returns>
        public static TlvString ToTlvString(TlvParameter parm)
        {
            TlvString stringParm = new TlvString(parm.Tag);
            stringParm.Data = parm.Data;
            return stringParm;
        }
    }
}
