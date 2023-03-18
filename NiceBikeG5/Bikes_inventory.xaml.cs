namespace NiceBikeG5;

public partial class Binventory : ContentPage
{
	public Binventory()
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
    private async void Gotocomponents(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Cinventory());
    }


}