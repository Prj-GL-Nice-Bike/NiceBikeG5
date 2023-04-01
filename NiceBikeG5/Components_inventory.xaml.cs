using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;
using System.Windows.Input;

namespace NiceBikeG5;

public partial class Cinventory : ContentPage 
{
	public Cinventory()
	{
		InitializeComponent();
        BindingContext = new ComponentViewModel();

    }
    public void Update()
    {
        ((ComponentViewModel)BindingContext).UpdateQuantity();
    }

    private async void Logout(object sender, EventArgs e)
    {
        Update();
        await Navigation.PushAsync(new MainPage());
    }
    private async void BacktoMenu(object sender, EventArgs e)
    {
        Update();
        await Navigation.PushAsync(new PM_Menu());
    }
    private async void Gotobikes(object sender, EventArgs e)
    {
        Update();
        await Navigation.PushAsync(new Binventory());
        
    }
}