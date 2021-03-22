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
    public class Zadanie3 : Controller
    {
        private readonly ILogger<Zadanie3> _logger;
        private readonly IHostingEnvironment _env;

        private int[] key_tab;
        private String key_lsfr;

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

            //key validation
            if (key == null || key.Length == 0 || key == "")
            {
                ViewBag.Message = "Nie podano żadnego klucza.";
                return View();
            }
            else
            {
                if(option == 2)
                {
                    if (!this.KeyCorrectnessDecipher(key))
                    {
                        ViewBag.Message = "Podczas deszyfrowania wiadomości algorytmem szyfr strumieniowy podanym kluczem muszą być 2 liczby oddzielone myślnikiem. " +
                                "Pierwsza z nich oznacza najwyższą potęgę wielomianu użytą podczas generowania klucza, a druga jest tym wygenerowanym kluczem.";
                        return View();
                    }
                }
                else {
                    if (!this.KeyCorrectnessCipher(key))
                    {
                        ViewBag.Message = "Podczas generowania klucza algorytmem Lsfr podanym kluczem muszą być liczby oddzielone myślnikami. " +
                                "Oznaczają one użyte potęgi wielomianu.";
                        return View();
                    }
                }
            }

            if (option != 3)
            {
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

                List<String> result = launchAlgorithmZad3(code, option);
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
            }
            else
            {
                //tylko lfsr
                this.PrepareKey();
            }


            ViewBag.Key = this.key_lsfr;
            return View();
        }

        public bool KeyCorrectnessCipher(String key)
        {
            String[] split_key = key.Split('-');
            int n = split_key.Length;
            if (n < 2) return false;

            int[] int_key = new int[n];
            for (int i = 0; i < n; i++) 
            {
                if (!int.TryParse(split_key[i], out int_key[i])) return false;
            }
            this.key_tab = int_key;
            return true;
        }

        public bool KeyCorrectnessDecipher(String key)
        {
            String[] split_key = key.Split('-');
            int n = split_key.Length;
            if (n != 2) return false;

            int[] int_key = new int[n];
            for (int i = 0; i < n; i++)
            {
                if (!int.TryParse(split_key[i], out int_key[i])) return false;
            }
            this.key_tab = int_key;
            return true;
        }

        private List<String> launchAlgorithmZad3(String[] code, int option)
        {
            List<String> result = new List<string>();
            //SzyfStrumieniowy ss = new SzyfrStrumieniowy();


            if (option == 1)
            {
                for (int i = 0; i < code.Length; i++)
                {
                    //WYWOŁANIE DZIAŁANIA ALGORYTMU LSFR I PRZYGOTOWANIE KLUCZA
                    this.PrepareKey();

                    //result.Add(ss.Cipher());
                }
            }
            else
            {
                for (int i = 0; i < code.Length; i++)
                {
                    this.key_lsfr = key_tab[1]+"";

                    //result.Add(ss.Decipher(key_tab));
                }
            }
            return result;
        }

        private byte[] PrepareKey()
        {
            byte[] key;
            Lsfr lsfr = new Lsfr(this.key_tab);
            //WYWOŁANIE DZIAŁANIA ALGORYTMU LSFR I PRZYGOTOWANIE KLUCZA
            key = lsfr.getBytes();

            this.key_lsfr = "";
            foreach(byte b in key)
            {
                this.key_lsfr = this.key_lsfr + b + " ";
            }

            return key;
        }

    }
}
