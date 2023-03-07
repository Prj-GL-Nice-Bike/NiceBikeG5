using NiceBikeG5;
using Microsoft.Maui.Controls;

namespace NiceBikeG5;

public partial class SR_Menu: ContentPage
{
	public SR_Menu()
	{
		InitializeComponent();
	}

    /*FCT BUTTON LOGOUT*/
    private async void OnButton_LogOut(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Session_SR());
    }

    /*FCT BUTTON CATALOGUE*/
    private async void OnButton_Catalogue(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SR_Catalogue());
    }
    private async void ListOrder(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Cart());
    }
}