using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace PTIAppRest
{
	public partial class RequestPage : ContentPage
	{
		private List<PurchOrders> purchOrdersList = null;
		Users user=null;

		public RequestPage(Users user)
		{
			InitializeComponent();
			this.user = user;
			loadRest(user);

		}

		public async void loadRest(Users userlogin)
		{
			string result = String.Empty;
			try
			{
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri("http://200.91.71.219:84");
				//client.BaseAddress = new Uri("http://192.168.100.106:84");
				//string url = string.Format("/PTIRestApprovalsOrders/API/PURCHORDERSNETWORKALIAS/{0}", userlogin.username);
				string url = string.Format("/PTIRestApprovalsOrders/API/PURCHORDERSNETWORKALIAS/{0}", "msalazarp");
				var response = await client.GetAsync(url);
				result = response.Content.ReadAsStringAsync().Result;
				purchOrdersList = JsonConvert.DeserializeObject <List<PurchOrders>> (result);
				RequestTitle.Text ="Solicitudes Pendientes ("+purchOrdersList.Count.ToString()+")";
				RequestInfo.Text = "User: "+userlogin.name;
				requestList.ItemsSource = purchOrdersList;
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "No hay conexión, intente más tarde", "Aceptar");
			}

			if (string.IsNullOrEmpty(result) || result == "null")
			{
				await DisplayAlert("","Usuario sin solicitudes", "Aceptar");
				return;
			}
		}

		async void aprobarWF(object sender, System.EventArgs e)
		{
			try
			{
				var mi = ((MenuItem)sender);
				PurchOrders ordenCompra = (PurchOrders)mi.CommandParameter;

				var ans = await DisplayAlert("Aprobación?", "Seguro que desea aprobar la solicitud:" + ordenCompra.purchReqId + " " + ordenCompra.BusinessJustification.Trim(), "Si", "No");
				if (ans == true)
				{
					this.moveWorkFlowAx(ordenCompra, ordenCompra.user, "");
				}
			}
			catch (Exception a)
			{
				await DisplayAlert("Error al Aprobar", a.Message.ToString(), "Aceptar");
			}

		}
		async void rechazarWF(object sender, System.EventArgs e)
		{
			try
			{
				var mi = ((MenuItem)sender);
				PurchOrders ordenCompra = (PurchOrders)mi.CommandParameter;

				var ans = await DisplayAlert("Rechazar?", "Seguro que desea rechazar la solicitud:" + ordenCompra.purchReqId + " " + ordenCompra.BusinessJustification.Trim(), "Si", "No");
				if (ans == true)
				{
					this.moveWorkFlowAx(ordenCompra, ordenCompra.user, "");
				}
			}
			catch (Exception a)
			{
				await DisplayAlert("Error al Rechazar", a.Message.ToString() , "Aceptar");
			}
		}

	

		void requestListView_Refreshing(object sender, System.EventArgs e)
		{
			loadRest(user);
			requestList.IsRefreshing = false;
		}

		public async void moveWorkFlowAx(PurchOrders order,string userDelegate,string approvalsEject)
		{
			string result = String.Empty;
			try
			{
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri("http://200.91.71.219:84");
				//client.BaseAddress = new Uri("http://192.168.100.106:84");
				//string url = string.Format("/PTIRestApprovalsOrders/API/PURCHORDERSNETWORKALIAS/{0}", userlogin.username);
				string url = string.Format("/PTIRestApprovalsOrders/API/PurchOrders/moveWorkFlowAX/{0}/{1}/{2}/{3}/{4}/{5}/{6}", order.purchReqId,order.user,userDelegate,approvalsEject,order.workItemID,order.area,order.comments);
				var response = await client.GetAsync(url);
				result = response.Content.ReadAsStringAsync().Result;
				purchOrdersList = JsonConvert.DeserializeObject<List<PurchOrders>>(result);

				requestList.ItemsSource = purchOrdersList;

			}
			catch (Exception err)
			{
				await DisplayAlert("Error", "No hay conexión, intente más tarde."+err.Message, "Aceptar");
			}

			if (string.IsNullOrEmpty(result) || result == "null")
			{
				await DisplayAlert("", "Usuario sin solicitudes", "Aceptar");
				return;
			}
		}

		async void requestList_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			try
			{
				var orden = (PurchOrders)e.Item;
				await Navigation.PushAsync(new RequestDetailPage(orden, user));
			}
			catch (Exception a)
			{
				await DisplayAlert("Error al Rechazar", a.Message.ToString(), "Aceptar");
			}
		}
	}
}
