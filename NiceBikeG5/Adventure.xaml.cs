using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;

namespace NiceBikeG5;

public partial class Adventure : ContentPage
{

    public Adventure()
    {
        InitializeComponent();

        //DEFAULT VALUES
        sizePicker.SelectedItem = 26;
        quantityPicker.SelectedItem = 1;
        colorPicker.SelectedItem = "Green";

    }

    // NAVIGATION BUTTONS
    private async void OnButton_Back(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SR_Catalogue());
    }
    private async void GoToCart(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Cart());
    }

    private async void AddToCart(object sender, EventArgs e)
    {
        // TYPE, SIZE, COLOR AND QUANTITY FROM THE PICKERS
        string productType = "Adventure";
        double productSize = double.Parse(sizePicker.SelectedItem.ToString());
        string productColor = colorPicker.SelectedItem.ToString();
        double productQuantity = double.Parse(quantityPicker.SelectedItem.ToString());

        // CONNECTION WITH MYSQL
        var connectionString = "Server=localhost;Database=bikes;Uid=root;Pwd=root;";
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        // INSERT NEW BIKE TO THE ORDERS TABLE
        var commandText = $"INSERT INTO orders (Type, Size, Color, Quantity) VALUES ('{productType}', '{productSize}', '{productColor}', '{productQuantity}')";
        using var command = new MySqlCommand(commandText, connection);
        command.ExecuteNonQuery();

        await DisplayAlert("Added to cart", "", "OK");

    }

    // TOTAL CALCULATION
    private void TotalPrice(object sender, EventArgs e)
    {
        if (int.TryParse(quantityPicker.SelectedItem.ToString(), out int selectedQuantityValue) && int.TryParse(sizePicker.SelectedItem.ToString(), out int selectedSizeValue))
        {
            if (selectedSizeValue == 26)
            {
                int total = selectedQuantityValue * 200;
                totalLabel.Text = $"{total} €";
            }
            if (selectedSizeValue == 28)
            {
                int total = selectedQuantityValue * 220;
                totalLabel.Text = $"{total} €";
            }
        }
    }
}
