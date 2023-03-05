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
    //CLICK ON THE BUTTON
    private void OnButton_LogIn(object sender, EventArgs e)
    {
        password= PasswordEntry.Text;
        //If Password: Next Page
        if(password=="123")
        {Navigation.PushAsync(new SR_Menu());}
        //If not Password: Error Message
        else
        {DisplayAlert("ERROR", "Incorrect password", "OK");}
    }

    //ENTER
    async void OnEntryCompleted(object sender, EventArgs e)
    {
        password= ((Entry)sender).Text;
        //If Password: Next Page
        if(password=="123")
        {await Navigation.PushAsync(new SR_Menu());}
        //If not Password: Error Message
        else
        {await DisplayAlert("ERROR", "Incorrect password", "OK");}
    }   
}
