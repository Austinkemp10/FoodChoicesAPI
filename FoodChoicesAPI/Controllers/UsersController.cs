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
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        public AppDB Db { get; }
        public UsersController(AppDB db)
        {
            Db = db;
        }


        // GET api/Users
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/Users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Users body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            body.DateCreated = DateTime.Now;
            body.Deleted = false;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Users body)
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            var result = await query.FindOneAsync(id);

            if (result is null)
                return new NotFoundResult();

            result.Name = body.Name;
            result.Password = body.Password;
            result.Age = body.Age;
            result.DateCreated = body.DateCreated;
            result.Deleted = body.Deleted;
            await result.UpdateAsync(id);
            return new OkObjectResult(result);
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.Deleted = true;
            await PutOne(id, result);
            return new OkResult();
        }

        // DELETE api/Users
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }
    }
}
