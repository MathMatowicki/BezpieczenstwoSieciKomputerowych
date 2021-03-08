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
            key.ToUpper();
            this.key = key;
            return true;
        }

        public String Cipher(String text, String key)
        {
            String outText = String.Empty;
            //Validate input key if not console err message and return empty value
            if (!PrepareKey(key))
            {
                Console.WriteLine("Wrong validation of Vigenere key");
                return String.Empty;
            }

            foreach (var letter in text)
            {

            }
            return outText;
        }
    }
}
