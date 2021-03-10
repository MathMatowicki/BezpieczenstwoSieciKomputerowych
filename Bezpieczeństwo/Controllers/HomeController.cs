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
using System.Text.RegularExpressions;
using System.Text;

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
            List<String> result = new List<string>();
            ViewBag.result = result;
            return View();
        }

        [HttpPost]
        public IActionResult Algorithms(String key, int algorithm, int option, IFormFile file, String sequence)
        {
            string filePath = "file.rtf";
            string[] code;
            var dir = _env.ContentRootPath;
            ViewBag.Message = "";
            ViewBag.key = key;
            ViewBag.option = option;
            ViewBag.algorithm = algorithm;
            ViewBag.result = new List<String>();

            if (file == null && (sequence == null || sequence == ""))
            {
                ViewBag.Message = "Nie podano żadnego ciągu do szyforwania/deszyfrowania";
                return View();
            }
            if (file != null && sequence != null && sequence != "")
            {
                ViewBag.Message =
                    "Podano jednoczesnie tekst do szyfrowania/deszyfrowania, jak i plik, dlatego plik został poddany wybranej operacji";
            }

            //key validation
            if (key == null || key.Length == 0 || key == "")
            {
                ViewBag.Message = "Nie podano żadnego klucza.";
                return View();
            }
            else if (algorithm == 1)
            {
                RailFence rf = new RailFence();
                if (!rf.PrepareKey(key))
                {
                    ViewBag.Message = "W algorytmie Rail Fence kluczem musi być liczba.";
                    return View();
                }
            }
            else if (algorithm == 2)
            {
                PrzestawieniaMacierzoweA pma = new PrzestawieniaMacierzoweA();
                if (!pma.PrepareKey(key, '-'))
                {
                    ViewBag.Message = "W algorytmie Przestawienia Macierzowe A kluczem muszą być liczby oddzielone myślnikami.";
                    return View();
                }
            }
            else if (algorithm == 3)
            {
                PrzestawieniaMacierzoweB pmb = new PrzestawieniaMacierzoweB();
                if (!pmb.PrepareKey(key))
                {
                    ViewBag.Message = "W algorytmie Przestawienia Macierzowe B kluczem musi być wyraz.";
                    return View();
                }
            }

            if (file == null)
            {
                code = new string[1];
                code[0] = sequence;
            }

            else
            {
                string type = file.ContentType;

                filePath = "file.txt";
                using (var fileStream = new FileStream(Path.Combine(dir, "file.txt"), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }

                code = System.IO.File.ReadAllLines(filePath);

                ViewBag.type = type;
            }

            List<String> result = launchAlgorithm(code, key, algorithm, option);
            ViewBag.result = result;

            filePath = "output.txt";
            using (var fileStream = new FileStream(Path.Combine(dir, "output.txt"), FileMode.Create, FileAccess.Write))
            {
                foreach (String line in result)
                {
                    fileStream.Write(Encoding.UTF8.GetBytes(line), 0, line.Length);
                    fileStream.Write(Encoding.UTF8.GetBytes(System.Environment.NewLine), 0, System.Environment.NewLine.Length);
                }
            }

            ViewBag.code = code;
            return View();
        }

        public IActionResult AlgorithmsZad2()
        {
            List<String> result = new List<string>();
            ViewBag.result = result;
            return View();
        }

        [HttpPost]
        public IActionResult AlgorithmsZad2(String key, int algorithm, int option, IFormFile file, String sequence)
        {
            string filePath = "file.rtf";
            string[] code;
            var dir = _env.ContentRootPath;
            ViewBag.Message = "";
            ViewBag.key = key;
            ViewBag.option = option;
            ViewBag.algorithm = algorithm;
            ViewBag.result = new List<String>();

            if (file == null && (sequence == null || sequence == ""))
            {
                ViewBag.Message = "Nie podano żadnego ciągu do szyforwania/deszyfrowania";
                return View();
            }
            if (file != null && sequence != null && sequence != "")
            {
                ViewBag.Message =
                    "Podano jednoczesnie tekst do szyfrowania/deszyfrowania, jak i plik, dlatego plik został poddany wybranej operacji";
            }

            //key validation
            if (key == null || key.Length == 0 || key == "")
            {
                ViewBag.Message = "Nie podano żadnego klucza.";
                return View();
            }
            else if (algorithm == 1)
            {
                RailFence rf = new RailFence();
                if (!rf.PrepareKey(key))
                {
                    ViewBag.Message = "W algorytmie Rail Fence kluczem musi być liczba.";
                    return View();
                }
            }
            else if (algorithm == 2)
            {
                PrzestawieniaMacierzoweA pma = new PrzestawieniaMacierzoweA();
                if (!pma.PrepareKey(key, '-'))
                {
                    ViewBag.Message = "W algorytmie Przestawienia Macierzowe A kluczem muszą być liczby oddzielone myślnikami.";
                    return View();
                }
            }
            else if (algorithm == 3)
            {
                PrzestawieniaMacierzoweB pmb = new PrzestawieniaMacierzoweB();
                if (!pmb.PrepareKey(key))
                {
                    ViewBag.Message = "W algorytmie Przestawienia Macierzowe B kluczem musi być wyraz.";
                    return View();
                }
            }

            if (file == null)
            {
                code = new string[1];
                code[0] = sequence;
            }

            else
            {
                string type = file.ContentType;

                filePath = "file.txt";
                using (var fileStream = new FileStream(Path.Combine(dir, "file.txt"), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }

                code = System.IO.File.ReadAllLines(filePath);

                ViewBag.type = type;
            }

            List<String> result = launchAlgorithm(code, key, algorithm, option);
            ViewBag.result = result;

            filePath = "output.txt";
            using (var fileStream = new FileStream(Path.Combine(dir, "output.txt"), FileMode.Create, FileAccess.Write))
            {
                foreach (String line in result)
                {
                    fileStream.Write(Encoding.UTF8.GetBytes(line), 0, line.Length);
                    fileStream.Write(Encoding.UTF8.GetBytes(System.Environment.NewLine), 0, System.Environment.NewLine.Length);
                }
            }

            ViewBag.code = code;
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

        private List<String> launchAlgorithm(String[] code, String key, int algorithm, int option)
        {
            List<String> result = new List<string>();
            switch (algorithm)
            {
                case 1:
                    RailFence rf = new RailFence();
                    rf.PrepareKey(key);
                    if (option == 1)
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            result.Add(rf.Cipher(code[i], key));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            result.Add(rf.Decrypt(code[i], key));
                        }
                    }
                    break;

                case 2:
                    PrzestawieniaMacierzoweA pma = new PrzestawieniaMacierzoweA();
                    pma.PrepareKey(key, '-');
                    if (option == 1)
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            result.Add(pma.CipherString(code[i]));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            result.Add(pma.DecipherString(code[i]));
                        }
                    }
                    break;

                case 3:
                    PrzestawieniaMacierzoweB pmb = new PrzestawieniaMacierzoweB();
                    pmb.PrepareKey(key);
                    if (option == 1)
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            result.Add(pmb.Cipher(code[i]));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            result.Add(pmb.Decipher(code[i]));
                        }
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
