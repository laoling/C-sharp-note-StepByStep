using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter03Example01
{
    class Program
    {
        static void Main(string[] args)
        {
            //这里我们主要实验下各种数据类型的变量
            //声明两个变量
            int myInteger;
            string myString;

            //给变量赋值
            myInteger = 20;
            myString = "myInteger is";

            //输出
            Console.WriteLine("{0} {1}.", myString, myInteger);
            Console.ReadKey();
        }
    }
}
