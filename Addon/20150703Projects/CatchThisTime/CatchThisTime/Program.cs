using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatchThisTime
{
    class Program
    {
        static void Main(string[] args)
        {
            //获取当前日期加时间
            Console.WriteLine(DateTime.Now.ToString());
            Console.WriteLine(DateTime.Now.ToLocalTime().ToString());

            //获取日期
            Console.WriteLine(DateTime.Now.ToLongDateString().ToString());  //显示年月日汉字
            Console.WriteLine(DateTime.Now.ToShortDateString().ToString());
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));   //按照括号内格式格式化日期
            Console.WriteLine(DateTime.Now.Date.ToString());   //只调用了日期数据，所以后面跟着的时间格式化为0:00:00

            //获取时间
            Console.WriteLine(DateTime.Now.ToLongTimeString().ToString()); //显示时分秒
            Console.WriteLine(DateTime.Now.ToShortTimeString().ToString());  //显示时分
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss"));  //按照括号内格式格式化时间
            Console.WriteLine(DateTime.Now.TimeOfDay.ToString());  //精确显示计算时间 

            //其他
            Console.WriteLine(DateTime.Now.ToFileTime().ToString()); //返回时间戳
            Console.WriteLine(DateTime.Now.ToFileTimeUtc().ToString());  //返回UTC时间戳
            Console.WriteLine(DateTime.Now.ToOADate().ToString());  
            Console.WriteLine(DateTime.Now.ToUniversalTime().ToString());
            Console.WriteLine(DateTime.Now.Year.ToString());  //获取年份 
            Console.WriteLine(DateTime.Now.Month.ToString());  //获取月份 
            Console.WriteLine(DateTime.Now.DayOfWeek.ToString());  //获取星期
            Console.WriteLine(DateTime.Now.DayOfYear.ToString());  //获取一年中第几天
            Console.WriteLine(DateTime.Now.Hour.ToString());  //获取小时
            Console.WriteLine(DateTime.Now.Minute.ToString());  //获取分钟
            Console.WriteLine(DateTime.Now.Second.ToString());  //获取秒数

            Console.ReadKey();
        }
    }
}
