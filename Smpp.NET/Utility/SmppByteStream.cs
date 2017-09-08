using System;
using System.IO;
using System.Text;

namespace JulMar.Smpp.Utility {
	/// <summary>
	/// ISupportByteStream allows arbitrary objects to be stuffed into and removed
	/// from the byte buffer.  This interface should be implemented by any object which
	/// needs to persist to a ByteStream buffer.
	/// </summary>
	public interface ISupportSmppByteStream {
		/// <summary>
		/// This method is used to add information to a ByteStream buffer.
		/// </summary>
		/// <param name="buff">Buffer</param>
		void AddToStream(SmppWriter buff);

		/// <summary>
		/// This method is used to read information from a stream.
		/// </summary>
		/// <param name="rdr">Smpp Reader</param>
		void GetFromStream(SmppReader rdr);
	}

	/// <summary>
	/// This class is used to read the SmppByteStream in a formatted
	/// fashion.  All primitives are written into the stream based on
	/// the SMPP specification.
	/// </summary>
	public class SmppReader {
		// The underlying byte stream.
		private Stream _stream = null;
		private byte _version;

		/// <summary>
		/// Constructor for the SmppReader
		/// </summary>
		/// <param name="stm">SmppByteStream to work from</param>
		public SmppReader(Stream stm)
			: this(stm, true, SmppVersion.SMPP_V50) {
		}

		/// <summary>
		/// Constructor for the SmppReader
		/// </summary>
		/// <param name="stm">SmppByteStream to work from</param>
		/// <param name="version">Stream version</param>
		public SmppReader(Stream stm, byte version)
			: this(stm, true, version) {
		}

		/// <summary>
		/// Constructor for the SmppReader
		/// </summary>
		/// <param name="stm">SmppByteStream to work from</param>
		/// <param name="resetPosition">True to reset position</param>
		public SmppReader(Stream stm, bool resetPosition)
			: this(stm, resetPosition, SmppVersion.SMPP_V50) {
		}

		/// <summary>
		/// Constructor for the SmppReader
		/// </summary>
		/// <param name="stm">SmppByteStream to work from</param>
		/// <param name="resetPosition">True to reset position</param>
		/// <param name="version">Stream version</param>
		public SmppReader(Stream stm, bool resetPosition, byte version) {
			_stream = stm;
			_version = version;
			if (_stream == null)
				throw new ArgumentNullException("stm", "The stream argument must be supplied.");
			if (resetPosition)
				_stream.Position = 0;
		}

		/// <summary>
		/// This propery allows control of the version of the
		/// byte stream (for Smpp versioning).
		/// </summary>
		/// <value>SmppVersion</value>
		public byte Version {
			get { return _version; }
			set { _version = value; }
		}

		/// <summary>
		/// This returns the underlying byte stream
		/// </summary>
		/// <value>Byte stream</value>
		public Stream Stream {
			get { return _stream; }
		}

		/// <summary>
		/// This allows an object to serialize itself to the stream
		/// </summary>
		/// <param name="ob"></param>
		public void ReadObject(ISupportSmppByteStream ob) {
			ob.GetFromStream(this);
		}

		/// <summary>
		/// This method changes the position of the stream by some
		/// count of bytes. 
		/// </summary>
		/// <param name="size">Count of bytes to skip</param>
		public void Skip(int size) {
			_stream.Position += size;
		}

		/// <summary>
		/// This method returns a byte from the buffer
		/// </summary>
		/// <returns>Byte</returns>
		public byte ReadByte() {
			return (byte)_stream.ReadByte();
		}

		/// <summary>
		/// This method returns a short from the buffer
		/// </summary>
		/// <returns>Short</returns>
		public short ReadShort() {
			byte[] resBuff = ReadBytes(2);
			return (short)((int)((resBuff[0] & 0xff) << 8) | (resBuff[1] & 0xff));
		}

		/// <summary>
		/// This method returns an integer from the buffer
		/// </summary>
		/// <returns>Integer</returns>
		public int ReadInt32() {
			byte[] resBuff = ReadBytes(4);
			int result = (resBuff[0] & 0xff);
			result <<= 8;
			result |= (resBuff[1] & 0xff);
			result <<= 8;
			result |= (resBuff[2] & 0xff);
			result <<= 8;
			result |= (resBuff[3] & 0xff);
			return result;
		}

		/// <summary>
		/// This method returns a null-terminated string from the buffer
		/// </summary>
		/// <returns>String without null</returns>
		public string ReadString() {
			return ReadString(Encoding.ASCII);
		}

		/// <summary>
		/// This method returns a specific length string from the buffer
		/// </summary>
		/// <param name="size">Number of characters to remove</param>
		/// <returns>String without null</returns>
		public string ReadString(int size) {
			return ReadString(size, false, new ASCIIEncoding());
		}

		/// <summary>
		/// This method returns a null-terminated string from the buffer using
		/// a specific text encoding type.
		/// </summary>
		/// <param name="encoder">Text encoder to use for decode</param>
		/// <returns>String without null</returns>
		public string ReadString(System.Text.Encoding encoder) {
			// Read until we either hit the end of the buffer, or a NULL.
			byte[] oneByte = new byte[1];
			StringBuilder sb = new StringBuilder();
			int ch = _stream.ReadByte();
			while (ch > 0) {
				oneByte[0] = (byte)ch;
				sb.Append(encoder.GetChars(oneByte));
				ch = _stream.ReadByte();
			}

			// Return the built string.
			return sb.ToString();
		}

		/// <summary>
		/// This method returns a string from the buffer
		/// </summary>
		/// <param name="size">Specific size to remove</param>
		/// <param name="removeNull">True to remove end null</param>
		/// <param name="encoder">Encoder type for decode</param>
		/// <returns>String without null</returns>
		public string ReadString(int size, bool removeNull, System.Text.Encoding encoder) {
			if (encoder == null)
				throw new System.ArgumentNullException("converter", "Text converter must be supplied.");
			if ((_stream.Length - _stream.Position) <= 0)
				throw new System.InvalidOperationException("No data available in ByteStream.");

			// Read the data out of the stream - remove the null if necessary
			byte[] buffer = ReadBytes(size);
			if (removeNull)
				ReadByte();

			// Convert to a string.
			return encoder.GetString(buffer);
		}

		/// <summary>
		/// This method returns a series of raw bytes from the buffer
		/// </summary>
		/// <param name="count">Number of bytes to remove and return</param>
		/// <returns>Byte array</returns>
		public byte[] ReadBytes(long count) {
			byte[] resBuf = null;
			if (count > 0 && count < Int32.MaxValue) {
				long len = _stream.Length;
				if (len >= count) {
					resBuf = new byte[count];
					_stream.Read(resBuf, 0, (int)count);
				} else
					throw new System.InvalidOperationException("Not enough data available in ByteStream.");
			} else
				resBuf = new byte[0];
			return resBuf;
		}
	}

	/// <summary>
	/// This class is used to write to the SmppByteStream in a formatted
	/// fashion.  All primitives are written into the stream based on
	/// the SMPP specification.
	/// </summary>
	public class SmppWriter {
		// The underlying byte stream.
		private Stream _stream = null;
		private byte _version = SmppVersion.SMPP_V50;

		/// <summary>
		/// Constructor for the SmppWriter
		/// </summary>
		/// <param name="stm">The required byte stream</param>
		public SmppWriter(Stream stm)
			: this(stm, false, SmppVersion.SMPP_V50) {
		}

		/// <summary>
		/// Constructor for the SmppWriter
		/// </summary>
		/// <param name="stm">The required byte stream</param>
		/// <param name="version">Stream version</param>
		public SmppWriter(Stream stm, byte version)
			: this(stm, false, version) {
		}

		/// <summary>
		/// Constructor for the SmppWriter
		/// </summary>
		/// <param name="stm">The required byte stream</param>
		/// <param name="reset">True to reset the buffer</param>
		public SmppWriter(Stream stm, bool reset)
			: this(stm, reset, SmppVersion.SMPP_V50) {
		}

		/// <summary>
		/// Constructor for the SmppWriter
		/// </summary>
		/// <param name="stm">The required byte stream</param>
		/// <param name="reset">True to reset the buffer</param>
		/// <param name="version">Stream version</param>
		public SmppWriter(Stream stm, bool reset, byte version) {
			if (stm == null)
				throw new ArgumentNullException("stm", "The byte stream must be supplied.");
			_stream = stm;
			_version = version;
			if (reset)
				_stream.Seek(0, SeekOrigin.Begin);
		}

		/// <summary>
		/// This returns the underlying byte stream
		/// </summary>
		/// <value>Byte stream</value>
		public Stream Stream {
			get { return _stream; }
		}

		/// <summary>
		/// This propery allows control of the version of the
		/// byte stream (for Smpp versioning).
		/// </summary>
		/// <value>SmppVersion</value>
		public byte Version {
			get { return _version; }
			set { _version = value; }
		}

		/// <summary>
		/// This method changes the position of the stream by some
		/// count of bytes.  If there are not enough bytes in the stream,
		/// then it is expanded with null characters to create the space.
		/// </summary>
		/// <param name="size">Count of bytes to skip</param>
		public void Skip(int size) {
			long currPos = _stream.Position;
			long newPos = _stream.Seek(size, SeekOrigin.Current);
			if (newPos < (currPos + size)) {
				byte[] b = new byte[((currPos + size) - newPos)];
				_stream.Write(b, 0, b.Length);
			}
		}

		/// <summary>
		/// This method appends a ByteStream to this buffer.
		/// </summary>
		/// <param name="buffer">Buffer to add</param>
		public void Add(Stream buffer) {
			Add(buffer, 0, buffer.Length);
		}

		/// <summary>
		/// This method appends a ByteStream to this buffer.
		/// </summary>
		/// <param name="buffer">Buffer to add</param>
		/// <param name="startPosition">Starting position of buffer</param>
		public void Add(Stream buffer, long startPosition) {
			Add(buffer, startPosition, buffer.Length - startPosition);
		}

		/// <summary>
		/// This method appends a ByteStream to this buffer.
		/// </summary>
		/// <param name="buffer">Buffer to add</param>
		/// <param name="startPosition">Starting position of buffer</param>
		/// <param name="count">Number of bytes to add</param>
		public void Add(Stream buffer, long startPosition, long count) {
			if (buffer == null)
				throw new ArgumentNullException("buffer");
			if (buffer.CanSeek == false)
				throw new ArgumentException("Stream must be random access to add to SMPP buffer");

			// Seek to the starting position.
			long currPos = buffer.Position;
			if (buffer.Seek(startPosition, SeekOrigin.Begin) != startPosition)
				throw new ArgumentOutOfRangeException("startPosition", "Starting position cannot be set on stream.");

			// Get the bytes from the stream.
			int availBytes = (int)Math.Min(count, buffer.Length - buffer.Position);
			if (availBytes != count)
				throw new ArgumentOutOfRangeException("count", "Not enough data available in stream.");

			// Read the data from the stream
			byte[] data = new byte[availBytes];
			int sizeRead = buffer.Read(data, 0, availBytes);

			// Reset the position
			buffer.Position = currPos;

			// Append the buffer.
			AppendBuffer(data);
		}

		/// <summary>
		/// This allows an object to serialize itself to the stream
		/// </summary>
		/// <param name="ob"></param>
		public void Add(ISupportSmppByteStream ob) {
			ob.AddToStream(this);
		}

		/// <summary>
		/// This method appends a byte array to the buffer.
		/// </summary>
		/// <param name="buffer">Byte array to add</param>
		public void Add(byte[] buffer) {
			AppendBuffer(buffer);
		}

		/// <summary>
		/// This method adds a byte to the end of the buffer
		/// </summary>
		/// <param name="data">Byte to add</param>
		public void Add(byte data) {
			_stream.WriteByte(data);
		}

		/// <summary>
		/// This method adds a word to the end of the buffer
		/// </summary>
		/// <param name="data">Word to add</param>
		public void Add(short data) {
			byte[] shortBuf = new byte[2];
			shortBuf[1] = (byte)(data & 0xff);
			shortBuf[0] = (byte)((data >> 8) & 0xff);
			AppendBuffer(shortBuf);
		}

		/// <summary>
		/// This method adds an integer to the end of the buffer
		/// </summary>
		/// <param name="data">Integer to add</param>
		public void Add(int data) {
			byte[] intBuf = new byte[4];
			intBuf[3] = (byte)(data & 0xff);
			intBuf[2] = (byte)((data >> 8) & 0xff);
			intBuf[1] = (byte)((data >> 16) & 0xff);
			intBuf[0] = (byte)((data >> 24) & 0xff);
			AppendBuffer(intBuf);
		}

		/// <summary>
		/// This method adds a null-terminated string to the end of the buffer
		/// </summary>
		/// <param name="str">String to add</param>
		public void Add(string str) {
			Add(str, true);
		}

		/// <summary>
		/// This method adds a (possibly) null-terminated string to the 
		/// end of the buffer
		/// </summary>
		/// <param name="str">String to add</param>
		/// <param name="appendNull">True to append a null, false to not.</param>
		public void Add(string str, bool appendNull) {
			Add(str, appendNull, Encoding.GetEncoding("iso-8859-1"));
		}

		/// <summary>
		/// This method appends a string to the end of the buffer with the
		/// given null-terminator setting and specific encoding requirements.
		/// </summary>
		/// <param name="str">String to add to the buffer</param>
		/// <param name="appendNull">True to append a null, false to not.</param>
		/// <param name="converter">System encoding type</param>
		public void Add(string str, bool appendNull, System.Text.Encoding converter) {
			// Require the encoder
			if (converter == null)
				throw new System.ArgumentNullException("converter", "Text converter must be supplied.");

			if (str != null && str.Length > 0)
				AppendBuffer(converter.GetBytes(str));

			if (appendNull)
				_stream.WriteByte(0);
		}

		/// <summary>
		/// This internal method is used to append a byte array to the buffer.
		/// </summary>
		/// <param name="bytes">Byte array to add to the buffer</param>
		private void AppendBuffer(byte[] bytes) {
			if (bytes.Length > 0)
				_stream.Write(bytes, 0, bytes.Length);
		}

	}

	/// <summary> 
	/// Data stream buffer for queued writting and reading of binary data.
	/// Provides methods for appending several data types to the end of the buffer
	/// and removing them from the beginning of the buffer.
	/// This class is very similar to System.IO.MemoryStream except that it
	/// allows for future expansion of the buffer when it is assigned directly
	/// to an array (MemoryStream will throw a NotSupportedException).
	/// </summary>
	public class SmppByteStream : Stream {
		// Class data
		private byte[] _buffer = null;
		private long _pos = 0;

		/// <summary>
		/// Constructor for the byte buffer
		/// </summary>
		public SmppByteStream()
			: base() {
		}

		/// <summary>
		/// Constructor for the byte buffer
		/// </summary>
		/// <param name="buffer">Raw data to assign to buffer</param>
		public SmppByteStream(byte[] buffer) {
			_buffer = buffer;
		}

		/// <summary>
		/// Returns whether we can read from the buffer
		/// </summary>
		/// <value>True</value>
		public override bool CanRead {
			get { return true; }
		}

		/// <summary>
		/// Returns whether we can write to the buffer
		/// </summary>
		/// <value>True</value>
		public override bool CanWrite {
			get { return true; }
		}

		/// <summary>
		/// Returns whether we can seek in the buffer
		/// </summary>
		/// <value>True</value>
		public override bool CanSeek {
			get { return true; }
		}

		/// <summary>
		/// This is called to flush the buffer to disk; not needed 
		/// in this implementation.
		/// </summary>
		public override void Flush() {
			// Do nothing
		}

		/// <summary>
		/// This returns whether we are at the end of the stream.
		/// </summary>
		public bool EOS {
			get {
				return (_buffer != null) ? _buffer.Length == _pos : true;
			}
		}

		/// <summary>
		/// This returns the allocated length of the buffer.
		/// </summary>
		/// <value>Length</value>
		public override long Length {
			get { return (_buffer != null) ? _buffer.Length : 0; }
		}

		/// <summary>
		/// This changes the current read/write position for the buffer.
		/// </summary>
		/// <value>New position</value>
		public override long Position {
			get {
				return _pos;
			}

			set {
				if (value < 0 || value > Length)
					throw new ArgumentOutOfRangeException("Position", "Invalid position value.");
				_pos = value;
			}
		}

		/// <summary>
		/// This method is used to read a series of bytes from the buffer.
		/// </summary>
		/// <param name="buffer">Buffer</param>
		/// <param name="offset">Offset to read into</param>
		/// <param name="count">Count of bytes</param>
		/// <returns>Count of bytes copied</returns>
		public override int Read(byte[] buffer, int offset, int count) {
			// Check boundaries
			if (_buffer == null || (_buffer.Length == _pos))
				return 0;

			// Restrict to available buffer space
			int availBytes = (int)(_buffer.Length - _pos);
			if (availBytes < count)
				count = availBytes;

			// Copy the buffer
			Array.Copy(_buffer, _pos, buffer, offset, count);
			_pos += count;

			return count;
		}

		/// <summary>
		/// This method is used to fill the buffer
		/// </summary>
		/// <param name="buffer">Buffer with data</param>
		/// <param name="offset">offset from passed buffer</param>
		/// <param name="count">Count of bytes to copy</param>
		public override void Write(byte[] buffer, int offset, int count) {
			// Special case -- this is the first write to the buffer.
			if (_buffer == null) {
				_buffer = new byte[count];
				Array.Copy(buffer, offset, _buffer, 0, count);
				_pos = count;
				return;
			}

			// Get available space
			int availBytes = (int)(_buffer.Length - _pos);

			// If we have enough space already (i.e. inserting in the middle)
			// then just replace that section.
			if (count < availBytes) {
				Array.Copy(buffer, offset, _buffer, _pos, count);
				_pos += count;
				return;
			}

			// Most common - we need to reallocate.
			byte[] newBuffer = new byte[_buffer.Length + count];

			// Copy in the existing data up to the current position since
			// we are replacing the end of the data buffer anyway.
			if (_pos > 0)
				Array.Copy(_buffer, newBuffer, _pos);

			// Add in the new data.
			Array.Copy(buffer, offset, newBuffer, _pos, count);
			_pos += count;

			// Replace our buffer.
			_buffer = newBuffer;
		}

		/// <summary>
		/// This is used to read a single byte from the buffer
		/// </summary>
		/// <returns>Byte</returns>
		public override int ReadByte() {
			if (_buffer != null) {
				long availBytes = _buffer.Length - _pos;
				if (availBytes >= 1) {
					_pos++;
					return _buffer[_pos - 1];
				}
			}
			return -1; // end of stream
		}

		/// <summary>
		/// This is used to write a single byte into the buffer
		/// </summary>
		/// <param name="value">Byte to write</param>
		public override void WriteByte(byte value) {
			if (_buffer != null) {
				long availBytes = _buffer.Length - _pos;
				if (availBytes >= 1) {
					_pos++;
					_buffer[_pos - 1] = value;
				} else {
					// Expand array
					Write(new byte[] { value }, 0, 1);
				}
			} else {
				_buffer = new byte[] { value };
				_pos = 1;
			}
		}

		/// <summary>
		/// This method repositions the current pointer in the buffer
		/// to a new position.
		/// </summary>
		/// <param name="offset">Offset</param>
		/// <param name="origin">Origin</param>
		/// <returns>New position within the stream</returns>
		public override long Seek(long offset, SeekOrigin origin) {
			// No buffer?  Cannot adjust.
			if (_buffer == null)
				return 0;

			// Based on the origin, do the adjustment.
			switch (origin) {
				case SeekOrigin.Begin:
					_pos = offset;
					break;
				case SeekOrigin.Current:
					_pos += offset;
					break;
				case SeekOrigin.End:
					_pos = _buffer.Length + offset;
					break;
				default:
					break;
			}

			// Keep us in bounds
			if (_pos < 0)
				_pos = 0;
			if (_pos > _buffer.Length)
				_pos = _buffer.Length;
			return _pos;
		}

		/// <summary>
		/// This changes the length of the buffer
		/// </summary>
		/// <param name="value">New length for the buffer</param>
		public override void SetLength(long value) {
			// No buffer yet?  Allocate it.
			if (_buffer == null && value > 0) {
				_buffer = new byte[value];
				_pos = 0;
				return;
			}

			// Zero buffer?
			if (value == 0) {
				_buffer = null;
				_pos = 0;
				return;
			}

			// Same length?
			if (_buffer.Length == value)
				return;

			// Allocate our new buffer
			byte[] newBuffer = new byte[value];

			// Copy the buffer.
			Array.Copy(_buffer, newBuffer, Math.Min(value, _buffer.Length));
			_buffer = newBuffer;
			if (_pos > value)
				_pos = value;
		}

		/// <summary>
		/// This returns the underlying buffer.
		/// </summary>
		/// <returns>Data buffer</returns>
		public byte[] GetBuffer() {
			return _buffer;
		}

		/// <summary>
		/// This changes our buffer and resets our position.
		/// </summary>
		/// <param name="buffer">Buffer</param>
		public void SetBuffer(byte[] buffer) {
			_buffer = buffer;
			_pos = 0;
		}

		/// <summary>
		/// This method copies the SMPP byte stream to a new buffer
		/// </summary>
		/// <param name="rhs">Existing byte buffer</param>
		/// <returns>New stream copy</returns>
		static public SmppByteStream Copy(SmppByteStream rhs) {
			if (rhs != null && rhs.Length > 0) {
				byte[] buffer = new byte[rhs.Length];
				System.Array.Copy(rhs.GetBuffer(), 0, buffer, 0, rhs.Length);
				return new SmppByteStream(buffer);
			}

			return new SmppByteStream();
		}

		/// <summary>
		/// This method clears the buffer
		/// </summary>
		public void Clear() {
			_pos = 0;
			_buffer = null;
		}

		/// <summary>
		/// Override to provide debug output
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			if (this.Length > 0) {
				byte[] buffer = this.GetBuffer();
				int len = buffer.Length;
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < len; ++i)
					sb.AppendFormat("{0:X2} ", buffer[i]);
				return sb.ToString();
			}
			return "";
		}
	}
}