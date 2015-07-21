using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11CardLib
{
    /// <summary>
    /// Deck类上实现ICloneable接口
    /// bug：Deck类无法修改它包含的扑克牌，所以无法洗牌
    /// fix：为Deck类定义一个新的私有构造函数，在实例化Deck对象时，可以传递指定的Cards集合
    /// </summary>

    public class Deck : ICloneable
    {
        public object Clone()
        {
            Deck newDeck = new Deck(cards.Clone() as Cards);
            return newDeck;
        }

        private Deck(Cards newCards)
        {
            cards = newCards;
        }

        private Cards cards = new Cards();

        public Deck()
        {
            
            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int rankVal = 1; rankVal < 14; rankVal++)
                {
                    cards.Add(new Card((Suit)suitVal, (Rank)rankVal));
                }
            }
        }

            public Card GetCard(int CardNum)
            {
                if (CardNum >= 0 && CardNum <= 51)
                    return cards[CardNum];
                else 
                    throw (new System.ArgumentOutOfRangeException("cardNum", CardNum, "Value must be between 0 and 51."));
            }

            public void Shuffle()
            {
                Cards newDeck = new Cards();
                bool[] assigned = new bool[52];
                Random sourceGen = new Random();
                for (int i = 0; i < 52; i++)
                {
                    int sourceCard = 0;
                    bool foundCard = false;
                    while (foundCard == false)
                    {
                        sourceCard = sourceGen.Next(52);
                        if (assigned[sourceCard] == false)
                            foundCard = true;
                    }
                    assigned[sourceCard] = true;
                    newDeck.Add(cards[sourceCard]);
                }
                newDeck.CopyTo(cards);
            }


            //public object Clone()
            //{
            //     throw new NotImplementedException();
            //}
    }
}
