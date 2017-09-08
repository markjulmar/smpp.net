using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The final_date parameter specifies the date/time when the queried message reached
	/// a final state.  For messages which have not yet reached a final state, this parameter
	/// will be DateTime.MinValue and a single zero in the byte stream.
	/// </summary>
	public class final_date : SmppTime
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public final_date() : base(DateTime.MinValue, true)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="tm">Time value</param>
		public final_date(SmppTime tm) : base(tm)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public final_date(DateTime dt) : base(dt, true)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public final_date(TimeSpan ts) : base(ts)
		{
		}

		/// <summary>
		/// Override of the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("final_date=");
			if (this.IsRelativeTime)
				sb.Append(this.RelativeTime);
			else
				sb.Append(this.AbsoluteTime);
			sb.AppendFormat(" [{0}]", this.Value);
			return sb.ToString();
		}
	}
}
