using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bezpieczeństwo.Algorithms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bezpieczeństwo.Controllers
{
    public class Zadanie4 : Controller
    {
        private readonly IHostingEnvironment _env;
        public Zadanie4(IHostingEnvironment env)
        {
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
        public IActionResult Algorithms(String key, int option, IFormFile file)
        {
            var dir = _env.ContentRootPath;
            using (var fileStream = new FileStream(Path.Combine(dir, file.FileName), FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }

            //walidacja klucza
            ulong keyValue;
            int keyCorrectness = this.KeyCorrectness(key, out keyValue);
            if(keyCorrectness == 0)
            {
                ViewBag.Message = "Nie podano żadnego klucza.";
                return View();
            }
            else if(keyCorrectness == 1)
            {
                ViewBag.Message = "Podano nieprawidłowy klucz. Klucz musi być liczbą dodatnią (max 64 bitową).";
                return View();
            }

            //szyfrowanie i deszyfrowanie
            if (option == 1)
                this.Cipher(file, dir, keyValue);
            else
                this.Decrypt(file, dir, keyValue);

            System.IO.File.Delete(file.FileName);
            return RedirectToAction("DownloadFile", new { fileName = "output" + file.FileName });
        }

        public FileResult DownloadFile(string fileName)
        {
            byte[] file = System.IO.File.ReadAllBytes(fileName);
            System.IO.File.Delete(fileName);
            return File(file, "application/octet-stream", fileName);
        }

        private void Cipher(IFormFile file, string dir, ulong key)
        {
            using (var fileStream = System.IO.File.OpenRead(file.FileName))
            using (var outputStream = new FileStream(Path.Combine(dir, "output" + file.FileName), FileMode.Create, FileAccess.Write))
            {
                while (fileStream.Length - fileStream.Position >= 8)
                {
                    byte[] code = new byte[8];
                    fileStream.Read(code, 0, 8);
                    byte[] result = launchAlgorithmZad4(code, key, 1);
                    outputStream.Write(result);
                }

                if (fileStream.Length - fileStream.Position > 0)
                {
                    byte[] code = new byte[8];
                    int numberOfBytesLeft = (int)(fileStream.Length - fileStream.Position);
                    fileStream.Read(code, 0, numberOfBytesLeft);
                    code[7] = (byte)(8 - numberOfBytesLeft);
                    byte[] result = launchAlgorithmZad4(code, key, 1);
                    outputStream.Write(result);
                }
                else
                {
                    byte[] code = new byte[8];
                    for (int i = 0; i < 7; i++)
                        code[i] = 0;

                    code[7] = 8;
                    byte[] result = launchAlgorithmZad4(code, key, 1);
                    outputStream.Write(result);
                }
            }
        }

        private void Decrypt(IFormFile file, string dir, ulong key)
        {
            using (var fileStream = System.IO.File.OpenRead(file.FileName))
            using (var outputStream = new FileStream(Path.Combine(dir, "output" + file.FileName), FileMode.Create, FileAccess.Write))
            {
                while (fileStream.Length - fileStream.Position > 8)
                {
                    byte[] code = new byte[8];
                    fileStream.Read(code, 0, 8);
                    byte[] result = launchAlgorithmZad4(code, key, 2);
                    outputStream.Write(result);
                }

                byte[] lastCode = new byte[8];
                fileStream.Read(lastCode, 0, 8);
                byte[] lastResult = launchAlgorithmZad4(lastCode, key, 2);
                for (int i = 0; i < 8 - lastResult[7]; i++)
                    outputStream.WriteByte(lastResult[i]);
            }
        }

        public int KeyCorrectness(String key, out ulong keyValue)
        {
            if (key == null || key.Length == 0 || key == "") { keyValue = 0; return 0; }
            if (!ulong.TryParse(key, out keyValue)) { keyValue = 0; return 1; }
            if (keyValue < 0 ) return 1;

            return 2;
        }

        //2 dla deszyfrowania, 1 dla szyfrowania
        private byte[] launchAlgorithmZad4(byte[] code, ulong key, int option)
        {
            ulong ulongKey = Convert.ToUInt64(key);

            DESkey deskey = new DESkey(key);
            deskey.generateKeyBit(key);
            ulong[] keys = deskey.getKeyCipher();
            byte[] output = new byte[8];

            if (option == 1)
            {
                DES des = new DES();
                output = des.desCipher(code, output, keys);
            }
            else if (option == 2)
            {
                DES des = new DES();
                output = des.desDecipher(code, output, keys);
            }
            
            return output;
        }
    }
}
