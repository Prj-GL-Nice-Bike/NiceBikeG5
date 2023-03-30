using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NiceBikeG5
{
    public class PPVM:PriOrder
    {
        public string couleur;
        private ObservableCollection<PriOrder> priorders;
        public ObservableCollection<PriOrder> Priorders
        {
            set { SetProperty(ref priorders, value); }
            get { return priorders; }
        }
        public PPVM()
        {
            Priorders = new ObservableCollection<PriOrder>();

            LoadData();
        }
        private async void LoadData()
        {
            // CONNECTION WITH MYSQL
            var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var commandText = "SELECT * FROM orderspr_pm order by Priority;";

            using var command = new MySqlCommand(commandText, connection);
            using var reader = command.ExecuteReader();

            // READS LINE BY LINE IN MYSQL DATABASE
            while (await reader.ReadAsync())
            {

                var id = reader.GetInt32("idorderspr");
                var priority = reader.GetInt32("Priority");
                var color = Color.FromHex("#A9A9A9");
                if (priority == 1)
                {
                    color = Color.FromHex("#FF0000");
                }
                if (priority == 2)
                {
                    color = Color.FromHex("#00FF00");
                }
                if (priority == 3)
                {
                    color = Color.FromHex("#FFFFFF");
                }


                Priorders.Add(new PriOrder { Id = id, Priority = priority, Color = color });

            }
        }

        public async void UpdatePriority()
        {
            var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var commandText = "SELECT * FROM orderspr_pm order by Priority;";

            using var command = new MySqlCommand(commandText, connection);

            foreach (PriOrder order in Priorders)
            {
                var commandText1 = "UPDATE `nicebike`.`orderspr_pm` SET `Priority` = @Priority WHERE (`idorderspr` = @Id);";
                using var commande = new MySqlCommand(commandText1, connection);
                commande.Parameters.AddWithValue("@Priority", order.Priority);
                commande.Parameters.AddWithValue("@Id", order.Id);
                commande.ExecuteNonQuery();
                
            }
            Priorders = new ObservableCollection<PriOrder>(Priorders.OrderBy(x => x.Priority));


        }

    }
}
