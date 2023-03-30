using System.Collections.ObjectModel;
using System.ComponentModel;
using MySql.Data.MySqlClient;


namespace NiceBikeG5;

public partial class SRListOfClients : ContentPage
{

    public SRListOfClients()
    {
        InitializeComponent();
        BindingContext = new ClientsViewModel();

    }

    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SRSellers());
    }

    private async void GoToSummary(object sender, SelectedItemChangedEventArgs args)
    {
            // Pass the selected client to the Summary page
            await Navigation.PushAsync(new Summary());
      
    }


}
// VIEW MODEL
public class ClientsViewModel
{
    public ObservableCollection<Client> Clients { get; }

    public ClientsViewModel()
    {
        Clients = new ObservableCollection<Client>();
        LoadData();
    }

    private async void LoadData()
    {
        // CONNECTION WITH MYSQL
        var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        var commandText = "SELECT * FROM clients_sr;";
        using var command = new MySqlCommand(commandText, connection);
        using var reader = command.ExecuteReader();

        // READS LINE BY LINE IN MYSQL DATABASE
        while (await reader.ReadAsync())
        {
            var id = reader.GetInt32("idclients");
            var clientName = reader.GetString("Name");
            var clientAddress = reader.GetString("Address");
            var clientPhone = reader.GetString("Phone");
            var clientEmail = reader.GetString("Email");
            var clientTVA = reader.GetString("TVA");
            Clients.Add(new Client { Id = id, ClientName = clientName, ClientAddress = clientAddress, ClientPhone = clientPhone, ClientEmail = clientEmail, ClientTVA = clientTVA });
        }

    }
}

// PARAMETERS
public class Client
{
    public int Id { get; set; }
    public string ClientName { get; set; }
    public string ClientAddress { get; set; }
    public string ClientPhone { get; set; }
    public string ClientEmail { get; set; }
    public string ClientTVA { get; set; }
}
