using Microsoft.Maui.Controls;
using NiceBikeG5;
using System.Collections.ObjectModel;

using static System.Net.WebRequestMethods;

namespace Projet_Beau_vélo.View;

public class Bike
    {
        int size;
        string color;
        int quantity;
        string type;
        ImageSource image;
        


        public Bike(int size, string color, int quantity,string type, ImageSource image )
        {
            this.size = size;
            this.color = color;
            this.quantity = quantity;
            this.type = type;
            this.image = image;
        }

        public string Color { get { return color; } set { color = value; } }
        public int Size { get { return size; } set { size= value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public string Type { get { return type; } set { type = value; } }
        public ImageSource Image { get { return image; } set { image = value; } }

        


    }
public partial class Cart : ContentPage
{

    public List<Bike> Bikes { get; set; } = new();
    //public ObservableCollection<Bike> Bikes { get; set; } = new ();
    Random random = new Random();
    string[] Colours = { "red", "blue" };
    string[] Types = { "City", "Adventure","Explorer"};
    int Count;
    int nbr;
    
    public ImageSource Image = ImageSource.FromFile("blue_bike.jpeg");

   
    //string tape;
    public Cart()
	{
        InitializeComponent();
       


        
        //base.OnAppearing();
        //Bikes.ToList();

    }
    
	public void Addbike(object sender, EventArgs e)
	{
        Bikes.Add(new Bike(random.Next(26, 28), Colours[random.Next(Colours.Length)], random.Next(1, 20), Types[random.Next(Types.Length)], Image));

        foreach (Bike item in Bikes)
        {
            Count += item.Quantity;
            nbr += 1;
            //tape += item.Type;
            //B.Text = Count.ToString();
            //A.Text = tape;

        }
        Count = 0;
        nbr = 0;
        //tape = "";
    }

    public void CountTotal(object sender, EventArgs e)
    {
        foreach (Bike item in Bikes) 
        {
            Count += item.Quantity;
            nbr += 1;
            //B.Text = item.Image;
            //A.Text = nbr.ToString();
            Console.WriteLine(item.Type);


        }
        Count = 0;
        nbr = 0;
    }
    private async void ReturnPrevious(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SR_Menu)); 
    }
    private async void GotoClients(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }

    private void Add(Bike obj)
    {
        Bikes.Add(obj);
    }



}
