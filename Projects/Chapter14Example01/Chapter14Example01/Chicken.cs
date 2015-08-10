using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter14Example01
{
    class Chicken : Animal 
    {
        public void LayEgg()
        {
            Console.WriteLine("{0} has layed an egg.", name);
        }

        //public Chicken(String NewName) : base(NewName)
        //{ 
        //    //do something...
        //}

        public override void MakeANoise()
        {
            Console.WriteLine("{0} says Cluck!", name);
        }
        
    }
}
