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

        private async void AddClientClicked(object sender, EventArgs e)
        {
            // ENTRY DATA
            string clientName = Name.Text;
            string clientAdress = Adress.Text;
            string clientPhone = Phone.Text;
            string clientEmail = Email.Text;
            string clientTVA = TVA.Text;

            // CONNECTION WITH MYSQL
            var connectionString = "Server=localhost;Database=bikes;Uid=root;Pwd=root;";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            // INSERT NEW CLIENT TO THE CLIENTS TABLE USING PARAMETERIZED QUERY
            var commandText = $"INSERT INTO clients (Name, Adress, Phone, Email, TVA) VALUES (@Name, @Adress, @Phone, @Email, @TVA)";
            using var command = new MySqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Name", clientName);
            command.Parameters.AddWithValue("@Adress", clientAdress);
            command.Parameters.AddWithValue("@Phone", clientPhone);
            command.Parameters.AddWithValue("@Email", clientEmail);
            command.Parameters.AddWithValue("@TVA", clientTVA);
            command.ExecuteNonQuery();

            await Navigation.PushAsync(new Summary());
         
        }
    }
}
