using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter06Example02
{
    class Program
    {
        //本实例用于展示函数交换数据

        static int MaxValue(int[] intArray)
        {
            int maxVal = intArray[0];
            for (int i = 1; i < intArray.Length; i++)
            {
                if (maxVal < intArray[i])
                    maxVal = intArray[i];
            }
            return maxVal;
        }

        static void Main(string[] args)
        {
            int[] myArray = {1, 8, 3, 6, 2, 5, 9, 3, 0, 2};
            int maxVal = MaxValue(myArray);

            Console.WriteLine("数组中的最大值是{0}",maxVal);
            Console.ReadKey();
        }
    }
}
