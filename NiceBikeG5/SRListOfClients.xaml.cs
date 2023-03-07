using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Xml.Linq;

namespace NiceBikeG5;

public class MyDictionarySingleton
{
    private static readonly Lazy<MyDictionarySingleton> lazy = new Lazy<MyDictionarySingleton>(() => new MyDictionarySingleton());

    public static MyDictionarySingleton Instance { get { return lazy.Value; } }

    private Dictionary<string, List<string>> myDictionary;

    public Dictionary<string, List<string>> Dictionary { get; } = new Dictionary<string, List<string>>();
    private MyDictionarySingleton()
    {
        myDictionary = new Dictionary<string, List<string>>();
    }

    public void AddToDictionary(string key, string value)
    {
        if (!myDictionary.ContainsKey(key))
        {
            myDictionary[key] = new List<string>();
        }
        myDictionary[key].Add(value);
    }

    public List<string> GetDictionaryValues(string key)
    {
        if (myDictionary.ContainsKey(key))
        {
            return myDictionary[key];
        }
        return null;
    }
}


public partial class SRListOfClients : ContentPage
{
    Dictionary<string, List<string>> clientInfos;
    public Dictionary<string, List<string>> ClientInfos { get { return new Dictionary<string, List<string>>(clientInfos); } }

    //public Dictionary<string, List<string>> clientInfos = new Dictionary<string, List<string>>()
    //{
    //    {"Names", new List<string>{"Victor"} },
    //    { "Adresses", new List<string> { "Dilbeek" } },
    //    { "Phones", new List<string> { "+32 123 456" } },
    //    { "Emails", new List<string> { "20253@ecam.be" } },
    //    { "TVAS", new List<string> { "987 654" } }
    //};

    int rowStart = 2;
    int rowIterator = 0;

    int ColumnIterator = 0;

    public SRListOfClients()
    {
        InitializeComponent();

        //First Method
        clientInfos = new Dictionary<string, List<string>>();

        clientInfos.Add("Names", new List<string> { "Victor" });
        clientInfos.Add("Adresses", new List<string> { "Dilbeek" });
        clientInfos.Add("Phones", new List<string> { "+32 123 456" });
        clientInfos.Add("Emails", new List<string> { "20253@ecam.be" });
        clientInfos.Add("TVAS", new List<string> { "987 654" });

        //clientInfos["Names"].Add("Oussama");

        //clientInfos["Adresses"].Add("Schaerbeek");
        //clientInfos["Phones"].Add("+32 684 238");
        //clientInfos["Emails"].Add("oussama@ecam.be");
        //clientInfos["TVAS"].Add("657 895");

        Grid ListGrid = (Grid)this.FindByName("ListGrid");

        RowDefinitionCollection rowDefinitions = new RowDefinitionCollection();

        // Add the first 2 rows in "List of clients" which contains the title and the header
        RowDefinition rowDefinitionTitle = new RowDefinition();
        rowDefinitionTitle.Height = new GridLength(60);
        RowDefinition rowDefinitionTop = new RowDefinition();
        rowDefinitionTop.Height = new GridLength(50);
        rowDefinitions.Add(rowDefinitionTitle);
        rowDefinitions.Add(rowDefinitionTop);

        // Parcourir la liste d'éléments et ajouter une nouvelle balise RowDefinition à la collection pour chaque élément
        for (int i = 0; i < clientInfos["Names"].Count; i++)
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(80);
            rowDefinitions.Add(rowDefinition);
        }

        ListGrid.RowDefinitions = rowDefinitions;

        // Add every client's infos in the "List of clients"
        foreach (KeyValuePair<string, List<string>> entry in clientInfos)
        {
            rowIterator = 0;
            foreach (string value in entry.Value)
            {
                ListGrid.Add(new Label
                {
                    Text = value,
                    FontSize = 20,
                    Margin = 20,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center
                }, ColumnIterator, rowStart + rowIterator);
                rowIterator++;
            }
            if (ColumnIterator == 5)
            {
                ColumnIterator = 0;
            }
            else { ColumnIterator++; }
        }


        //Second Method
        //MyDictionarySingleton.Instance.AddToDictionary("Names", "Victor");
        //MyDictionarySingleton.Instance.AddToDictionary("Adresses", "Dilbeek");
        //MyDictionarySingleton.Instance.AddToDictionary("Phones", "+32 123 456");
        //MyDictionarySingleton.Instance.AddToDictionary("Emails", "20253@ecam.be");
        //MyDictionarySingleton.Instance.AddToDictionary("TVAS", "987 654");

        //for (int i = 0; i < MyDictionarySingleton.Instance.GetDictionaryValues("Names").Count; i++)
        //{
        //    RowDefinition rowDefinition = new RowDefinition();
        //    rowDefinition.Height = new GridLength(80);
        //    rowDefinitions.Add(rowDefinition);
        //}

        //ListGrid.RowDefinitions = rowDefinitions;

        //foreach (KeyValuePair<string, List<string>> keyValuePair in MyDictionarySingleton.Instance.Dictionary)
        //{
        //    rowIterator = 0;
        //    foreach (string value in keyValuePair.Value)
        //    {
        //        ListGrid.Add(new Label
        //        {
        //            Text = value,
        //            FontSize = 20,
        //            Margin = 20,
        //            HorizontalOptions = LayoutOptions.Start,
        //            VerticalOptions = LayoutOptions.Center
        //        }, ColumnIterator, rowStart + rowIterator);
        //        rowIterator++;
        //    }
        //    if (ColumnIterator == 5)
        //    {
        //        ColumnIterator = 0;
        //    }
        //    else { ColumnIterator++; }
        //}
    }
    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SRSellers());
    }
}