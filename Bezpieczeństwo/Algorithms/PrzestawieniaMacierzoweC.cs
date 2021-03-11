using System;
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

        public string Cipher(string sequence)
        {
            string result = "";
            int n = this.key.Length;
            List<List<char>> list = new List<List<char>>();
            for (int i = 0; i < n; i++)
            {
                list.Add(new List<char>());
            }

            int s=0, pom = 1;
            //uzupełnianie listy
            while (s < sequence.Length)
            {
                for (int i = 0; i < n; i++)
                {
                    if (this.key[i] != pom)
                    {
                        if (s >= sequence.Length) break;
                        list[i].Add(sequence[s]);
                        s++;
                    }
                    else
                    {
                        if (s >= sequence.Length) break;
                        list[i].Add(sequence[s]);
                        s++;
                        pom++;
                        if (pom > n) pom = 1;
                        break;
                    }
                }
            }
            s = 0;
            pom = 1;
            //odczytywanie wyniku

            while (s < sequence.Length)
            {
                for (int i = 0; i < n; i++)
                {
                    if (key[i] == pom)
                    {
                        for(int j = 0;j < list[i].Count(); j++)
                        {
                            result += list[i][j];
                            s++;
                        }
                        pom++;
                        if (pom > n) pom = 1;
                        break;
                    }
                }
            }


            return result;
        }
        public string Decipher(string sequence)
        {
            if(key == null) return "ERROR";
            int n = key.Length + 1;
            this.PrepareInvertedKey();
            int[] numOfElemInColumn = new int[n];
            int[] currentIndexOfElemInColumn = new int[n];
            int numberOfFullKeys = 
                sequence.Length/ numberOfLettersWithWholeMatrixFilled();
            int numberOfLeftOvers = sequence.Length % numberOfLettersWithWholeMatrixFilled();

            for (int i = 0; i<n; i++)
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

            numberOfRows--;
            numOfElemInColumn[key[n - 2]] = key[n - 2] <= numberOfRows ? 1 : 0;
            for (int i = n - 3; i >= 0; i--)
            {
                if (key[i] <= numberOfRows)
                    numOfElemInColumn[key[i]] = numOfElemInColumn[key[i + 1]] + 1;
                else
                    numOfElemInColumn[key[i]] = numOfElemInColumn[key[i + 1]];
            }

            if (numberOfLeftOvers < numberOfLetters)
            {
                int index = invertedKey[numberOfRows];
                for (int i = index; numberOfLetters - numberOfLeftOvers > invertedKey[numberOfRows] - i; i--)
                    numOfElemInColumn[key[i]]--;
            }
            for (int i=0; i< key.Length; i++)
                numOfElemInColumn[key[i]] += (key.Length - invertedKey[key[i]]) * numberOfFullKeys;

            for(int i=1; i<n; i++)
                currentIndexOfElemInColumn[i] = currentIndexOfElemInColumn[i - 1] + numOfElemInColumn[i - 1];

            StringBuilder output = new StringBuilder("");
            for(int i=1; i <= numberOfRows + key.Length * numberOfFullKeys; i++)
            {
                for(int j=0; j< key.Length ; j++)
                {
                    int x = currentIndexOfElemInColumn[key[j]];
                    output.Append(sequence[x]);
                    currentIndexOfElemInColumn[key[j]]++;
                    if (key[j] == i % key.Length || (key[j] == key.Length && i % key.Length == 0)) break;
                    if (output.Length == sequence.Length) break;
                }
                if (output.Length == sequence.Length) break;
            }
            String returnValue = output.ToString();
            return returnValue;
        }
    }
}
