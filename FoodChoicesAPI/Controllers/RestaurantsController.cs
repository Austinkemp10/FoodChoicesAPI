using FoodChoicesAPI.Data;
using FoodChoicesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodChoicesAPI.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        public AppDB Db { get; }
        public RestaurantsController(AppDB db)
        {
            Db = db;
        }


        // GET api/Restaurants
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new RestaurantsQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new RestaurantsQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/Restaurants
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Restaurants body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/Restaurants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Restaurants body)
        {
            await Db.Connection.OpenAsync();
            var query = new RestaurantsQuery(Db);
            var result = await query.FindOneAsync(id);

            if (result is null)
                return new NotFoundResult();

            result.Name = body.Name;
            result.Category = body.Category;
            result.City = body.City;
            result.State = body.State;
            result.County = body.County;
            result.ImageName = body.ImageName;
            await result.UpdateAsync(id);
            return new OkObjectResult(result);
        }

        // DELETE api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new RestaurantsQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            
            await PutOne(id, result);
            return new OkResult();
        }

        // DELETE api/Restaurants
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new RestaurantsQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }
    }
}
