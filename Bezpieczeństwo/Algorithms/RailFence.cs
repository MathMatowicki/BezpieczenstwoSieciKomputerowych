﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bezpieczeństwo.Algorithms
{
    public class RailFence
    {
        private int key;
        public bool PrepareKey(String key)
        {
            int n;
            if (int.TryParse(key, out n) && n > 0)
            {
                this.key = n;
                return true;
            }
            return false;
        }

        public String Cipher(String text, String key)
        {
            if (!PrepareKey(key)) return "ERROR";
            // Edge case if line is only 1 than algorithm will return decrypted text
            if (this.key == 1) return text;

            var lines = new List<StringBuilder>();

            for (int i = 0; i < this.key; i++)
                lines.Add(new StringBuilder());

            // Here we follow a zig-zag pattern around the lines by turning
            // direction into 1 or -1 if we hit a 'wall' (first or last line)

            int currentLine = 0;
            int direction = 1;

            for (int i = 0; i < text.Length; i++)
            {
                lines[currentLine].Append(text[i]);

                if (currentLine == 0)
                    direction = 1;
                else if (currentLine == this.key - 1)
                    direction = -1;

                currentLine += direction;
            }

            // Now we just take all the lines all put them in the result

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < this.key; i++)
                result.Append(lines[i].ToString());

            return result.ToString();
        }
        public String Decrypt(string text, String key)
        {

            if (!PrepareKey(key)) return "ERROR";
            // Edge case if line is only 1 than algorithm will return encrypted text
            if (this.key == 1) return text;

            var lines = new List<StringBuilder>();

            for (int i = 0; i < this.key; i++)
                lines.Add(new StringBuilder());

            // We can't decrypt it without knowing the lenght of each line first
            int[] linesLenght = Enumerable.Repeat(0, this.key).ToArray();

            // To discover the lenght of each line, we follow the same loop as the 
            // encryption method, but instead of adding characters, we add to the 
            // total count of line lenghts

            int currentLine = 0;
            int direction = 1;

            for (int i = 0; i < text.Length; i++)
            {
                linesLenght[currentLine]++;

                if (currentLine == 0)
                    direction = 1;
                else if (currentLine == this.key - 1)
                    direction = -1;

                currentLine += direction;
            }

            // Now that we know the lenght of each line, we can
            // take the appropriate amount of characters from the text

            int currentChar = 0;

            for (int line = 0; line < this.key; line++)
            {
                for (int c = 0; c < linesLenght[line]; c++)
                {
                    lines[line].Append(text[currentChar]);
                    currentChar++;
                }
            }

            // Now that we have the lines separated, we follow the contrary
            // of what is done in Encrypt: take away the characters from
            // each line and put them in the result

            StringBuilder result = new StringBuilder();

            currentLine = 0;
            direction = 1;

            // This stores the position we last read the line at, so we know which
            // characters to take from which line
            int[] currentReadLine = Enumerable.Repeat(0, this.key).ToArray();

            for (int i = 0; i < text.Length; i++)
            {

                result.Append(lines[currentLine][currentReadLine[currentLine]]);
                currentReadLine[currentLine]++;

                if (currentLine == 0)
                    direction = 1;
                else if (currentLine == this.key - 1)
                    direction = -1;

                currentLine += direction;
            }

            return result.ToString();
        }


    }
}
