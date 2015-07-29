using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//本实例用于演示List<T>的搜索和排序
namespace Chapter12Example03
{
    class Program
    {
        static void Main(string[] args)
        {
            Vectors route = new Vectors();
            route.Add(new Vector(2.0, 90.0));
            route.Add(new Vector(1.0, 180.0));
            route.Add(new Vector(0.5, 45.0));
            route.Add(new Vector(2.5, 315.0));

            Console.WriteLine(route.Sum());

            Comparison<Vector> sorter = new Comparison<Vector>(VectorDelegates.Compare);
            route.Sort(sorter);
            Console.WriteLine(route.Sum());

            Predicate<Vector> searcher = new Predicate<Vector>(VectorDelegates.TopRightquadrant);
            Vectors topRightQuadrantRoute = new Vectors(route.FindAll(searcher));
            Console.WriteLine(topRightQuadrantRoute.Sum());

            Console.ReadKey();
        }
    }
}
