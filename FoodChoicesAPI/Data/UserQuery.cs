using FoodChoicesAPI.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace FoodChoicesAPI.Data
{    
    public class UserQuery
    {
        public AppDB Db { get; }

        public UserQuery(AppDB db)
        {
            Db = db;
        }

        public async Task<Users> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Name`, `Password`, `Age`, `DateCreated`, `Deleted` FROM `foodchoices`.`users` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<List<Users>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Name`, `Password`, `Age`, `DateCreated`, `Deleted` FROM `foodchoices`.`users` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }


        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `foodchoices`.`users`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }


        private async Task<List<Users>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Users>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Users(Db)
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Password = reader.GetString(2),
                        Age = reader.GetInt32(3),
                        DateCreated = reader.GetDateTime(4),
                        Deleted = reader.GetBoolean(5)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
