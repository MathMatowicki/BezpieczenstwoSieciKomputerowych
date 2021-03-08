using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bezpieczeństwo.Algorithms
{
    public class WordKey
    {
        private int[] key;
        public bool PrepareKey(String key)
        {
            key = key.ToUpper();
            Regex reg = new Regex("[A-Z]");
            if (key.Length > 0 && reg.Matches(key).Count == key.Length)
            {
                this.key = null;
                this.key = NumericKey(key);
                return true;
            }
            return false;
        }

        private char[] QuickSort(char[] d, int l, int r)
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

        public int[] NumericKey(String key)
        {
            char[] ckey = new char[key.Length];
            for (int j = 0; j < key.Length; j++)
                ckey[j] = key[j];

            int l = 0, r = key.Length - 1;
            ckey = QuickSort(ckey, l, r);

            int[] i = new int[key.Length];
            for (int j = 0; j < ckey.Length; j++)
            {
                int p = 0;
                while (ckey[j] != key[p] || i[p] != 0) p++;
                i[p] = j + 1;
            }
            return i;
        }

        public int[] GetKey(String key)
        {
            bool b = this.PrepareKey(key);
            if (b) return this.key;
            else return null;
        }

    }
}
