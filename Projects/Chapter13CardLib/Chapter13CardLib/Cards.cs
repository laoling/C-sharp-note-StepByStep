﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter13CardLib
{
    //public class Cards : CollectionBase
    //{
    //    public void Add(Card newCard)
    //    {
    //        List.Add(newCard);
    //    }

    //    public void Remove(Card oldCard)
    //    {
    //        List.Remove(oldCard);
    //    }

    //    public Cards()
    //    { 
    //    }

    //    public Card this[int cardIndex]
    //    {
    //        get
    //        {
    //            return (Card)List[cardIndex];
    //        }
    //        set
    //        {
    //            List[cardIndex] = value;
    //        }
    //    }

    //    ///<summary>
    //    ///Utility method for copying card instance into another Cards
    //    ///instance -- used in Deck.shuffle(). This implementation assume that
    //    ///source and target collections are the same size.
    //    ///</summary>

    //    public void CopyTo(Cards targetCards)
    //    {
    //        for (int index = 0; index < this.Count; index++)
    //        {
    //            targetCards[index] = this[index];
    //        }
    //    }

    //    ///<summary>
    //    ///Check to see if the Cards collection contains a particular card.
    //    ///This calls the Contains method of the ArrayList for the collection,
    //    ///which we access through the InnerList porperty.
    //    ///</summary>

    //    public bool Contains(Card card)
    //    {
    //        return InnerList.Contains(card);
    //    }
    //}

    public class Cards : List<Card>, ICloneable
    {
        /// <summary>
        /// Utility method for copying card instances into another Cards
        /// instance—used in Deck.Shuffle(). This implementation assumes that
        /// source and target collections are the same size.
        /// </summary>
        public void CopyTo(Cards targetCards)
        {
            for (int index = 0; index < this.Count; index++)
            {
                targetCards[index] = this[index];
            }
        }

        public object Clone()
        {
            Cards newCards = new Cards();
            foreach (Card sourceCard in this)
            {
                newCards.Add((Card)sourceCard.Clone());
            }
            return newCards;
        }
    }
}
