using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11Example05
{
    class Person : IComparable
    {
        public string Name;
        public int Age;

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public int CompareTo(object obj)
        {
            if (obj is Person)
            {
                Person otherPerson = obj as Person;
                return this.Age - otherPerson.Age;
            }
            else
            {
                throw new ArgumentException("用于比较的对象不是一个Person object.");
            }
        }
    }
}
