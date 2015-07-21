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

            //添加一个客户端测试
            Deck deck1 = new Deck();
            Deck deck2 = (Deck)deck1.Clone();
            Console.WriteLine("The first card in the original deck is : {0}", deck1.GetCard(0));
            Console.WriteLine("The first card in the cloned deck is : {0}", deck2.GetCard(0));
            deck1.Shuffle();
            Console.WriteLine("Original deck shuffled.");
            Console.WriteLine("The first card in the original deck is : {0}", deck1.GetCard(0));
            Console.WriteLine("The first card in the cloned deck is : {0}", deck2.GetCard(0));
            Console.ReadKey();
        }
    }
}
