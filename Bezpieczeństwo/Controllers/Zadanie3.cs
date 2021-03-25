using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bezpieczeństwo.Controllers
{
    public class Zadanie3 : Controller
    {
        private readonly ILogger<Zadanie3> _logger;
        private readonly IHostingEnvironment _env;
        private Generator _generator;
        public Zadanie3(ILogger<Zadanie3> logger, IHostingEnvironment env, IHostedService generator)
        {
            _logger = logger;
            _env = env;
            _generator = generator as Generator;
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
        public IActionResult Algorithms(String key, int option, IFormFile file, String sequence)
        {
            string filePath = "file.rtf";
            string[] code;
            var dir = _env.ContentRootPath;
            ViewBag.Message = "";
            ViewBag.key = key;
            ViewBag.option = option;
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
            else
            {
                if (!this.KeyCorrectness(key))
                {
                    ViewBag.Message = "W algorytmie szyfr strumieniowy kluczem muszą być liczby oddzielone myślnikami.";
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

            List<String> result = launchAlgorithmZad3(code, key, option);
            ViewBag.result = result;

            using (var fileStream = new FileStream(Path.Combine(dir, "output3.txt"), FileMode.Create, FileAccess.Write))
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

        public bool KeyCorrectness(String key)
        {
            String[] split_key = key.Split('-');
            int n = split_key.Length;
            int[] int_key = new int[n];
            for (int i = 0; i < n; i++) 
            {
                if (!int.TryParse(split_key[i], out int_key[i])) return false;
            }
            return true;
        }

        private List<String> launchAlgorithmZad3(String[] code, String key, int option)
        {
            List<String> result = new List<string>();
            //SzyfStrumieniowy ss = new SzyfrStrumieniowy();

//WYWOŁANIE DZIAŁANIA ALGORYTMU LSFR I PRZYGOTOWANIE KLUCZA

            if (option == 1)
            {
                for (int i = 0; i < code.Length; i++)
                {
                    //result.Add(ss.Cipher(code[i]));
                } }
            else
            {
                for (int i = 0; i < code.Length; i++)
                {
                    //result.Add(ss.Decipher(code[i]));
                }
            }
            return result;
        }

    }
}
