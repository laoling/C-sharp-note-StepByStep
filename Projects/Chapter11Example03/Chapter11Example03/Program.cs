using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11Example03
{
    class Program
    {
        static void Main(string[] args)
        {
            //本实例用于演示一个获取素数的迭代器
            Primes PrimesFrom2To1000 = new Primes(2, 1000);

            foreach (long i in PrimesFrom2To1000)
                Console.Write("{0} ", i);
            Console.ReadKey();
        }
    }
}
