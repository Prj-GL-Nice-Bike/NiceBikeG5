﻿using Microsoft.Maui.Controls;
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

    private async void OnButton_Home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SR_Catalogue());
    }

    private async void OnButton_CartCatalogue(object sender, EventArgs e)
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
        var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        // INSERT BIKE TO bike_sr
        var commandText = $"INSERT INTO bike_sr (Type, Size, Color, Quantity, Price) VALUES ('{productType}', '{productSize}', '{productColor}', '{productQuantity}', '{productPrice}')";
        using var command = new MySqlCommand(commandText, connection);
        command.ExecuteNonQuery();

        // INSERT BIKES TO bike_pm
        for (int i = 0; i < productQuantity; i++)
        {
            var commandText_pm = $"INSERT INTO bike_pm (Type, Size, Color) VALUES ('{productType}', '{productSize}', '{productColor}')";
            using var command_pm = new MySqlCommand(commandText_pm, connection);
            command_pm.ExecuteNonQuery();
        }


        await DisplayAlert("Added to cart", "", "OK");
    }

    // TOTAL CALCULATION METHOD
    static private double CalculateTotalPrice(double size, double quantity)
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
