using System.Collections.Generic;
using System.Linq;

namespace ColonyOfAnt
{
    public class Butterfly : Ant
    {
        public Butterfly(Colony colony)
        {
            hp = 24;
            defense = 7;
            damage = 13;
            myClass = "особый";
            myColony = colony;
            myModifier = new List<string>() {"ленивый", "неуязвимый", "мирный", "эпический"};
            ICanTakeResource[0] = 0;
            ICanTakeResource[1] = 0;
        }

        public override void ModifierAction(Heap heap, List<Ant> ants, Location location)
        {
            foreach (var ant in ants)
            {
                switch (location.nameLocation)
                {
                    case "утро":
                        ant.IncreaseParameters();
                        break;
                    case "вечер":
                        ant.ReduceParameters();
                        break;
                }
            }
        }
    }
}