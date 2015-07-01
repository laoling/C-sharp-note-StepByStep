using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter09Example01
{
    public abstract class MyBase
    {
    }

    internal class MyClass : MyBase
    {
    }

    public interface IMyBaseInterface
    {
    }

    internal interface IMyBaseInterface2
    {
    }

    internal interface IMyInterface : IMyBaseInterface, IMyBaseInterface2
    {
    }

    internal sealed class MyComplexClass : MyClass, IMyInterface
    { 
    }

    class Program
    {
        static void Main(string[] args)
        {
            //本实例用于定义类
            MyComplexClass myObj = new MyComplexClass();
            Console.WriteLine(myObj.ToString());
            Console.ReadKey();
        }
    }
}
