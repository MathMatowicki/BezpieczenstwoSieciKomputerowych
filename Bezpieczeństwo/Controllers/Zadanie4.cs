using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bezpieczeństwo.Controllers
{
    public class Zadanie4 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Algorithms()
        {
            return View();
        }
    }
}
