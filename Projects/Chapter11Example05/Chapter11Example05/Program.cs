using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//本实例演示使用默认和非默认的比较方式给列表排序
namespace Chapter11Example05
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList List = new ArrayList();
            List.Add(new Person("Jim", 30));
            List.Add(new Person("Bob", 25));
            List.Add(new Person("Bert", 27));
            List.Add(new Person("Ernie", 22));

            Console.WriteLine("Unsorted persons:");
            for (int i = 0; i < List.Count; i++)
            {
                Console.WriteLine("{0} <{1}>", (List[i] as Person).Name, (List[i] as Person).Age);
            }
            Console.WriteLine();

            Console.WriteLine("People sorted with default comparer (by age):");
            List.Sort();
            for (int i = 0; i < List.Count; i++)
            {
                Console.WriteLine("{0} <{1}>", (List[i] as Person).Name, (List[i] as Person).Age);
            }
            Console.WriteLine();

            Console.WriteLine("people sorted with nondefault comparer (by name):");
            List.Sort(PersonComparerName.Default);
            for (int i = 0; i < List.Count; i++)
            {
                Console.WriteLine("{0} <{1}>", (List[i] as Person).Name, (List[i] as Person).Age);
            }
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
