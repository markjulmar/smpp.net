using System;
using System.Text;
using JulMar.Smpp.Elements;
using JulMar.Smpp.Utility;

namespace JulMar.Smpp.Pdu {
	/// <summary>
	/// This flag is used to determine the operation type requested in a mod_dl
	/// PDU operation.
	/// </summary>
	public enum DistributionListModifyType {
		/// <summary>
		/// Add a new member to the distribution list.
		/// </summary>
		AddMember = 1,
		/// <summary>
		/// Delete an existing member from the distribution list.
		/// </summary>
		DeleteMember = 2
	}

	/// <summary>
	/// The mod_dl operation is sent by the ESME to the SMSC to modify an existing
	/// distribution list in the SMSC system. This command allows the ESME to add or
	/// delete individual members of the distribution list.  Note that a customer
	/// record is not created in the SMSC for a new member added to a provisioned
	/// distribution list.
	/// </summary>
	public class mod_dl : SmppRequest {
		// Class data - required parameters
		private address sourceAddr_ = new address();
		private SmppCOctetString dlname_ = new SmppCOctetString();
		private dl_member_details memberDetails_ = new dl_member_details();
		private DistributionListModifyType type_ = DistributionListModifyType.AddMember;

		/// <summary>
		/// Default constructor
		/// </summary>
		public mod_dl()
			: base(Commands.MOD_DL) {
		}

		/// <summary>
		/// Primary constructor for the mod_dl PDU
		/// </summary>
		/// <param name="type">The operation to perform</param>
		/// <param name="source">ESME source address</param>
		/// <param name="dl_name">Distribution list name</param>
		/// <param name="details">The member details to add/remove</param>
		public mod_dl(DistributionListModifyType type, address source,
							 string dl_name, dl_member_details details)
			: this() {
			type_ = type;
			sourceAddr_ = source;
			dlname_.Value = dl_name;
			memberDetails_ = details;
		}

		/// <summary>
		/// The command (add/remove) to perform
		/// </summary>
		public DistributionListModifyType Command {
			get { return type_; }
			set { type_ = value; }
		}

		/// <summary>
		/// The address of the source ESME which owns this distribution list.
		/// </summary>
		public address SourceAddress {
			get { return sourceAddr_; }
			set { sourceAddr_ = value; }
		}

		/// <summary>
		/// The distribution list name
		/// </summary>
		public string DistributionListName {
			get { return dlname_.Value; }
			set { dlname_.Value = value; }
		}

		/// <summary>
		/// The member details to work with
		/// </summary>
		public dl_member_details MemberDetails {
			get { return memberDetails_; }
			set { memberDetails_ = value; }
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.AddToStream
		/// method so that the PDU can serialize itself to the data stream.
		/// </summary>
		/// <param name="writer">StreamWriter</param>
		public override void AddToStream(SmppWriter writer) {
			writer.Add(sourceAddr_);
			writer.Add(dlname_);
			writer.Add(new SmppByte((byte)type_));
			if (type_ == DistributionListModifyType.AddMember)
				writer.Add(memberDetails_);
			else
				writer.Add(memberDetails_.Description);
		}

		/// <summary>
		/// This method implements the ISupportSmppByteStream.GetFromStream
		/// method so that the PDU can serialize itself from the data stream.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		public override void GetFromStream(SmppReader reader) {
			reader.ReadObject(sourceAddr_);
			reader.ReadObject(dlname_);
			type_ = (DistributionListModifyType)reader.ReadByte();
			if (type_ == DistributionListModifyType.AddMember)
				reader.ReadObject(memberDetails_);
			else {
				memberDetails_.Address = new address();
				memberDetails_.Description = reader.ReadString();
			}
		}

		/// <summary>
		/// Override of the ToString method for debugging
		/// </summary>
		/// <returns>string</returns>
		public override string ToString() {
			return string.Format("mod_dl: {0},addr={1},dl_name={2},type={3},details={4}",
				 base.ToString(), sourceAddr_.ToString(), dlname_.ToString(),
				 type_.ToString(), memberDetails_.ToString());
		}
	}
}
