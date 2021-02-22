using NUnit.Framework;
using Bezpieczeñstwo.Algorithms;
using System;

namespace UnitTests
{
    public class Tests
    {
        private RailFence alghoritmRailFence;

        [SetUp]
        public void Setup()
        {
            alghoritmRailFence = new RailFence();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }


        [Test]
        public void TestRailFenceKey()
        {

            String key = "1";

            Assert.AreEqual(true, alghoritmRailFence.PrepareKey(key));

            key = "0";

            Assert.AreEqual(false, alghoritmRailFence.PrepareKey(key));

            key = "-2";

            Assert.AreEqual(false, alghoritmRailFence.PrepareKey(key));

            key = "Dowolny String";

            Assert.AreEqual(false, alghoritmRailFence.PrepareKey(key));
        }

        [Test]
        public void TestRailFenceCipherAlghoritm()
        {
            string notCiphered = "CRYPTOGRAPHY";
            string ciphered = "CYTGAHRPORPY";
            String key = "2";

            Assert.AreEqual(ciphered, alghoritmRailFence.Cipher(notCiphered, key));

            key = "3";
            ciphered = "CTARPORPYYGH";

            Assert.AreEqual(ciphered, alghoritmRailFence.Cipher(notCiphered, key));
        }

        [Test]
        public void TestRailFenceDecryptAlghoritm()
        {
            string notDecrypted = "CYTGAHRPORPY";
            string decrypted = "CRYPTOGRAPHY";
            String key = "2";

           Assert.AreEqual(decrypted, alghoritmRailFence.Decrypt(notDecrypted, key));

            key = "3";
            notDecrypted = "CTARPORPYYGH";

            Assert.AreEqual(decrypted, alghoritmRailFence.Decrypt(notDecrypted, key));
        }

        [Test]
        public void TestPrzestawieniaMacierzoweABasic()
        {
            PrzestawieniaMacierzoweA algorytm = new PrzestawieniaMacierzoweA();
            string key = "3,1,4,2";
            string notCiphered = "CRYPTOGRAPHY";
            string ciphered = "YCPRGTROHAYP";

            Assert.AreEqual(true, algorytm.PrepareKey(key, ','));
            string cipheredWithAlgorythm =algorytm.CipherString(notCiphered);
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

    }
}