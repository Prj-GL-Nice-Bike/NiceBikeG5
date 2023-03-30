namespace NiceBikeG5;
using System;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;


public partial class Binventory : ContentPage
{
    
	public Binventory()
	{
		InitializeComponent();
        BindingContext = new BikeViewModel();
         

    }
    public void Update()
    {
        ((BikeViewModel)BindingContext).UpdateQuantity();
    }
    private async void Logout(object sender, EventArgs e)
    {
        Update();
        await Navigation.PushAsync(new Session_PM());
    }
    private async void BacktoMenu(object sender, EventArgs e)
    {
        Update();
        await Navigation.PushAsync(new PM_Menu());
    }
    private async void Gotocomponents(object sender, EventArgs e)
    {
        Update();
        await Navigation.PushAsync(new Cinventory());
    }
   
}