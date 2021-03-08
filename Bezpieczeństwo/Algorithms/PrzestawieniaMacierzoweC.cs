using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bezpieczeństwo.Algorithms
{
    public class PrzestawieniaMacierzoweC
    {
        private int[] key;
        public bool PrepareKey(String key)
        {
            WordKey wk = new WordKey();
            this.key = wk.GetKey(key);
            if (this.key != null) return true;
            else return false;
        }

        public string Decipher(string sequence)
        {
            int n = key.Length + 1;
            int[] invertedKey = new int[n];
            int[] numOfElemInColumn = new int[n];
            int[] currentIndexOfElemInColumn = new int[n];
            for(int i=0; i < key.Length; i++)
            {
                invertedKey[key[i]] = i;
                numOfElemInColumn[i] = 0;
                currentIndexOfElemInColumn[i] = 0;
            }
            invertedKey[0] = 0;
            numOfElemInColumn[0] = 0;
            currentIndexOfElemInColumn[0] = 0;

            int numberOfRows = 1, numberOfLetters = 0;
            while(numberOfLetters < sequence.Length)
            {
                numberOfLetters += invertedKey[numberOfRows] + 1;
                numberOfRows++;
            }

            numOfElemInColumn[key[n - 2]] = key[n - 2] <= numberOfRows ? 1 : 0;
            for (int i = n - 3; i >= 0; i--)
            {
                if (key[i] <= numberOfRows)
                    numOfElemInColumn[key[i]] = numOfElemInColumn[key[i + 1]] + 1;
                else
                    numOfElemInColumn[key[i]] = numOfElemInColumn[key[i + 1]];
            }
            return "";
        }
    }
}
