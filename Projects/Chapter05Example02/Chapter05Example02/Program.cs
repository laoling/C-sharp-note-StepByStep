using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter05Example02
{
    //这个是枚举的实例
    enum orientation : byte
    {
        //定义变量并赋值
        north = 1,
        south = 2,
        east  = 3,
        west  = 4
    }

    class Program
    {
        static void Main(string[] args)
        {
         /* orientation myDirection = orientation.north;
            Console.WriteLine("My Direction is {0}.", myDirection);
            Console.ReadKey(); 
          */
            //输出为“My Direction is north.”
            //下面我们转换为其他类型看看输出结果如何：
            byte directionByte;
            string directionString;

            orientation myDirection = orientation.north;
            Console.WriteLine("My Direction is {0}.", myDirection);

            directionByte = (byte)myDirection;
            directionString = Convert.ToString(myDirection);
            /* 与上面转换结果相同的一种写法：
            directionString = myDirection.ToString();
             */

            Console.WriteLine("Byte 输出为： {0}", directionByte);
            Console.WriteLine("String 输出为： {0}", directionString);
            Console.ReadKey();
        }
    }
}
