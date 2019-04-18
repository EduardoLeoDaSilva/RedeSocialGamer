using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstaProj.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Home()
        {
           ViewBag.msg= User.Identity.Name;
            return View();
        }
    }
}