using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace PTIAppRest
{
	public partial class Login : ContentPage
	{
		public Login()
		{
			InitializeComponent();
			enterButton.Clicked += enterButton_Clicked;
		}
		private async void enterButton_Clicked(object sender,EventArgs e)
		{
			if (string.IsNullOrEmpty(userEntry.Text))
			{
				await DisplayAlert("Error","Debe Ingresar un usuario","Aceptar");
				userEntry.Focus();
				return;
			}
			if (string.IsNullOrEmpty(passwordEntry.Text))
			{
				await DisplayAlert("Error", "Debe Ingresar una clave", "Aceptar");
				passwordEntry.Focus();
				return;
			}
			this.loadRest();

		}
		public async void loadRest()
		{
			string result=String.Empty;
			Users user=null;
			try
			{
				waitActivityIndicator.IsRunning = true;
				enterButton.IsEnabled = false;
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri("http://200.91.71.219:84");
				//client.BaseAddress = new Uri("http://192.168.100.106:84");
				string url = string.Format("/PTIRestApprovalsOrders/API/users/user/{0}/{1}", userEntry.Text.Trim(),passwordEntry.Text.Trim());
				var response = await client.GetAsync(url);
				result = response.Content.ReadAsStringAsync().Result;
				user = JsonConvert.DeserializeObject<Users>(result);
				enterButton.IsEnabled = true;

				if (string.IsNullOrEmpty(result) || result == "null")
				{
					await DisplayAlert("Error", "Usuario o clave no válidos", "Aceptar");
					passwordEntry.Text = string.Empty;
					passwordEntry.Focus();
					waitActivityIndicator.IsRunning = false;
					return;
				}

				await Navigation.PushAsync(new RequestPage(user));
			}
			catch (Exception err) { 
				await DisplayAlert("Error", "No hay conexión, intente más tarde" + err.Message, "Aceptar");
				enterButton.IsEnabled = true;
				waitActivityIndicator.IsRunning = false;
			}
				waitActivityIndicator.IsRunning = false;


				
		}
	}
}
