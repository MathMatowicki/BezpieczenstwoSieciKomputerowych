using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bezpieczeństwo.Algorithms
{
    public class Lsfr
    {
        int size;
        int[] indexes;
        long output=0;
        long register=0;
        long maxPower = 1;

        public Lsfr(int size, int[] indexes)
        {
            SetUp(size, indexes);
        }

        public Lsfr(int[] indexes)
        {
            int n = 0;
            for (int i = 0; i < indexes.Length; i++)
                n = Math.Max(n, indexes[i]);
            SetUp(n,indexes);
        }

        public void Initialize()
        {
            Random rand = new Random();
            for(int i=0; i<size; i++)
            {
                register = 2 * register + rand.Next(2);
                maxPower *= 2;
            }
            maxPower /= 2;
        }

        public void Iteration()
        {
            output = 2 * output + register % 2;

            //xor dla 1 bitu
            long element = (register >> (size - indexes[0]))%2;
            for(int i=1; i < indexes.Length; i++)
            {
                element = element ^ (register >> (size - indexes[i])) % 2;
            }
            register = register >> 1;
            register = register + maxPower * element;
        }

        public void SetUp(int size, int[] indexes)
        {
            this.indexes = indexes;
            this.size = size;
        }

        public void Clear()
        {
            size = 0;
            indexes = null;
            output = 0;
            register = 0;
            maxPower = 1;

        }
    }
}
