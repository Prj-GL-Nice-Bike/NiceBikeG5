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
            var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var commandText = "SELECT * FROM stockcomponents_pm;";

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
                Components.Add(new Component { Id = id, Name = Name, City = City, Explorer = Explorer,Adventure=Adventure, Size = Size, Color = Color, Quantity = Quantity });

            }


        }
        public async void UpdateQuantity()
        {
            var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var commandText = "SELECT * FROM stockcomponents_pm;";

            using var command = new MySqlCommand(commandText, connection);

            foreach (Component component in Components)
            {
                var commandText1 = "UPDATE `nicebike`.`stockcomponents_pm` SET `quantity` = @Quantity WHERE (`idcomponentstock` = @Id);";
                using var commande = new MySqlCommand(commandText1, connection);
                commande.Parameters.AddWithValue("@Quantity", component.Quantity);
                commande.Parameters.AddWithValue("@Id", component.Id);
                commande.ExecuteNonQuery();
            }
        }
    }
}
