using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bezpieczeństwo.Algorithms;
using Microsoft.AspNetCore.Mvc;

namespace Bezpieczeństwo.Controllers
{
    public class Test : Controller
    {
        public IActionResult Index()
        {
            DES des = new DES();
            byte[] bytes = new byte[8];
            byte[] u = new byte[8];
            bytes[0] = 10;
            bytes[1] = 13;
            bytes[2] = 12;
            bytes[3] = 12;
            bytes[4] = 14;
            bytes[5] = 15;
            bytes[6] = 11;
            bytes[7] = 2;

            u = des.Test(bytes);

            ViewBag.u = u;

            return View();
        }
    }
}
