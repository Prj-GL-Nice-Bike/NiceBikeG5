
using MySql.Data.MySqlClient;

using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;

namespace NiceBikeG5;

public partial class Production_Planning : ContentPage
{
    public int statew = 0;
    public int stateg = 0;
    public int stater = 0;
    public ObservableCollection<PriOrder> Orders;
    public Production_Planning()
	{
        
        InitializeComponent();
        BindingContext = new PPVM();
        Orders = ((PPVM)BindingContext).Priorders;

    }
    public void Update()
    {
        ((PPVM)BindingContext).UpdatePriority();
    }
    public void Load()
    {
        ((PPVM)BindingContext).UpdatePriority();
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
    private void SetLowP(object sender, EventArgs e)
    {
        var gray = Color.FromHex("#A9A9A9");
        var white = Color.FromHex("#CCCCCC");
        var button = (Button)sender;
        var order = (PriOrder)button.BindingContext;

        if (statew == 3 & order.Priority == 3 || stater == 0 & order.Priority == 3) { order.Color = gray; statew = 0; order.Priority = 4; Update(); }

        else if (statew == 0| statew == 3)
        {
            order.Priority = 3;
            order.Color = white;
            statew = 3;
            Update();
        }
        
       
        

    }
    private void SetNormalP(object sender, EventArgs e)
    {
        var green = Color.FromHex("#00AA00");
        var gray = Color.FromHex("#A9A9A9");
        var button = (Button)sender;
        var order = (PriOrder)button.BindingContext;

        if (stateg == 2 & order.Priority == 2 || stater == 0 & order.Priority == 2) { order.Color = gray; stateg = 0; order.Priority = 4; Update(); }

        else if (stateg == 0| stateg == 2)
        {
            order.Priority = 2;
            order.Color = green;
            stateg = 2;
            Update();
        }
    }
    public void SetHighP(object sender, EventArgs e)
    {
        var red = Color.FromHex("#CC0000");
        var gray = Color.FromHex("#A9A9A9");
        var button = (Button)sender;
        var order = (PriOrder)button.BindingContext;
        //if (order.Priority == 3 & order.Color == gray){ order.Color = black; }

        if (stater == 1 & order.Priority ==1 || stater == 0 & order.Priority == 1) { order.Color = gray; stater = 0; order.Priority = 4; Update(); }

        else if (stater == 0| stater == 1)
        {
            order.Priority = 1;
            order.Color= red;
            stater = 1;
            Update();
        }

    }
}