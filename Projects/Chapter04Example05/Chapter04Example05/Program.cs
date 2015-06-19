using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter04Example05
{
    class Program
    {
        static void Main(string[] args)
        {
            //本实例用于演示while循环
            //通过年利率我们来计算一个账户多少年后的余额达到预期额度 和本章例四相同

            double balance, interestRate, targetBalance;

            Console.WriteLine("您现在账户的余额是多少？");
            balance = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("你存款时的年利率是百分之多少？");
            interestRate = 1 + Convert.ToDouble(Console.ReadLine()) / 100.0;
            Console.WriteLine("您的预期账户额度是多少钱？");
            targetBalance = Convert.ToDouble(Console.ReadLine());

            int totalYears = 0;
            while (balance <= targetBalance)
            {
                balance *= interestRate;
                ++totalYears;
            } 

            Console.WriteLine("在{0}年之后，您的账户会超过预期，余额是{1}元！", totalYears, balance);

            if (totalYears == 0)
                Console.WriteLine("你的预期额度比存款的余额还少，不用存了，拿出来花吧！");

            Console.ReadKey();
        }
    }
}
