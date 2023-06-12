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
        
        await Navigation.PushAsync(new MainPage());
    }
    private async void BacktoMenu(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new PM_Menu());
    }
    private async void Gotocomponents(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new Cinventory());
    }
    private async void Apply(object sender, EventArgs e)
    {
        Update();
        var button = (Button)sender;
        button.BackgroundColor = Color.FromHex("#818181");
        button.TextColor = Color.FromHex("#000000");
    }

}