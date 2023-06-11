namespace NiceBikeG5;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;



public partial class PM_DeliveryOrders: ContentPage
{
    private ObservableCollection<string> _items;
    public double OrderNumber;
    //private int clickCount= 0;

    public PM_DeliveryOrders()
    {
        InitializeComponent();
        _items= new ObservableCollection<string>();
        listView.ItemsSource= _items;
        LoadData();
    }


    /*FCT BUTTON BACK*/
    private async void OnButton_Back(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_ListOrders());
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






    /*CLASS BIKE FOR DATABASE*/
    public class Bike
    {
        public string Type {get; set;}
        public string Size {get; set;}
        public string Color {get; set;}
        public string IdOrder {get; set;}
        public string State {get; set;}
    }



    /*FCT DATABASE -SHOWS BUTTON ORDERS*/
    public void LoadData()
    {
        var connectionString= "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection= new MySqlConnection(connectionString);
        connection.Open();

        var commandText= "SELECT * FROM `order_pm` WHERE State='FINISH';";
        using var command= new MySqlCommand(commandText, connection);
        using var reader= command.ExecuteReader();

        while(reader.Read())
        {
            OrderNumber= reader.GetDouble("idorder_pm");
            _items.Add($"ORDER N°{OrderNumber}");
        }
    }


    /*FCT BUTTON -SENDS THE N°ORDER TO THE PAGE WITH DETAILS*/
    private async void OnButton_OrderNumber(object sender, EventArgs e)
    {
        var button= sender as Button;
        var buttonText= button.Text;
        var orderNumber= buttonText.Replace("ORDER N°", "");
        await Navigation.PushAsync(new PM_DeliveryDetails(orderNumber));
    }


    /*FCT BUTTON DELIVERED -ORDER'S STATE "NULL"-->"DELIVERED"*/
    private async void OnButton_Delivered(object sender, EventArgs e)
    {
        bool answer= await Application.Current.MainPage.DisplayAlert(
        "ARE YOU SURE?",
        "This action confirms that the order has been delivered",
        "YES",
        "NO");

        if (answer==true)
        {
            ((Button)sender).BackgroundColor= Color.FromRgb(128, 128, 128);
            ((Button)sender).IsEnabled= false;


            var bike= ((Button)sender).BindingContext as Bike;
            var connectionString= "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection= new MySqlConnection(connectionString);
            connection.Open();

            var commandText= $"UPDATE order_pm SET State='DELIVERED' WHERE idorder_pm='{OrderNumber}'";
            using var command= new MySqlCommand(commandText, connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}




//private async void OnButtonChanged(object sender, EventArgs e)
//{
//    Button button = (Button)sender;
//    clickCount++;

//    if (clickCount == 2)
//    {
//        button.IsEnabled = false;
//        ((Button)sender).BackgroundColor = Color.FromRgb(128, 128, 128);
//        clickCount = 0;
//    }

//    button.Text = "DELIVERED";
//}
