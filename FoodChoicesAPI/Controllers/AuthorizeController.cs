using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodChoicesAPI.Controllers
{
    [ApiController]
    [Route("authorize")]
    public class AuthorizeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Authorize/Index.cshtml");
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            return RedirectToAction("Index");
        }
    }
}
