using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter14Example01
{
    //本实例用于使用初始化器
    class Program
    {
        static void Main(string[] args)
        {
            Farm<Animal> farm = new Farm<Animal> 
            { 
                //new Cow { Name = "Norris"},
                //new Chicken { Name = "Rita"},
                //new Chicken(),
                //new SuperCow { Name = "Chesney"}

                Animals =
                {
                    new Cow { Name = "Norris"},
                    new Chicken { Name = "Rita"},
                    new Chicken(),
                    new SuperCow { Name = "Chesney"}
                }
            };

            farm.MakeNoises();
            Console.ReadKey();
        }
    }
}
