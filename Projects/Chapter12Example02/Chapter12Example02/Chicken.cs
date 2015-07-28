using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter12Example02
{
    class Chicken : Animal 
    {
        public void LayEgg()
        {
            Console.WriteLine("{0} has layed an egg.", name);
        }

        public Chicken(String NewName) : base(NewName)
        { 
            //do something...
        }
    }
}
