using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter14Example01
{
    public class Cow : Animal
    {
        public void Milk()
        {
            Console.WriteLine("{0} has been milked.", name);
        }

        //public Cow(String NewName) : base(NewName)
        //{ 
        //    //do something...
        //}

        public override void MakeANoise()
        {
            Console.WriteLine("{0} says Moo!", name);
        }
    }
}
