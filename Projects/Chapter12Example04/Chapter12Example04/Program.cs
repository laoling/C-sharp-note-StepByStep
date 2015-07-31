using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter12Example04
{
    class Program
    {
        static void Main(string[] args)
        {
            Farm<Animal> farm = new Farm<Animal>();
            farm.Animals.Add(new Cow("Jack"));
            farm.Animals.Add(new Chicken("Vera"));
            farm.Animals.Add(new Chicken("Sally"));
            farm.Animals.Add(new SuperCow("Kevin"));
            farm.MakeNoises();

            Farm<Cow> dairyFarm = farm.GetCows();
            dairyFarm.FeedTheAnimals();

            foreach (Cow cow in dairyFarm)
            {
                if (cow is SuperCow)
                {
                    (cow as SuperCow).Fly();
                }
            }
            Console.ReadKey();
        }
    }
}
