namespace NiceBikeG5;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

public partial class ASSEMBLER_PAGE : ContentPage
{
	public ASSEMBLER_PAGE()
	{
		InitializeComponent();
        LoadData();
    }


    public class Bike
    {
        public string Type {get; set;}
        public string Size {get; set;}
        public string Color { get; set; }
        public string IdOrder { get; set; }
    }


    public void LoadData()
    {
        // Charger les données de la table bike_assembler
        var connectionString = "Server=localhost;Database=orders;Uid=root;Pwd=root;";
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var commandText = "SELECT * FROM bike_assembler";
        using var command = new MySqlCommand(commandText, connection);
        using var reader = command.ExecuteReader();

        var bikes = new List<Bike>();
        while (reader.Read())
        {
            var bikeType = reader.GetString("Type");
            var bikeSize = reader.GetString("Size");
            var bikeColor = reader.GetString("Color");
            var bikeIdOrder = reader.GetString("idorder");

            bikes.Add(new Bike { Type = bikeType, Size = bikeSize, Color = bikeColor, IdOrder = bikeIdOrder });
        }

        // Afficher les données dans une ListView
        listView.ItemsSource = bikes;
    }


}
