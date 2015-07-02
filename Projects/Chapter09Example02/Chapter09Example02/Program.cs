using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chapter09ClassLib;

namespace Chapter09Example02
{
    class Program
    {
        static void Main(string[] args)
        {
            //使用类库
            MyExternalClass myObj = new MyExternalClass();
            Console.WriteLine(myObj.ToString());
            Console.ReadKey();
        }
    }
}
