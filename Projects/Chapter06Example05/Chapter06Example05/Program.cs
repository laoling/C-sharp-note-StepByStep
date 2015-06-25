using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter06Example05
{
    class Program
    {
        //本实例用于演示使用委托调用函数
        delegate double ProcessDelegate(double param1, double param2);

        static double Multiply(double param1, double param2)
        { 
            return  param1 * param2;
        }
        static double Divide(double param1, double param2)
        {
            return param1 / param2;
        }
        static void Main(string[] args)
        {
            ProcessDelegate process;

            Console.WriteLine("请输入两个数字，中间用英文逗号隔开：");
            string input = Console.ReadLine();
            int commaPos = input.IndexOf(',');
            double param1 = Convert.ToDouble(input.Substring(0, commaPos));
            double param2 = Convert.ToDouble(input.Substring(commaPos + 1, input.Length - commaPos -1));

            Console.WriteLine("输入M选择乘法，输入D选择除法：");
            input = Console.ReadLine();
            if (input == "M")
                //把一个函数引用赋给委托变量，这里采用的是简单缩略写法，下面再写一种完整写法
                //process = Multiply;
                process = new ProcessDelegate(Multiply);
            else
                //process = Divide;
                //同样的
                process = new ProcessDelegate(Divide);
            Console.WriteLine("计算结果是：{0}", process(param1, param2));
            Console.ReadKey();
        }
    }
}
