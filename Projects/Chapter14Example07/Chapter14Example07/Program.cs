using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter14Example07
{
    //本实例用于演示使用Lambda表达式和集合
    class Program
    {
        static void Main(string[] args)
        {
            string[] curries = {"pathis", "jalfreeze", "korma"};

            Console.WriteLine(curries.Aggregate((a, b) => a +" "+ b));
            Console.WriteLine(curries.Aggregate<string, int>(
                0,
                (a, b) => a + b.Length));
            Console.WriteLine(curries.Aggregate<string, string, string>(
                "Some curries: ",
                (a, b) => a + " " + b,
                a => a));
            Console.WriteLine(curries.Aggregate<string, string, int>(
                "Some curries: ",
                (a, b) => a + " " + b,
                a => a.Length));

            Console.ReadKey();
        }
    }
}
