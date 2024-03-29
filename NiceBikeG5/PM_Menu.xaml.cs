namespace NiceBikeG5;

public partial class PM_Menu : ContentPage
{
	public PM_Menu()
	{
		InitializeComponent();
	}

    /*FCT BUTTON LOGOUT*/
    private async void OnButton_LogOut(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    /*FCT BUTTON LIST OF ORDERS*/
    private async void OnButton_ListOrders(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_ListOrders());
    }

    /*FCT BUTTON INVENTORY*/
    private async void OnButton_Inventory(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Binventory());
    }
    private async void OnButton_Planning(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Production_Planning());
    }
}