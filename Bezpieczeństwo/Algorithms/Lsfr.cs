using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bezpieczeństwo.Algorithms
{
    public class Lsfr
    {
        //przetestowac get bytes i poprawic to string
        public int size;
        int[] indexes;
        public ulong output=0;
        ulong register=0;
        ulong maxPower = 1;
        public int numberOfIterations = 0;

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
            maxPower = 1;
            Random rand = new Random();
            for(int i=0; i<size; i++)
            {
                register = 2 * register + (ulong)rand.Next(2);
                maxPower *= 2;
            }
            maxPower /= 2;
            if (register == 0 || register == maxPower) Initialize();
        }

        public void Iteration()
        {
            numberOfIterations++;
            //output = 2 * output + register % 2;
            output = (output << 1) + register % 2;

            //xor dla 1 bitu
            ulong element = (register >> (size - indexes[0]))%2;
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

        public byte[] getBytes()
        {
            int n = numberOfIterations == 0 ? 1 : numberOfIterations/8;
            n = numberOfIterations % 8 > 0 ? n + 1 : n;
            n = Math.Min(n, 64/8);
            ulong helpregister = output;
            byte[] tab = new byte[n];
            for(int i = n - 1; helpregister > 0; i --)
            {
                tab[i] = (byte)helpregister;
                helpregister = helpregister >> 8;
            }
            return tab;
        }

        public ulong getLong()
        {
            return output;
        }

        public override string ToString()
        {
            StringBuilder outputl = new StringBuilder("Ostatni rejestr: ");
            for (int i = size-1; i >= 0; i--)
                outputl.Append((register >> i) % 2);

            outputl.Append("\nWynik: ");
            for (int i = size - 1; i >= 0; i--)
                outputl.Append((output >> i) % 2);

            return outputl.ToString();
        }

        public void setKey(ulong key)
        {
            this.output = key;
        }
    }
}
