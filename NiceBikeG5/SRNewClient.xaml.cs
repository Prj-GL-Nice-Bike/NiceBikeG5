
using static NiceBikeG5.SRListOfClients;

namespace NiceBikeG5;

public partial class SRNewClient : ContentPage
{
    SRListOfClients myListOfClients = new SRListOfClients();

    public SRNewClient()
	{

		InitializeComponent();
	}
    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SRSellers());
    }
    private async void AddClientClicked(object sender, EventArgs e)
    {
        //MyDictionarySingleton.Instance.AddToDictionary("Names", Name.Text);

        //myListOfClients.AddName(Name.Text);

        //myListOfClients.clientInfos["Names"].Add(Name.Text);
        //myListOfClients.clientInfos["Adresses"].Add(Adress.Text);
        //myListOfClients.clientInfos["Phones"].Add(Phone.Text);
        //myListOfClients.clientInfos["Emails"].Add(Email.Text);
        //myListOfClients.clientInfos["TVAS"].Add(TVA.Text);

        await Navigation.PushAsync(new SRListOfClients());
    }
}