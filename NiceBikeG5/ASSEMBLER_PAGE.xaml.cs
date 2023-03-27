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
        var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var commandText = "SELECT * FROM bike_pm WHERE State='SEND'";
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
        listView.ItemsSource = bikes;
    }


    private async void OnSendClicked(object sender, EventArgs e)
    {
        bool answer = await Application.Current.MainPage.DisplayAlert(
        "YES",
        "Do you want to perform this action?",
        "Yes",
        "No");

        if (answer == true)
        {
            ((Button)sender).BackgroundColor = Color.FromRgb(128, 128, 128);
            ((Button)sender).IsEnabled = false;


            var bike = ((Button)sender).BindingContext as Bike;
            var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            var commandText = $"UPDATE bike_pm SET State='FINISH' WHERE idorder='{bike.IdOrder}' AND Type='{bike.Type}' AND Size='{bike.Size}' AND Color='{bike.Color}'";
            using var command = new MySqlCommand(commandText, connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}
