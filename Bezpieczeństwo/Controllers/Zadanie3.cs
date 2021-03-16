using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bezpieczeństwo.Controllers
{
    public class Zadanie3 : Controller
    {
        private readonly ILogger<Zadanie3> _logger;
        private readonly IHostingEnvironment _env;
        public Zadanie3(ILogger<Zadanie3> logger, IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Algorithms()
        {
            List<String> result = new List<string>();
            ViewBag.result = result;
            return View();
        }





    }
}
