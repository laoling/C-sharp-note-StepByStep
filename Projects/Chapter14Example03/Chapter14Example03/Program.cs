using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;

namespace Chapter14Example03
{
    //本实例用于演示使用动态类型
    //本例子仅用来演示！
    //一般情况下，应仅在动态类型是唯一的选项时使用它们，例如处理非.NET对象

    class MyClass1
    {
        public int Add(int var1, int var2)
        {
            return var1 + var2;
        }
    }

    class MyClass2
    {
    }

    class Program
    {
        static int callCount = 0;

        static dynamic GetValue()
        {
            if (callCount++ == 0)
            {
                return new MyClass1();
            }
            return new MyClass2();
        }

        static void Main(string[] args)
        {
            try
            {
                dynamic firstResult = GetValue();
                dynamic secondResult = GetValue();
                Console.WriteLine("firstResult is: {0}", firstResult);
                Console.WriteLine("secondResult is: {0}", secondResult);
                Console.WriteLine("firstResult call: {0}", firstResult.Add(2, 3));
                Console.WriteLine("secondResult call: {0}", secondResult.Add(2, 3));
            }
            catch (RuntimeBinderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
