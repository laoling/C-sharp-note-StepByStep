using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter14Example04
{
    //本实例用于演示命名参数和可选参数的使用
    class Program
    {
        static void Main(string[] args)
        {
            string sentence = "thahdai adjudn, this isn andml jdaojaoj p!";
            List<string> words;

            words = WordProcessor.GetWords(sentence);
            Console.WriteLine("Original sentence:");
            foreach (string word in words)
            {
                Console.Write(word);
                Console.Write(' ');
            }
            Console.WriteLine("\n");

            words = WordProcessor.GetWords(
                sentence,
                reverseWords: true,
                capitalizeWords: true);
            Console.WriteLine("Capitalized sentence with reversed words:");
            foreach (string word in words)
            {
                Console.Write(word);
                Console.Write(' ');
            }

            Console.ReadKey();
        }
    }
}
