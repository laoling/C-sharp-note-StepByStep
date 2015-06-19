using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter04Example01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入一个整数：");
            int myInt = Convert.ToInt32(Console.ReadLine());
            bool isLessThan10 = myInt < 10;
            bool isBetween0And5 = (0 <= myInt) && (myInt <=5);

            Console.WriteLine("1.这个整数比10小？ {0}",isLessThan10);
            Console.WriteLine("2.这个整数在0到5之间？ {0}",isBetween0And5);
            Console.WriteLine("3.上面两条只有一个真正成立？ {0}",isLessThan10 ^ isBetween0And5);
            Console.ReadKey();
        }
    }
}
