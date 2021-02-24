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
                while (ckey[j] != key[p] || i[p]!=0) p++;
                i[p] = j+1;
            }
            
            return i;
        }


        //sprawdzanie poprawności klucza - do usunięcia potem
        public String key(String key)
        {
            String s="";
            char[] ckey = new char[key.Length];
            for (int j = 0; j < key.Length; j++)
                ckey[j] = key[j];
            int l = 0, r = key.Length - 1;
            ckey = QuickSort(ckey, l, r);

            foreach (char c in ckey) s += c;
            return s;
        }

    }
}

//1 10 7 11 3 8 6 4 9 2 5 
//H  E R  E   I S   S E C
//R  E T    M E S S A G E
//   E N  C I P H E R E D
//   B Y    T R A N S P O
//S  I T  I O N 0 0 0 0 0

//HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION