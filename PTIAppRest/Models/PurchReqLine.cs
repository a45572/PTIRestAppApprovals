using System;
namespace PTIAppRest
{
	public class PurchReqLine
	{
			public DateTime CREATEDDATETIME { get; set; }

			public decimal PURCHQTY { get; set; }

			public string NAME { get; set; }

			public decimal PURCHPRICE { get; set; }

			public string CURRENCYCODE { get; set; }

			public string VEND { get; set; }

			public string ITEMID { get; set; }
	}
}
