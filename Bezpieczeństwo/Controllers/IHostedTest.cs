using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Bezpieczeństwo.Controllers
{
    public class IHostedTest : Controller
    {
        private Generator _generator;
        int[] bytes = { 0, 1, 1, 0, 1, 0, 1 };
        public IHostedTest(IHostedService generator)
        {
            _generator = generator as Generator;
            _generator.SetLsfr(bytes);
            _generator.SetActive(true);
        }
        
        public IActionResult Index()
        {
            ViewBag.test = _generator.GetOutput().numberOfIterations;
            return View();
        }
    }
}
