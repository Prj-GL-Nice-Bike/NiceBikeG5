namespace NiceBikeG5;

public partial class Session_SR: ContentPage
{
    string password;

    public Session_SR()
	{
		InitializeComponent();
	}

    /*FCT BUTTON BACK*/
    private async void OnButton_Back(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    /*FCT BUTTON LOGIN*/
    
    async void OnEntryCompleted(object sender, EventArgs e)
    {
        password = ((Entry)sender).Text;

        if (password == "123")
        {await Navigation.PushAsync(new SR_Menu());}

        else
        {await DisplayAlert("Error", "Incorrect password", "OK");}

    }
    private void OnButton_LogIn(object sender, EventArgs e)
    {
        password = PasswordEntry.Text;
        //If Password: Next Page
        if (password=="123")
        {Navigation.PushAsync(new SR_Menu());}
        //If not Password: Error Message
        else
        {DisplayAlert("ERROR", "Incorrect password", "OK");}
    }
    
}
