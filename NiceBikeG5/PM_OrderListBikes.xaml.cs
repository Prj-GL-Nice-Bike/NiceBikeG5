namespace NiceBikeG5;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;



public partial class PM_OrderListBikes: ContentPage
{
    public string OrderNumber;

    public PM_OrderListBikes(string orderNumber)
    {
        InitializeComponent();
        OrderNumber= orderNumber;
        LoadData(orderNumber);
        LoadData2(orderNumber);
        LoadData3(orderNumber);
        orderNumberLabel.Text= $"ORDER N°{orderNumber}";
    }


    /*FCT BUTTON BACK*/
    private async void OnButton_Back(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_ListOrders());
    }

    /*FCT BUTTON LIST OF ORDERS*/
    private async void OnButton_ListOrders(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_ListOrders());
    }

    /*FCT BUTTON INVENTORY*/
    private async void OnButton_Inventory(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Binventory());
    }

    /*FCT BUTTON DELIVERY*/
    private async void OnButton_Delivery(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_DeliveryOrders());
    }

    /*FCT BUTTON PLANNING*/
    private async void OnButton_Planning(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Production_Planning());
    }

    /*FCT BUTTON HISTORY*/
    private async void OnButton_History(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PM_ArchiveOrders());
    }





    /*CLASS BIKE FOR DATABASE*/
    public class Bike
    {
        public string Type {get; set;}
        public string Size {get; set;}
        public string Color {get; set;}
        public string IdOrder {get; set;}
        public string State {get; set;}
    }


    /*FCT DATABASE PART1:TO SEND -SHOWS BIKES TO SEND*/
    public void LoadData(string orderNumber)
    {
        var connectionString= "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection= new MySqlConnection(connectionString);
        connection.Open();

        var commandText= $"SELECT * FROM bike_pm WHERE idorder='{orderNumber}' AND State IS NULL;";
        using var command= new MySqlCommand(commandText, connection);
        using var reader= command.ExecuteReader();

        var bikes= new List<Bike>();
        while (reader.Read())
        {
            var bikeType= reader.GetString("Type");
            var bikeSize= reader.GetString("Size");
            var bikeColor= reader.GetString("Color");
            var bikeIdOrder= reader.GetString("idorder");
            var bikeState= reader.IsDBNull(reader.GetOrdinal("State")) ? null: reader.GetString("State");

            bikes.Add(new Bike {Type= bikeType, Size= bikeSize, Color= bikeColor, IdOrder= bikeIdOrder, State= bikeState});
        }
        listView.ItemsSource= bikes;
    }


    /*FCT BUTTON SEND ALL -BIKE'S STATE "NULL"-->"SEND"*/
    private async void OnButton_SendAll(object sender, EventArgs e)
    {
        var bikes = listView.ItemsSource.OfType<Bike>().ToList();
        var bikesent = 0;
        var bikenotsent = 0;
        var compo = 0.0;
        foreach (var bike in bikes)
        {
            var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

          

            var selectQuantitybCommandText = $"SELECT Quantity FROM nicebike.stockbikes_pm WHERE type = '{bike.Type}' AND size = '{bike.Size}' AND color = '{bike.Color}'";
            using var selectQuantitybCommand = new MySqlCommand(selectQuantitybCommandText, connection);
            var quantityb = (double)await selectQuantitybCommand.ExecuteScalarAsync();

            var selectQuantitycCommandText = $"SELECT Quantity FROM nicebike.stockcomponents_pm WHERE {bike.Type} > 0 ";
            using var selectQuantitycCommand = new MySqlCommand(selectQuantitycCommandText, connection);
            var quantities = new List<double>();
            using var reader = selectQuantitycCommand.ExecuteReader();
            {
                while (reader.Read())
                {
                    var quantity = reader.GetDouble("Quantity");
                    quantities.Add(quantity);
                }
            }
            reader.Close();

            var selectTypeCommandText = $"SELECT {bike.Type} FROM nicebike.stockcomponents_pm WHERE {bike.Type} > 0 ";
            using var selectTypeCommand = new MySqlCommand(selectTypeCommandText, connection);
            var Types = new List<double>();
            using var readertype = selectTypeCommand.ExecuteReader();
            {
                while (readertype.Read())
                {
                    var Type = readertype.GetDouble(bike.Type);
                    Types.Add(Type);
                }
            }
            readertype.Close();

            if (quantityb > 0 )
            {
                var bike_stock_command = $"UPDATE nicebike.stockbikes_pm SET Quantity = Quantity - 1 WHERE type = '{bike.Type}' and size ='{bike.Size}'and color ='{bike.Color}';";
                using var command_bike = new MySqlCommand(bike_stock_command, connection);
                await command_bike.ExecuteNonQueryAsync();

                var commandText = $"UPDATE bike_pm SET State='SEND' WHERE idorder='{bike.IdOrder}' AND Type='{bike.Type}' AND Size='{bike.Size}' AND Color='{bike.Color}'";
                using var command = new MySqlCommand(commandText, connection);
                await command.ExecuteNonQueryAsync();
                bikesent++;

            }
            else
            {
                var Stock = "sufficient";
                
                var counter = 0;
                foreach (var item in quantities)
                {
                    if (item > Types[counter])
                    {
                        counter++;
                    }
                    else
                    {
                        Stock = "insufficient" ;
                        compo += Types[counter] - item;
                        break;
                    }
                }
                if (Stock == "sufficient")
                {
                    var component_stock_command = $"UPDATE nicebike.stockcomponents_pm SET Quantity = Quantity - {bike.Type} WHERE {bike.Type} > 0;";
                    using var command_component = new MySqlCommand(component_stock_command, connection);
                    await command_component.ExecuteNonQueryAsync();

                    var commandText = $"UPDATE bike_pm SET State='SEND' WHERE idorder='{bike.IdOrder}' AND Type='{bike.Type}' AND Size='{bike.Size}' AND Color='{bike.Color}'";
                    using var command = new MySqlCommand(commandText, connection);
                    await command.ExecuteNonQueryAsync();
                    bikesent++;
                }
                if (Stock == "insufficient")
                {
                    bikenotsent++;
                    
                }
                
            }
        }
        await DisplayAlert( "Summary" , bikesent + " SENT and " + bikenotsent + " NOT SENT DUE TO " + compo + " components missing ", "OK");
        ((Button)sender).BackgroundColor= Color.FromRgb(128, 128, 128);
        ((Button)sender).IsEnabled= false;


        // Refresh the data source
        LoadData(OrderNumber);
        LoadData2(OrderNumber);
    }









    /*FCT DATABASE PART2:IN PROCESS -SHOWS BIKES IN PROCESS BY ASSEMBLER*/
    public void LoadData2(string orderNumber)
    {
        var connectionString= "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection= new MySqlConnection(connectionString);
        connection.Open();

        var commandText= $"SELECT * FROM bike_pm WHERE idorder='{orderNumber}' AND State='SEND';";
        using var command= new MySqlCommand(commandText, connection);
        using var reader= command.ExecuteReader();

        var bikes= new List<Bike>();
        while(reader.Read())
        {
            var bikeType= reader.GetString("Type");
            var bikeSize= reader.GetString("Size");
            var bikeColor= reader.GetString("Color");
            var bikeIdOrder= reader.GetString("idorder");
            var bikeState= reader.IsDBNull(reader.GetOrdinal("State")) ? null : reader.GetString("State");

            bikes.Add(new Bike {Type= bikeType, Size= bikeSize, Color= bikeColor, IdOrder= bikeIdOrder, State= bikeState});
        }
        listView2.ItemsSource= bikes;
    }


    /*FCT DATABASE PART3:FINISH -SHOWS FINISHED BIKES*/
    public void LoadData3(string orderNumber)
    {
        var connectionString= "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
        using var connection= new MySqlConnection(connectionString);
        connection.Open();

        var commandText= $"SELECT * FROM bike_pm WHERE idorder='{orderNumber}' AND State='FINISH';";
        using var command= new MySqlCommand(commandText, connection);
        using var reader= command.ExecuteReader();

        var bikes= new List<Bike>();
        while(reader.Read())
        {
            var bikeType= reader.GetString("Type");
            var bikeSize= reader.GetString("Size");
            var bikeColor= reader.GetString("Color");
            var bikeIdOrder= reader.GetString("idorder");
            var bikeState= reader.IsDBNull(reader.GetOrdinal("State")) ? null : reader.GetString("State");

            bikes.Add(new Bike {Type= bikeType, Size= bikeSize, Color= bikeColor, IdOrder= bikeIdOrder, State= bikeState });
        }
        listView3.ItemsSource= bikes;
    }





    /*FCT BUTTON FINALIZE -ORDER'S STATE "NULL"-->"FINISH"*/
    private async void OnButton_FinalizeOrder(object sender, EventArgs e)
    {
        bool answer= await Application.Current.MainPage.DisplayAlert(
        "ARE YOU SURE?",
        "This action finalizes this order and sends it to delivery.",
        "YES",
        "NO");

        if (answer==true)
        {
            ((Button)sender).BackgroundColor= Color.FromRgb(128, 128, 128);
            ((Button)sender).IsEnabled= false;


            var bike= ((Button)sender).BindingContext as Bike;
            var connectionString= "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection= new MySqlConnection(connectionString);
            connection.Open();

            var commandText= $"UPDATE order_pm SET State='FINISH' WHERE idorder_pm='{OrderNumber}'";
            using var command= new MySqlCommand(commandText, connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}