using MySql.Data.MySqlClient;
using Microsoft.Maui.Controls;

namespace NiceBikeG5
{
    public partial class SRNewClient : ContentPage
    {
        public SRNewClient()
        {
            InitializeComponent();
        }

        private async void BackClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SRSellers());
        }
        private async void GoHome(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SR_Catalogue());
        }

        private async void AddClientClicked(object sender, EventArgs e)
        {
            // ENTRY DATA
            string clientName = Name.Text;
            string clientAddress = Address.Text;
            string clientPhone = Phone.Text;
            string clientEmail = Email.Text;
            string clientTVA = TVA.Text;

            // CONNECTION WITH MYSQL
            var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            // INSERT NEW CLIENT TO THE CLIENTS TABLE USING PARAMETERIZED QUERY
            var commandText = $"INSERT INTO clients_sr (Name, Address, Phone, Email, TVA) VALUES (@Name, @Address, @Phone, @Email, @TVA)";
            using var command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Name", clientName);
            command.Parameters.AddWithValue("@Address", clientAddress);
            command.Parameters.AddWithValue("@Phone", clientPhone);
            command.Parameters.AddWithValue("@Email", clientEmail);
            command.Parameters.AddWithValue("@TVA", clientTVA);
            command.ExecuteNonQuery();

            await Navigation.PushAsync(new SRListOfClients());
         
        }
    }
}
