namespace NiceBikeG5;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

    //BUTTONS LEADING TO SESSION OF USER'S PROFILE

	private async void Session_SR(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new Session_SR());
    }
    private async void Session_PM(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Session_PM());
    }
    private async void Session_Assembler(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Session_Assembler());
    }
}

