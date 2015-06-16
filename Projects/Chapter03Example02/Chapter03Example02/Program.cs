using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter03Example02
{
    class Program
    {
        static void Main(string[] args)
        {
            //本实例使用数学运算符处理变量
            double firstNumber, secondNumber;
            string userName;

            Console.WriteLine("输入你的姓名：");
            userName = Console.ReadLine();
            Console.WriteLine("欢迎你 {0}!", userName);
            Console.WriteLine("现在请输入一个数字：");
            firstNumber = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("现在再输入一个数字.");
            secondNumber = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("第一个数字{0}和第二个数字{1}的和是{2}.", firstNumber, secondNumber, firstNumber + secondNumber);
            Console.WriteLine("第一个数字{0}减去第二个数字{1}的差是{2}.", firstNumber, secondNumber, firstNumber - secondNumber);
            Console.WriteLine("第一个数字{0}和第二个数字{1}相乘积是{2}.", firstNumber, secondNumber, firstNumber * secondNumber);
            Console.WriteLine("第一个数字{0}除以第二个数字{1}的商是{2}.", firstNumber, secondNumber, firstNumber / secondNumber);
            Console.WriteLine("第一个数字{0}和第二个数字{1}求同余值是{2}.", firstNumber, secondNumber, firstNumber % secondNumber);
            Console.ReadKey();
        }
    }
}
