using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter14Example01
{
    public abstract class Animal
    {
        protected string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public Animal()
        {
            name = "This animal with no name";
        }

        public Animal(string NewName)
        {
            name = NewName;
        }

        public void Feed()
        {
            Console.WriteLine("{0} has been fed.", name);
        }

        public abstract void MakeANoise();
    }
}
