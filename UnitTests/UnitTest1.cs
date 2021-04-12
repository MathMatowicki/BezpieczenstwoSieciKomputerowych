using NUnit.Framework;
using Bezpieczeństwo.Algorithms;
using System;
using System.IO;

namespace UnitTests
{
    public class Tests
    {
        private RailFence algorithmRailFence;
        private Vigenere algorithmViegnere;
        private PrzestawieniaMacierzoweA algorithmPMA;
        private string keyRailFence = "";
        [SetUp]
        public void Setup()
        {
            algorithmRailFence = new RailFence();
            algorithmPMA = new PrzestawieniaMacierzoweA();
            algorithmViegnere = new Vigenere();
        }

        [Test, Category("Exercies1")]
        public void TestRailFenceKey()
        {

            keyRailFence = "1";

            Assert.AreEqual(true, algorithmRailFence.PrepareKey(keyRailFence));

            keyRailFence = "0";

            Assert.AreEqual(false, algorithmRailFence.PrepareKey(keyRailFence));

            keyRailFence = "-2";

            Assert.AreEqual(false, algorithmRailFence.PrepareKey(keyRailFence));

            keyRailFence = "Random string";

            Assert.AreEqual(false, algorithmRailFence.PrepareKey(keyRailFence));
        }

        [Test, Category("Exercies1")]
        public void TestRailFenceCipherAlghoritm()
        {
            string notCiphered = "CRYPTOGRAPHY";
            string ciphered = "CYTGAHRPORPY";

            keyRailFence = "1";
            Assert.AreEqual(notCiphered, algorithmRailFence.Cipher(notCiphered, keyRailFence));

            keyRailFence = "2";
            Assert.AreEqual(ciphered, algorithmRailFence.Cipher(notCiphered, keyRailFence));

            keyRailFence = "3";
            ciphered = "CTARPORPYYGH";

            Assert.AreEqual(ciphered, algorithmRailFence.Cipher(notCiphered, keyRailFence));

            keyRailFence = "3";
            notCiphered = "CART CART";
            ciphered = "C TATCRRA";
            Assert.AreEqual(ciphered, algorithmRailFence.Cipher(notCiphered, keyRailFence));

            keyRailFence = "2";
            notCiphered = "CART CART";
            ciphered = "CR ATATCR";
            Assert.AreEqual(ciphered, algorithmRailFence.Cipher(notCiphered, keyRailFence));
        }

        [Test, Category("Exercies1")]
        public void TestRailFenceDecryptAlghoritm()
        {
            string notDecrypted = "CYTGAHRPORPY";
            string decrypted = "CRYPTOGRAPHY";

            keyRailFence = "1";
            Assert.AreEqual(notDecrypted, algorithmRailFence.Decrypt(notDecrypted, keyRailFence));

            keyRailFence = "2";
            Assert.AreEqual(decrypted, algorithmRailFence.Decrypt(notDecrypted, keyRailFence));

            keyRailFence = "3";
            notDecrypted = "CTARPORPYYGH";
            Assert.AreEqual(decrypted, algorithmRailFence.Decrypt(notDecrypted, keyRailFence));

            keyRailFence = "3";
            notDecrypted = "C TATCRRA";
            decrypted = "CART CART";
            Assert.AreEqual(decrypted, algorithmRailFence.Decrypt(notDecrypted, keyRailFence));

            keyRailFence = "2";
            notDecrypted = "CR ATATCR";
            decrypted = "CART CART";
            Assert.AreEqual(decrypted, algorithmRailFence.Decrypt(notDecrypted, keyRailFence));
        }

        [Test, Category("Exercies1")]
        public void TestPrzestawieniaMacierzoweABasic()
        {
            string key = "3,1,4,2";
            string notCiphered = "CRY,?OGRAP.Y";
            string ciphered = "YC,RG?RO.AYP";

            algorithmPMA.ClearKey();
            Assert.AreEqual(true, algorithmPMA.PrepareKey(key, ','));
            string cipheredWithAlgorythm = algorithmPMA.CipherString(notCiphered);
            string notCipheredWithAlgorythm = algorithmPMA.DecipherString(ciphered);


            Assert.AreEqual(ciphered, cipheredWithAlgorythm);
            Assert.AreEqual(notCiphered, notCipheredWithAlgorythm);
        }

        [Test, Category("Exercies1")]
        public void TestPrzestawieniaMacierzoweABasic2()
        {
            string key = "3,1,4,2";
            string notCiphered = "CRYP   TOGRA  PHY";

            algorithmPMA.ClearKey();
            Assert.AreEqual(true, algorithmPMA.PrepareKey(key, ','));
            string cipheredWithAlgorythm = algorithmPMA.CipherString(notCiphered);
            string notCipheredWithAlgorythm = algorithmPMA.DecipherString(cipheredWithAlgorythm);

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

            algorithmPMA.ClearKey();
            Assert.AreEqual(true, algorithmPMA.PrepareKey(key1, '-'));
            algorithmPMA.ClearKey();
            Assert.AreEqual(false, algorithmPMA.PrepareKey(key2, '-'));
            algorithmPMA.ClearKey();
            Assert.AreEqual(false, algorithmPMA.PrepareKey(key3, '-'));
            algorithmPMA.ClearKey();
            Assert.AreEqual(false, algorithmPMA.PrepareKey(key4, '-'));
            algorithmPMA.ClearKey();
            Assert.AreEqual(false, algorithmPMA.PrepareKey(key5, '-'));
            algorithmPMA.ClearKey();
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
            algorithmPMA.ClearKey();
            Assert.AreEqual(true, algorithmPMA.PrepareKey(key, '-'));
            string ciphered = algorithmPMA.CipherString(notCiphered);

            string notCipheredAgain = algorithmPMA.DecipherString(ciphered);
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

            algorithmPMA.ClearKey();
            Assert.AreEqual(true, algorithmPMA.PrepareKey(key1, ','));
            Assert.AreEqual(notCiphered, algorithmPMA.DecipherString(ciphered1));
            Assert.AreEqual(ciphered1, algorithmPMA.CipherString(notCiphered));

            algorithmPMA.ClearKey();
            Assert.AreEqual(true, algorithmPMA.PrepareKey(key2, ','));
            Assert.AreEqual(notCiphered, algorithmPMA.DecipherString(ciphered2));
            Assert.AreEqual(ciphered2, algorithmPMA.CipherString(notCiphered));

            algorithmPMA.ClearKey();
            Assert.AreEqual(true, algorithmPMA.PrepareKey(key3, ','));
            Assert.AreEqual(notCipheredLong, algorithmPMA.DecipherString(cipheredLong));
            Assert.AreEqual(cipheredLong, algorithmPMA.CipherString(notCipheredLong));
        }

        [Test, Category("Exercies1")]
        public void TestWordKey()
        {
            WordKey wk = new WordKey();
            string key1 = "CONVENIENCE";
            int[] key1result = new int[] { 1, 10, 7, 11, 3, 8, 6, 4, 9, 2, 5 };
            string key2 = "BEZPIECZENSTWO";
            int[] key2result = new int[] { 1, 3, 13, 9, 6, 4, 2, 14, 5, 7, 10, 11, 12, 8 };
            string key3 = "abcd";
            int[] key3result = new int[] { 1, 2, 3, 4 };

            Assert.AreEqual(wk.GetKey(key1), key1result);
            Assert.AreEqual(wk.GetKey(key2), key2result);
            Assert.AreEqual(wk.GetKey(key3), key3result);
        }

        [Test, Category("Exercies1")]
        public void TestWordKeyPrepareKey()
        {
            WordKey wk = new WordKey();
            string key1 = "bezpieczenstwo";
            string key2 = "";
            string key3 = "bez pieczenstwo";

            Assert.AreEqual(wk.PrepareKey(key1), true);
            Assert.AreEqual(wk.PrepareKey(key2), false);
            Assert.AreEqual(wk.PrepareKey(key3), false);
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
        public void TestVigenerePrepareKey()
        {
            Assert.True(algorithmViegnere.PrepareKey("23asdf", "23asdf"));
            Assert.False(algorithmViegnere.PrepareKey("1", "1"));
            Assert.True(algorithmViegnere.PrepareKey("23as^#df", "23as^#df"));
            Assert.False(algorithmViegnere.PrepareKey(" ", " "));
            Assert.True(algorithmViegnere.PrepareKey("ASFDAS", "ASFDAS"));
            Assert.True(algorithmViegnere.PrepareKey("asdf", "asdf"));
            Assert.True(algorithmViegnere.PrepareKey("asdfaasdf", "asdf"));
            Assert.True(algorithmViegnere.PrepareKey("asdfasdf", "asdf"));
            Assert.True(algorithmViegnere.PrepareKey("asdf", "asdfasdgasdf"));

        }

        [Test, Category("Exercies2")]
        public void TestVigenereCipher()
        {
            string text = "CRYPTOGRAPHY";
            string key = "BREAKBREAKBR";

            Assert.True(algorithmViegnere.PrepareKey(key, text));
            Assert.AreEqual("DICPDPXVAZIP", algorithmViegnere.Cipher(text, key));

            text = "cryptography";
            key = "breakbreakbr";

            Assert.True(algorithmViegnere.PrepareKey(key, text));
            Assert.AreEqual("DICPDPXVAZIP", algorithmViegnere.Cipher(text, key));

            text = "cryptogr$$#?.  aphy";
            key = "breakbr  eakbr";

            Assert.True(algorithmViegnere.PrepareKey(key, text));
            Assert.AreEqual("DICPDPXVAZIP", algorithmViegnere.Cipher(text, key));

        }

        [Test, Category("Exercies2")]
        public void TestVigenereDecipher()
        {
            string text = "DICPDPXVAZIP";
            string key = "BREAKBREAKBR";
            Assert.True(algorithmViegnere.PrepareKey(key, text));

            Assert.AreEqual("CRYPTOGRAPHY", algorithmViegnere.Decrypt(text, key));

            text = "DICPDPXVAZIP";
            key = "breakbreakbr";

            Assert.True(algorithmViegnere.PrepareKey(key, text));
            Assert.AreEqual("CRYPTOGRAPHY", algorithmViegnere.Decrypt(text, key));

        }

        [Test, Category("Exercies2")]
        public void TestPrzestawieniaMacierzoweCipher()
        {
            PrzestawieniaMacierzoweC algorytm = new PrzestawieniaMacierzoweC();
            algorytm.PrepareKey("cbda");
            string m = "SZYFROWANIEJESTOK";
            string c = "FSZONJOSRWAETKYIE";
            Assert.AreEqual(c, algorytm.Cipher(m));

            algorytm.PrepareKey("CONVENIENCE");
            m = "HEREISASECRETMESSAGEENCIPHEREDBYTRANSPOSITION";
            c = "HEESPNIRRSSEESEIYASCBTEMGEPNANDICTRTAHSOIEERO";
            Assert.AreEqual(c, algorytm.Cipher(m));
        }

        [Test, Category("Exercies2")]
        public void TestPrzestawieniaMacierzoweCipherSpecialSigns()
        {
            PrzestawieniaMacierzoweC algorytm = new PrzestawieniaMacierzoweC();
            algorytm.PrepareKey("CONVENIENCE");
            string m = "HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION";
            string c = "HEE   NONSEITSITIAEED GHAERENYPISAPRO RRCMEBSS ESC T";
            Assert.AreEqual(c, algorytm.Cipher(m));
        }

        [Test, Category("Exercies2")]
        public void TestPrzestawieniaMacierzoweCipherShortKey()
        {
            PrzestawieniaMacierzoweC algorytm = new PrzestawieniaMacierzoweC();
            algorytm.PrepareKey("C");
            string m = "HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION";
            string c = "HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION";
            Assert.AreEqual(c, algorytm.Cipher(m));
        }

        [Test, Category("Exercies2")]
        public void TestPrzestawieniaMacierzoweCipherKeyLonger()
        {
            PrzestawieniaMacierzoweC algorytm = new PrzestawieniaMacierzoweC();
            algorytm.PrepareKey("CONVENIENCE");
            string m = "Szyfr!";
            string c = "Sz!fyr";
            Assert.AreEqual(c, algorytm.Cipher(m));
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

        [Test, Category("Exercies2")]
        public void TestPrzestawieniaMacierzoweDecipherShorts()
        {
            PrzestawieniaMacierzoweC algorytm = new PrzestawieniaMacierzoweC();
            algorytm.PrepareKey("Klucz");
            string ciphered = "";
            string answer = "";
            Assert.AreEqual(answer, algorytm.Decipher(ciphered));
            ciphered = "E";
            answer = "E";
            Assert.AreEqual(answer, algorytm.Decipher(ciphered));
        }

        [Test, Category("Exercies2")]
        public void TestPrzestawieniaMacierzoweCipherDecipher()
        {
            PrzestawieniaMacierzoweC algorytm = new PrzestawieniaMacierzoweC();
            algorytm.PrepareKey("Klucz");
            string notCiphered = "here is a secret message, please keep it a secret! It will be awesome :)";
            string ciphered = algorytm.Cipher(notCiphered);
            Assert.AreEqual(notCiphered, algorytm.Decipher(ciphered));
        }

        [Test, Category("Exercies2")]
        public void TestCezarPrepareKey()
        {
            Cezara algorytm = new Cezara();
            Assert.AreEqual(algorytm.PrepareKey("3"), true);
            Assert.AreEqual(algorytm.PrepareKey("2345621"), true);
            Assert.AreEqual(algorytm.PrepareKey("3 "), true);
            Assert.AreEqual(algorytm.PrepareKey("3 3"), false);
            Assert.AreEqual(algorytm.PrepareKey("3 sdax"), false);
            Assert.AreEqual(algorytm.PrepareKey(""), false);
        }

        [Test, Category("Exercies2")]
        public void TestCezarPrepareText()
        {
            Cezara algorytm = new Cezara();
            Assert.AreEqual(algorytm.PrepareText("3"), true);
            Assert.AreEqual(algorytm.PrepareText("3oidui ox cPx"), true);
            Assert.AreEqual(algorytm.PrepareText("wed.dcloc posd"), false);
            Assert.AreEqual(algorytm.PrepareText("(33qeaSDF)"), false);
        }

        [Test, Category("Exercies2")]
        public void TestCezarCipher()
        {
            Cezara algorytm = new Cezara();
            Assert.AreEqual(algorytm.Cipher("CRYPTOGRAPHY", "3"), "FU1SWRJUDSK1");
            Assert.AreEqual(algorytm.Cipher("Bezpieczenstwo", "7"), "IL6WPLJ6LUZ03V");
            Assert.AreEqual(algorytm.Cipher("Szyfrowanie", "7"), "Z65MYV3HUPL");
            Assert.AreEqual(algorytm.Cipher("Szyfrowanie", "73"), "RYXEQNV MHD");
            Assert.AreEqual(algorytm.Cipher("Szyfrowanie", "1"), "T0ZGSPXBOJF");
            Assert.AreEqual(algorytm.Cipher("Szyfrowanie", "0"), "SZYFROWANIE");
            Assert.AreEqual(algorytm.Cipher("Szyfrowanie!!!", "0"), "Text can not have special characters");
            Assert.AreEqual(algorytm.Cipher("Szyfrowanie", "5,9"), "ERROR");
        }

        [Test, Category("Exercies2")]
        public void TestCezarDecipher()
        {
            Cezara algorytm = new Cezara();
            Assert.AreEqual(algorytm.Decrypt("FU1SWRJUDSK1", "3"), "CRYPTOGRAPHY");
            Assert.AreEqual(algorytm.Decrypt("IL6WPLJ6LUZ03V", "7"), "BEZPIECZENSTWO");
            Assert.AreEqual(algorytm.Decrypt("Szyfrowanie", "7"), "LSR9KHP4GB8");
            Assert.AreEqual(algorytm.Decrypt("Szyfrowanie", "73"), "T0ZGSPXBOJF");
            Assert.AreEqual(algorytm.Decrypt("Szyfrowanie", "1"), "RYXEQNV MHD");
            Assert.AreEqual(algorytm.Decrypt("Szyfrowanie", "0"), "SZYFROWANIE");
            Assert.AreEqual(algorytm.Decrypt("Szyfrowanie!!!", "0"), "Text can not have special characters");
            Assert.AreEqual(algorytm.Decrypt("Szyfrowanie", "5,9"), "ERROR");
        }

        [Test, Category("Exercies2")]
        public void TestCezarCipherAndDecipher()
        {
            Cezara algorytm = new Cezara();
            string m = "HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION";
            string c = algorytm.Cipher(m, "5");
            Assert.AreEqual(algorytm.Decrypt(c, "5"), m);
            c = algorytm.Cipher(m, "15");
            Assert.AreEqual(algorytm.Decrypt(c, "15"), m);
            c = algorytm.Cipher(m, "9");
            Assert.AreEqual(algorytm.Decrypt(c, "9"), m);
        }

        [Test, Category("Exercies3")]
        public void TestLsfrTextFile()
        {
            Lsfr lsfr = new Lsfr(new int[] { 5, 4, 2, 15, 9, 8 });
            lsfr.Initialize();
            for (int i = 0; i <= 65; i++)
                lsfr.Iteration();

            byte[] content = System.IO.File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "szyfrstrumieniowy.txt"));
            SzyfrStrumieniowy ss = new SzyfrStrumieniowy(lsfr);
            byte[] ciphered = ss.Cipher(content);
            byte[] decrypted = ss.Decrypt(ciphered);

            Assert.AreEqual(decrypted.Length, content.Length);
            for (int i = 0; i < content.Length; i++)
                Assert.AreEqual(content[i], decrypted[i]);
        }

        [Test, Category("Exercies3")]
        public void TestSzyfrStrumieniowyPNG()
        {
            Lsfr lsfr = new Lsfr(new int[] { 5, 20, 9, 8 });
            lsfr.Initialize();
            for (int i = 0; i <= 65; i++)
                lsfr.Iteration();

            byte[] content = System.IO.File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "szyfrstrumieniowyobraz.png"));
            SzyfrStrumieniowy ss = new SzyfrStrumieniowy(lsfr);
            byte[] ciphered = ss.Cipher(content);
            byte[] decrypted = ss.Decrypt(ciphered);

            Assert.AreEqual(decrypted.Length, content.Length);
            for (int i = 0; i < content.Length; i++)
                Assert.AreEqual(content[i], decrypted[i]);
        }


        [Test, Category("Exercies3")]
        public void TestSzyfrStrumieniowyShorterKey()
        {
            Lsfr lsfr = new Lsfr(new int[] { 5, 4, 2, 15, 9, 8 });
            lsfr.Initialize();
            for (int i = 0; i <= 30; i++)
                lsfr.Iteration();

            byte[] content = System.IO.File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "szyfrstrumieniowy.txt"));
            SzyfrStrumieniowy ss = new SzyfrStrumieniowy(lsfr);
            byte[] ciphered = ss.Cipher(content);
            byte[] decrypted = ss.Decrypt(ciphered);

            Assert.AreEqual(decrypted.Length, content.Length);
            for (int i = 0; i < content.Length; i++)
                Assert.AreEqual(content[i], decrypted[i]);
        }

        [Test, Category("Exercies3")]
        public void TestSzyfrStrumieniowyMP4()
        {
            Lsfr lsfr = new Lsfr(new int[] { 5, 4, 2, 20, 17, 8 });
            lsfr.Initialize();
            for (int i = 0; i <= 30; i++)
                lsfr.Iteration();

            byte[] content = System.IO.File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "szyfrstrumieniowymp4.mp4"));
            SzyfrStrumieniowy ss = new SzyfrStrumieniowy(lsfr);
            byte[] ciphered = ss.Cipher(content);
            byte[] decrypted = ss.Decrypt(ciphered);

            Assert.AreEqual(decrypted.Length, content.Length);
            for (int i = 0; i < content.Length; i++)
                Assert.AreEqual(content[i], decrypted[i]);
        }

        [Test, Category("Exercies4")]
        public void Test4()
        {
            DES des = new DES();
            des.functionF(2875535566, 254632997710694);
            Assert.AreEqual(true, true);
        }

        [Test, Category("Exercies4")]
        public void TestKeyPC1()
        {
            long i = 1383827165325090800;
            // i w systemie dwójkowym "0001001100110100010101110111100110011011101111001101111111110001";
            // wynik w systemie dziesietnym long r = 67779029043144591;
            string sr = "11110000110011001010101011110101010101100110011110001111";

            DESkey dk = new DESkey(i);
            //permutedChoice1
            int[] result =  dk.permutedChoice1();
            string resultstring = "";
            foreach (int j in result) resultstring += j;
            Assert.AreEqual(sr, resultstring);

            //split
            long l = 252496559;
            long p = 89548687;
            long spl_l = dk.split(result, 0, 27);
            //long spl_p = dk.split(result, 28, 55);
            Assert.AreEqual(l, spl_l);

        }

        /*
        [Test, Category("Exercies4")]
        public void TestKey()
        {
            DESkey dk = new DESkey();
            byte value1=0;
            int int1 = 128;
            try
            {
                value1 = (byte)int1;
                Console.WriteLine(value1);
            }
            catch (OverflowException)
            {
                Console.WriteLine("{0} is out of range of a byte.", int1);
            }
            long l = Convert.ToInt64(value1);

            long i = 1383827165325090801;
            long o1 = 29699430183026;
            dk.generateKey(i);
            long[] key = dk.getKeyCipher();
            Assert.AreEqual(o1, key[0]);

        }*/

    }
}