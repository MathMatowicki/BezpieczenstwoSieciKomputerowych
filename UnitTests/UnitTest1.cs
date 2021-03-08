using NUnit.Framework;
using Bezpieczeństwo.Algorithms;
using System;

namespace UnitTests
{
    public class Tests
    {
        private RailFence alghoritmRailFence;
        private PrzestawieniaMacierzoweA alghoritmPMA;
        private string keyRailFence = "";
        [SetUp]
        public void Setup()
        {
            alghoritmRailFence = new RailFence();
            alghoritmPMA = new PrzestawieniaMacierzoweA();
        }

        [Test, Category("Exercies1")]
        public void TestRailFenceKey()
        {

            keyRailFence = "1";

            Assert.AreEqual(true, alghoritmRailFence.PrepareKey(keyRailFence));

            keyRailFence = "0";

            Assert.AreEqual(false, alghoritmRailFence.PrepareKey(keyRailFence));

            keyRailFence = "-2";

            Assert.AreEqual(false, alghoritmRailFence.PrepareKey(keyRailFence));

            keyRailFence = "Random string";

            Assert.AreEqual(false, alghoritmRailFence.PrepareKey(keyRailFence));
        }

        [Test, Category("Exercies1")]
        public void TestRailFenceCipherAlghoritm()
        {
            string notCiphered = "CRYPTOGRAPHY";
            string ciphered = "CYTGAHRPORPY";

            keyRailFence = "1";
            Assert.AreEqual(notCiphered, alghoritmRailFence.Cipher(notCiphered, keyRailFence));

            keyRailFence = "2";
            Assert.AreEqual(ciphered, alghoritmRailFence.Cipher(notCiphered, keyRailFence));

            keyRailFence = "3";
            ciphered = "CTARPORPYYGH";

            Assert.AreEqual(ciphered, alghoritmRailFence.Cipher(notCiphered, keyRailFence));

            keyRailFence = "3";
            notCiphered = "CART CART";
            ciphered = "C TATCRRA";
            Assert.AreEqual(ciphered, alghoritmRailFence.Cipher(notCiphered, keyRailFence));

            keyRailFence = "2";
            notCiphered = "CART CART";
            ciphered = "CR ATATCR";
            Assert.AreEqual(ciphered, alghoritmRailFence.Cipher(notCiphered, keyRailFence));
        }

        [Test, Category("Exercies1")]
        public void TestRailFenceDecryptAlghoritm()
        {
            string notDecrypted = "CYTGAHRPORPY";
            string decrypted = "CRYPTOGRAPHY";

            keyRailFence = "1";
            Assert.AreEqual(notDecrypted, alghoritmRailFence.Decrypt(notDecrypted, keyRailFence));

            keyRailFence = "2";
            Assert.AreEqual(decrypted, alghoritmRailFence.Decrypt(notDecrypted, keyRailFence));

            keyRailFence = "3";
            notDecrypted = "CTARPORPYYGH";
            Assert.AreEqual(decrypted, alghoritmRailFence.Decrypt(notDecrypted, keyRailFence));

            keyRailFence = "3";
            notDecrypted = "C TATCRRA";
            decrypted = "CART CART";
            Assert.AreEqual(decrypted, alghoritmRailFence.Decrypt(notDecrypted, keyRailFence));

            keyRailFence = "2";
            notDecrypted = "CR ATATCR";
            decrypted = "CART CART";
            Assert.AreEqual(decrypted, alghoritmRailFence.Decrypt(notDecrypted, keyRailFence));
        }

        [Test, Category("Exercies1")]
        public void TestPrzestawieniaMacierzoweABasic()
        {
            string key = "3,1,4,2";
            string notCiphered = "CRY,?OGRAP.Y";
            string ciphered = "YC,RG?RO.AYP";

            alghoritmPMA.ClearKey();
            Assert.AreEqual(true, alghoritmPMA.PrepareKey(key, ','));
            string cipheredWithAlgorythm = alghoritmPMA.CipherString(notCiphered);
            string notCipheredWithAlgorythm = alghoritmPMA.DecipherString(ciphered);


            Assert.AreEqual(ciphered, cipheredWithAlgorythm);
            Assert.AreEqual(notCiphered, notCipheredWithAlgorythm);
        }

        [Test, Category("Exercies1")]
        public void TestPrzestawieniaMacierzoweABasic2()
        {
            string key = "3,1,4,2";
            string notCiphered = "CRYP   TOGRA  PHY";

            alghoritmPMA.ClearKey();
            Assert.AreEqual(true, alghoritmPMA.PrepareKey(key, ','));
            string cipheredWithAlgorythm = alghoritmPMA.CipherString(notCiphered);
            string notCipheredWithAlgorythm = alghoritmPMA.DecipherString(cipheredWithAlgorythm);

            Assert.AreEqual(notCiphered, notCipheredWithAlgorythm);
        }

        [Test, Category("Exercies1")]
        public void TestPrzestawieniaMacierzoweAKey()
        {
            string key1 = "3-1-4-2";
            string key2 = "3-1-4";
            string key3 = "3-a-4-2";
            string key4 = "3-0-4-2";
            string key5 = "3-2-4-2";

            alghoritmPMA.ClearKey();
            Assert.AreEqual(true, alghoritmPMA.PrepareKey(key1, '-'));
            alghoritmPMA.ClearKey();
            Assert.AreEqual(false, alghoritmPMA.PrepareKey(key2, '-'));
            alghoritmPMA.ClearKey();
            Assert.AreEqual(false, alghoritmPMA.PrepareKey(key3, '-'));
            alghoritmPMA.ClearKey();
            Assert.AreEqual(false, alghoritmPMA.PrepareKey(key4, '-'));
            alghoritmPMA.ClearKey();
            Assert.AreEqual(false, alghoritmPMA.PrepareKey(key5, '-'));
            alghoritmPMA.ClearKey();
        }

        [Test, Category("Exercies1")]
        public void TestPrzestawieniaMacierzoweA()
        {
            Random rand = new Random();
            string key = "4 -  3-1-2";
            string notCiphered = "";
            int n = rand.Next(10, 50);
            for (int i = 0; i < n; i++)
            {
                char letter = (char)rand.Next(65, 90);
                notCiphered += letter.ToString();
            }
            alghoritmPMA.ClearKey();
            Assert.AreEqual(true, alghoritmPMA.PrepareKey(key, '-'));
            string ciphered = alghoritmPMA.CipherString(notCiphered);

            string notCipheredAgain = alghoritmPMA.DecipherString(ciphered);
            Assert.AreEqual(notCipheredAgain, notCiphered);
        }

        [Test, Category("Exercies1")]
        public void TestPrzestawieniaMacierzoweAMore()
        {
            string key1 = "9,4,3,1,8,5,7,6,2";
            string key2 = "4,3,1,9,5,8,7,6,2";
            string key3 = "8,7,9,5,2,1,3,6,4";
            string notCiphered = "GABRYSIA";
            string notCipheredLong = "GABRYSIAGABRYSI";
            string ciphered1 = "RBGAYISA";
            string ciphered2 = "RBGYAISA";
            string cipheredLong = "AIGYAGBSRSBARIY";

            alghoritmPMA.ClearKey();
            Assert.AreEqual(true, alghoritmPMA.PrepareKey(key1, ','));
            Assert.AreEqual(notCiphered, alghoritmPMA.DecipherString(ciphered1));
            Assert.AreEqual(ciphered1, alghoritmPMA.CipherString(notCiphered));

            alghoritmPMA.ClearKey();
            Assert.AreEqual(true, alghoritmPMA.PrepareKey(key2, ','));
            Assert.AreEqual(notCiphered, alghoritmPMA.DecipherString(ciphered2));
            Assert.AreEqual(ciphered2, alghoritmPMA.CipherString(notCiphered));

            alghoritmPMA.ClearKey();
            Assert.AreEqual(true, alghoritmPMA.PrepareKey(key3, ','));
            Assert.AreEqual(notCipheredLong, alghoritmPMA.DecipherString(cipheredLong));
            Assert.AreEqual(cipheredLong, alghoritmPMA.CipherString(notCipheredLong));
        }

        [Test, Category("Exercies1")]
        public void TestPrzestawieniaMacierzoweBKey()
        {
            PrzestawieniaMacierzoweB algorytm = new PrzestawieniaMacierzoweB();
            string key1 = "CONVENIENCE";
            int[] key1result = new int[] { 1, 10, 7, 11, 3, 8, 6, 4, 9, 2, 5 };
            string key2 = "BEZPIECZENSTWO";
            int[] key2result = new int[] { 1, 3, 13, 9, 6, 4, 2, 14, 5, 7, 10, 11, 12, 8 };

            Assert.AreEqual(algorytm.PrepareKey(key1), key1result);
            Assert.AreEqual(algorytm.PrepareKey(key2), key2result);

        }

        [Test, Category("Exercies1")]
        public void TestPrzestawieniaMacierzoweBPrepareKey()
        {
            PrzestawieniaMacierzoweB algorytm = new PrzestawieniaMacierzoweB();
            string key1 = "CONVENIENCE";
            string key2 = "beZPIECzENSTWo";
            string key3 = "bezpieczenstwo";
            string key4 = "";
            string key5 = "bez pieczenstwo";
            string key6 = "bez-pieczenstwo";
            string key7 = "bez1pie2czen3stwo";

            Assert.AreEqual(algorytm.PrepareKey(key1), true);
            Assert.AreEqual(algorytm.PrepareKey(key2), true);
            Assert.AreEqual(algorytm.PrepareKey(key3), true);
            Assert.AreEqual(algorytm.PrepareKey(key4), false);
            Assert.AreEqual(algorytm.PrepareKey(key5), false);
            Assert.AreEqual(algorytm.PrepareKey(key6), false);
            Assert.AreEqual(algorytm.PrepareKey(key7), false);

        }

        [Test, Category("Exercies1")]
        public void TestPrzestawieniaMacierzoweBCipher()
        {
            PrzestawieniaMacierzoweB algorytm = new PrzestawieniaMacierzoweB();
            string m1 = "HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION";
            string c1 = "HEGEP SEN TNYT EPRNSARSSMITORR  SI C IASHAECEDOEEEBI";
            string key1 = "CONVENIENCE";

            algorytm.PrepareKey(key1);
            Assert.AreEqual(algorytm.Cipher(m1), c1);

            string m2 = "CRYPTOGRAPHYOSA";
            string c2 = "RTRHSCPGPOYOAYA";
            string key2 = "TRY";

            algorytm.PrepareKey(key2);
            Assert.AreEqual(algorytm.Cipher(m2), c2);

        }

        [Test, Category("Exercies1")]
        public void TestPrzestawieniaMacierzoweBDecipher()
        {
            PrzestawieniaMacierzoweB algorytm = new PrzestawieniaMacierzoweB();
            string m1 = "HECRNCEYIISEPSGDIRNTOAAESRMPNSSROEEBTETIAEEHS";
            string c1 = "HEREISASECRETMESSAGEENCIPHEREDBYTRANSPOSITION";
            string key1 = "CONVENIENCE";

            algorytm.PrepareKey(key1);
            Assert.AreEqual(algorytm.Decipher(m1), c1);

            string m2 = "RTRHSCPGPOYOAYA";
            string c2 = "CRYPTOGRAPHYOSA";
            string key2 = "TRY";

            algorytm.PrepareKey(key2);
            Assert.AreEqual(algorytm.Decipher(m2), c2);

        }

        [Test, Category("Exercies2")]
        public void TestVigenereCipher()
        {

        }

        [Test, Category("Exercies2")]
        public void TestPrzestawieniaMacierzoweDecipher()
        {
            PrzestawieniaMacierzoweC algorytm = new PrzestawieniaMacierzoweC();
            algorytm.PrepareKey("CONVENIENCE");
            string ciphered = "ABLR1!FIOZ267DIRK0CFPW5CMT+HMWIZ8XAPZ+HY7EW?OYDNT3AHKR49FKTGX6DNU!NXJ9YBCMS2?GJP138EJSEOU4BLS50GLU";
            string answer = "ABCDEFGHIJKLMNOPRSTUWXYZ1234567890+!?ABCDEFGHIJKLMNOPRSTUWXYZ1234567890+!?ABCDEFGHIJKLMNOPRSTUWXYZ";
            Assert.AreEqual(answer, algorytm.Decipher(ciphered));
        }

        public void TestPrzestawieniaMacierzoweDecipherBasic()
        {
            PrzestawieniaMacierzoweC algorytm = new PrzestawieniaMacierzoweC();
            algorytm.PrepareKey("CONVENIENCE");
            string ciphered = "HEESPNIRRSSEESEIYASCBTEMGEPNANDICTRTAHSOIEERO";
            string answer = "HEREISASECRETMESSAGEENCIPHEREDBYTRANSPOSITION";
            Assert.AreEqual(answer, algorytm.Decipher(ciphered));
        }

        [Test, Category("Exercies2")]
        public void TestPrzestawieniaMacierzoweDecipherBasic2()
        {
            PrzestawieniaMacierzoweC algorytm = new PrzestawieniaMacierzoweC();
            algorytm.PrepareKey("MAMYTO");
            string ciphered = "oe.fniosT,jtyawadoszramczswao ";
            string answer = "To,jest.zaszyfrowana wiadomosc";
            Assert.AreEqual(answer, algorytm.Decipher(ciphered));
        }

        [Test, Category("Exercies2")]
        public void TestPrzestawieniaMacierzoweDecipherBasicShortKey()
        {
            PrzestawieniaMacierzoweC algorytm = new PrzestawieniaMacierzoweC();
            algorytm.PrepareKey("M");
            string ciphered = "To,jest.zaszyfrowana wiadomosc";
            string answer = "To,jest.zaszyfrowana wiadomosc";
            Assert.AreEqual(answer, algorytm.Decipher(ciphered));
        }

        [Test, Category("Exercies2")]
        public void TestPrzestawieniaMacierzoweDecipherBasicKeyLonger()
        {
            PrzestawieniaMacierzoweC algorytm = new PrzestawieniaMacierzoweC();
            algorytm.PrepareKey("MAMYTO");
            string ciphered = "E,JST";
            string answer = "JEST,";
            Assert.AreEqual(answer, algorytm.Decipher(ciphered));
        }
    }
}