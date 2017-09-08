using System;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
    /// <summary>
    /// Summary description for Elements.
    /// </summary>
    public class SmppCOctetString : ISupportSmppByteStream
    {
        // Class data
        private string data_;

        /// <summary>
        /// Default constructor
        /// </summary>
        public SmppCOctetString()
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="s">Data</param>
        public SmppCOctetString(string s)
        {
            data_ = s;
            ValidateData();
        }

        /// <summary>
        /// This method may be overridden to provide validation when the value is set or changed.
        /// </summary>
        protected virtual void ValidateData()
        {
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public string Value
        {
            get { return data_; }
            set { data_ = value; ValidateData(); }
        }

        /// <summary>
        /// This method retrieves the C-Octet string from the byte stream
        /// </summary>
        /// <param name="reader">Byte stream</param>
        public void GetFromStream(SmppReader reader)
        {
            data_ = reader.ReadString();
            ValidateData();
        }

        /// <summary>
        /// This method adds our information to the byte stream.
        /// </summary>
        /// <param name="writer"></param>
        public void AddToStream(SmppWriter writer)
        {
            writer.Add(data_);
        }

        /// <summary>
        /// Overrides the debug ToString method
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return data_;
        }
    }

    /// <summary>
    /// Summary description for Elements.
    /// </summary>
    public class SmppOctetString : ISupportSmppByteStream
    {
        // Class data
        private string data_;

        /// <summary>
        /// Default constructor
        /// </summary>
        public SmppOctetString()
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="s">Data</param>
        public SmppOctetString(string s)
        {
            data_ = s;
            ValidateData();
        }

        /// <summary>
        /// This method may be overridden to provide validation when the value is set or changed.
        /// </summary>
        protected virtual void ValidateData()
        {
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public string Value
        {
            get { return data_; }
            set { data_ = value; ValidateData(); }
        }

        /// <summary>
        /// This method retrieves the C-Octet string from the byte stream
        /// </summary>
        /// <param name="reader">Byte stream</param>
        public void GetFromStream(SmppReader reader)
        {
            int len = (int)reader.ReadByte();
            if (len > 0)
                data_ = reader.ReadString(len);
            ValidateData();
        }

        /// <summary>
        /// This method adds our information to the byte stream.
        /// </summary>
        /// <param name="writer"></param>
        public void AddToStream(SmppWriter writer)
        {
            writer.Add((byte)data_.Length);
            writer.Add(data_, false);
        }

        /// <summary>
        /// Overrides the debug ToString method
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return data_;
        }
    }

    /// <summary>
    /// This class provides base element support for single-byte storage.
    /// </summary>
    public class SmppByte : ISupportSmppByteStream
    {
        // Class data
        private byte data_;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="b">Data</param>
        public SmppByte(byte b)
        {
            data_ = b;
            ValidateData();
        }

        /// <summary>
        /// This method may be overridden to provide validation when the value is set or changed.
        /// </summary>
        protected virtual void ValidateData()
        {
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public byte Value
        {
            get { return data_; }
            set { data_ = value; ValidateData(); }
        }

        /// <summary>
        /// This method retrieves the byte from the byte stream
        /// </summary>
        /// <param name="reader">Byte stream</param>
        public void GetFromStream(SmppReader reader)
        {
            data_ = reader.ReadByte();
            ValidateData();
        }

        /// <summary>
        /// This method adds our information to the byte stream.
        /// </summary>
        /// <param name="writer"></param>
        public void AddToStream(SmppWriter writer)
        {
            writer.Add(data_);
        }

        /// <summary>
        /// Overrides the debug ToString method
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return string.Format("{0:X}", data_);
        }
    }

    /// <summary>
    /// This class overrides the BYTE version to include only bool values
    /// </summary>
    public class SmppBool : SmppByte
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SmppBool()
            : base(0)
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public SmppBool(bool b)
            : base((b == true) ? (byte)1 : (byte)0)
        {
        }

        /// <summary>
        /// Returns the value as a boolean
        /// </summary>
        public new bool Value
        {
            get { return (base.Value == 1) ? true : false; }
            set { base.Value = (value == true) ? (byte)1 : (byte)0; }
        }
    }

    /// <summary>
    /// This class provides base element support for 4-byte storage integers.
    /// </summary>
    public class SmppInteger : ISupportSmppByteStream
    {
        // Class data
        private int data_;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="i">Data</param>
        public SmppInteger(int i)
        {
            data_ = i;
            ValidateData();
        }

        /// <summary>
        /// This method may be overridden to provide validation when the value is set or changed.
        /// </summary>
        protected virtual void ValidateData()
        {
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public int Value
        {
            get { return data_; }
            set { data_ = value; ValidateData(); }
        }

        /// <summary>
        /// This method retrieves the integer from the byte stream
        /// </summary>
        /// <param name="reader">Byte stream</param>
        public void GetFromStream(SmppReader reader)
        {
            data_ = reader.ReadInt32();
            ValidateData();
        }

        /// <summary>
        /// This method adds our information to the byte stream.
        /// </summary>
        /// <param name="writer"></param>
        public void AddToStream(SmppWriter writer)
        {
            writer.Add(data_);
        }

        /// <summary>
        /// Overrides the debug ToString method
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return string.Format("{0:X}", data_);
        }
    }

    /// <summary>
    /// This class provides base element support for a time field.  All time/date related fields
    /// are in ASCII with the following format: "YYMMDDhhmmsstnnp" and specified in either absolute
    /// or relative time.
    /// </summary>
    public class SmppTime : ISupportSmppByteStream
    {
        // Constants
		private const int MIN_SHORT_SIZE = 10;
		private const int MIN_REQUIRED_SIZE = 12;
        private const int MAX_REQUIRED_SIZE = 16;

        // Class data
        private DateTime data_;
        private TimeSpan data2_;
        bool isRelative_ = false;
        bool includeUTC_ = false;

        /// <summary>
        /// Default constructor
        /// </summary>
        public SmppTime()
        {
        }

        /// <summary>
        /// Constructor to assign absolute time form.
        /// </summary>
        /// <param name="dt">Date time to assign</param>
        /// <param name="includeUTC">True to include UTC information (time zone)</param>
        public SmppTime(DateTime dt, bool includeUTC)
        {
            data_ = dt;
            includeUTC_ = includeUTC;
            isRelative_ = false;
        }

        /// <summary>
        /// Constructor to assign relative time form.
        /// </summary>
        /// <param name="ts">Timespan</param>
        public SmppTime(TimeSpan ts)
        {
            data2_ = ts;
            isRelative_ = true;
            includeUTC_ = true;
        }

        /// <summary>
        /// Constructor which copies from existing time object
        /// </summary>
        /// <param name="tm">Existing referenec</param>
        public SmppTime(SmppTime tm)
        {
            this.data2_ = tm.data2_;
            this.data_ = tm.data_;
            this.includeUTC_ = tm.includeUTC_;
            this.isRelative_ = tm.isRelative_;
        }

        /// <summary>
        /// This method may be overridden to provide validation when the value is set or changed.
        /// </summary>
        protected virtual void ValidateData()
        {
        }

        /// <summary>
        /// This property returns whether the time format is relative.
        /// </summary>
        public bool IsRelativeTime
        {
            get { return isRelative_; }
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public DateTime AbsoluteTime
        {
            get { return data_; }
            set { isRelative_ = false; data_ = value; ValidateData(); }
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public TimeSpan RelativeTime
        {
            get { return data2_; }
            set { isRelative_ = true; data2_ = value; ValidateData(); }
        }

        /// <summary>
        /// This method retrieves the value from the byte stream
        /// </summary>
        /// <param name="reader">Byte stream</param>
        public void GetFromStream(SmppReader reader)
        {
            string s = reader.ReadString();
            if (s.Length >= MIN_SHORT_SIZE)
            {
                int MM, DD, YY, mm, hh, ss;

                YY = Convert.ToInt32(s.Substring(0, 2));
                MM = Convert.ToInt32(s.Substring(2, 2));
                DD = Convert.ToInt32(s.Substring(4, 2));
                hh = Convert.ToInt32(s.Substring(6, 2));
                mm = Convert.ToInt32(s.Substring(8, 2));
				ss = 0;
				if (s.Length >= MIN_REQUIRED_SIZE)
				{
					ss = Convert.ToInt32(s.Substring(10, 2));
				}

				if (s.Length >= MAX_REQUIRED_SIZE)
				{
					int t, nn;
					string type;

					t = Convert.ToInt32(s.Substring(12, 1));
					nn = Convert.ToInt32(s.Substring(13, 2));
					type = s.Substring(15, 1);

					// If it is a relative time, then use a timespan.
					if (type == "R")
					{
						isRelative_ = true;
						includeUTC_ = true;
						//TODO: we are generalizing here; 365 days per year, 30 days per month
						data2_ = new TimeSpan((YY * 365) + (MM * 30) + DD, hh, mm, ss, t * 100);
					}
					else
					{
						includeUTC_ = true;
						isRelative_ = false;

						// Calculate the date and then convert it to UTC.
						DateTime dt = new DateTime(YY + 2000, MM, DD, hh, mm, ss, t * 100);
						if (type == "+")
							dt += new TimeSpan(0, 0, nn * 15, 0, 0);
						else if (type == "-")
							dt -= new TimeSpan(0, 0, nn * 15, 0, 0);

						// Now convert it to our local time.
						TimeZone localZone = TimeZone.CurrentTimeZone;
						data_ = localZone.ToLocalTime(dt);
					}
				}
				else
				{
					data_ = new DateTime(YY, MM, DD, hh, mm, ss);
					includeUTC_ = false;
					isRelative_ = false;
				}
            }

            ValidateData();
        }

        /// <summary>
        /// This method adds our information to the byte stream.
        /// </summary>
        /// <param name="writer"></param>
        public void AddToStream(SmppWriter writer)
        {
            writer.Add(this.Value);
        }

        /// <summary>
        /// This property returns the native string sent over the network.
        /// </summary>
        public string Value
        {
            get
            {
                string s = "";
                if (isRelative_)
                {
                    if (data2_.TotalSeconds > 0)
                    {
                        //TODO: Again, generalizing
                        int totalDays = (int)data2_.TotalDays;
                        int totalYears = totalDays / 365;
                        totalDays -= (totalYears * 365);
                        int totalMonths = totalDays / 30;
                        totalDays -= (totalMonths * 30);

                        s = string.Format("{0:d2}{1:d2}{2:d2}{3:d2}{4:d2}{5:d2}000R",
                            totalYears, totalMonths, totalDays,
                            data2_.Hours, data2_.Minutes, data2_.Seconds);
                    }
                }
                else
                {
                    // DateTime.MinValue is used to indicate immediate delivery.
                    if (data_ != DateTime.MinValue)
                    {
                        if (includeUTC_)
                        {
                            TimeZone thisZone = TimeZone.CurrentTimeZone;
                            int nn = data_.Millisecond / 100;
                            int yy = data_.Year;
                            if (yy > 100)
                            {
                                string syy = yy.ToString();
                                yy = Convert.ToInt32(syy.Substring(syy.Length - 2));
                            }
                            string type = (thisZone.GetUtcOffset(data_).Hours > 0) ? "+" : "-";
                            s = string.Format("{0:d2}{1:d2}{2:d2}{3:d2}{4:d2}{5:d2}{6}{7:d2}{8}",
                                yy,
                                data_.Month, data_.Day, data_.Hour, data_.Minute, data_.Second, nn.ToString().Substring(0, 1),
                                (int)Math.Abs(thisZone.GetUtcOffset(data_).TotalMinutes) / 15, type);
                        }
                        else
                        {
                            int yy = data_.Year;
                            if (yy > 100)
                            {
                                string syy = yy.ToString();
                                yy = Convert.ToInt32(syy.Substring(syy.Length - 2));
                            }
                            s = string.Format("{0:d2}{1:d2}{2:d2}{3:d2}{4:d2}{5:d2}",
                                yy, data_.Month, data_.Day, data_.Hour, data_.Minute, data_.Second);
                        }
                    }
                }
                return s;
            }
        }

        /// <summary>
        /// Overrides the debug ToString method
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return (isRelative_) ? data2_.ToString() : data_.ToString();
        }
    }
}
