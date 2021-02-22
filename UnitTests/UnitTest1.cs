using NUnit.Framework;
using Bezpieczeñstwo.Algorithms;
using System;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
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
            for(int i = 0; i < n; i++)
            {
                char letter = (char)rand.Next(65, 90);
                notCiphered += letter.ToString();
            }
            Assert.AreEqual(true, algorytm.PrepareKey(key, '-'));
            string ciphered = algorytm.CipherString(notCiphered);

            string notCipheredAgain = algorytm.DecipherString(ciphered);
            Assert.AreEqual(notCipheredAgain, notCiphered);
        }
    }
}