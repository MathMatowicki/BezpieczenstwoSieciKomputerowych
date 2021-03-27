using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bezpieczeństwo.Algorithms;
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
        public IActionResult Stop()
        {
            _generator.SetActive(false);
            return RedirectToAction("Algorithms");
        }

        [HttpPost]
        public IActionResult Algorithms(String key, int option, IFormFile file, String sequence)
        {
            string filePath = "file.rtf";
            byte[] code;
            var dir = _env.ContentRootPath;
            ViewBag.Message = "";
            ViewBag.key = key;
            ViewBag.option = option;
            ViewBag.result = new List<String>();
            ViewBag.lfsrActive = false;

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
            int[] key_table;
            ulong keyValue;
            if ((key == null || key.Length == 0 || key == "") && option != 1)
            {
                ViewBag.Message = "Nie podano żadnego klucza.";
                return View();
            }
            else
            {
                key_table = this.KeyCorrectness(key,out keyValue);
                if (key_table == null && option != 1)
                {
                    ViewBag.Message = "W algorytmie szyfr strumieniowy kluczem muszą być liczby oddzielone myślnikami(lfsr), a desszyfrowaniu pojedyncza liczba.";
                    return View();
                }
            }

            if (file == null)
            {
                code = ToBytesArray(sequence);
            }
            else
            {
                string type = file.ContentType;

                filePath = "file.txt";
                using (var fileStream = new FileStream(Path.Combine(dir, "file.txt"), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }

                code = System.IO.File.ReadAllBytes(filePath);

                ViewBag.type = type;
            }

            byte[] result = launchAlgorithmZad3(code, key_table, option, keyValue);

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

        public byte[] ToBytesArray(string seq)
        {
            char[] text = seq.ToCharArray();
            byte[] content = new byte[2*text.Length];
            for(int i = 0; i < 2*text.Length; i++)
            {
                content[i] = (byte)(text[i/2] >> (8 * i%2));
            }
            return content;
        }

        public int[] KeyCorrectness(String key, out ulong keyValue)
        {
            String[] split_key = key.Split('-');
            int n = split_key.Length;
            int[] int_key = new int[n];
            if (!ulong.TryParse(split_key[0], out keyValue))
                keyValue = 0;
            for (int i = 0; i < n; i++) 
            {
                if (!int.TryParse(split_key[i], out int_key[i])) return null;
            }
            return int_key;
        }

        private byte[] launchAlgorithmZad3(byte[] code, int[] key_table, int option, ulong key)
        {
            SzyfrStrumieniowy ss;
            if (option == 1)
            {
                ss = new SzyfrStrumieniowy(_generator.GetOutput());
                return ss.Cipher(code);
            }
            else if(option == 2)
            {
                ss = new SzyfrStrumieniowy(key);
                return ss.Decrypt();
            }
            else
            {
                _generator.SetLsfr(key_table);
                _generator.SetActive(true);
                ViewBag.lfsrActive = true;
                return null;
            }
        }

    }
}
