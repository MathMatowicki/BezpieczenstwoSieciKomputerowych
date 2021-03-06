﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                if (!result)
                {
                    key = null;
                    return result;
                }
                index++;
            }
            buffer = new char[key.Length];
            int n = key.Length;

            //niewłaściwa wartosc w tablicy
            foreach (int x in key)
            {
                if (x > n || x <= 0)
                {
                    key = null;
                    return false;
                } 
            }

            for(int i = 1; i <= n; i++)
            {
                if (!key.Contains(i))
                {
                    key = null;
                    return false;
                }
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
            if (key == null) return "ERROR";
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
            if (key == null) return "ERROR";
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
            if (key == null) return "ERROR";
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < sequence.Length; i += key.Length)
            {
                foreach (int index in key)
                {
                    try
                    {
                        output.Append(sequence[i + index - 1]);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine("Exception caught.", e);
                    }

                }
            }

            return output.ToString();
        }

        public String DecipherString(String sequence)
        {
            if (key == null) return "ERROR";
            int cipheredIndex = 0;
            StringBuilder output = new StringBuilder("");
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
                int index2 = 0;
                while (index2 < key.Length && buffer[index2] != '\0')
                {
                    output.Append(buffer[index2]);
                    index2++;
                } 
            }
            return output.ToString();
        }
    }
}
