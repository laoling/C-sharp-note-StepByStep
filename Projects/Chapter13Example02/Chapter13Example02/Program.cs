using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter13Example02
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection myConnection = new Connection();
            Display myDisplay = new Display();
            myConnection.MessageArrived += new MessageHaddler(myDisplay.DisplayMessage);
            myConnection.Connect();
            Console.ReadKey();
        }
    }
}
