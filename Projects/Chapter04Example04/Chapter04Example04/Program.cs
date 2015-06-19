using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter04Example04
{
    class Program
    {
        static void Main(string[] args)
        {
            //这里我们举例说明do循环
            //通过年利率我们来计算一个账户多少年后的余额达到预期额度

            double balance, interestRate, targetBalance;

            Console.WriteLine("您现在账户的余额是多少？");
            balance = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("你存款时的年利率是百分之多少？");
            interestRate = 1 + Convert.ToDouble(Console.ReadLine()) / 100.0;
            Console.WriteLine("您的预期账户额度是多少钱？");
            targetBalance = Convert.ToDouble(Console.ReadLine());

            int totalYears = 0;
            do
            {
                balance *= interestRate;
                ++totalYears;
            } while (balance <= targetBalance);

            Console.WriteLine("在{0}年之后，您的账户会超过预期，余额是{1}元！", totalYears, balance);
            Console.ReadKey();

            //这段代码用来演示do循环，但作为问题本身的处理方案并不完美
            //如果预期值比本来的余额少，本来按逻辑应返回0，但这里do循环必须执行一次，返回年数是1，这是不合理的
        }
    }
}
