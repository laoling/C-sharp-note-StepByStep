using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chapter14ExtensionLib;

namespace Chapter14Example05
{
    //本实例用于演示定义和使用扩展方法
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Emter a string to convert:");
            string sourceString = Console.ReadLine();
            Console.WriteLine("String with title casing: {0}", sourceString.GetWords(capitalizeWords: true).AsSentence());
            Console.WriteLine("String backwards: {0}", sourceString.GetWords(reverseOrder: true, reverseWords: true).AsSentence());
            Console.WriteLine("String length backwards: {0}", sourceString.Length.ToStringReversed());

            Console.ReadKey();
        }
    }
}
