using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter05Example01
{
    class Program
    {
        static void Main(string[] args)
        {
            //类型转换的实例
            short shortResult, shortVal = 4;
            int integerVal = 67;
            long longResult;
            float floatVal = 10.5F;
            double doubleResult, doubleVal = 99.999;
            string stringResult, stringVal = "17";
            bool boolVal = true;

            Console.WriteLine("变量转换实例如下：");
            doubleResult = floatVal * shortVal;
            Console.WriteLine("隐式转换-> double: {0} * {1} -> {2}", floatVal, shortVal, doubleResult);
            shortResult = (short)floatVal;
            Console.WriteLine("显式转换-> short: {0} -> {1}", floatVal, shortResult);
            stringResult = Convert.ToSingle(boolVal) + Convert.ToString(doubleVal);
            Console.WriteLine("显式转换-> string: \"{0}\" + \"{1}\" -> {2}", boolVal, doubleVal, stringResult);
            longResult = integerVal + Convert.ToInt64(stringVal);
            Console.WriteLine("混合转换-> long: {0} + {1} -> {2}", integerVal, stringVal, longResult);

            Console.ReadKey();

        }
    }
}
