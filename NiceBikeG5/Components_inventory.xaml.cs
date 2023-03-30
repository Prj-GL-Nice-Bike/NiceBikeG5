using System.Windows.Input;

namespace NiceBikeG5;

public partial class Cinventory : ContentPage
{
	public Cinventory()
	{
		InitializeComponent();

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