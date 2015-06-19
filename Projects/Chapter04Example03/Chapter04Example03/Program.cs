using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter04Example03
{
    class Program
    {
        static void Main(string[] args)
        {
            //使用switch语句
            const string myName = "karli";
            const string sexyName = "angelina";
            const string sillyName = "ploppy";
            string name;

            Console.WriteLine("你叫什么名字？");
            name = Console.ReadLine();

            switch (name.ToLower())
            {
                case myName:
                    Console.WriteLine("你和我的名字一模一样！");
                    break;

                case sexyName:
                    Console.WriteLine("噢，你的名字真是太好听了！");
                    break;

                case sillyName:
                    Console.WriteLine("这个名字听起来太傻了！");
                    break;

                default:
                    Console.WriteLine("抱歉，这个名字还是第一次听说。");
                    break;
            }
            Console.WriteLine("你好，很高兴认识你，{0}！",name);
            Console.ReadKey();

        }
    }
}
