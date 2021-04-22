using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodChoicesAPI.Controllers
{
    public class RestaurantsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
