namespace NiceBikeG5;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;



public partial class PM_DeliveryDetails : ContentPage
{
	public PM_DeliveryDetails(string orderNumber)
	{
		InitializeComponent();
		orderNumberLabel.Text= $"ORDER N°{orderNumber}";
	}


	/*FCT BUTTON BACK*/
    private async void OnButton_Back(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_DeliveryOrders());
    }

    /*FCT BUTTON LIST OF ORDERS*/
    private async void OnButton_ListOrders(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_ListOrders());
    }

    /*FCT BUTTON INVENTORY*/
    private async void OnButton_Inventory(object sender, EventArgs e)
    {
    }

    /*FCT BUTTON DELIVERY*/
    private async void OnButton_Delivery(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_DeliveryOrders());
    }

    /*FCT BUTTON PLANNING*/
    private async void OnButton_Planning(object sender, EventArgs e)
    {
    }

    /*FCT BUTTON HISTORY*/
    private async void OnButton_History(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_ArchiveOrders());
    }
}