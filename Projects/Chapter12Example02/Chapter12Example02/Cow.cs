﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter12Example02
{
    public class Cow : Animal
    {
        public void Milk()
        {
            Console.WriteLine("{0} has been milked.", name);
        }

        public Cow(String NewName) : base(NewName)
        { 
            //do something...
        }
    }
}
