using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The schedule_delivery_time parameter specifies the scheduled time at which the message delivery
	/// should be first attempted.  It can be either an absolute or relative time from the current SMSC
	/// time at which the delivery will be attempted by the SMSC.
	/// </summary>
	public class schedule_delivery_time : SmppTime
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public schedule_delivery_time() : base(DateTime.MinValue, true)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="tm">Time value</param>
		public schedule_delivery_time(SmppTime tm) : base(tm)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public schedule_delivery_time(DateTime dt) : base(dt, true)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public schedule_delivery_time(TimeSpan ts) : base(ts)
		{
		}

		/// <summary>
		/// Override of the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("schedule_delivery_time=");
			if (this.IsRelativeTime)
				sb.Append(this.RelativeTime);
			else
				sb.Append(this.AbsoluteTime);
			sb.AppendFormat(" [{0}]", this.Value);
			return sb.ToString();
		}
	}
}
