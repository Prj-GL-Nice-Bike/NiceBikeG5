namespace NiceBikeG5;

public partial class Assembler_Bikes : ContentPage
{
	public Assembler_Bikes()
	{
		InitializeComponent();

        Grid ListGrid = (Grid)this.FindByName("ListGrid");

        RowDefinitionCollection rowDefinitions = new RowDefinitionCollection();

        // Add the first 2 rows in "List of clients" which contains the title and the header
        RowDefinition rowDefinitionTitle = new RowDefinition();
        rowDefinitionTitle.Height = new GridLength(60);
        RowDefinition rowDefinitionTop = new RowDefinition();
        rowDefinitionTop.Height = new GridLength(50);
        rowDefinitions.Add(rowDefinitionTitle);
        rowDefinitions.Add(rowDefinitionTop);

        ListGrid.RowDefinitions = rowDefinitions;
    }
    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}