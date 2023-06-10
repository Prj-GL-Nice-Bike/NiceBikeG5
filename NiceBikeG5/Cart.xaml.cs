using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;



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
            // SENDER
            var button = (ImageButton)sender;

            // RETREIVES ORDER ASSOCIATED WITH BUTTON
            var order = (Order)button.BindingContext;

            // CONNECTION WITH MYSQL
            var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var commandText = $"DELETE FROM bike_sr WHERE idbiketha = {order.Id};";
            using var command = new MySqlCommand(commandText, connection);
            await command.ExecuteNonQueryAsync();

            ((CartViewModel)BindingContext).Orders.Remove(order);
            ((CartViewModel)BindingContext).UpdateTotal();

        }
        // NAVIGATION BUTTONS
        private async void OnButton_Back(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SR_Catalogue());
        }

        private async void OnButton_Home(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SR_Catalogue());
        }

        private async void GotoClients(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SRSellers));
        }

    }

    // VIEW MODEL
    public class CartViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set { if (_orders != value)
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
            set { if (value != _total)
                    {
                        _total = value;
                        OnPropertyChanged();
                    }
                }
        }

        public CartViewModel()
        {
            Orders = new ObservableCollection<Order>();
            LoadData();
        }

        private async void LoadData()
        {
            // CONNECTION WITH MYSQL
            var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var commandText = "SELECT * FROM bike_sr WHERE idorder IS NULL";
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


