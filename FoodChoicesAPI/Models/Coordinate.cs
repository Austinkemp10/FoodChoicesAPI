using FoodChoicesAPI.Data;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FoodChoicesAPI.Models
{
    public class Coordinate
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }


        internal AppDB Db { get; set; }

        public Coordinate()
        {

        }

        internal Coordinate(AppDB db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `foodchoices`.`Coordinate` (`RestaurantId`, `Longitude`, `Latitude`) VALUES (@restaurantId, @longitude, @latitude);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `foodchoices`.`Coordinate` SET `RestaurantId` = @restaurantId, `Longitude` = @longitude, `Latitude` = @latitude WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `foodchoices`.`Coordinate` WHERE `Id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }




        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@restaurantId",
                DbType = DbType.Int32,
                Value = RestaurantId,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@longitude",
                DbType = DbType.Double,
                Value = Longitude,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@latitude",
                DbType = DbType.Double,
                Value = Latitude,
            }); 
        }
    }
}
