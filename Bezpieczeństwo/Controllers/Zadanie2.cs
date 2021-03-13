using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bezpieczeństwo.Algorithms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bezpieczeństwo.Controllers
{
    public class Zadanie2 : Controller
    {
        private readonly ILogger<Zadanie2> _logger;
        private readonly IHostingEnvironment _env;
        public Zadanie2(ILogger<Zadanie2> logger, IHostingEnvironment env)
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
                PrzestawieniaMacierzoweC pmc = new PrzestawieniaMacierzoweC();
                if (!pmc.PrepareKey(key))
                {
                    ViewBag.Message = "W algorytmie Przestawienia Macierzowe C kluczem musi być wyraz.";
                    return View();
                }
            }
            else if (algorithm == 2)
            {
                Cezara cezar = new Cezara();
                if (!cezar.PrepareKey(key))
                {
                    ViewBag.Message = "W algorytmie szyfr Cezara kluczem musi być liczba";
                    return View();
                }
            }
            else if (algorithm == 3)
            {
                Vigenere vig = new Vigenere();
                if (!vig.PrepareKey(key))
                {
                    ViewBag.Message = "W algorytmie szyfr Vigenere'a kluczem musi być wyraz.";
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

            List<String> result = launchAlgorithmZad2(code, key, algorithm, option);
            ViewBag.result = result;

            filePath = "output2.txt";
            using (var fileStream = new FileStream(Path.Combine(dir, "output2.txt"), FileMode.Create, FileAccess.Write))
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

        private List<String> launchAlgorithmZad2(String[] code, String key, int algorithm, int option)
        {
            List<String> result = new List<string>();
            switch (algorithm)
            {
                case 1:
                    PrzestawieniaMacierzoweC pmc = new PrzestawieniaMacierzoweC();
                    pmc.PrepareKey(key);
                    if (option == 1)
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            result.Add(pmc.Cipher(code[i]));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            result.Add(pmc.Decipher(code[i]));
                        }
                    }
                    break;

                case 2:
                    Cezara cezar = new Cezara();
                    cezar.PrepareKey(key);
                    if (option == 1)
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            result.Add(cezar.Cipher(code[i], key));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            result.Add(cezar.Decrypt(code[i], key));
                        }
                    }
                    break;

                case 3:
                    Vigenere vig = new Vigenere();
                    vig.PrepareKey(key);
                    if (option == 1)
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            result.Add(vig.Cipher(code[i], key));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            result.Add(vig.Decrypt(code[i], key));
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
