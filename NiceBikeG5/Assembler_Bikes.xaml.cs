namespace NiceBikeG5;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Collections.ObjectModel;

public partial class Assembler_Bikes : ContentPage
{
    public Assembler_Bikes(string assemblerName)
	{
		InitializeComponent();
        BindingContext = new OrdersViewModel();

        AssemblerConnected.Text = $"Connecté en tant que {assemblerName}";

        //Grid ListGrid = (Grid)this.FindByName("ListGrid");

        //RowDefinitionCollection rowDefinitions = new RowDefinitionCollection();

        //// Add the first 2 rows in "List of clients" which contains the title and the header
        //RowDefinition rowDefinitionTitle = new RowDefinition();
        //rowDefinitionTitle.Height = new GridLength(60);
        //RowDefinition rowDefinitionTop = new RowDefinition();
        //rowDefinitionTop.Height = new GridLength(50);
        //rowDefinitions.Add(rowDefinitionTitle);
        //rowDefinitions.Add(rowDefinitionTop);

        //ListGrid.RowDefinitions = rowDefinitions;
    }
    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}
public class OrdersViewModel
{
    public ObservableCollection<Client> Orders { get; }

    public OrdersViewModel()
    {
        Orders = new ObservableCollection<Client>();
        LoadData();
    }

    private async void LoadData()
    {
        // CONNECTION WITH MYSQL
        var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        var commandText = "SELECT * FROM bike_assembler;";
        using var command = new MySqlCommand(commandText, connection);
        using var reader = command.ExecuteReader();

        // READS LINE BY LINE IN MYSQL DATABASE
        while (await reader.ReadAsync())
        {
            var id = reader.GetInt32("idbike_assembler");
            var bikeType = reader.GetString("Type");
            var bikeSize = reader.GetString("Size");
            var bikeColor = reader.GetString("Color");
            var orderNumber = reader.GetString("idorder");
            var assignedAssembler = reader.GetString("Assembler");
            Orders.Add(new Client { Id = id, BikeType = bikeType, BikeSize = bikeSize, BikeColor = bikeColor, OrderNumber = orderNumber, AssignedAssembler = assignedAssembler });
        }
    }
}

// PARAMETERS
public class Client
{
    public int Id { get; set; }
    public string BikeType { get; set; }
    public string BikeSize { get; set; }
    public string BikeColor { get; set; }
    public string OrderNumber { get; set; }
    public string AssignedAssembler { get; set; }
}