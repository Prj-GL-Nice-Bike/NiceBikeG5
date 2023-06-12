namespace NiceBikeG5;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;



public partial class PM_DeliveryDetails : ContentPage
{
	public PM_DeliveryDetails(string orderNumber)
	{
		InitializeComponent();
        LoadData(orderNumber);
        LoadData2(orderNumber);
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








    /*CLASS BIKE FOR DATABASE*/
    public class Bike
    {
        public string Type {get; set;}
        public string Size {get; set;}
        public string Color {get; set;}
        public string Quantity {get; set;}
        public string IdOrder {get; set;}
    }


    /*FCT DATABASE  -SHOWS BIKES OF THE ORDER*/
    public void LoadData(string orderNumber)
    {
        var connectionString= "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection= new MySqlConnection(connectionString);
        connection.Open();

        var commandText= $"SELECT * FROM bike_sr WHERE idorder='{orderNumber}'";
        using var command= new MySqlCommand(commandText, connection);
        using var reader= command.ExecuteReader();

        var bikes= new List<Bike>();
        while (reader.Read())
        {
            var bikeType= reader.GetString("Type");
            var bikeSize= reader.GetString("Size");
            var bikeColor= reader.GetString("Color");
            var bikeQuantity= reader.GetString("Quantity");
            var bikeIdOrder= reader.GetString("idorder");

            bikes.Add(new Bike {Type= bikeType, Size= bikeSize, Color= bikeColor, Quantity= bikeQuantity, IdOrder= bikeIdOrder});
        }
        listView.ItemsSource= bikes;
    }





    /*CLASS CLIENT FOR DATABASE*/
    public class Client
    {
        public int Id {get; set;}
        public string ClientName {get; set;}
        public string ClientAddress {get; set;}
        public string ClientPhone {get; set;}
        public string ClientEmail {get; set;}
        public string ClientTVA {get; set;}

    }

    /*FCT DATABASE  -SHOWS CLIENT'S DETAILS OF THE ORDER*/
    public void LoadData2(string orderNumber)
    {
        var connectionString= "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection= new MySqlConnection(connectionString);
        connection.Open();

        var commandText1= $"SELECT * FROM order_pm WHERE idorder_pm='{orderNumber}'";
        using var command1= new MySqlCommand(commandText1, connection);
        using var reader1= command1.ExecuteReader();

        int idClient= 0;
        while (reader1.Read())
        {
            idClient= reader1.GetInt32("idclient");
        }
        reader1.Close();

        var commandText2= $"SELECT * FROM clients_sr WHERE idclients='{idClient}'";
        using var command2= new MySqlCommand(commandText2, connection);
        using var reader2= command2.ExecuteReader();

        var clients= new List<Client>();
        while (reader2.Read())
        {
            var id= reader2.GetInt32("idclients");
            var clientName= reader2.GetString("Name");
            var clientAddress= reader2.GetString("Address");
            var clientPhone= reader2.GetString("Phone");
            var clientEmail= reader2.GetString("Email");
            var clientTVA= reader2.GetString("TVA");

            clients.Add(new Client {Id= id, ClientName= clientName, ClientAddress= clientAddress, ClientPhone= clientPhone, ClientEmail= clientEmail, ClientTVA= clientTVA });
        }
        listView2.ItemsSource= clients;
    }  
}