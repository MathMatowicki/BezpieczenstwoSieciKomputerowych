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
            //int[] wSpace = this.whereSpace(text);
            //text = this.withoutSpace(text);
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
                }
            }

            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < (int)text.Length / key.Length + 1; j++)
                {
                    if(tab[Array.IndexOf(key, i + 1), j] != '\0')
                        output += tab[Array.IndexOf(key, i + 1), j]; 
                }
            }

            //output = this.withoutSpace(output);
            //output = this.addSpace(output, wSpace);

            return output;
        }

        public String Decipher(String text, String userKey)
        {
            int[] key = GetKey(userKey);
            Console.WriteLine(key.Length);
            String output = "";
            //int[] wSpace = this.whereSpace(text);
            //text = this.withoutSpace(text);
            char[,] tab = new char[key.Length, (int)text.Length / key.Length + 1];
            int modulo = text.Length % key.Length;
            Console.WriteLine(text.Length);
            Console.WriteLine(modulo);
            Console.WriteLine((int)(text.Length / key.Length));

            int index = 0;

            for (int i = 0; i < key.Length; i++)
            {
                if (Array.IndexOf(key, i + 1) < modulo)
                {
                    for (int j = 0; j < (int)text.Length / key.Length + 1; j++)
                    {
                        if (index <= text.Length - 1)
                        {
                            tab[Array.IndexOf(key, i + 1), j] = text[index];

                            index++;
                        }
                    }
                }
                else
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

            }

            for (int k = 0; k < (int)text.Length / key.Length + 1; k++)
            {

                for (int g = 0; g < key.Length; g++)
                {
                    if(tab[g, k] != '\0')
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

        public String withoutSpace(String text)
        {
            String result = "";
            foreach (char c in text)
                if (c != ' ') result += c;
            return result;
        }

        public int[] whereSpace(String text)
        {
            int i = 0;
            foreach (char c in text)
                if (c == ' ') i++;
            int[] result = new int[i];
            i = 0;
            for (int j = 0; j < text.Length; j++)
                if (text[j] == ' ')
                {
                    result[i] = j;
                    i++;
                }

            return result;
        }

        public String addSpace(String text, int[] where)
        {
            String result = "";
            int p = 0, index = 0, ile = 0;
            if (where.Length > 0)
            {
                p = where[index];
                for (int i = 0; i < text.Length; i++)
                {
                    if (i == p + ile)
                    {
                        result += " ";
                        index++;
                        if (index < where.Length)
                        {
                            p = where[index];
                            ile--;
                        }
                    }
                    result += text[i];
                }
            }
            else return text;

            return result;
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
//N  i e    w i e m   o  
//c  o    h o d z i   w   
//t  y m    p r o b l e m  
//i  e    i   n i e   w i
//e  m    c z y   j e s t
//   d o  b r z e 0 0 0 0

//HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION