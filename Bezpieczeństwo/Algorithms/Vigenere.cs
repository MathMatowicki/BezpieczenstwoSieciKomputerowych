using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bezpieczeństwo.Algorithms
{
    public class Vigenere
    {

        private String key;
        private String text = "";

        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        public bool PrepareKey(String key, String text)
        {
            text = text.ToUpper();
            key = key.ToUpper();

            text = Regex.Replace(text, @"[^A-Z]", "");
            key = Regex.Replace(key, @"[^A-Z]", "");

            this.text = text;
            foreach (var sign in key)
            {
                //Each sign is checked by validation method
                if (!char.IsLetter(sign))
                    return false;
            }
            if (key.Length > this.text.Length)
            {
                key = key.Remove(this.text.Length);
            }
            else if (key.Length < this.text.Length)
            {
                int mod = this.text.Length % key.Length;
                int howMany = this.text.Length / key.Length;

                key = String.Concat(Enumerable.Repeat(key, howMany));
                for (int i = 0; i < mod; i++)
                {
                    key = key + key[i];
                }
            }

            if (this.text.Length == key.Length)
            {
                this.key = key;
                return true;
            }
            return false;
        }

        public String Cipher(String text, String key)
        {

            if (PrepareKey(key, text))
            {
                String cipher_text = "";

                for (int i = 0; i < this.text.Length; i++)
                {
                    // converting in range 0-25 
                    int x = (this.text[i] + this.key[i]) % 26;

                    // convert into alphabets(ASCII) 
                    x += 'A';

                    cipher_text += (char)(x);
                }

                return cipher_text;
            }
            return "ERROR! Długość klucza i tekstu muszą byc sobie równe (Pod uwagę brane są tylko litery)";
        }

        public String Decrypt(String text, String key)
        {

            if (PrepareKey(key, text))
            {
                String orig_text = "";

                for (int i = 0; i < this.text.Length; i++)
                {
                    // converting in range 0-25 
                    int x = (text[i] - this.key[i] + 26) % 26;

                    // convert into alphabets(ASCII) 
                    x += 'A';

                    orig_text += (char)(x);
                }
                return orig_text;
            }
            return "ERROR! Długość klucza i tekstu muszą byc sobie równe (Pod uwagę brane są tylko litery)";
        }
    }
}
