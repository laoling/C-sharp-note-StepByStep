using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter12VarianceDemo
{
   public class AnimalNameLengthComparer : IComparer<Animal>
   {
      public int Compare(Animal x, Animal y)
      {
         return x.Name.Length.CompareTo(y.Name.Length);
      }
   }
}
