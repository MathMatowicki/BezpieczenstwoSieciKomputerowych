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
        public IActionResult Algorithms(String key, int algorithm, int option, IFormFile file, String sequence)
        {
            string filePath = "file.rtf";
            string code;
            var dir = _env.ContentRootPath;

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
                code = sequence;
            else
            {
                string type = file.ContentType;
                if (type == "text/plain")
                {
                    filePath = "file.txt";
                    using (var fileStream = new FileStream(Path.Combine(dir, "file.txt"), FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fileStream);
                    }

                    code = System.IO.File.ReadAllText(filePath);
                }
                else
                {
                    filePath = "file.rtf";
                    using (var fileStream = new FileStream(Path.Combine(dir, "file.rtf"), FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fileStream);
                    }

                    string m = ReadFromRTF();
                    code = RemoveRTFFormatting(m);

                }

                ViewBag.type = type;
            }
            
            ViewBag.Message = "";
            ViewBag.key = key;
            ViewBag.option = option;
            ViewBag.algorithm = algorithm;
            ViewBag.result = launchAlgorithm(code, key, algorithm, option);
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

        private String launchAlgorithm(String code, String key, int algorithm, int option)
        {
            String result = "";
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
            return result;
        }

        private string ReadFromRTF()
        {
            StreamReader myRTFReader = new StreamReader("file.rtf");
            string output = myRTFReader.ReadToEnd();
            myRTFReader.Close();
            return output;
        }


        private string RemoveRTFFormatting(string rtfContent)
        {
            rtfContent = rtfContent.Trim();


            Regex rtfRegEx = new Regex("({\\\\)(.+?)(})|(\\\\)(.+?)(\\b)",
                                            RegexOptions.IgnoreCase
                                            | RegexOptions.Multiline
                                            | RegexOptions.Singleline
                                            | RegexOptions.ExplicitCapture
                                            | RegexOptions.IgnorePatternWhitespace
                                            | RegexOptions.Compiled
                                            );
            string output = rtfRegEx.Replace(rtfContent, string.Empty);
            output = Regex.Replace(output, @"\}", string.Empty); //replacing the remaining braces
            string text = output.Remove(output.Length - 1);

            return text.Remove(0, 6);


        }
    }
}
