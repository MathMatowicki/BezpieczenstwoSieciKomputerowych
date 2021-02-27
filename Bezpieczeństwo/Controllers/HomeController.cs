using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Bezpieczeństwo.Models;
using Bezpieczeństwo.Algorithms;


namespace Bezpieczeństwo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env)
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
            return View();
        }

        [HttpPost]
        public IActionResult Algorithms(String key, int algorithm, int option, IFormFile file)
        {
            string filePath = "file.txt";
            var dir = _env.ContentRootPath;
            using (var fileStream = new FileStream(Path.Combine(dir, "file.txt"), FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }
            //var file = Request.Form.Files.Count != 0 ? Request.Form.Files[0] : null;
            if (file == null)
            {
                ViewBag.Message = "Nie wybrano obrazu do przesłania";
                return View("AddImage");
            }

            string code = System.IO.File.ReadAllText(filePath);

            //Uruchamianie algorytmow szkielet
            String result = "abcd";
            switch (algorithm)
            {
                case 1:
                    RailFence rf = new RailFence();
                    rf.PrepareKey(key);
                    if (option == 1)
                        result = rf.Cipher(code, key);
                    else
                        result = rf.Decrypt(code, key);
                    break;

                case 2:
                    PrzestawieniaMacierzoweA pma = new PrzestawieniaMacierzoweA();
                    pma.PrepareKey(key, '-');
                    if (option == 1)
                        result = pma.CipherString(code);
                    else
                        result = pma.DecipherString(code);
                    break;

                case 3:
                    PrzestawieniaMacierzoweB pmb = new PrzestawieniaMacierzoweB();
                    pmb.key(key);
                    if (option == 1)
                        result = pmb.Cipher(code, key);
                    else
                        result = pmb.Decipher(code, key);

                    break;
                default:
                    break;
            }
            ViewBag.key = key;
            ViewBag.option = option;
            ViewBag.algorithm = algorithm;
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
