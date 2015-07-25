using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11Example05
{
    public class PersonComparerName : IComparer
    {
        public static IComparer Default = new PersonComparerName();

        public int Compare(object x, object y)
        { 
            if (x is Person && y is Person)
            {
                return Comparer.Default.Compare(((Person)x).Name, ((Person)y).Name);
            }
            else
            {
                throw new ArgumentException("一个或者两个比较的对象不是Person object.");
            }
        }

    }
}
