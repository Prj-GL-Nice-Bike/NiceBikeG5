using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceBikeG5
{
    public class ComponentViewModel : Component
    {
        private ObservableCollection<Component> components;
        public ObservableCollection<Component> Components
        {
            get { return components; }
            set
            {
                if (components != value)
                {
                    components = value;
                    OnPropertyChanged();

                }
            }
        }
        public ComponentViewModel()
        {
            Components = new ObservableCollection<Component>();
            //UpdateQuantity();
            LoadData();

        }
        private async void LoadData()
        {
            // CONNECTION WITH MYSQL
            var connectionString = "Server=localhost;Database=nicebike;Uid=root;Pwd=root;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var commandText = "SELECT * FROM stockcomponents;";

            using var command = new MySqlCommand(commandText, connection);
            using var reader = command.ExecuteReader();


            // READS LINE BY LINE IN MYSQL DATABASE
            while (await reader.ReadAsync())
            {

                var id = reader.GetInt32("idcomponentstock");
                var Name= reader.GetString("Name");
                var City = reader.GetString("City");
                var Explorer = reader.GetString("Explorer");
                var Adventure = reader.GetString("Adventure");
                var Size = reader.GetString("Size");
                var Color = reader.GetString("Color");
                var Quantity = reader.GetDouble("Quantity");
                //foreach (Component bike in Components)
                //{
                //    if (bike.Quantity != reader.GetDouble("quantity"))
                //    {

                //        var commande = "UPDATE `nicebike`.`stockbikes` SET `quantity` = '20' WHERE(`idstockbikes` = '1');";
                //        using var commandu = new MySqlCommand(commande, connection);
                //    }
                //}

                Components.Add(new Component { Id = id, Name = Name, City = City, Explorer = Explorer,Adventure=Adventure, Size = Size, Color = Color, Quantity = Quantity });

            }


        }
    }
}
