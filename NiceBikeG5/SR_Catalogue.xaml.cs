namespace NiceBikeG5;

public partial class SR_Catalogue: ContentPage
{
	public SR_Catalogue()
	{
		InitializeComponent();
	}

    /*FCT BUTTON BACK*/
    private async void OnButton_Back(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SR_Menu());
    }

    /*FCT BUTTON CART*/
    private async void OnButton_CartCatalogue(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Cart());
    }

    /*FCT IMAGEBUTTON BIKE N°1*/
    private async void OnButton_Bike1(object sender, EventArgs e)
    {
    }

    /*FCT IMAGEBUTTON BIKE N°2*/
    private async void OnButton_Bike2(object sender, EventArgs e)
    {
    }

    /*FCT IMAGEBUTTON BIKE N°3*/
    private async void OnButton_Bike3(object sender, EventArgs e)
    {
    }
}