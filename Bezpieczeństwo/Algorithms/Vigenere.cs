using System;

namespace Bezpieczeństwo.Algorithms
{
    public class Vigenere
    {

        private String key;

        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        public bool PrepareKey(String key)
        {
            foreach (var sign in key)
            {
                //Each letter is checked by validation method
                if (!char.IsLetter(sign))
                    return false;
            }
            this.key = key;
            return true;
        }

        public String Cipher(String text, String key)
        {
            if (PrepareKey(key))
            {
                String cipher_text = "";

                for (int i = 0; i < text.Length; i++)
                {
                    // converting in range 0-25 
                    int x = (text[i] + this.key[i]) % 26;

                    // convert into alphabets(ASCII) 
                    x += 'A';

                    cipher_text += (char)(x);
                }

                return cipher_text;
            }
            return "ERROR!";
        }

        public String Decrypt(String text, String key)
        {
            if (PrepareKey(key))
            {
                String orig_text = "";

                for (int i = 0; i < text.Length && i < key.Length; i++)
                {
                    // converting in range 0-25 
                    int x = (text[i] - key[i] + 26) % 26;

                    // convert into alphabets(ASCII) 
                    x += 'A';

                    orig_text += (char)(x);
                }
                return orig_text;
            }
            return "ERROR!";
        }
    }
}
