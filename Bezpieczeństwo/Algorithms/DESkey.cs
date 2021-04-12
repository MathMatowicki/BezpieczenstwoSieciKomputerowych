using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bezpieczeństwo.Algorithms
{
    public class DESkey
    {
        /*
        private static byte[,] pc1 = new byte[8, 7]
        {
            { 57, 49, 41, 33, 25, 17, 9},
            { 1, 58, 50, 42, 34, 26, 18},
            { 10, 2, 59, 51, 43, 35, 27},
            { 19, 11, 3, 60, 52, 44, 36},
            { 63, 55, 47, 39, 31, 23, 15},
            { 7, 62, 54, 46, 38, 30, 22},
            { 14, 6, 61, 53, 45, 37, 29},
            { 21, 13, 5, 28, 20, 12, 4}
        };
        private static byte[,] pc2 = new byte[8, 6]
        {
            { 14, 17, 11, 24, 1, 5},
            { 3, 28, 15, 6, 21, 10},
            { 23, 19, 12, 4, 26, 8},
            { 16, 7, 27, 20, 13, 2},
            { 41, 52, 31, 37, 47, 55},
            { 30, 40, 51, 45, 33, 48},
            { 44, 49, 39, 56, 34, 53},
            { 46, 42, 50, 36, 29, 32}
        };
        */

        private static int[] nols = new int[16]
            {1, 1, 2, 2, 2 ,2 ,2 ,2 ,1, 2, 2, 2, 2, 2, 2, 1};

        private static int[] pc1 = new int[56]
        { 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18, 10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36,
             63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22, 14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4
        };
        private static int[] pc2 = new int[48]
        { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55,
             30, 40, 51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32
        };


        private long key;
        private long[] keyCipher = new long[16];

        public DESkey() { }
        public DESkey(long k) { this.key = k; }
        public int[] permutedChoice1()
        {
            String s = Convert.ToString(this.key, 2);
            while (s.Length < 64) s = "0" + s;
            int[] result = new int[56];
            for(int i = 0; i < 56; i++)
            {
                int x = pc1[i] - 1;
                result[i] = int.Parse(s[x]+"");
            }
            return result;
        }

        //proba bez zamiany na string
        public byte[] permutedChoice1bit()
        {
            byte[] bytes = BitConverter.GetBytes(this.key);
            byte[] result = new byte[7];
            for (int i = 0; i < 56; i++)
            {
                int x = pc1[i] - 1;
                //nie wiem jak zrobic odwolanie do konkretego bitu 

            }
            return result;
        }

        public int[] permutedChoice2(long left, long right)
        {
            String s = Convert.ToString(left, 2) + Convert.ToString(right, 2);
            int[] result = new int[48];
            for (int i = 0; i < 48; i++)
            {
                int x = pc2[i] - 1;
                if (x > s.Length) result[i] = 0;
                else result[i] = s[x];
            }
            return result;
        }

        public long split(int[] tab, int l, int r)
        {
            string s="";
            byte[] b = new byte[4];
            for (int i = l; i <= r; i++)
            {
                s += tab[i];
            }
            /*int k = 0;
            for (int i = r; i >= l; i--)
            {
                s = tab[i]+s;
                k++;
                if(k%8==0)
                {
                    b[k / 8] = Byte.Parse(s, System.Globalization.NumberStyles.)
                    s = "";
                }
            }*/

            //return Convert.ToInt64(Convert.FromBase64String(s));
            //return BitConverter.ToInt64(Convert.ToByte(s));
            return Convert.ToInt64(b);
        }

        public void generateKey(long key)
        {
            this.key = key;
            int[] result = this.permutedChoice1();

            long left = this.split(result, 0, 27);
            long right = this.split(result, 28, 55);

            for (int i=0; i<16; i++)
            {
                int ls = nols[i];
                left = left << ls;
                right = right << ls;

                result = this.permutedChoice2(left, right);
                String s = "";
                for (int j = 0; j < 48; j++) s += result[j];

                this.keyCipher[i] = Convert.ToInt64(s);
            }

        }


        public long[] getKeyCipher()
        {
            return keyCipher;
        }


    }
}
