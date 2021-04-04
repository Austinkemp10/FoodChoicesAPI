using FoodChoicesAPI.Data;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FoodChoicesAPI.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Deleted { get; set; }

        internal AppDB Db { get; set; }

        public Users()
        {

        }

        internal Users(AppDB db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `foodchoices`.`users` (`Name`, `Password`, `Age`, `DateCreated`, `Deleted`) VALUES (@name, @password, @Age, @DateCreated, @Deleted);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `foodchoices`.`users` SET `Name` = @name, `Password` = @password, `Age` = @age, `DateCreated` = @datecreated, `Deleted` = @deleted WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `foodchoices`.`users` WHERE `Id` = @id;";
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
                ParameterName = "@name",
                DbType = DbType.String,
                Value = Name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@password",
                DbType = DbType.String,
                Value = Password,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@age",
                DbType = DbType.Int32,
                Value = Age,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@datecreated",
                DbType = DbType.DateTime,
                Value = DateCreated,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@deleted",
                DbType = DbType.String,
                Value = Deleted,
            });
        }
    }
}
