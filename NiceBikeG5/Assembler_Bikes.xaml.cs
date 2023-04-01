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

        AssemblerConnected.Text = $"Connected as {assemblerName}";
    }

    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
    private async void ChooseBike(object sender, EventArgs e)
    {
        // R�cup�rer l'objet de donn�es de la ligne correspondante
        var button = sender as Button;
        var dataObject = button.BindingContext as Bike;

        // ID de la ligne � mettre � jour
        int id_ligne = dataObject.Id;
        string assembler_ligne = dataObject.AssignedAssembler;
        string assemblerNumber;

        // Nouvelle valeur pour la colonne "Assembler"
        if (AssemblerConnected.Text == "Connected as ASSEMBLER #1") 
            { 
                assemblerNumber = "1";
                if (assembler_ligne == "")
                {
                    assembler_ligne = "1";
                }
            }
        else if (AssemblerConnected.Text == "Connected as ASSEMBLER #2") 
            { 
                assemblerNumber = "2";
                if (assembler_ligne == "")
                {
                    assembler_ligne = "2";
                }
        }
        else 
            { 
                assemblerNumber = "3";
                if (assembler_ligne == "")
                {
                    assembler_ligne = "3";
                }
        }

        // Cha�ne de connexion MySQL
        var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";

        // Requ�te SQL pour mettre � jour la colonne "Assembler" dans la ligne sp�cifi�e
        string sql = "UPDATE bike_pm SET Assembler = @Assembler WHERE idbike_pm = @id_ligne";

        // Cr�er un objet de connexion MySQL
        using var connection = new MySqlConnection(connectionString);

        // Cr�er un objet de commande MySQL avec la requ�te SQL et la connexion associ�e
        using var command = new MySqlCommand(sql, connection);

        // Ajouter des param�tres pour la nouvelle valeur de la colonne "Assembler" et l'ID de la ligne
        command.Parameters.AddWithValue("@Assembler", assembler_ligne);
        command.Parameters.AddWithValue("@id_ligne", id_ligne);

        await connection.OpenAsync();

        // Ex�cuter la commande MySQL pour mettre � jour la ligne sp�cifi�e
        int rowsAffected = await command.ExecuteNonQueryAsync();

        connection.Close();

        string AssemblerName = "ASSEMBLER #" + assemblerNumber;
        await Navigation.PushAsync(new Assembler_Bikes(AssemblerName));
    }
    private async void CancelBike(object sender, EventArgs e)
    {
        // R�cup�rer l'objet de donn�es de la ligne correspondante
        var button = sender as Button;
        var dataObject = button.BindingContext as Bike;

        // ID de la ligne � mettre � jour
        int id_ligne = dataObject.Id;

        string assembler_ligne = dataObject.AssignedAssembler;
        string assemblerNumber;

        // Nouvelle valeur pour la colonne "Assembler"
        if (AssemblerConnected.Text == "Connected as ASSEMBLER #1") 
            { 
                assemblerNumber = "1";
                if (assembler_ligne == "1")
                {
                    assembler_ligne = "";
                }
            }
        else if (AssemblerConnected.Text == "Connected as ASSEMBLER #2") 
            { 
                assemblerNumber = "2";
                if (assembler_ligne == "2")
                {
                    assembler_ligne = "";
                }
        }
        else 
            { 
                assemblerNumber = "3";
                if (assembler_ligne == "3")
                {
                    assembler_ligne = "";
                }
        }

        // Cha�ne de connexion MySQL
        var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";

        // Requ�te SQL pour mettre � jour la colonne "Assembler" dans la ligne sp�cifi�e
        string sql = "UPDATE bike_pm SET Assembler = @Assembler WHERE idbike_pm = @id_ligne";

        // Cr�er un objet de connexion MySQL
        using var connection = new MySqlConnection(connectionString);

        // Cr�er un objet de commande MySQL avec la requ�te SQL et la connexion associ�e
        using var command = new MySqlCommand(sql, connection);

        // Ajouter des param�tres pour la nouvelle valeur de la colonne "Assembler" et l'ID de la ligne
        command.Parameters.AddWithValue("@Assembler", assembler_ligne);
        command.Parameters.AddWithValue("@id_ligne", id_ligne);

        await connection.OpenAsync();

        // Ex�cuter la commande MySQL pour mettre � jour la ligne sp�cifi�e
        int rowsAffected = await command.ExecuteNonQueryAsync();

        connection.Close();

        string AssemblerName = "ASSEMBLER #" + assemblerNumber;
        await Navigation.PushAsync(new Assembler_Bikes(AssemblerName));
    }
    private async void OnSendClicked(object sender, EventArgs e)
    {
        // R�cup�rer l'objet de donn�es de la ligne correspondante
        var button = sender as Button;
        var dataObject = button.BindingContext as Bike;

        // ID de la ligne � mettre � jour
        int id_ligne = dataObject.Id;
        string assignedAssembler = dataObject.AssignedAssembler;

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

            var commandText = $"UPDATE bike_pm SET State='FINISH' WHERE idorder='{id_ligne}'";
            using var command = new MySqlCommand(commandText, connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}
public class OrdersViewModel
{
    public ObservableCollection<Bike> Orders { get; }

    public OrdersViewModel()
    {
        Orders = new ObservableCollection<Bike>();
        LoadData();
    }

    private async void LoadData()
    {
        // CONNECTION WITH MYSQL
        var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        var commandText = "SELECT * FROM bike_pm WHERE State='SEND';";
        using var command = new MySqlCommand(commandText, connection);
        using var reader = command.ExecuteReader();

        // READS LINE BY LINE IN MYSQL DATABASE
        while (await reader.ReadAsync())
        {
            var id = reader.GetInt32("idbike_pm");
            var bikeType = reader.GetString("Type");
            var bikeSize = reader.GetInt32("Size");
            var bikeColor = reader.GetString("Color");
            var orderNumber = reader.GetString("idorder");
            var assignedAssembler = reader.GetString("Assembler");
            Orders.Add(new Bike { Id = id, Type = bikeType, Size = bikeSize, Color = bikeColor, OrderNumber = orderNumber, AssignedAssembler = assignedAssembler });
        }
    }
}

// PARAMETERS
