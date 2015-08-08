using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chapter13CardLib;

namespace Chapter13CardClient
{
   public class Player
   {
      public string Name { get; private set; }

      public Cards PlayHand { get; private set; }

      private Player()
      {
      }

      public Player(string name)
      {
         Name = name;
         PlayHand = new Cards();
      }

      public bool HasWon()
      {
         bool won = true;
         Suit match = PlayHand[0].suit;
         for (int i = 1; i < PlayHand.Count; i++)
         {
            won &= PlayHand[i].suit == match;
         }
         return won;
      }

      // Exercise answer.
      //public bool HasWon()
      //{
      //   // get temporary copy of hand, which may get modified.
      //   Cards tempHand = (Cards)PlayHand.Clone();

      //   // find three and four of a kind sets
      //   bool fourOfAKind = false;
      //   bool threeOfAKind = false;
      //   int fourRank = -1;
      //   int threeRank = -1;

      //   int cardsOfRank;
      //   for (int matchRank = 0; matchRank < 13; matchRank++)
      //   {
      //      cardsOfRank = 0;
      //      foreach (Card c in tempHand)
      //      {
      //         if (c.rank == (Rank)matchRank)
      //         {
      //            cardsOfRank++;
      //         }
      //      }
      //      if (cardsOfRank == 4)
      //      {
      //         // mark set of four
      //         fourRank = matchRank;
      //         fourOfAKind = true;
      //      }
      //      if (cardsOfRank == 3)
      //      {
      //         // two threes means no win possible
      //         // (threeOfAKind will only be true if this code
      //         // has already executed)
      //         if (threeOfAKind == true)
      //         {
      //            return false;
      //         }
      //         // mark set of three
      //         threeRank = matchRank;
      //         threeOfAKind = true;
      //      }
      //   }

      //   // check simple win condition
      //   if (threeOfAKind && fourOfAKind)
      //   {
      //      return true;
      //   }

      //   // simplify hand if three or four of a kind is found, by removing used cards
      //   if (fourOfAKind || threeOfAKind)
      //   {
      //      for (int cardIndex = tempHand.Count - 1; cardIndex >= 0; cardIndex--)
      //      {
      //         if ((tempHand[cardIndex].rank == (Rank)fourRank)
      //             || (tempHand[cardIndex].rank == (Rank)threeRank))
      //         {
      //            tempHand.RemoveAt(cardIndex);
      //         }
      //      }
      //   }

      //   // at this point the method may have returned, because:
      //   // - a set of four and a set of three has been found, winning.
      //   // - two sets of three have been found, losing.
      //   // if the method hasn’t returned then either:
      //   // - no sets have been found, and tempHand contains 7 cards.
      //   // - a set of three has been found, and tempHand contains 4 cards.
      //   // - a set of four has been found, and tempHand contains 3 cards.

      //   // find run of four sets, start by looking for cards of same suit in the same
      //   // way as before
      //   bool fourOfASuit = false;
      //   bool threeOfASuit = false;
      //   int fourSuit = -1;
      //   int threeSuit = -1;

      //   int cardsOfSuit;
      //   for (int matchSuit = 0; matchSuit < 4; matchSuit++)
      //   {
      //      cardsOfSuit = 0;
      //      foreach (Card c in tempHand)
      //      {
      //         if (c.suit == (Suit)matchSuit)
      //         {
      //            cardsOfSuit++;
      //         }
      //      }
      //      if (cardsOfSuit == 7)
      //      {
      //         // if all cards are the same suit then two runs
      //         // are possible, but not definite.
      //         threeOfASuit = true;
      //         threeSuit = matchSuit;
      //         fourOfASuit = true;
      //         fourSuit = matchSuit;
      //      }
      //      if (cardsOfSuit == 4)
      //      {
      //         // mark four card suit.
      //         fourOfASuit = true;
      //         fourSuit = matchSuit;
      //      }
      //      if (cardsOfSuit == 3)
      //      {
      //         // mark three card suit.
      //         threeOfASuit = true;
      //         threeSuit = matchSuit;
      //      }
      //   }

      //   if (!(threeOfASuit || fourOfASuit))
      //   {
      //      // need at least one run possibility to continue.
      //      return false;
      //   }

      //   if (tempHand.Count == 7)
      //   {
      //      if (!(threeOfASuit && fourOfASuit))
      //      {
      //         // need a three and a four card suit.
      //         return false;
      //      }

      //      // create two temporary sets for checking.
      //      Cards set1 = new Cards();
      //      Cards set2 = new Cards();

      //      // if all 7 cards are the same suit...
      //      if (threeSuit == fourSuit)
      //      {
      //         // get min and max cards
      //         int maxVal, minVal;
      //         GetLimits(tempHand, out maxVal, out minVal);
      //         for (int cardIndex = tempHand.Count - 1; cardIndex >= 0; cardIndex--)
      //         {
      //            if (((int)tempHand[cardIndex].rank < (minVal + 3))
      //                || ((int)tempHand[cardIndex].rank > (maxVal - 3)))
      //            {
      //               // remove all cards in a three card set that
      //               // starts at minVal or ends at maxVal.
      //               tempHand.RemoveAt(cardIndex);
      //            }
      //         }
      //         if (tempHand.Count != 1)
      //         {
      //            // if more then one card is left then there aren’t two runs.
      //            return false;
      //         }
      //         if ((tempHand[0].rank == (Rank)(minVal + 3))
      //             || (tempHand[0].rank == (Rank)(maxVal - 3)))
      //         {
      //            // if spare card can make one of the three card sets into a
      //            // four card set then there are two sets.
      //            return true;
      //         }
      //         else
      //         {
      //            // if spare card doesn’t fit then there are two sets of three
      //            // cards but no set of four cards.
      //            return false;
      //         }
      //      }

      //      // if three card and four card suits are different...
      //      foreach (Card card in tempHand)
      //      {
      //         // split cards into sets.
      //         if (card.suit == (Suit)threeSuit)
      //         {
      //            set1.Add(card);
      //         }
      //         else
      //         {
      //            set2.Add(card);
      //         }
      //      }

      //      // check if sets are sequential.
      //      if (isSequential(set1) && isSequential(set2))
      //      {
      //         return true;
      //      }
      //      else
      //      {
      //         return false;
      //      }
      //   }

      //   // if four cards remain (three of a kind found)
      //   if (tempHand.Count == 4)
      //   {
      //      // if four cards remain then they must be the same suit.
      //      if (!fourOfASuit)
      //      {
      //         return false;
      //      }
      //      // won if cards are sequential.
      //      if (isSequential(tempHand))
      //      {
      //         return true;
      //      }
      //   }

      //   // if three cards remain (four of a kind found)
      //   if (tempHand.Count == 3)
      //   {
      //      // if three cards remain then they must be the same suit.
      //      if (!threeOfASuit)
      //      {
      //         return false;
      //      }
      //      // won if cards are sequential.
      //      if (isSequential(tempHand))
      //      {
      //         return true;
      //      }
      //   }

      //   // return false if two valid sets don’t exist.
      //   return false;
      //}

      //// utility method to get max and min ranks of cards
      //// (same suit assumed)
      //private void GetLimits(Cards cards, out int maxVal, out int minVal)
      //{
      //   maxVal = 0;
      //   minVal = 14;
      //   foreach (Card card in cards)
      //   {
      //      if ((int)card.rank > maxVal)
      //      {
      //         maxVal = (int)card.rank;
      //      }
      //      if ((int)card.rank < minVal)
      //      {
      //         minVal = (int)card.rank;
      //      }
      //   }
      //}

      //// utility method to see if cards are in a run
      //// (same suit assumed)
      //private bool isSequential(Cards cards)
      //{
      //   int maxVal, minVal;
      //   GetLimits(cards, out maxVal, out minVal);
      //   if ((maxVal - minVal) == (cards.Count - 1))
      //   {
      //      return true;
      //   }
      //   else
      //   {
      //      return false;
      //   }
      //}
   }
}
