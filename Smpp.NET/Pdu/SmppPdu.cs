using System;
using System.Collections;
using System.Text;
using System.IO;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary> 
	/// The SmppPdu class is an abstract base class for all classes which
	/// represent a PDU. It contains methods for manipulating PDU header,
	/// checking validity of PDU, automatic parsing and generation of optional
	/// part of PDU, methods for creating instance of proper class representing
	/// certain PDU based only in command id, methods for detection if the 
	/// PDU is request or response PDU, automatic sequence number
	/// assignment, etc. 
	/// The PDU has two descendants, Request and Response, which serve as 
	/// a base classes for concrete PDU classes like SubmitSM, SubmitSMResp etc.
	/// </summary>
	public abstract class SmppPdu : ISupportSmppByteStream {
		/// <summary>
		/// Required size of a protocol data unit.
		/// </summary>
		public const int REQUIRED_SIZE = 16;	// Size in bytes of the header

		/// <summary>
		/// PDU status flags
		/// </summary>
		[Flags]
		public enum PduStatus {
			/// <summary>
			/// Nothing is valid in the PDU
			/// </summary>
			invalid = 0,
			/// <summary>
			/// The header portion is valid.
			/// </summary>
			validHeader = 1,
			/// <summary>
			/// The body of the PDU is valid.
			/// </summary>
			validBody = 2,
			/// <summary>
			/// Both header and body are valid.
			/// </summary>
			valid = (validHeader | validBody)
		}

		private PduStatus state_ = PduStatus.valid;
		private int commandId_ = 0;
		private int length_ = 0;
		private int status_ = StatusCodes.ESME_ROK;
		private int sequenceNumber_ = 0;
		private ArrayList optionalParameters_ = ArrayList.Synchronized(new ArrayList());

		/// <summary> 
		/// This is counter of sequence numbers. Each time a PDU header is sent,
		/// this counter is incremented and used as the next sequence number for that PDU.
		/// </summary>
		private static int nextSeqNumber_ = 1;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public SmppPdu(int commandId) {
			commandId_ = commandId;
			sequenceNumber_ = GetNextSequenceNumber();
		}

		/// <summary>
		/// Primary constructor
		/// </summary>
		/// <param name="commandId">PDU command Id</param>
		/// <param name="status">Command status</param>
		/// <param name="seqNum">Sequence number (0 for next)</param>
		/// <param name="length">Length (0 for SMPP header only)</param>
		public SmppPdu(int commandId, int status, int seqNum, int length) {
			commandId_ = commandId;
			status_ = status;
			sequenceNumber_ = (seqNum == 0) ? GetNextSequenceNumber() : seqNum;
			length_ = (length > 0) ? length : REQUIRED_SIZE;
		}

		/// <summary>
		/// Override of the GetHashCode method
		/// </summary>
		/// <returns>Hash code</returns>
		public override int GetHashCode() {
			return sequenceNumber_.GetHashCode();
		}

		/// <summary> 
		/// Compares two PDU. Two PDUs are equal if their sequence number is equal.
		/// </summary>
		public override bool Equals(object obj) {
			if (obj != null && (obj is SmppPdu)) {
				SmppPdu pdu = (SmppPdu)obj;
				return (CommandId == pdu.CommandId && sequenceNumber_ == pdu.SequenceNumber);
			}
			return false;
		}

		/// <summary>
		/// The command id field indentifies the particular SMPP PDU, e.g.
		/// submit_sm, query_sm, etc.  A unique command identifier is assigned
		/// to each SMPP request in the range: 0 - 0x1ff.
		/// The response is mapped to an identical range with bit 31 set (0x8000000).
		/// </summary>
		public int CommandId {
			get { return commandId_; }
		}

		/// <summary> 
		/// Returns the validity of the PDU
		/// </summary>
		public PduStatus PduState {
			get { return state_; }
		}

		/// <summary>
		/// The command length field defines the total octet length
		/// of the SMPP PDU packet, including the length field itself.
		/// </summary>
		public int Length {
			get { return length_; }
			set { length_ = value; }
		}

		/// <summary>
		/// The command status field indicates the success or failure of the 
		/// SMPP request.  It is relevant only in the SMPP response PDU and must
		/// contain a zero value in the SMPP request PDU.
		/// </summary>
		public int Status {
			get { return status_; }
			set { status_ = value; }
		}

		/// <summary>
		/// The sequence number allows SMPP requests and responses to be
		/// coorelated together.  The use of sequence numbers allows multiple
		/// requests to be issued asynchronously.  Assignment of the sequence
		/// number is the responsibility of the PDU originator.  The sequence
		/// number may range from 0x1 to 0x7ffffff.
		/// </summary>
		public int SequenceNumber {
			get { return sequenceNumber_; }
			set { sequenceNumber_ = value; }
		}

		/// <summary>
		/// Simply property to return whether this PDU is valid or not.
		/// </summary>
		public bool IsValid {
			get { return (state_ == PduStatus.valid); }
			set { state_ = (value == true) ? PduStatus.valid : PduStatus.invalid; }
		}

		/// <summary>
		/// This propery returns whether a response is required for this PDU.
		/// </summary>
		/// <value>True/False</value>
		public virtual bool RequiresResponse {
			get { return true; }
		}

		/// <summary> 
		/// Adds a Tlv as an optional parameter which can be containd in
		/// the PDU. Derived classes are expected that they will define appropriate
		/// Tlvs and register them using this method. Only registered Tlvs
		/// can be read as optional parameters from binary PDU received from SMSC.
		/// </summary>
		protected internal void AddOptionalElements(params TlvParameter[] arrTlv) {
			optionalParameters_.AddRange(arrTlv);
		}

        /// <summary> 
        /// Adds Vendor Specific Tlv's as optional parameters which can be containd in
        /// the PDU. Derived classes are expected that they will define appropriate
        /// Tlvs and register them using this method. Only registered Tlvs
        /// can be read as optional parameters from binary PDU received from SMSC.
        /// </summary>
        public void AddVendorSpecificElements(params TlvParameter[] arrTlv) {
            // Skip any TLV that is not in the correct range (0x1400 - 0x3FF)
            foreach (TlvParameter tlvParm in arrTlv)
            {
                if((tlvParm.Tag >= 0x1400) && (tlvParm.Tag <= 0x3FFF))
                    optionalParameters_.AddRange(arrTlv);
            }
        }

		/// <summary> 
		/// Locates any registered Tlv with the given Tlv tag.
		/// </summary>
		/// <param name="tag">the tag of the Tlv required</param>
		/// <returns> the Tlv</returns>
		public TlvParameter GetOptionalElement(short tag) {
			int size = optionalParameters_.Count;
			TlvParameter tlv = null;
			for (int i = 0; i < size; i++) {
				tlv = (TlvParameter)optionalParameters_[i];
				if (tlv != null) {
					if (tlv.Tag == tag)
						return tlv;
				}
			}
			return null;
		}

		/// <summary>
		/// This method returns the count of elements matching the given tag.
		/// </summary>
		/// <param name="tag">Element Tag of the Tlv</param>
		/// <returns>Count of found elements</returns>
		public int GetOptionalElementCount(short tag) {
			int size = optionalParameters_.Count, count = 0;
			for (int i = 0; i < size; i++) {
				TlvParameter tlv = (TlvParameter)optionalParameters_[i];
				if (tlv != null && tlv.Tag == tag)
					count++;
			}
			return count;
		}

		/// <summary>
		/// Locates and returns Tlvs which have the same tag (i.e. repeating Tlvs)
		/// </summary>
		/// <param name="tag">Tag of the Tlv to find</param>
		/// <returns>Array of TlvParameter objects</returns>
		public TlvParameter[] GetRepeatedOptionalElements(short tag) {
			ArrayList al = new ArrayList();
			int size = optionalParameters_.Count;
			for (int i = 0; i < size; i++) {
				TlvParameter tlv = (TlvParameter)optionalParameters_[i];
				if (tlv != null && tlv.Tag == tag)
					al.Add(tlv);
			}
			return (TlvParameter[])al.ToArray(typeof(TlvParameter));
		}

		/// <summary>
		/// This method retrieves a PDU from a byte stream.
		/// </summary>
		/// <param name="stm">Byte stream</param>
		public void Deserialize(SmppReader stm) {
			state_ = PduStatus.invalid;
			try {
				// Get the header from the buffer
				length_ = stm.ReadInt32();
				int id = stm.ReadInt32();
				status_ = stm.ReadInt32();
				sequenceNumber_ = stm.ReadInt32();

				if (id != this.CommandId)
					throw new UnexpectedPduException(id);

				state_ = PduStatus.validHeader;

				// Now read the body of the PDU
				if (length_ > REQUIRED_SIZE) {
					this.GetFromStream(stm);
					state_ |= PduStatus.validBody;

					// Read any optional parameters that are present.
					while (!((SmppByteStream)stm.Stream).EOS) {
						TlvParameter tlv = new TlvParameter();

						try {
							tlv.GetFromStream(stm);
							TlvParameter tlvExisting = GetOptionalElement(tlv.Tag);
							if (tlvExisting == null)
								AddOptionalElements(tlv);
							else {
								if (tlvExisting.Tag == tlv.Tag)
									tlvExisting.Data = tlv.Data;
								else
									throw new TlvException("Read bad Tlv stream!");
							}
						} catch (ArgumentOutOfRangeException) {
						}
					}
				}
			} catch (InvalidOperationException e) {
				throw new IncompletePduException(this, e);
			} catch (PduException e) {
				e.PDU = this;
				throw e;
			} catch (System.Exception e) {
				throw new PduException(this, e);
			}
		}

		/// <summary>
		/// This method adds the PDU to the given byte stream.
		/// </summary>
		/// <param name="stm"></param>
		public void Serialize(SmppWriter stm) {
			bool includeBody = (this.Status == StatusCodes.ESME_ROK);
			int length = REQUIRED_SIZE;
			byte version = stm.Version;
			SmppByteStream body = null, optBody = null;

			// Get the optional (Tlv) parameters
			if (includeBody == true) {
				// Serialize the body into a stream.
				body = new SmppByteStream();
				new SmppWriter(body, version).Add(this);
				length += (int)body.Length;

				// If we are using at least V3.4 of the SMPP spec
				// then include Tlvs, otherwise we do not.
				if (version >= SmppVersion.SMPP_V34) {
					int size = optionalParameters_.Count;
					if (size > 0) {
						optBody = new SmppByteStream();
						SmppWriter writer = new SmppWriter(optBody, version);

						for (int i = 0; i < size; i++) {
							TlvParameter tlv = (TlvParameter)optionalParameters_[i];
							if (tlv != null && tlv.HasValue)
								writer.Add(tlv);
						}

						// Add the length of all optional parameters
						length += (int)optBody.Length;
					}
				}
			}

			// Now serialize the whole thing together.
			stm.Add(length);
			stm.Add(CommandId);
			stm.Add(status_);
			stm.Add(sequenceNumber_);
			if (includeBody == true) {
				if (body != null && body.Length > 0)
					stm.Add(body);
				if (optBody != null && optBody.Length > 0)
					stm.Add(optBody);
			}
		}

		/// <summary> 
		/// This method gets the buffer and returns an instance of class
		/// corresponding to the type of the PDU which was in the buffer; the
		/// fields of the returned PDU are set to the data from the buffer.
		/// </summary>
		public static SmppPdu Create(Stream buffer) {
			// Read the length and id from the buffer
			SmppReader reader = new SmppReader(buffer);
			int length = reader.ReadInt32();
			int id = reader.ReadInt32();

			// If we don't have the entire message yet, then throw an exception.
			if (buffer.Length < length)
				throw new IncompletePduException();

			// Create a new PDU
			SmppPdu pdu = PduFactory.CreatePdu(id);
			if (pdu != null) {
				// Reset back to the length
				buffer.Seek(-8, SeekOrigin.Current);

				// Stream the whole thing in
				pdu.Deserialize(reader);
				return pdu;
			}

			// Not found in our list of supported PDUs
			throw new UnknownPduException(id);
		}

		/// <summary> 
		/// Returns the next sequence number to assign to a PDU header.
		/// </summary>
		public static int GetNextSequenceNumber() {
			return nextSeqNumber_++;
		}

		/// <summary>
		/// This function resets the PDU for use as a seperate request.
		/// </summary>
		public void ResetSequenceNumber() {
			sequenceNumber_ = GetNextSequenceNumber();
		}

		/// <summary> 
		/// Override to provide debug information
		/// </summary>
		public override string ToString() {
			return string.Format("len={0},id={1:X},status={2:X},seq={3:X}", length_, CommandId, status_, sequenceNumber_);
		}

		/// <summary>
		/// This is used for diagnostics - it places the PDU into binary
		/// byte array form.
		/// </summary>
		/// <returns>Byte array</returns>
		public string ToString(string fmt) {
			if (fmt.ToLower() == "b") {
				SmppByteStream stm = new SmppByteStream();
				SmppWriter writer = new SmppWriter(stm);
				this.Serialize(writer);
				return stm.ToString();
			} else return this.ToString();
		}

		/// <summary>
		/// This is used by derived classes to dump the optional parameter list
		/// </summary>
		/// <returns></returns>
		protected string DumpOptionalParams() {
			StringBuilder sb = new StringBuilder();
			foreach (TlvParameter tlv in optionalParameters_) {
				if (tlv.HasValue)
					sb.AppendFormat(",{0}", tlv);
			}
			return sb.ToString();
		}

		/// <summary>
		/// This implements the AddToStream method required for serialization
		/// </summary>
		/// <param name="buff">StreamWriter</param>
		public virtual void AddToStream(SmppWriter buff) {
		}

		/// <summary>
		/// This implements the GetFromStream method required for serialization
		/// </summary>
		/// <param name="rdr">StreamReader</param>
		public virtual void GetFromStream(SmppReader rdr) {
		}
	}
}