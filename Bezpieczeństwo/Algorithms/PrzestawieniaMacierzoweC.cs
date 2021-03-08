﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bezpieczeństwo.Algorithms
{
    public class PrzestawieniaMacierzoweC
    {
        private int[] key;
        private int[] invertedKey;
        public bool PrepareKey(String key)
        {
            WordKey wk = new WordKey();
            this.key = wk.GetKey(key);
            if (this.key != null) return true;
            else return false;
        }

        private void PrepareInvertedKey()
        {
            invertedKey = new int[key.Length+1];
            for (int i = 0; i < key.Length; i++)
            {
                invertedKey[key[i]] = i;
            }
            invertedKey[0] = 0;
        }

        private int numberOfLettersWithWholeMatrixFilled()
        {
            int numberOfLetters = 0;
            for(int i = 1; i <= key.Length; i++)
                numberOfLetters += invertedKey[i] + 1;
            return numberOfLetters;
        }

        /*private int[] fullMatrixFilled()
        {
            int[] numOfElemInColumn = new int[key.Length + 1];
            numOfElemInColumn[key[key.Length - 1]] = 1;
            for (int i = key.Length - 2; i >= 0; i--)
                numOfElemInColumn[key[i]] = numOfElemInColumn[key[i + 1]] + 1;

            return numOfElemInColumn;
        }*/

        public string Decipher(string sequence)
        {
            int n = key.Length + 1;
            this.PrepareInvertedKey();
            int[] numOfElemInColumn = new int[n];
            int[] currentIndexOfElemInColumn = new int[n];
            int numberOfFullKeys = 
                sequence.Length/ numberOfLettersWithWholeMatrixFilled();
            int numberOfLeftOvers = sequence.Length % numberOfLettersWithWholeMatrixFilled();

            for (int i = 0; i<=n; i++)
            {
                numOfElemInColumn[i] = 0;
                currentIndexOfElemInColumn[i] = 0;
            }

            int numberOfRows = 1, numberOfLetters = 0;
            while(numberOfLetters < numberOfLeftOvers)
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

            for(int i=0; i< key.Length; i++)
                numOfElemInColumn[key[i]] += (key.Length - invertedKey[key[i]]) * numberOfFullKeys;

            for(int i=1; i<=n; i++)
                currentIndexOfElemInColumn[i] = currentIndexOfElemInColumn[i - 1] + numOfElemInColumn[i - 1];

            StringBuilder output = new StringBuilder("");
            for(int i=1; i < numberOfRows + key.Length * numberOfFullKeys; i++)
            {
                for(int j=0; j< key.Length && key[j] != i%(key.Length + 1)+1 ; j++)
                {
                    output.Append(sequence[currentIndexOfElemInColumn[key[j]]]);
                    currentIndexOfElemInColumn[key[j]]++;
                }
            }
            return output.ToString();
        }
    }
}
