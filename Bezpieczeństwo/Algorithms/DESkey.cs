using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bezpieczeństwo.Algorithms
{
    public class DESkey
    {
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


        private ulong key;
        private ulong[] keyCipher = new ulong[16];

        public DESkey() { }
        public DESkey(ulong k) { this.key = k; }

        public ulong permutedChoice1bit()
        {
            ulong result = 0;
            for (int i = 0; i < 56; i++)
            {
                result = result << 1;
                ulong x = (key >> (64 - pc1[i])) % 2;
                result += x;

            }
            return result;
        }

        public ulong permutedChoice2bit(int left, int right)
        {
            ulong leftHelp = (ulong)left;
            ulong toConversion = (leftHelp << 28) + (ulong)right;
            ulong result = 0;
            for (int i = 0; i < 48; i++)
            {
                result = result << 1;
                ulong x = (toConversion >> (56 - pc2[i])) % 2;
                result += x;
            }
            return result;
        }

        public int splitBit(ulong key, bool left)
        {
            if (left)
                return (int)(key >> 28);

            return (int)(key % (268435456));
        }

        public void generateKeyBit(ulong key)
        {
            this.key = key;
            ulong result = this.permutedChoice1bit();

            int left = this.splitBit(result,true);
            int right = this.splitBit(result,false);

            for (int i = 0; i < 16; i++)
            {
                int ls = nols[i];
                int add = left >> (28 - ls);
                left = (left << ls) + add;
                left = left % 268435456;
                add = right >> (28 - ls);
                right = (right << ls) + add;
                right = right % 268435456;

                result = this.permutedChoice2bit(left, right);

                this.keyCipher[i] = result;
            }

        }

        public ulong[] getKeyCipher()
        {
            return keyCipher;
        }


    }
}
