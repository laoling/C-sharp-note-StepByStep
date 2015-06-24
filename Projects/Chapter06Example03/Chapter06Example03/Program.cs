using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter06Example03
{
    class Program
    {
        //本实例用于展示用函数展示数据 这里使用params参数
        static int SumVals(params int[] vals)
        {
            int sum = 0;
            foreach (int val in vals)
            {
                sum += val;
            }
            return sum;
        }

        static void Main(string[] args)
        {
            int sum = SumVals(1, 5, 2, 9, 8);
            Console.WriteLine("和是{0}", sum);
            Console.ReadKey();
        }
    }
}
