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
        _items = new ObservableCollection<string>();
        listView.ItemsSource = _items;
        LoadData();
    }


    /*FCT DATABASE WITH BUTTONS ORDER*/
    public void LoadData()
    {
        var connectionString = "Server=localhost;Database=orders;Uid=root;Pwd=root;";
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var commandText = "SELECT * FROM `order`;";
        using var command = new MySqlCommand(commandText, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var number_order = reader.GetDouble("idorder");
            _items.Add($"ORDER N°{number_order}");
        }
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var buttonText = button.Text;
        var orderNumber = buttonText.Replace("ORDER N°", ""); // récupère l'identifiant de commande à partir du texte du bouton
        await Navigation.PushAsync(new PM_OrderListBikes(orderNumber)); // passe l'identifiant de commande à la deuxième page
    }
}