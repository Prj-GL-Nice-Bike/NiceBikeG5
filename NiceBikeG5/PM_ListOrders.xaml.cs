namespace NiceBikeG5;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;



public partial class PM_ListOrders: ContentPage
{
    private ObservableCollection<string> _items;

    public PM_ListOrders()
    {
        InitializeComponent();
        _items= new ObservableCollection<string>();
        listView.ItemsSource= _items;
        LoadData();
    }


    /*FCT BUTTON BACK*/
    private async void OnButton_Back(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_Menu());
    }

    /*FCT BUTTON LIST OF ORDERS*/
    private async void OnButton_ListOrders(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_ListOrders());
    }

    /*FCT BUTTON INVENTORY*/
    private async void OnButton_Inventory(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Binventory());
    }

    /*FCT BUTTON DELIVERY*/
    private async void OnButton_Delivery(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_DeliveryOrders());
    }

    /*FCT BUTTON PLANNING*/
    private async void OnButton_Planning(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Production_Planning());
    }

    /*FCT BUTTON HISTORY*/
    private async void OnButton_History(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_ArchiveOrders());
    }



    /*FCT DATABASE SHOWS BUTTON ORDERS*/
    public void LoadData()
    {
        var connectionString= "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection= new MySqlConnection(connectionString);
        connection.Open();

        var commandText= "SELECT * FROM `order_pm` WHERE State IS NULL;";
        using var command= new MySqlCommand(commandText, connection);
        using var reader= command.ExecuteReader();

        while(reader.Read())
        {
            var number_order= reader.GetDouble("idorder_pm");
            _items.Add($"ORDER N°{number_order}");
        }
    }


    /*FCT BUTTON SENDS THE N°ORDER TO THE PAGE WITH DETAILS*/
    private async void OnButton_OrderNumber(object sender, EventArgs e)
    {
        var button= sender as Button;
        var buttonText= button.Text;
        var orderNumber= buttonText.Replace("ORDER N°", "");
        await Navigation.PushAsync(new PM_OrderListBikes(orderNumber));
    }
}
