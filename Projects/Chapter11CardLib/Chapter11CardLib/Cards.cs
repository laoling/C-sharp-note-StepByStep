using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11CardLib
{
    /// <summary>
    /// 对Cards集合类实现ICloneable接口
    /// 由于涉及到复制源集合中的每个Card对象，需要进行深复制
    /// </summary>
    
    public class Cards : CollectionBase, ICloneable
    {
        public object Clone()
        {
            Cards newCards = new Cards();
            foreach (Card sourceCard in List)
            {
                newCards.Add(sourceCard.Clone() as Card);
            }
            return newCards;
        }

        public void Add(Card newCard)
        {
            List.Add(newCard);
        }

        public void Remove(Card oldCard)
        {
            List.Remove(oldCard);
        }

        public Cards()
        { 
        }

        public Card this[int cardIndex]
        {
            get
            {
                return (Card)List[cardIndex];
            }
            set
            {
                List[cardIndex] = value;
            }
        }

        ///<summary>
        ///Utility method for copying card instance into another Cards
        ///instance -- used in Deck.shuffle(). This implementation assume that
        ///source and target collections are the same size.
        ///</summary>

        public void CopyTo(Cards targetCards)
        {
            for (int index = 0; index < this.Count; index++)
            {
                targetCards[index] = this[index];
            }
        }

        ///<summary>
        ///Check to see if the Cards collection contains a particular card.
        ///This calls the Contains method of the ArrayList for the collection,
        ///which we access through the InnerList porperty.
        ///</summary>

        public bool Contains(Card card)
        {
            return InnerList.Contains(card);
        }
    }
}
