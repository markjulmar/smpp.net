using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// The validity_period parameter indicates the SMSC expiration time, after which the message
	/// should be discarded if not delivered to the destination.  It can be defined in either absolute
	/// or relative time formate.
	/// </summary>
	public class validity_period : SmppTime
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public validity_period() : base(DateTime.MinValue, true)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="tm">Time value</param>
		public validity_period(SmppTime tm) : base(tm)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public validity_period(DateTime dt) : base(dt, true)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		public validity_period(TimeSpan ts) : base(ts)
		{
		}

		/// <summary>
		/// Override of the object.ToString method.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("validity_period=");
			if (this.IsRelativeTime)
				sb.Append(this.RelativeTime);
			else
				sb.Append(this.AbsoluteTime);
			sb.AppendFormat(" [{0}]", this.Value);
			return sb.ToString();
		}
	}
}
