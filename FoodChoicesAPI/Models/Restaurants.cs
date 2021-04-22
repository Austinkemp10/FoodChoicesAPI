using FoodChoicesAPI.Data;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FoodChoicesAPI.Models
{
    public class Restaurants
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public Coordinate Location { get; set; }
        public string ImageName { get; set; }

        internal AppDB Db { get; set; }

        public Restaurants()
        {

        }

        internal Restaurants(AppDB db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `foodchoices`.`Restaurants` (`Name`, `Category`, `City`, `State`, `County`, `ImageName` ) VALUES (@Name, @Category, @City, @State, @County, @ImageName);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `foodchoices`.`Restaurants` SET `Name` = @Name, `Category` = @Category, `City` = @City, `State` = @State, `County` = @County, `ImageName` = @ImageName WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `foodchoices`.`Restaurants` WHERE `Id` = @id;";
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
                ParameterName = "@Name",
                DbType = DbType.String,
                Value = Name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Category",
                DbType = DbType.String,
                Value = Category,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@City",
                DbType = DbType.String,
                Value = City,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@State",
                DbType = DbType.String,
                Value = State,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@County",
                DbType = DbType.String,
                Value = County,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@ImageName",
                DbType = DbType.String,
                Value = ImageName,
            });
        }

    }
}
