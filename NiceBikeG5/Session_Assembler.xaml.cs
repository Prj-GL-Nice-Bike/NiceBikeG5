namespace NiceBikeG5;

public partial class Session_Assembler : ContentPage
{
    public Session_Assembler()
	{
		InitializeComponent();
	}
    private async void AssemblerOne(object sender, EventArgs e)
    {
        string assemblerName = ((Button)sender).Text;
        await Navigation.PushAsync(new Assembler_Bikes(assemblerName));
    }
    private async void AssemblerTwo(object sender, EventArgs e)
    {
        string assemblerName = ((Button)sender).Text;
        await Navigation.PushAsync(new Assembler_Bikes(assemblerName));
    }
    private async void AssemblerThree(object sender, EventArgs e)
    {
        string assemblerName = ((Button)sender).Text;
        await Navigation.PushAsync(new Assembler_Bikes(assemblerName));
    }
}
