namespace NiceBikeG5;

public partial class SRSellers : ContentPage
{
	public SRSellers()
	{
		InitializeComponent();
	}
    private async void OnButton_Back(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Cart());
    }

    private async void NewClientClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SRNewClient());
    }

    private async void ListOfClientsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SRListOfClients());
    }
}