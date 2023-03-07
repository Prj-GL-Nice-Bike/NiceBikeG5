namespace NiceBikeG5;

public partial class City : ContentPage
{
	public City()
	{
		InitializeComponent();
	}
    private async void OnButton_Back(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SR_Catalogue());
    }
    private void AddToCart(object sender, EventArgs e)
    {
    }
    private async void GoToCart(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new Cart());
    }
}
