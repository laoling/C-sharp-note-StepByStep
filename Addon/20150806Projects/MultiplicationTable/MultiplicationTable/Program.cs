using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiplicationTable
{
    class Program
    {
        static void Main(string[] args)
        {
            //本实例使用for循环输出九九乘法表
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write(j + "*" + i + "=" + j * i + "\t");
                }
                Console.Write("\n");
            }
            Console.ReadKey();
        }
    }
}
