using System;
using System.Text;
using JulMar.Smpp.Pdu;

namespace JulMar.Smpp.Utility {
	/// <summary>
	/// Base class for all exceptions from the SMPP library
	/// </summary>
	[Serializable]
	public class SmppException : System.Exception {
		/// <summary>
		/// Primary constructor for the SmppException class
		/// </summary>
		/// <param name="s">message</param>
		public SmppException(string s)
			: base(s) {
		}

		/// <summary>
		/// Primary constructor for the SmppException class
		/// </summary>
		/// <param name="s">message</param>
		/// <param name="innerException">Inner exception caught</param>
		public SmppException(string s, System.Exception innerException)
			: base(s, innerException) {
		}
	}

	/// <summary>
	/// This exception class is used by the PDU encoder/decoder section
	/// of the library and adds a PDU to the exception.
	/// </summary>
	[Serializable]
	public class PduException : SmppException {
		// Class data
		private SmppPdu pdu_;

		/// <summary>
		/// Primary constructor for the PduException class
		/// </summary>
		/// <param name="pdu">PDU</param>
		/// <param name="s">message</param>
		public PduException(SmppPdu pdu, string s)
			: base(s) {
			pdu_ = pdu;
		}

		/// <summary>
		/// Primary constructor for the PduException class
		/// </summary>
		/// <param name="pdu">PDU</param>
		/// <param name="innerException">Inner exception caught</param>
		public PduException(SmppPdu pdu, System.Exception innerException)
			: base("", innerException) {
			pdu_ = pdu;
		}

		/// <summary>
		/// Primary constructor for the PduException class
		/// </summary>
		/// <param name="pdu">PDU</param>
		/// <param name="s">message</param>
		/// <param name="innerException">Inner exception caught</param>
		public PduException(SmppPdu pdu, string s, System.Exception innerException)
			: base(s, innerException) {
			pdu_ = pdu;
		}

		/// <summary>
		/// This allows access to the underlying PDU
		/// </summary>
		public SmppPdu PDU {
			get { return pdu_; }
			set { pdu_ = value; }
		}

		/// <summary>
		/// This returns whether a PDU is included with the exception.
		/// </summary>
		public bool HasPDU {
			get { return pdu_ != null; }
		}

		/// <summary>
		/// Override which adds the PDU to the string.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.Append(base.ToString());
			if (HasPDU && pdu_.IsValid) {
				sb.Append(" ");
				sb.Append(pdu_.ToString());
			}
			return sb.ToString();
		}
	}

	/// <summary>
	/// This exception is thrown when there isn't enough data in a byte buffer for
	/// a delivered Pdu.
	/// </summary>
	[Serializable]
	public class IncompletePduException : PduException {
		/// <summary>
		/// Primary constructor for the IncompletePduException
		/// </summary>
		public IncompletePduException(SmppPdu pdu)
			: base(pdu, "Not enough data in buffer for PDU.") {
		}

		/// <summary>
		/// Primary constructor for the IncompletePduException
		/// </summary>
		public IncompletePduException(string s)
			: base(null, s) {
		}

		/// <summary>
		/// Primary constructor for the IncompletePduException
		/// </summary>
		public IncompletePduException()
			: base(null, "Not enough data in buffer for PDU.") {
		}

		/// <summary>
		/// Primary constructor for the IncompletePduException
		/// </summary>
		public IncompletePduException(SmppPdu pdu, Exception e)
			: base(pdu, "Not enough data in buffer for PDU.", e) {
		}

		/// <summary>
		/// Primary constructor for the IncompletePduException
		/// </summary>
		public IncompletePduException(Exception e)
			: base(null, "Not enough data in buffer for PDU.", e) {
		}
	}

	/// <summary>
	/// This exception is thrown when a PDU is streamed in from a buffer with the wrong id.
	/// </summary>
	[Serializable]
	public class UnexpectedPduException : PduException {
		/// <summary>
		/// Primary constructor for the UnexpectedPduException
		/// </summary>
		/// <param name="id">Command ID encountered</param>
		public UnexpectedPduException(int id)
			: base(null, "Unexpected PDU ID " + id.ToString() + " encountered.") {
		}

		/// <summary>
		/// Primary constructor for the UnexpectedPduException
		/// </summary>
		/// <param name="id">Command ID encountered</param>
		/// <param name="e">Previously thrown exception</param>
		public UnexpectedPduException(int id, Exception e)
			: base(null, "Unexpected PDU ID " + id.ToString() + " encountered.", e) {
		}
	}

	/// <summary>
	/// This exception is thrown when a PDU is passed to the library which is not recognized.
	/// </summary>
	[Serializable]
	public class UnknownPduException : PduException {
		/// <summary>
		/// Primary constructor for the UnknownPduException
		/// </summary>
		/// <param name="id">Command ID encountered</param>
		public UnknownPduException(int id)
			: base(null, "Unknown PDU ID " + id.ToString() + " encountered.") {
		}

		/// <summary>
		/// Primary constructor for the UnknownPduException
		/// </summary>
		/// <param name="id">Command ID encountered</param>
		/// <param name="e">Previously thrown exception</param>
		public UnknownPduException(int id, Exception e)
			: base(null, "Unknown PDU ID " + id.ToString() + " encountered.", e) {
		}
	}

	/// <summary>
	/// InvalidStreamOperationException is thrown by problems with the Tlv engine.
	/// </summary>
	[Serializable]
	public class InvalidStreamOperationException : SmppException {
		/// <summary>
		/// Primary constructor for the InvalidStreamOperationException
		/// </summary>
		/// <param name="s">message</param>
		public InvalidStreamOperationException(string s)
			: base(s) {
		}

		/// <summary>
		/// Primary constructor for the InvalidStreamOperationException
		/// </summary>
		/// <param name="s">message</param>
		/// <param name="innerException">Inner exception caught</param>
		public InvalidStreamOperationException(string s, System.Exception innerException)
			: base(s, innerException) {
		}
	}

	/// <summary>
	/// TlvException is thrown by problems with the Tlv engine.
	/// </summary>
	[Serializable]
	public class TlvException : SmppException {
		/// <summary>
		/// Primary constructor for the TlvException
		/// </summary>
		/// <param name="s">message</param>
		public TlvException(string s)
			: base(s) {
		}

		/// <summary>
		/// Primary constructor for the TlvException
		/// </summary>
		/// <param name="s">message</param>
		/// <param name="innerException">Inner exception caught</param>
		public TlvException(string s, System.Exception innerException)
			: base(s, innerException) {
		}
	}

	/// <summary>
	/// This exception is thrown by the state classes when an invalid operation
	/// is attempted.
	/// </summary>
	[Serializable]
	public class InvalidSmppStateException : SmppException {
		/// <summary>
		/// Primary constructor for the invalid smpp state exception
		/// </summary>
		/// <param name="s">message</param>
		public InvalidSmppStateException(string s)
			: base(s) {
		}

		/// <summary>
		/// Primary constructor for the invalid smpp state exception
		/// </summary>
		/// <param name="s">message</param>
		/// <param name="innerException">other thrown exception</param>
		public InvalidSmppStateException(string s, System.Exception innerException)
			: base(s, innerException) {
		}
	}
}
