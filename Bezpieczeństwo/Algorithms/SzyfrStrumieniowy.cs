using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bezpieczeństwo.Algorithms
{
    public class SzyfrStrumieniowy
    {
        private Lsfr lsfr;
        private byte[] content;

        public SzyfrStrumieniowy(ulong key)
        {
            this.lsfr.setKey(key);
        }

        public SzyfrStrumieniowy(Lsfr lsfr)
        {
            this.lsfr = lsfr;
        }

        /*public SzyfrStrumieniowy(byte[] content, Lsfr lsfr)
        {
            this.content = content;
            this.lsfr = lsfr;
        }*/

        public void LoadKey()
        {

        }

        public void ChangeContent(byte[] content)
        {
            this.content = content;
        }

        private byte[] XorOperation()
        {
            byte[] key = lsfr.getBytes();
            byte[] output = new byte[content.Length];
            int keyIndex = 0;
            for(int i = 0; i < content.Length; i++)
            {
                output[i] = (byte)(content[i] ^ key[keyIndex]);
                keyIndex++;
                keyIndex = keyIndex % key.Length;
            }
            return output;
        }

        public byte[] Cipher(byte[] content)
        {
            ChangeContent(content);
            return XorOperation();
        }

        public byte[] Decrypt()
        {
            ChangeContent(content);
            return XorOperation();
        }
    }
}
