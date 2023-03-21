using System.Collections.ObjectModel;
using System.ComponentModel;
using MySql.Data.MySqlClient;

namespace NiceBikeG5;

public partial class Summary : ContentPage
{
	public Summary()
	{
		InitializeComponent();
        BindingContext = new SummaryViewModel();
    }
    private async void BackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SRSellers));
    }
    private async void Confirm(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SRSellers));
    }
    private async void LogOut(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SRSellers());
    }
}

public class SummaryViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Client> Clients { get; }

    public SummaryViewModel()
    {
        Clients = new ObservableCollection<Client>();
        LoadData();
    }

    public async void LoadData()
    {
        // CONNECTION WITH MYSQL
        var connectionString = "Server=localhost;Database=bikes;Uid=root;Pwd=root;";
        using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        var commandText = "SELECT * FROM clients ORDER BY idclients DESC LIMIT 1;";
        using var command = new MySqlCommand(commandText, connection);
        using var reader = command.ExecuteReader();

        // READS LINE BY LINE IN MYSQL DATABASE
        while (await reader.ReadAsync())
        {
            var id = reader.GetInt32("idclients");
            var clientName = reader.GetString("Name");
            var clientAdress = reader.GetString("Adress");
            var clientPhone= reader.GetString("Phone");
            var clientEmail = reader.GetString("Email");
            var clientTVA = reader.GetString("TVA");
            Clients.Add(new Client { Id = id, ClientName = clientName, ClientAdress = clientAdress, ClientPhone = clientPhone, ClientEmail = clientEmail, ClientTVA = clientTVA });
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public class Client
    {

        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientAdress { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public string ClientTVA { get; set; }
        
    }
}
