using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bezpieczeństwo.Algorithms
{
    public class PrzestawieniaMacierzoweA
    {
        private int[] key;
        private char[] buffer;

        public bool PrepareKey(String sequence, char separator)
        {
            string[] stringOrder = sequence.Split(separator);
            this.key = new int[stringOrder.Length];
            int index = 0;
            foreach (String column in stringOrder)
            {
                bool result = int.TryParse(column, out this.key[index]);
                //niewlasciwa wartosc
                if (!result) return result;
                index++;
            }
            buffer = new char[key.Length];
            int n = key.Length;

            //niewłaściwa wartosc w tablicy
            foreach (int x in key)
            {
                if (x > n || x <= 0) return false;
            }
            return true;
        }

        public void ClearKey()
        {
            this.key = null;
        }

        //pojedyncze elementy ciagu - rozmiar klucza
        private String Cipher(String orginal)
        {
            String output = "";
            if (orginal.Length <= key.Length)
            {
                foreach (int index in key)
                {
                    try
                    {
                        output += orginal[index - 1];
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine("Exception caught.", e);
                    }
                }
            }
            return output;
        }

        //pojedyncze elementy ciagu - rozmiar klucza
        private String Decipher(String ciphered)
        {
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = '\0';

            int cipheredIndex = 0;
            if (ciphered.Length <= key.Length)
            {
                foreach (int index in key)
                {
                    try
                    {
                        if (index > ciphered.Length) continue;
                        buffer[index - 1] = ciphered[cipheredIndex];
                        cipheredIndex++;
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine("Exception caught.", e);
                    }
                }
                return buffer.ToString();
            }
            return "";
        }

        public String CipherString(String sequence)
        {
            String output = "";
            for (int i = 0; i < sequence.Length; i += key.Length)
            {
                foreach (int index in key)
                {
                    try
                    {
                        output += sequence[i + index - 1];
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine("Exception caught.", e);
                    }

                }
            }

            return output;
        }

        public String DecipherString(String sequence)
        {
            int cipheredIndex = 0;
            String output = "";
            for (int i = 0; i < sequence.Length; i += key.Length)
            {
                for (int j = 0; j < buffer.Length; j++)
                    buffer[j] = '\0';

                cipheredIndex = 0;

                foreach (int index in key)
                {
                    try
                    {
                        if (i + index > sequence.Length) continue;
                        buffer[index - 1] = sequence[i + cipheredIndex];
                        cipheredIndex++;
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine("Exception caught.", e);
                    }
                }
                if (buffer[0] != '\0') output += new String(buffer).TrimEnd('\0');
            }
            return output;
        }
    }
}
