using CaeserCipher.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CaeserCipher
{
    public class Program
    {
        static char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };


        static void Main(string[] args)
        {
            DecryptFrequencyAnalysis();

            Console.ReadKey();
        }

        static private void DecryptFileSimple()
        {
            string textInputFile = @"E:\\Development\\Software\\CaeserCipher\\CaeserCipher\\CaeserCipher\\EncryptedFiles\\important.txt";

            using (var input = new StreamReader(File.OpenRead(textInputFile), Encoding.ASCII))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    for (int i = 0; i < alphabet.Length - 1; i++)
                    {
                        Console.WriteLine("Shift by: " + i);

                        var potentialText = ShiftLettersByValue(line, i);

                        Console.WriteLine(potentialText.Take(20).ToArray());
                        Console.WriteLine("\n");
                    }
                    Console.WriteLine("Which line looks correct?");

                    var correctVal = Console.ReadLine();

                    var correctText = ShiftLettersByValue(line, Convert.ToInt32(correctVal));
                    Console.WriteLine(correctText);
                }
            }

        }

        static private void DecryptFrequencyAnalysis()
        {
            string textInputFile = @"E:\\Development\\Software\\CaeserCipher\\CaeserCipher\\CaeserCipher\\EncryptedFiles\\important.txt";

            var letterCounts = new List<LetterCount>();

            for (int i = 0; i < alphabet.Length; i++)
            {
                letterCounts.Add(new LetterCount(alphabet[i], 0));
            }

            using (var input = new StreamReader(File.OpenRead(textInputFile), Encoding.ASCII))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    int highestCount = 0;
                    char highestCountLetter = 'a';

                    foreach (char key in line)
                    {
                        if (alphabet.Contains(key))
                        {
                            letterCounts.First(x => x.Letter == key).Count++;
                            if (letterCounts.First(x => x.Letter == key).Count > highestCount)
                            {
                                highestCount = letterCounts.First(x => x.Letter == key).Count;
                                highestCountLetter = key;
                            }
                        }
                    }
                    int indexOfE = Array.IndexOf(alphabet, 'e');
                    int likelyShift = indexOfE - Array.IndexOf(alphabet, highestCountLetter);

                    var correctText = ShiftLettersByValue(line, likelyShift);
                    Console.WriteLine(correctText);
                }
            }

        }

        static public string ShiftLettersByValue(string text, int shiftValue)
        {
            string outputText = "";

            foreach (char key in text)
            {
                int position = Array.IndexOf(alphabet, Char.ToLower(key));
                if (position == -1)
                {
                    outputText += Char.ToLower(key);
                    continue;
                }

                int newPosition = (position + shiftValue) % alphabet.Length;
      
                if (newPosition < 0) newPosition = (26 + newPosition);
                outputText += alphabet.GetValue(newPosition);
            }
            return outputText;
        }

        static async private void SetTextFile()
        {
            Random _random = new Random();
            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            string textInputFile = @"E:\\Development\\Software\\CaeserCipher\\CaeserCipher\\CaeserCipher\\UnencryptedFiles\\important.txt";
            string textOutputFile = @"E:\\Development\\Software\\CaeserCipher\\CaeserCipher\\CaeserCipher\\EncryptedFiles\\important.txt";

            string outputText = "";
            int shiftVal = _random.Next(alphabet.Length - 1);

            Console.WriteLine("Shift Value: " + shiftVal);

            using (var input = new StreamReader(File.OpenRead(textInputFile), Encoding.ASCII))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    foreach (char key in line)
                    {
                        int position = Array.IndexOf(alphabet, Char.ToLower(key));
                        if (position == -1)
                        {
                            outputText += Char.ToLower(key);
                            continue;
                        }
                        int newPosition = (position + shiftVal) % alphabet.Length;

                        outputText += alphabet.GetValue(newPosition);
                    }
                }
            }

            Console.WriteLine("Finished text: ");
            Console.WriteLine(outputText);
            await File.WriteAllTextAsync(textOutputFile, outputText);
        }
    }
}
