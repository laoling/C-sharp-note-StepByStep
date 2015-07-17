using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chapter11CardLib;

namespace Chapter11CardClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck myDeck = new Deck();
            myDeck.Shuffle();
            for (int i = 0; i < 52; i++)
            {
                Card tempCard = myDeck.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != 51)
                    Console.Write(", ");
                else
                    Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
