using System;
using System.IO;
using System.Text;

namespace CaeserCipher
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            DecryptFile();

            Console.ReadKey();
        }

        static async private void DecryptFile()
        {
            Console.WriteLine("Now decrypt me!");
        }

        static async private void SetTextFile()
        {
            Random _random = new Random();
            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            string textInputFile = @"E:\\Development\\Software\\CaeserCipher\\CaeserCipher\\CaeserCipher\\UnencryptedFiles\\important.txt";
            string textOutputFile = @"E:\\Development\\Software\\CaeserCipher\\CaeserCipher\\CaeserCipher\\EncryptedFiles\\important.txt";
            string text;
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
