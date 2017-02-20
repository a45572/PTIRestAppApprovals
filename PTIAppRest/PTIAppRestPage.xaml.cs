using System;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace PTIAppRest
{
	public partial class PTIAppRestPage : ContentPage
	{
		void button_Clicked(object sender, System.EventArgs e)
		{
			loadpage();
		}
		public async void loadpage()
		{
			try
			{
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri("http://200.91.71.219:84");
				//client.BaseAddress = new Uri("http://192.168.100.106:84");
				string url = string.Format("/PTIRestApprovalsOrders/API/users/user/{0}", "msalazarp");
				var response = await client.GetAsync(url);
				var result = response.Content.ReadAsStringAsync().Result;
				//etiqueta.Text = result;

				var user = JsonConvert.DeserializeObject<Users>(result);
				await DisplayAlert("ggh", user.name, "dd", "s");
			}
			catch (Exception ) { }
		}
		public PTIAppRestPage()
		{
			InitializeComponent();
		}
	}
}
