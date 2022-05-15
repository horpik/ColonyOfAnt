using System.Collections.Generic;
using System.Linq;
using static ColonyOfAnt.Utility;
namespace ColonyOfAnt
{
    public class Dragonfly : Ant
    {
        public Dragonfly(Colony colony)
        {
            hp = 24;
            defense = 7;
            damage = 13;
            myClass = "особый";
            myModifier = new List<string>() {"ленивый", "обычный", "агрессивный", "аномальный"};
            ICanAttak[0] = 3;
            ICanAttak[1] = 2;
        }

        
    }
}