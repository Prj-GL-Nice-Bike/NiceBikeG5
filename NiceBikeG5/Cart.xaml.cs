using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.ObjectModel;


namespace NiceBikeG5
{
    public partial class Cart : ContentPage
    {
        private ObservableCollection<Order> _orders;

        public Cart()
        {
            InitializeComponent();
            BindingContext = new CartViewModel();

            //REFRESHES THE PAGE 
            _orders = ((CartViewModel)BindingContext).Orders;
        }

        // DELETE BUTTON
        private async void DeleteOrder(object sender, EventArgs e)
        {
            var button = (Button)sender;

            // BINDS THE PARAMETERS OF THE CLASS ORDER
            var order = (Order)button.BindingContext;

            // CONNECTION WITH MYSQL
            var connectionString = "Server=localhost;Database=bikes;Uid=root;Pwd=root;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var commandText = $"DELETE FROM orders WHERE idorders = {order.Id};";
            using var command = new MySqlCommand(commandText, connection);
            await command.ExecuteNonQueryAsync();

            _orders.Remove(order);
        }
        // NAVIGATION BUTTONS
        private async void ReturnPrevious(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SR_Catalogue));
        }

        private async void GotoClients(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SRSellers));
        }

    }

    // VIEW MODEL
    public class CartViewModel
    {
        public ObservableCollection<Order> Orders { get; }

        public CartViewModel()
        {
            Orders = new ObservableCollection<Order>();
            LoadData();
        }

        private async void LoadData()
        {
            // CONNECTION WITH MYSQL
            var connectionString = "Server=localhost;Database=bikes;Uid=root;Pwd=root;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var commandText = "SELECT * FROM orders;";
            using var command = new MySqlCommand(commandText, connection);
            using var reader = command.ExecuteReader();

            // READS LINE BY LINE IN MYSQL DATABASE
            while (await reader.ReadAsync())
            {
                var id = reader.GetInt32("idorders");
                var productName = reader.GetString("Type");
                var productSize = reader.GetDouble("Size");
                var productColor = reader.GetString("Color");
                var productQuantity = reader.GetDouble("Quantity");
                Orders.Add(new Order { Id = id, ProductName = productName, ProductSize = productSize, ProductColor = productColor, ProductQuantity = productQuantity });
            }
        }
    }

    // PARAMETERS
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double ProductSize { get; set; }
        public string ProductColor { get; set; }
        public double ProductQuantity { get; set; }
    }

}

