using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bezpieczeństwo.Models;
using Bezpieczeństwo.Algorithms;
using System.IO;

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
            //String tab = b.Cipher("To jest szyfr i nie wiesz co tu jest napisane","afbdhcegij");
            //String result = b.Decipher("Tywui rejas  t0j sentnc 0ofi s ion0eizsese a0z tp0", "afbdhcegij");
            String tab = b.Cipher("kupić gęślą", "CONVENIENCE");
            String result = b.Decipher("k0l0ć0ę0ą0g0p0 0ś0u0i0", "CONVENIENCE");
            int[] i = b.GetKey("CONVENIENCE");
            int leanth = i.Length;

            //sprawdzanie czy klucz dobrze dziala - do usuniecia potem
            /*
            String k = "CONVENIENCE";
            int[] i =  b.GetKey(k);
            String tab = "";
            foreach(int j in i)tab = tab+j+" ";
            String result = b.key(k);*/
            ViewBag.tab = tab;
            ViewBag.i = i;
            ViewBag.leanth = leanth;
            ViewBag.result = result;
            return View();
        }

        public IActionResult Algorithms()
        {
            /*var result = "";
            Array userData = null;
            char[] delimiterChar = { ',' };

            var dataFile = Server.MapPath("~/App_Data/data.txt");

            if (File.Exists(dataFile))
            {
                userData = File.ReadAllLines(dataFile);
                if (userData == null)
                {
                    // Empty file.
                    result = "The file is empty.";
                }
            }
            else
            {
                // File does not exist.
                result = "The file does not exist.";
            }*/
            return View();
        }

        [HttpPost]
        public IActionResult Algorithms(int id)
        {
            var file = Request.Form.Files.Count != 0 ? Request.Form.Files[0] : null;
            if (file == null)
            {
                ViewBag.Message = "Nie wybrano obrazu do przesłania";
                return View("AddImage");
            }
            /*if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }*/
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
