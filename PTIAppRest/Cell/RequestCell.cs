using System;
using Xamarin.Forms;

namespace PTIAppRest
{
	public class RequestCell: ViewCell
	{
		public RequestCell()
		{
			var companyLabel = new Label
			{
				HorizontalTextAlignment = TextAlignment.Start,
				FontSize = 16
			};
			companyLabel.SetBinding(Label.TextProperty,new Binding("company"));

			var purchReqIdLabel = new Label
			{
				HorizontalTextAlignment = TextAlignment.Start,
				FontSize = 14
			};
			purchReqIdLabel.SetBinding(Label.TextProperty, new Binding("getInfoRequest"));




			View = new StackLayout
			{
				Children = { companyLabel,purchReqIdLabel },
				Orientation = StackOrientation.Vertical
				                              

			};
		}
	}
}
