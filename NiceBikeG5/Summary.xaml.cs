using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;

namespace NiceBikeG5;

public partial class Summary : ContentPage
{
    private Client _client;

    public Summary(Client client)
    {
        InitializeComponent();
        _client = client;
        BindingContext = new SummaryViewModel();
        (BindingContext as SummaryViewModel)?.LoadData(client.Id);
    }

    // BUTTONS
    private async void BackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SRSellers));
    }
    private async void LogOut(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SRSellers());
    }


    private async void Confirm(object sender, EventArgs e)
    {
        
        bool answer = await Application.Current.MainPage.DisplayAlert(
        "ARE YOU SURE?",
        "This action confirms the order and sends it to the PM.",
        "YES",
        "NO");

        if (answer)
        {
            // GOES BACK TO MENU PAGE
            await Navigation.PushAsync(new SR_Menu());

            // ASSIGNS ID ORDER NUMBER TO BIKES
            // CONNECTION WITH MYSQL
            var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            // MAX IDORDER + 1
            var selectMaxIdOrderCommandText = "SELECT MAX(idorder) FROM bike_sr";
            using var selectMaxIdOrderCommand = new MySqlCommand(selectMaxIdOrderCommandText, connection);
            var maxIdOrderObj = await selectMaxIdOrderCommand.ExecuteScalarAsync();
            // If MAX IDORDER = NULL
            int newIdOrder = 1;
            if (maxIdOrderObj != null && maxIdOrderObj != DBNull.Value)
            {
                newIdOrder = Convert.ToInt32(maxIdOrderObj) + 1;
            }

            // UPDATE bike_sr
            var updateCommandText = "UPDATE bike_sr SET idorder = @newIdOrder WHERE idorder IS NULL";
            using var updateCommand = new MySqlCommand(updateCommandText, connection);
            updateCommand.Parameters.AddWithValue("@newIdOrder", newIdOrder);
            var affectedRows = await updateCommand.ExecuteNonQueryAsync();

            // UPDATE bike_pm
            var updateCommandText_pm = "UPDATE bike_pm SET idorder = @newIdOrder_pm WHERE idorder IS NULL";
            using var updateCommand_pm = new MySqlCommand(updateCommandText_pm, connection);
            updateCommand_pm.Parameters.AddWithValue("@newIdOrder_pm", newIdOrder.ToString());
            var affectedRows_pm = await updateCommand_pm.ExecuteNonQueryAsync();

            // UPDATE order_pm
            var updateCommandText_order = "UPDATE order_pm SET idorder_pm = @newIdOrder_order WHERE idorder_pm IS NULL";
            using var updateCommand_order = new MySqlCommand(updateCommandText_order, connection);
            updateCommand_order.Parameters.AddWithValue("@newIdOrder_order", newIdOrder);
            var affectedRows_order = await updateCommand_order.ExecuteNonQueryAsync();

            if (affectedRows > 0 || affectedRows_pm > 0 || affectedRows_order > 0)
            {
                // UPDATE COUNTER
                var updateCounterCommandText = "UPDATE counter_sr SET counter = @newIdOrder";
                using var updateCounterCommand = new MySqlCommand(updateCounterCommandText, connection);
                updateCounterCommand.Parameters.AddWithValue("@newIdOrder", newIdOrder + 1);
                await updateCounterCommand.ExecuteNonQueryAsync();

                var updateCounterCommandText_pm = "UPDATE counter_pm SET counter = @newIdOrder_pm";
                using var updateCounterCommand_pm = new MySqlCommand(updateCounterCommandText_pm, connection);
                updateCounterCommand_pm.Parameters.AddWithValue("@newIdOrder_pm", newIdOrder + 1);
                await updateCounterCommand_pm.ExecuteNonQueryAsync();
            }
        }
    }
}

public class SummaryViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Client> Clients { get; }
    private ObservableCollection<Order> _orders;
    public ObservableCollection<Order> Orders
    {
        get { return _orders; }
        set
        {
            if (_orders != value)
            {
                _orders = value;
                OnPropertyChanged();
                UpdateTotal();
            }
        }
    }
    private double _total;
    public double Total
    {
        get => _total;
        set
        {
            if (value != _total)
            {
                _total = value;
                OnPropertyChanged();
            }
        }
    }

    public SummaryViewModel() 
    {
        Clients = new ObservableCollection<Client>();
        Orders = new ObservableCollection<Order>();
        LoadData2();
    }

    public async void LoadData(int clientId)
    {
        // CONNECTION WITH MYSQL
        var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        // READS SELECTED CLIENT DATA
        var commandText = "SELECT * FROM clients_sr WHERE idclients = @clientId;";
        using var command = new MySqlCommand(commandText, connection);
        command.Parameters.AddWithValue("@clientId", clientId);
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

    public class Client
    {

        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public string ClientTVA { get; set; }

    }

    public async void LoadData2()
    {
        // CONNECTION WITH MYSQL
        var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        var commandText = "SELECT * FROM bike_sr WHERE idorder IS NULL;";
        using var command = new MySqlCommand(commandText, connection);
        using var reader = command.ExecuteReader();

        // READS LINE BY LINE IN MYSQL DATABASE
        while (await reader.ReadAsync())
        {
            var id = reader.GetInt32("idbike");
            var productName = reader.GetString("Type");
            var productSize = reader.GetDouble("Size");
            var productColor = reader.GetString("Color");
            var productQuantity = reader.GetDouble("Quantity");
            var productPrice = reader.GetDouble("Price");
            Orders.Add(new Order { Id = id, ProductName = productName, ProductSize = productSize, ProductColor = productColor, ProductQuantity = productQuantity, ProductPrice = productPrice});
        }
        UpdateTotal();
        
    }
    public void UpdateTotal()
    {
        double totalPrice = 0;
        foreach (Order order in Orders)
        {
            totalPrice += order.ProductPrice;
        }
        Total = totalPrice;
    }
    public event PropertyChangedEventHandler PropertyChanged;

    private void Order_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        UpdateTotal();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    // PARAMETERS
    public class Order : INotifyPropertyChanged
    {
        private double _productPrice;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get; set; }
        public string ProductName { get; set; }
        public double ProductSize { get; set; }
        public string ProductColor { get; set; }
        public double ProductQuantity { get; set; }
        public double ProductPrice
        {
            get { return _productPrice; }
            set
            {
                if (_productPrice != value)
                {
                    _productPrice = value;
                    OnPropertyChanged();
                }
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
