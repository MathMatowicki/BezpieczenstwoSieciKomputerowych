using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bezpieczeństwo.Algorithms
{
    public class PrzestawieniaMacierzoweB
    {
        public String Cipher(string text)
        {
            int[] key = GetKey();
            char[,] tab = new char[key.Length, (int)text.Length / key.Length + 1];
            String output = "";
            int index = 0;

            for (int j = 0; j < (int)text.Length / key.Length + 1; j++)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if (index <= text.Length - 1)
                    {
                        tab[i, j] = text[index];
                        index++;
                    }
                    else
                    {
                        tab[i, j] = '0';
                    }
                }
            }

            for (int i = 0; i < key.Length ; i++)
            {
                for (int j = 0; j < (int)text.Length / key.Length + 1; j++)
                {
                    //if(tab[i,j] != '0')
                        output += tab[Array.IndexOf(key, i+1), j];
                }
            }
            return output;
        }

        public String Decipher(String text)
        {
            int[] key = GetKey();
            Console.WriteLine(key.Length);
            String output = "";
            char[,] tab = new char[key.Length, (int)text.Length / key.Length + 1];
            int index = 0;

            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < (int)text.Length / key.Length; j++)
                {
                    if (index <= text.Length - 1)
                    {
                        tab[Array.IndexOf(key, i + 1), j] = text[index];

                        index++;
                    }
                }
            }

            for (int k = 0; k < (int)text.Length / key.Length; k++)
            {

                for (int g = 0; g < key.Length; g++)
                {
                    output += tab[g, k];
                }
            }

            return output;
        }

        //Zwraca tablicę na podstawie słowa klucz
        public int[] GetKey()
        {
            int[] i = new int[] { 1, 5, 2, 4, 8, 3, 6, 7, 9};
            return i; 
        }
    }
}

//1 5 2 4 8 3 6 7 9
//T o   j e s t   s
//z y f r   i   n i 
//e   w i e s z   c 
//o   t u   j e s t  
//n a p i s a n e 0