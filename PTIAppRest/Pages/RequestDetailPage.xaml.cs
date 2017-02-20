using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace PTIAppRest
{
	public partial class RequestDetailPage : ContentPage
	{
		PurchOrders purchOrderSelected = null;
		Users userActual ;
		public RequestDetailPage(PurchOrders purchOrderSelected, Users userActual)
		{
			
			InitializeComponent();

			companiaLabel.Text 		= purchOrderSelected.company;
			departamentoLabel.Text = purchOrderSelected.workItemID.ToString();
			ColonesLabel.Text 		= String.Format("{0:n}", purchOrderSelected.purchQtyTotal)		+ " CRC";
			dolaresLabel.Text 		= String.Format("{0:n}",purchOrderSelected.purchQtyDollars) 	+ " USP";
			eurosLabel.Text 		= String.Format("{0:n}",purchOrderSelected.purchQtyEuros) 		+ " EUR";
			justificacionLabel.Text = purchOrderSelected.BusinessJustification;
			estadoLabel.Text 		= purchOrderSelected.purchStatus;
			solicitanteLabel.Text 	= purchOrderSelected.nameRequisitioner.Trim();
			compradorLabel.Text 	= purchOrderSelected.buyer;
			vistoBuenoLabel.Text 	= purchOrderSelected.approver;
			ComentariosLabel.Text 	= purchOrderSelected.comments.Trim();
			listViewProducts.ItemsSource = purchOrderSelected.PurchLinesProducts;
			this.purchOrderSelected = purchOrderSelected;
			this.userActual = userActual;
			this.Title = "Solicitud #" + purchOrderSelected.purchReqId;
		}

		async void ListViewProducts_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			var lineProducts = (PurchReqLine)e.Item;
			await Navigation.PushAsync(new HtmlHistoricalProducts(lineProducts.ITEMID));
		}

		async void aprobar_Clicked(object sender, System.EventArgs e)
		{
			try
			{
				waitActivityIndicator.IsRunning = true;
				var ans = await DisplayAlert("Aprobación?", "Seguro que desea aprobar la solicitud:" + purchOrderSelected.purchReqId + " " + purchOrderSelected.BusinessJustification.Trim(), "Si", "No");
				if (ans == true)
				{
					this.moveWorkFlowAx(purchOrderSelected, purchOrderSelected.user, "PurchReqApprovalApprove");
					await Navigation.PushAsync(new RequestPage(userActual));
				
				}
				waitActivityIndicator.IsRunning = false;
			}
			catch (Exception a)
			{
				await DisplayAlert("Error al Aprobar", a.Message.ToString(), "Aceptar");
			}
		}
		async void rechazar_Clicked(object sender, System.EventArgs e)
		{
			try
			{
				waitActivityIndicator.IsRunning = true;
				var ans = await DisplayAlert("Rechazar?", "Seguro que desea rechazar la solicitud:" + purchOrderSelected.purchReqId + " " + purchOrderSelected.BusinessJustification.Trim(), "Si", "No");
				if (ans == true)
				{
					this.moveWorkFlowAx(purchOrderSelected, purchOrderSelected.user, "PurchReqApprovalReject");
					await Navigation.PushAsync(new RequestPage(userActual));
				}
				waitActivityIndicator.IsRunning = false;
			}
			catch (Exception a)
			{
				await DisplayAlert("Error al Rechazar", a.Message.ToString(), "Aceptar");
			}
		}
		async void delegar_Clicked(object sender, System.EventArgs e)
		{
			try
			{
				await Navigation.PushAsync(new DelegatePage(purchOrderSelected,this.userActual));
			}
			catch (Exception a)
			{
				await DisplayAlert("Error al delegar", a.Message.ToString(), "Aceptar");
			}
		}

		public async void moveWorkFlowAx(PurchOrders order, string userDelegate, string approvalsEject)
		{
			string result = String.Empty;
			try
			{
				waitActivityIndicator.IsRunning = true;
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri("http://200.91.71.219:84");
				//client.BaseAddress = new Uri("http://192.168.100.106:84");
				//string url = string.Format("/PTIRestApprovalsOrders/API/PURCHORDERSNETWORKALIAS/{0}", userlogin.username);
				string url = string.Format("/PTIRestApprovalsOrders/API/PurchOrders/moveWorkFlowAX/{0}/{1}/{2}/{3}/{4}/{5}/{6}", order.purchReqId, order.user, userDelegate, approvalsEject, order.workItemID, order.area, order.comments);
				var response = await client.GetAsync(url);
				result = response.Content.ReadAsStringAsync().Result;
				waitActivityIndicator.IsRunning = false;
			}
			catch (Exception err)
			{
				await DisplayAlert("Error", "No hay conexión, intente más tarde." + err.Message, "Aceptar");
				waitActivityIndicator.IsRunning = false;
			}

			if (string.IsNullOrEmpty(result) || result == "null")
			{
				await DisplayAlert("", "Sin respuesta", "Aceptar");
				waitActivityIndicator.IsRunning = false;
				return;
			}
		}
	}
}
