using Microsoft.Maui.Controls;
namespace NiceBikeG5;

public partial class Explorer : ContentPage
{
    string color = "";


    public Explorer()
    {
        InitializeComponent();

        //DEFAULT VALUES
        sizePicker.SelectedItem = 26;
        quantityPicker.SelectedItem = 1;
        colorPicker.SelectedItem = "Red";
    }

    //BUTTONS
    private async void OnButton_Back(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SR_Catalogue());
    }
    private async void AddToCart(object sender, EventArgs e)
    {
        color = colorPicker.SelectedItem.ToString();
        await DisplayAlert("Added to cart", "", "OK");

    }
    private async void GoToCart(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Cart());
    }

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
