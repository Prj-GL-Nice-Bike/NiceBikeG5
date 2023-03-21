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
        public string IdOrder { get; set; }
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
            var bikeIdOrder = reader.GetString("idorder");

            bikes.Add(new Bike { Type = bikeType, Size = bikeSize, Color = bikeColor, IdOrder= bikeIdOrder });
        }

        listView.ItemsSource = bikes;
    }




    private async void OnSendClicked(object sender, EventArgs e)
    {
        var bike = ((Button)sender).BindingContext as Bike;

        // Insérer les données du vélo sélectionné dans la table bike_assembler
        var connectionString = "Server=localhost;Database=orders;Uid=root;Pwd=root;";
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var commandText = $"INSERT INTO bike_assembler (Type, Size, Color, idorder) VALUES ('{bike.Type}', '{bike.Size}', '{bike.Color}', '{bike.IdOrder}')";
        using var command = new MySqlCommand(commandText, connection);
        await command.ExecuteNonQueryAsync();

        // Afficher un message de confirmation
        await DisplayAlert("Confirmation", "Le vélo a été ajouté à la liste d'assemblage.", "OK");


        ((Button)sender).BackgroundColor = Color.FromRgb(128, 128, 128); // Changer la couleur du bouton en gris
        ((Button)sender).IsEnabled = false; // Désactiver le bouton
    }


    private async void ASSEMBLERPAGE(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ASSEMBLER_PAGE());
    }
}