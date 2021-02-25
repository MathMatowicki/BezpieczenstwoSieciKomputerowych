using NUnit.Framework;
using Bezpieczeństwo.Algorithms;
using System;

namespace UnitTests
{
    public class Tests
    {
        private RailFence alghoritmRailFence;
        private string keyRailFence = "";
        [SetUp]
        public void Setup()
        {
            alghoritmRailFence = new RailFence();
        }

        [Test]
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

        [Test]
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
        }

        [Test]
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
        }

        [Test]
        public void TestPrzestawieniaMacierzoweABasic()
        {
            PrzestawieniaMacierzoweA algorytm = new PrzestawieniaMacierzoweA();
            string key = "3,1,4,2";
            string notCiphered = "CRYPTOGRAPHY";
            string ciphered = "YCPRGTROHAYP";

            Assert.AreEqual(true, algorytm.PrepareKey(key, ','));
            string cipheredWithAlgorythm = algorytm.CipherString(notCiphered);
            string notCipheredWithAlgorythm = algorytm.DecipherString(ciphered);


            Assert.AreEqual(ciphered, cipheredWithAlgorythm);
            Assert.AreEqual(notCiphered, notCipheredWithAlgorythm);
        }

        [Test]
        public void TestPrzestawieniaMacierzoweAKey()
        {
            PrzestawieniaMacierzoweA algorytm = new PrzestawieniaMacierzoweA();
            string key1 = "3-1-4-2";
            string key2 = "3-1-4";
            string key3 = "3-a-4-2";
            string key4 = "3-0-4-2";

            Assert.AreEqual(true, algorytm.PrepareKey(key1, '-'));
            algorytm.ClearKey();
            Assert.AreEqual(false, algorytm.PrepareKey(key2, '-'));
            algorytm.ClearKey();
            Assert.AreEqual(false, algorytm.PrepareKey(key3, '-'));
            algorytm.ClearKey();
            Assert.AreEqual(false, algorytm.PrepareKey(key4, '-'));
            algorytm.ClearKey();
        }

        [Test]
        public void TestPrzestawieniaMacierzoweA()
        {
            PrzestawieniaMacierzoweA algorytm = new PrzestawieniaMacierzoweA();
            Random rand = new Random();
            string key = "4-3-1-2";
            string notCiphered = "";
            int n = rand.Next(10, 50);
            for (int i = 0; i < n; i++)
            {
                char letter = (char)rand.Next(65, 90);
                notCiphered += letter.ToString();
            }
            Assert.AreEqual(true, algorytm.PrepareKey(key, '-'));
            string ciphered = algorytm.CipherString(notCiphered);

            string notCipheredAgain = algorytm.DecipherString(ciphered);
            Assert.AreEqual(notCipheredAgain, notCiphered);
        }

        [Test]
        public void TestPrzestawieniaMacierzoweAMore()
        {
            PrzestawieniaMacierzoweA algorytm = new PrzestawieniaMacierzoweA();
            string key1 = "9,4,3,1,8,5,7,6,2";
            string key2 = "4,3,1,9,5,8,7,6,2";
            string key3 = "8,7,9,5,2,1,3,6,4";
            string notCiphered = "GABRYSIA";
            string notCipheredLong = "GABRYSIAGABRYSI";
            string ciphered1 = "RBGAYISA";
            string ciphered2 = "RBGYAISA";
            string cipheredLong = "AIGYAGBSRSBARIY";


            Assert.AreEqual(true, algorytm.PrepareKey(key1, ','));
            Assert.AreEqual(notCiphered, algorytm.DecipherString(ciphered1));
            Assert.AreEqual(ciphered1, algorytm.CipherString(notCiphered));

            algorytm.ClearKey();
            Assert.AreEqual(true, algorytm.PrepareKey(key2, ','));
            Assert.AreEqual(notCiphered, algorytm.DecipherString(ciphered2));
            Assert.AreEqual(ciphered2, algorytm.CipherString(notCiphered));

            algorytm.ClearKey();
            Assert.AreEqual(true, algorytm.PrepareKey(key3, ','));
            Assert.AreEqual(notCipheredLong, algorytm.DecipherString(cipheredLong));
            Assert.AreEqual(cipheredLong, algorytm.CipherString(notCipheredLong));
        }

        [Test]
        public void TestPrzestawieniaMacierzoweBKey()
        {
            PrzestawieniaMacierzoweB algorytm = new PrzestawieniaMacierzoweB();
            string key1 = "CONVENIENCE";
            int[] key1result = new int[] {1,10,7,11,3,8,6,4,9,2,5};
            string key2 = "BEZPIECZENSTWO";
            int[] key2result = new int[] { 1, 3, 13, 9, 6, 4, 2, 14, 5, 7, 10, 11, 12, 8 };

            Assert.AreEqual(algorytm.GetKey(key1), key1result);
            Assert.AreEqual(algorytm.GetKey(key2), key2result);
            
        }

        [Test]
        public void TestPrzestawieniaMacierzoweBSpaces()
        {
            PrzestawieniaMacierzoweB algorytm = new PrzestawieniaMacierzoweB();
            string s = "A B";
            int[] i = algorytm.whereSpace(s);

            Assert.AreEqual(algorytm.withoutSpace(s), "AB");
            Assert.AreEqual(algorytm.addSpace("AB", i), s);

        }

        [Test]
        public void TestPrzestawieniaMacierzoweBCipher()
        {
            PrzestawieniaMacierzoweB algorytm = new PrzestawieniaMacierzoweB();
            string m1 = "HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION";
            string c1 = "HEGEP SEN TNYT EPRNSARSSMITORR  SI C IASHAECEDOEEEBI";
            string key1 = "CONVENIENCE";

            string m2 = "CRYPTOGRAPHYOSA";
            string c2 = "RTRHSCPGPOYOAYA";
            string key2 = "TRY";

            Assert.AreEqual(algorytm.Cipher(m1,key1), c1);
            Assert.AreEqual(algorytm.Cipher(m2,key2), c2);

        }

        [Test]
        public void TestPrzestawieniaMacierzoweBDecipher()
        {
            PrzestawieniaMacierzoweB algorytm = new PrzestawieniaMacierzoweB();
            string m1 = "HECRNCEYIISEPSGDIRNTOAAESRMPNSSROEEBTETIAEEHS";
            string c1 = "HEREISASECRETMESSAGEENCIPHEREDBYTRANSPOSITION";
            //string m1 = "HECRNCEYIISEPSGDIRNTOAAESRMPNSSROEEBTETIAEEHS";
            //string c1 = "HEREISASECRETMESSAGEENCIPHEREDBYTRANSPOSITION";
            string key1 = "CONVENIENCE";

            string m2 = "RTRHSCPGPOYOAYA";
            string c2 = "CRYPTOGRAPHYOSA";
            string key2 = "TRY";

            Assert.AreEqual(algorytm.Decipher(m1, key1), c1);
            Assert.AreEqual(algorytm.Decipher(m2, key2), c2);

        }



    }
}