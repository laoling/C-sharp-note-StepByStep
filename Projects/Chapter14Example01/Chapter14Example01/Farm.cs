﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter14Example01
{
    public class Farm<T> : IEnumerable<T> where T : Animal 
    {
        //public void Add(T animal)
        //{
        //    animals.Add(animal);
        //}

        private List<T> animals = new List<T>();

        public List<T> Animals
        {
            get
            {
                return animals;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return animals.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return animals.GetEnumerator();
        }

        public void MakeNoises()
        {
            foreach (T animal in animals)
            {
                animal.MakeANoise();
            }
        }

        public void FeedTheAnimals()
        {
            foreach (T animal in animals)
            {
                animal.Feed();
            }
        }

        public Farm<Cow> GetCows()
        {
            Farm<Cow> cowFarm = new Farm<Cow>(); 
            foreach (T animal in animals)
            {
                if (animal is Cow)
                {
                    cowFarm.Animals.Add(animal as Cow);
                }
            }
            return cowFarm;
        }
    }
}
