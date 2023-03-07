namespace NiceBikeG5;

public partial class Summary : ContentPage
{
	public Summary()
	{
		InitializeComponent();

        
	}
    private async void BackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SRSellers));
    }
    private async void LogOut(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SRSellers());
    }
}