using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InstaProj.Models;
using Microsoft.AspNetCore.Authorization;

namespace InstaProj.Controllers
{
    public class PrincipalController : Controller
    {
        public IActionResult Index(string isValid)
        {
            if(isValid != null)
            {
                var isTrue = String.Equals(isValid, "Success");
                if (isTrue)
                {
                    ViewBag.msg = "Usuario cadastrado com sucesso!";
                }
            }
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
