using Projet_Beau_vélo.View;

namespace NiceBikeG5;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(Cart), typeof(Cart));
        Routing.RegisterRoute(nameof(SR_Menu), typeof(SR_Menu));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
