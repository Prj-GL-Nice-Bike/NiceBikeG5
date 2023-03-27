using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;
using System.Windows.Input;

namespace NiceBikeG5;

public partial class Cinventory : ContentPage 
{
	public Cinventory()
	{
		InitializeComponent();
        BindingContext = new ComponentViewModel();
        

    }
   public async void UpdateQuantity()
    {
            var connectionString = "Server=localhost;Database=nicebike;Uid=root;Pwd=root;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
            var commandText = "SELECT * FROM stockcomponents;";

            using var command = new MySqlCommand(commandText, connection);
            using var reader = command.ExecuteReader();

        //foreach (Component bike in Components)
        //{
        //    if (bike.Quantity != reader.GetDouble("quantity"))
        //    {

        //        var commande = "UPDATE `nicebike`.`stockbikes` SET `quantity` = '20' WHERE(`idstockbikes` = '1');";
        //        using var commandu = new MySqlCommand(commande, connection);
        //    }
        //}


    }
   
    private async void Logout(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Session_PM());
    }
    private async void BacktoMenu(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_Menu());
    }
    private async void Gotobikes(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Binventory());
    }
}