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
            ulong keyinput = 4325;
            DESkey deskey = new DESkey(keyinput);
            deskey.generateKeyBit(keyinput);
            ulong[] keys = deskey.getKeyCipher();

            DES des = new DES();
            byte[] input = new byte[8];
            byte[] Cipheroutput = new byte[8];
            byte[] Decipheroutput = new byte[8];
            input[0] = 10;
            input[1] = 13;
            input[2] = 12;
            input[3] = 12;
            input[4] = 14;
            input[5] = 15;
            input[6] = 11;
            input[7] = 2;

            Cipheroutput = des.desCipher(input, Cipheroutput, keys);
            Decipheroutput = des.desDecipher(Cipheroutput, Decipheroutput, keys);

            ViewBag.input = input;
            ViewBag.Cipheroutput = Cipheroutput;
            ViewBag.Decipheroutput = Decipheroutput;

            return View();
        }
        //1
        //4 14 204 104 41 153 140 14

        //2
        //10 13 12 12 14 15 11 2
    }
}
