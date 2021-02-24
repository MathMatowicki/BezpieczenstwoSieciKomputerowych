using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bezpieczeństwo.Models;
using Bezpieczeństwo.Algorithms;

namespace Bezpieczeństwo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            PrzestawieniaMacierzoweB b = new PrzestawieniaMacierzoweB();
            String tab = b.Cipher("Tojestszyfriniewieszcotujestnapisane");
            String result = b.Decipher("Tfst0jica0teus0enop0orzn0swja0zien0siti0yese0");
            ViewBag.tab = tab;
            ViewBag.result = result;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
