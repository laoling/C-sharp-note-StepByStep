using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter14Example01
{
    public class SuperCow : Cow
    {
        public void Fly()
        {
            Console.WriteLine("{0} is Flying!", name);
        }

        //public SuperCow(string newName) : base(newName)
        //{
        //}
        
        public override void MakeANoise()
        {
            Console.WriteLine("{0} says Here I come to save the day!", name);
        }
    }
}
