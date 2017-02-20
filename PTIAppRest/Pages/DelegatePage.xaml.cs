using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace PTIAppRest
{
	public partial class DelegatePage : ContentPage
	{
		List<Users> users = null;
		Users userActual;
		PurchOrders purchOrderSelected = null;
		public DelegatePage(PurchOrders purchOrderSelected,Users userActual)
		{
			InitializeComponent();
			loadRest();
			this.userActual = userActual;
			confirmacionLabel.Text = "Seguro que desea delegar la Solicitud: " + purchOrderSelected.purchReqId + " ?";
		}

		public async void loadRest()
		{
			string result = String.Empty;
			try
			{
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri("http://200.91.71.219:84");
				//client.BaseAddress = new Uri("http://192.168.100.106:84");
				//string url = string.Format("/PTIRestApprovalsOrders/API/PURCHORDERSNETWORKALIAS/{0}", userlogin.username);
				string url = string.Format("/PTIRestApprovalsOrders/API/USERS");
				var response = await client.GetAsync(url);
				result = response.Content.ReadAsStringAsync().Result;
				users= JsonConvert.DeserializeObject<List<Users>>(result);
				//users.Remove();
				foreach (var user in users)
				{
					if (user.username != userActual.username)
					{
						userPicker.Items.Add(user.name);
					}
				}
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "No hay conexión, intente más tarde", "Aceptar");
			}

			if (string.IsNullOrEmpty(result) || result == "null")
			{
				await DisplayAlert("", "Usuario sin solicitudes", "Aceptar");
				return;
			}
		}

		 void delegar_Clicked(object sender, System.EventArgs e)
		{
			this.moveWorkFlowAx(purchOrderSelected, purchOrderSelected.user, this.users[userPicker.SelectedIndex].userIdAx);
		}

		public async void moveWorkFlowAx(PurchOrders order, string userDelegate, string approvalsEject)
		{
			string result = String.Empty;
			try
			{
				//waitActivityIndicator.IsRunning = true;
				HttpClient client = new HttpClient();
				//client.BaseAddress = new Uri("http://200.91.71.219:84");
				client.BaseAddress = new Uri("http://192.168.100.106:84");
				//string url = string.Format("/PTIRestApprovalsOrders/API/PURCHORDERSNETWORKALIAS/{0}", userlogin.username);
				string url = string.Format("/PTIRestApprovalsOrders/API/PurchOrders/moveWorkFlowAX/{0}/{1}/{2}/{3}/{4}/{5}/{6}", order.purchReqId, order.user, userDelegate, approvalsEject, order.workItemID, order.area, order.comments);
				var response = await client.GetAsync(url);
				result = response.Content.ReadAsStringAsync().Result;
				//waitActivityIndicator.IsRunning = false;

			}
			catch (Exception err)
			{
				await DisplayAlert("Error", "No hay conexión, intente más tarde." + err.Message, "Aceptar");
				//waitActivityIndicator.IsRunning = false;
			}

			if (string.IsNullOrEmpty(result) || result == "null")
			{
				await DisplayAlert("", "Usuario sin solicitudes", "Aceptar");
				//waitActivityIndicator.IsRunning = false;
				return;
			}
		}
	}
}
