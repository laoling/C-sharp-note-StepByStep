using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter05Example03
{
    //本实例用于演示结构变量类型
    //这里的代码由例二扩展所得

    enum orientation : byte
    { 
        north = 1,
        south = 2,
        east  = 3,
        west  = 4
    }

    struct route
    {
        public orientation direction;
        public double      distance;
    }

    class Program
    {
        static void Main(string[] args)
        {
            route myRoute;
            int myDirection = -1;
            double myDistance;
            Console.WriteLine("1) 北\n2) 南\n3) 东\n4) 西");

            do
            {
                Console.WriteLine("从上面选项中选择一个方向：");
                myDirection = Convert.ToInt32(Console.ReadLine());           
            } while ((myDirection < 1) || (myDirection > 4));
            Console.WriteLine("请输入一个距离：（公里数）");
            myDistance = Convert.ToDouble(Console.ReadLine());

            myRoute.direction = (orientation)myDirection;
            myRoute.distance = myDistance;
            Console.WriteLine("我的路线指定的方向是 {0}，指定的距离是 {1}!", myRoute.direction, myRoute.distance);

            Console.ReadKey();

        }
    }
}
