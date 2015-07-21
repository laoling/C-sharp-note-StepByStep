using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11CardLib
{
    /// <summary>
    /// 使用ICloneable接口复制Card类
    /// Card是只包含值类型的数据，其形式为字段
    /// 所以只需要进行浅复制即可
    /// </summary>
    
    public class Card : ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }

        public readonly Suit suit;
        public readonly Rank rank;

        public Card(Suit newSuit, Rank newRank)
        {
            suit = newSuit;
            rank = newRank;
        }

        private Card()
        { 
        }

        public override string ToString()
        {
            return "The " + rank + " of " + suit + "s";
        }
    }
}
