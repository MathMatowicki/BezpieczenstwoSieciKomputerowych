using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bezpieczeństwo.Algorithms
{
    public class PrzestawieniaMacierzoweB
    {
        public String Cipher(string text, String userKey)
        {
            int[] key = GetKey(userKey);
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

        public String Decipher(String text, String userKey)
        {
            int[] key = GetKey(userKey);
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
        /*public int[] GetKey()
        {
            int[] i = new int[] { 1, 6, 2, 4, 8, 3, 5, 7, 9, 10};
            return i; 
        }*/

        private char[] QuickSort(char[]d, int l, int r)
        {
            int i, j;
            char p, swap;

            i = (l + r) / 2;
            p = d[i]; d[i] = d[r];
            for (j = i = l; i < r; i++)
                if (d[i] < p)
                {
                    swap = d[i];
                    d[i] = d[j];
                    d[j] = swap;
                    j++;
                }
            d[r] = d[j]; d[j] = p;
            if (l < j - 1) QuickSort(d, l, j - 1);
            if (j + 1 < r) QuickSort(d, j + 1, r);

            return d;
        }

        public int[] GetKey(String key)
        {
            char[] ckey = new char[key.Length];
            for (int j = 0; j < key.Length; j++)
                ckey[j] = key[j];

            int l = 0, r = key.Length - 1;
            ckey = QuickSort(ckey, l, r);

            int[] i = new int[key.Length];
            for(int j = 0; j < ckey.Length; j++)
            {
                int p = 0;
                while (ckey[j] != key[p]) p++;
                i[p] = j+1;
            }
            
            return i;
        }


    }
}

//1 6 2 4 8 3 5 7 9 10
//T o   j e s t   s z
//y f r   i   n i e 
//w i e s z   c o   t
//u   j e s t   n a p
//i s a n e 0 0 0 0 0