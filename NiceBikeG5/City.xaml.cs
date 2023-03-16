using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;

namespace NiceBikeG5;

public partial class City : ContentPage
{

    public City()
	{
		InitializeComponent();

        // DEFAULT VALUES
        sizePicker.SelectedItem = 26;
        quantityPicker.SelectedItem = 1;
        colorPicker.SelectedItem = "Red";
        totalLabel.Text = "200 €";

        // UPDATES TOTAL LABEL
        sizePicker.SelectedIndexChanged += TotalPrice;
        quantityPicker.SelectedIndexChanged += TotalPrice;

    }

    // NAVIGATION BUTTONS (BACK AND CART)
    private async void OnButton_Back(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SR_Catalogue());
    }
    private async void GoToCart(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Cart());
    }

    // ADD BIKE(S) DATA TO CART BUTTON
    private async void AddToCart(object sender, EventArgs e)
    {
        // TYPE, SIZE, COLOR AND QUANTITY FROM THE PICKERS
        string productType = "City";
        double productSize = double.Parse(sizePicker.SelectedItem.ToString());
        string productColor = colorPicker.SelectedItem.ToString();
        double productQuantity = double.Parse(quantityPicker.SelectedItem.ToString());
        // PRICE 
        double productPrice = CalculateTotalPrice(productSize, productQuantity);

        // CONNECTION WITH MYSQL
        var connectionString = "Server=localhost;Database=bikes;Uid=root;Pwd=root;";
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        // INSERT NEW BIKE TO THE ORDERS TABLE
        var commandText = $"INSERT INTO orders (Type, Size, Color, Quantity, Price) VALUES ('{productType}', '{productSize}', '{productColor}', '{productQuantity}', '{productPrice}')";
        using var command = new MySqlCommand(commandText, connection);
        command.ExecuteNonQuery();

        await DisplayAlert("Added to cart", "", "OK");
    }

    // TOTAL CALCULATION METHOD
    private double CalculateTotalPrice(double size, double quantity)
    {
        if (size == 26) {
            double total = quantity * 200;
            return total;
        }
        else if (size == 28)
        {
            double total = quantity * 220;
            return total;
        }
        else
        { return 0; }
    }
    // TOTAL LABEL METHOD
    private void TotalPrice(object sender, EventArgs e)
    {
        if (int.TryParse(quantityPicker.SelectedItem.ToString(), out int selectedQuantityValue) && int.TryParse(sizePicker.SelectedItem.ToString(), out int selectedSizeValue))
        {
            double totalPrice = CalculateTotalPrice(selectedSizeValue, selectedQuantityValue);
            totalLabel.Text = $"{totalPrice} €";
        }
    }
}
