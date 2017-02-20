using System;
using System.Collections.Generic;

namespace PTIAppRest
{
	public class PurchOrders
	{
		public string purchReqId { get; set; }

		public string user { get; set; }

		public string purchReqName { get; set; }

		public string name { get; set; }

		public DateTime createdDateTime { get; set; }

		public string requisitioner { get; set; }

		public string nameRequisitioner { get; set; }

		public string projid { get; set; }

		public DateTime sumittedDateTime { get; set; }

		public int purchReqType { get; set; }

		public Guid workItemID { get; set; }

		public string workItemSubject { get; set; }

		public string company { get; set; }

		public string inventSiteId { get; set; }

		public string inventLocationID { get; set; }

		public string comments { get; set; }

		public Decimal purchQtyTotal { get; set; }

		public Decimal purchQtyDollars { get; set; }

		public Decimal purchQtyEuros { get; set; }

		public string BusinessJustification { get; set; }

		public string purchStatus { get; set; }

		public string buyer { get; set; }

		public string approver { get; set; }

		public string area { get; set; }

		public long workItemRecId { get; set; }

		//public string getImg { get { return ("req.png");}}

		public List<PurchReqLine> PurchLinesProducts { get; set; }
	}
}
