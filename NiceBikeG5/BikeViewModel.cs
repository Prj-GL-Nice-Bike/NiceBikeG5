using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceBikeG5
{
    public class BikeViewModel : Bike
    {
        private ObservableCollection<Bike> bikes;
        public ObservableCollection<Bike> Bikes
        {
            get { return bikes; }
            set
            {
                if (bikes != value)
                {
                    bikes = value;
                    OnPropertyChanged();
                    
                }
            }
        }
        public BikeViewModel()
        {
            Bikes = new ObservableCollection<Bike>();
            //UpdateQuantity();
            LoadData();

            
        }

        private async void LoadData()
        {
            // CONNECTION WITH MYSQL
            var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var commandText = "SELECT * FROM stockbikes_pm;";
            
            using var command = new MySqlCommand(commandText, connection);
            using var reader = command.ExecuteReader();

            // READS LINE BY LINE IN MYSQL DATABASE
            while (await reader.ReadAsync())
            {
                
                var id = reader.GetInt32("idstockbikes");
                var Type = reader.GetString("type");
                var Size = reader.GetDouble("size");
                var Color = reader.GetString("color");
                var Quantity = reader.GetDouble("quantity");

                    
                Bikes.Add(new Bike {Id = id, Type = Type, Size = Size, Color = Color, Quantity = Quantity});
               

            }
        }
        

        public async void UpdateQuantity()
        {
            var connectionString = "Server=pat.infolab.ecam.be;Port=63320;Database=nicebike;Uid=newuser;Pwd=pa$$word;";
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
            var commandText = "UPDATE `nicebike`.`stockbikes_pm` SET `quantity` = '10' WHERE(`idstockbikes` = '1');";
            


            using var command = new MySqlCommand(commandText, connection);
            //using var reader = command.ExecuteReader();
            //foreach (Bike bike in Bikes)
            //{
            //     bike.Quantity = reader.GetDouble("quantity");
            //    //var commandText = "UPDATE `nicebike`.`stockbikes` SET `quantity` = 10 WHERE (`idstockbikes` = 1);";

            //    //using var command = new MySqlCommand(commandText, connection);
            //    //using var reader = command.ExecuteReader();
            //    //command.Parameters.AddWithValue("@Quantity", 10);
            //    //command.Parameters.AddWithValue("@Id", 1);


            //}
                


                //using var command = new MySqlCommand("UPDATE `nicebike`.`stockbikes` SET `quantity` = '1' WHERE(`idstockbikes` = '1');", new MySqlConnection("Server=localhost;Database=nicebike;Uid=root;Pwd=root;"));
        }

    }
}
