using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PTIAppRest
{
	public partial class HtmlHistoricalProducts : ContentPage
	{
		public HtmlHistoricalProducts(string itemId)
		{
			InitializeComponent();

			Browser.Source = "http://200.91.71.219:84/PTIApprovalsPurchOrders/Products/HistoricalProducts.aspx?itemid=" + itemId;
			//Browser.Source = "http://192.168.100.106:84/PTIApprovalsPurchOrders/Products/HistoricalProducts.aspx?itemid=" + itemId;

		}


	}
}
