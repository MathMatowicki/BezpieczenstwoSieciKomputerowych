using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Bezpieczeństwo.Algorithms
{
    public class Cezara
    {
        static char[] alfabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
                            'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
                            'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ' };
        private int key;
        public bool PrepareKey(String key)
        {
            int n;
            if (int.TryParse(key, out n) && n >= 0)
            {
                this.key = n % alfabet.Length;
                return true;
            }
            return false;
        }
        public bool PrepareText(string text)
        {
            foreach(char letter in text)
            {
                if (!Char.IsLetterOrDigit(letter) && letter != ' ')
                {
                    return false;
                }
            }
            return true;
        }

        public string Cipher(string text, string k)
        {
            if (!PrepareKey(k)) return "ERROR";
            if (!PrepareText(text)) return "Text can not have special characters";

            string result = "";
            string upper = text.ToUpper();
            for (int i = 0; i < text.Length; i++)
            {
                result += Alfabet(Array.IndexOf(alfabet, upper[i]) + key);
            }
            return result;
        }

        public string Decrypt(string text, string k)
        {
            if (!PrepareKey(k)) return "ERROR";
            if (!PrepareText(text)) return "Text can not have special characters";

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
