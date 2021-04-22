using FoodChoicesAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace FoodChoicesAPI.Data
{
    public class CoordinateQuery
    {
        public AppDB Db { get; }

        public CoordinateQuery(AppDB db)
        {
            Db = db;
        }

        public async Task<Coordinate> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `RestaurantId`, `Longitude`, `Latitude` FROM `foodchoices`.`Coordinates` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<List<Coordinate>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT  `Id`, `RestaurantId`, `Longitude`, `Latitude` FROM `foodchoices`.`Coordinates` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }


        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `foodchoices`.`Coordinates`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }


        private async Task<List<Coordinate>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Coordinate>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Coordinate(Db)
                    {
                        Id = reader.GetInt32(0),
                        RestaurantId = reader.GetInt32(1),
                        Longitude = reader.GetDouble(2),
                        Latitude = reader.GetDouble(3)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
