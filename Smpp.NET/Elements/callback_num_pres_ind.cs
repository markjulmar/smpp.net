using System;
using System.Text;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Elements
{
	/// <summary>
	/// CallerID presentation indicator
	/// </summary>
	public enum PresentationIndicator
	{
		/// <summary>
		/// Allow call presentation to flow through
		/// </summary>
		ALLOWED			= 0,
		/// <summary>
		/// Restrict call information from the destination
		/// </summary>
		RESTRICTED		= 1,
		/// <summary>
		/// Call information is unavailable.
		/// </summary>
		NOT_AVAILABLE	= 2
	}

	/// <summary>
	/// CallerID screen indicator
	/// </summary>
	[Flags]
	public enum ScreenIndicator
	{
		/// <summary>
		/// Information was not verified.
		/// </summary>
		NOT_SCREENED	= 0,
		/// <summary>
		/// Information was verified and validated.
		/// </summary>
		VERIFIED_PASS   = 4,
		/// <summary>
		/// Information was verified, but failed validation.
		/// </summary>
		VERIFIED_FAILED = 8,
		/// <summary>
		/// Information is network provided.
		/// </summary>
		NETWORK_PROVIDED= 12
	}

	/// <summary>
	/// The callback_num_pres_ind parameter indicates the privacy level of the message.
	/// </summary>
	public class callback_num_pres_ind : TlvByte
	{
		// Constants
		private const byte PRESIND_MASK = 0x3;
		private const byte SCREENIND_MASK = 0xC;

		/// <summary>
		/// SMPP element tag
		/// </summary>
		public const short TlvTag = ParameterTags.TAG_CALLBACK_NUM_PRES_ID;

		/// <summary>
		/// Default constructor
		/// </summary>
		public callback_num_pres_ind() : base(TlvTag)
		{
		}

		/// <summary>
		/// Parameterized constructor
		/// </summary>
		/// <param name="presind">Presentation indicator</param>
		/// <param name="screenind">Screen indicator</param>
		public callback_num_pres_ind(PresentationIndicator presind, ScreenIndicator screenind) : base(TlvTag)
		{
			this.PresentationIndicator = presind;
			this.ScreenIndicator = screenind;
		}

		/// <summary>
		/// This method returns the presentation indicator
		/// </summary>
		public PresentationIndicator PresentationIndicator
		{
			get
			{
				if (HasValue)
				{
					int val = Data.GetBuffer()[0];
					return (PresentationIndicator)(val & PRESIND_MASK);
				}
				return 0;
			}

			set
			{
				int currValue = 0;
				if (HasValue)
					currValue = (int) Data.ReadByte();
				Data.Clear();
				currValue &= ~PRESIND_MASK;
				currValue |= ((int)value & PRESIND_MASK);
				new SmppWriter(Data).Add((byte)currValue);
			}
		}

		/// <summary>
		/// This method returns the presentation indicator
		/// </summary>
		public ScreenIndicator ScreenIndicator
		{
			get
			{
				if (HasValue)
				{
					int val = Data.GetBuffer()[0];
					return (ScreenIndicator)(val & SCREENIND_MASK);
				}
				return 0;
			}

			set
			{
				int currValue = 0;
				if (HasValue)
					currValue = (int) Data.ReadByte();
				Data.Clear();
				currValue &= ~SCREENIND_MASK;
				currValue |= ((int)value & SCREENIND_MASK);
                Data.WriteByte((byte)currValue);
            }
		}

		/// <summary>
		/// Data validation support
		/// </summary>
		protected override void ValidateData()
		{
			if ((Value & (SCREENIND_MASK | PRESIND_MASK)) != Value)
				throw new System.ArgumentOutOfRangeException("The callback_num_pres_ind is not valid.");
		}

		/// <summary>
		/// Override of the object.ToString method
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (HasValue)
				sb.AppendFormat("PresInd={0},ScreenInd={1}", this.PresentationIndicator, this.ScreenIndicator);
			return string.Format("{0}{1}", base.ToString(), sb.ToString());
		}
	}
}
