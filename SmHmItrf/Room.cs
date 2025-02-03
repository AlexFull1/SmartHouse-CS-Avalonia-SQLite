using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;

namespace SmHmItrf
{
    public class Room
    {
        public int ID { get; set; }
        public string RoomName { get; set; }
        public string PeripheralLightName { get; set; }
        public string PeripheralLightCode { get; set; }
        public string MainLightName { get; set; }
        public string MainLightCode { get; set; }
    }

    public class DatabaseService
    {
        private string connectionString = "Data Source=yourdatabase.db";

        public ObservableCollection<Room> GetRooms()
        {
            var rooms = new ObservableCollection<Room>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                SELECT 
                    r.room_id,
                    r.room_name,
                    p.object_name AS peripheral_light_name,
                    p.device_code AS peripheral_light_code,
                    m.object_name AS main_light_name,
                    m.device_code AS main_light_code
                FROM 
                    Rooms r
                LEFT JOIN PeripheralLighting p ON r.peripheral_light_id = p.peripheral_light_id
                LEFT JOIN MainLighting m ON r.main_light_id = m.main_light_id";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var room = new Room
                        {
                            ID = reader.GetInt32(0),
                            RoomName = reader.GetString(1),
                            PeripheralLightName = reader.GetString(2),
                            PeripheralLightCode = reader.GetString(3),
                            MainLightName = reader.GetString(4),
                            MainLightCode = reader.GetString(5)
                        };

                        rooms.Add(room);
                    }
                }
            }

            return rooms;
        }      
    }
}
