using NiceBikeG5;

namespace NiceBikeG5;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(SRSellers), typeof(SRSellers));
        Routing.RegisterRoute(nameof(Cart), typeof(Cart));
        Routing.RegisterRoute(nameof(SR_Menu), typeof(SR_Menu));
        Routing.RegisterRoute(nameof(SR_Catalogue), typeof(SR_Catalogue));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
