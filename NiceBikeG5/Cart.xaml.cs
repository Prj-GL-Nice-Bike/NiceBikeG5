using Microsoft.Maui.Controls;
using NiceBikeG5;
using System.Collections.ObjectModel;

using static System.Net.WebRequestMethods;

namespace NiceBikeG5;
public partial class Cart : ContentPage
{
    public Cart()
	{
        InitializeComponent();
    }
    
    private async void ReturnPrevious(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SR_Catalogue)); 
    }
    private async void GotoClients(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SRSellers));
    }
}
