using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter04Example02
{
    class Program
    {
        static void Main(string[] args)
        {
            //本节实例用于if语句的例子
            string comparison;

            Console.WriteLine("请输入第一个数字：");
            double var1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("请输入第二个数字：");
            double var2 = Convert.ToDouble(Console.ReadLine());

            if (var1 < var2)
                comparison = "小于";
            else
            {
                if (var1 == var2)
                    comparison = "等于";
                else
                    comparison = "大于";
            }

            Console.WriteLine("两个数的关系是：第一个数字{0}第二个数字",comparison);
            Console.ReadKey();

        }
    }
}
