using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bezpieczeństwo.Algorithms
{
    public class Cezara
    {
        char[] alfabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private int key;
        public bool PrepareKey(String key)
        {
            int n;
            if (int.TryParse(key, out n) && n > 0)
            {
                this.key = n;
                return true;
            }
            return false;
        }

        public string Cipher(string text, string k)
        {
            PrepareKey(k);
            string result = "";
            string upper = text.ToUpper();
            for(int i = 0; i<text.Length; i++)
            {
                result += Alfabet(Array.IndexOf(alfabet, upper[i]) + key);
            }
            return result;
        }

        public string Decrypt(string text, string k)
        {
            PrepareKey(k);
            string result = "";
            string upper = text.ToUpper();
            for (int i = 0; i < text.Length; i++)
            {
                result += Alfabet(Array.IndexOf(alfabet, upper[i]) - key);
            }
            return result;
        }

        public char Alfabet(int i)
        {
            char result;
            if (i > alfabet.Length)
            {
                result = alfabet[i - alfabet.Length];
            }
            else if (i < 0)
            {
                result = alfabet[alfabet.Length + i];
            }
            else
            {
                result = alfabet[i];
            }
            return result;
        }
    }
}
