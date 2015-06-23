using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter05Example04
{
    class Program
    {
        static void Main(string[] args)
        {
            //本实例用于数组的使用
            string[] friendNames = {"Robert Barwell", "Mike Parry", "Jeremy Beacock"};
            Console.WriteLine("这里有我的{0}位好朋友！", friendNames.Length);

            int i;
            for (i = 0; i < friendNames.Length; i++)
            {
                Console.WriteLine(friendNames[i]);
            }

            Console.ReadKey();
        }
    }
}
