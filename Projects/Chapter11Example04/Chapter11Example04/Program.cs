using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11Example04
{
    class Checker
    {
        public void Check(object param1)
        {
            if (param1 is classA)
                Console.WriteLine("变量能够转换成classA.");
            else
                Console.WriteLine("变量不能够转换成classA.");

            if (param1 is IMyInterface)
                Console.WriteLine("变量能够转换成IMyInterface.");
            else
                Console.WriteLine("变量不能够转换成IMyInterface.");

            if (param1 is MyStruct)
                Console.WriteLine("变量能够转换成MyStruct.");
            else
                Console.WriteLine("变量不能够转换成MyStruct.");

        }
    }

    interface IMyInterface
    { 
    }

    class classA : IMyInterface
    { 
    }

    class classB : IMyInterface
    { 
    }

    class classC
    { 
    }

    class classD : classA
    { 
    }

    struct MyStruct : IMyInterface
    { 
    }

    class Program
    {
        static void Main(string[] args)
        {
            //本实例用于使用is运算符
            Checker check = new Checker();
            classA try1 = new classA();
            classB try2 = new classB();
            classC try3 = new classC();
            classD try4 = new classD();
            MyStruct try5 = new MyStruct();
            object try6 = try5;

            Console.WriteLine("分析结果ClassA的变量类型：");
            check.Check(try1);
            Console.WriteLine("分析结果ClassB的变量类型：");
            check.Check(try2);
            Console.WriteLine("分析结果ClassC的变量类型：");
            check.Check(try3);
            Console.WriteLine("分析结果ClassD的变量类型：");
            check.Check(try4);
            Console.WriteLine("分析结果MyStruct的变量类型：");
            check.Check(try5);
            Console.WriteLine("分析结果MyStruct封箱的变量类型：");
            check.Check(try6);

            Console.ReadKey();

        }
    }
}
