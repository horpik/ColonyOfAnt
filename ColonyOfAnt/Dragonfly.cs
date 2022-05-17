using System.Collections.Generic;
using System.Linq;
using static ColonyOfAnt.Utility;
namespace ColonyOfAnt
{
    public class Dragonfly : SpecialInsect
    {
        public Dragonfly(Colony colony)
        {
            hp = 24;
            defense = 7;
            damage = 13;
            myClass = "особый";
            myColony = colony;
            myModifier = new List<string>() {"ленивый", "обычный", "агрессивный", "аномальный"};
            ICanAttak[0] = 3;
            ICanAttak[1] = 2;
        }

        
    }
}