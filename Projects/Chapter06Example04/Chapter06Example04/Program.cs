using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter06Example04
{
    class Program
    {
        static void Main(string[] args)
        {
            //本实例用于命令行参数的展示
            Console.WriteLine("{0} command line arguments were specified:", args.Length);
            foreach (string arg in args)
                Console.WriteLine(arg);
            Console.ReadKey();
        }
    }
}
