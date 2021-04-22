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
    public class RestaurantsQuery
    {
        public AppDB Db { get; }

        public RestaurantsQuery(AppDB db)
        {
            Db = db;
        }

        public async Task<Restaurants> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"Select restaurants.id, name, category, city, state, county, imagename, longitude, latitude From foodchoices.restaurants 
                                Inner Join foodchoices.coordinate
                                ON foodchoices.restaurants.Id = foodchoices.coordinate.restaurantId where foodchoices.restaurants.id = @id;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<List<Restaurants>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT  `Id`, `Name`, `Category`, `City`, `State`, `County`, `ImageName` FROM `foodchoices`.`Restaurants` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }


        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `foodchoices`.`Restaurants`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }


        private async Task<List<Restaurants>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Restaurants>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Restaurants(Db)
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Category = reader.GetString(2),
                        City = reader.GetString(3),
                        State = reader.GetString(4),
                        County = reader.GetString(5),
                        ImageName = reader.GetString(6),
                        Location = new Coordinate
                        {
                            Longitude = reader.GetDouble(7),
                            Latitude = reader.GetDouble(8)
                        }
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
