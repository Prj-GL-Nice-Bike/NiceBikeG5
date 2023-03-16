namespace NiceBikeG5;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;


public partial class PM_OrderListBikes: ContentPage
{
    public PM_OrderListBikes(string orderNumber)
    {
        InitializeComponent();
        LoadData(orderNumber);

        // Ajouter le numéro de commande dans le label correspondant
        orderNumberLabel.Text = $"ORDER N°{orderNumber}";

    }

    public class Bike
    {
        public string Type {get; set;}
        public string Size {get; set;}
        public string Color {get; set;}
    }



    public void LoadData(string orderNumber)
    {
        var connectionString = "Server=localhost;Database=orders;Uid=root;Pwd=root;";
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var commandText = $"SELECT * FROM bike WHERE idorder='{orderNumber}';";
        using var command = new MySqlCommand(commandText, connection);
        using var reader = command.ExecuteReader();

        var bikes = new List<Bike>();
        while (reader.Read())
        {
            var bikeType = reader.GetString("Type");
            var bikeSize = reader.GetString("Size");
            var bikeColor = reader.GetString("Color");

            bikes.Add(new Bike { Type = bikeType, Size = bikeSize, Color = bikeColor });
        }

        listView.ItemsSource = bikes;
    }
}

//public partial class PM_OrderListBikes: ContentPage
//{
//    public PM_OrderListBikes(string orderNumber)
//    {
//        InitializeComponent();
//        LoadData(orderNumber);
//    }

//    public class Bike
//    {
//        public string Type {get; set;}
//        public string Size {get; set;}
//        public string Color {get; set;}
//    }



//    public void LoadData(string orderNumber)
//    {
//        var connectionString = "Server=localhost;Database=orders;Uid=root;Pwd=root;";
//        using var connection = new MySqlConnection(connectionString);
//        connection.Open();

//        var commandText = $"SELECT * FROM bike WHERE idorder='{orderNumber}';";
//        using var command = new MySqlCommand(commandText, connection);
//        using var reader = command.ExecuteReader();

//        var bikes = new List<Bike>();
//        while (reader.Read())
//        {
//            var bikeType = reader.GetString("Type");
//            var bikeSize = reader.GetString("Size");
//            var bikeColor = reader.GetString("Color");

//            bikes.Add(new Bike {Type= bikeType, Size= bikeSize, Color= bikeColor});
//        }

//        listView.ItemsSource = bikes;
//    }
//}