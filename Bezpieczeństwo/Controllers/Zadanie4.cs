using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Algorithms(String key, int option, IFormFile file)
        {
            var dir = _env.ContentRootPath;

            //szyfrowanie
            using (var fileStream = new FileStream(Path.Combine(dir, file.FileName), FileMode.Create, FileAccess.Write))
            using (var outputStream = new FileStream(Path.Combine(dir, "output" + file.FileName), FileMode.Create, FileAccess.Write))
            {
                while (fileStream.Length - fileStream.Position + 1 >= 8)
                {
                    byte[] code = new byte[8];
                    fileStream.Read(code, 0, 8);
                    byte[] result = launchAlgorithmZad4(code, key, option);
                    outputStream.Write(result);
                }

                if(fileStream.Length - fileStream.Position + 1 > 0)
                {
                    byte[] code = new byte[8];
                    fileStream.Read(code, 0, (int)(fileStream.Length - fileStream.Position + 1));
                    for(int i = 0; i < 8 - (fileStream.Length - fileStream.Position + 1) - 1; i++)
                    {
                        code[(int)(fileStream.Length - fileStream.Position + 1 + i)] = 0;
                    }
                    code[7] = (byte)(fileStream.Length - fileStream.Position + 1);
                    byte[] result = launchAlgorithmZad4(code, key, option);
                    outputStream.Write(result);
                }
                else
                {
                    byte[] code = new byte[8];
                    for (int i = 0; i < 7; i++)
                        code[i] = 0;

                    code[7] = 8;
                    byte[] result = launchAlgorithmZad4(code, key, option);
                    outputStream.Write(result);
                }
            }

            //deszyfrowanie
            using (var fileStream = new FileStream(Path.Combine(dir, file.FileName), FileMode.Create, FileAccess.Write))
            using (var outputStream = new FileStream(Path.Combine(dir, "output" + file.FileName), FileMode.Create, FileAccess.Write))
            {
                while (fileStream.Length - fileStream.Position + 1 > 8)
                {
                    byte[] code = new byte[8];
                    fileStream.Read(code, 0, 8);
                    byte[] result = launchAlgorithmZad4(code, key, option);
                    outputStream.Write(result);
                }

                byte[] lastCode = new byte[8];
                fileStream.Read(lastCode, 0, 8);
                byte[] lastResult = launchAlgorithmZad4(lastCode, key, option);
                for (int i = 0; i < lastResult[7]; i++)
                    outputStream.WriteByte(lastResult[i]);
            }
            return RedirectToAction("DownloadFile", new { fileName = "output" + file.FileName });

            return View();
        }

        public FileResult DownloadFile(string fileName)
        {
            byte[] file = System.IO.File.ReadAllBytes(fileName);
            System.IO.File.Delete(fileName);
            return File(file, "application/octet-stream", fileName);
        }

        private byte[] launchAlgorithmZad4(byte[] code, string key, int option)
        {
            return new byte[] { };
        }
    }
}
