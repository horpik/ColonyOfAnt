using System.Collections.Generic;

namespace ColonyOfAnt
{
    public class LocationEvening : Location
    {
        public LocationEvening(List<Heap> heaps, List<List<Ant>> allAnt) : base(heaps, allAnt)
        {
            nameLocation = "вечер";
        }

        protected override void ActionOnHeap(Heap heap, List<List<Ant>> ListAntColony)
        {
            var AllAnts = new List<Ant>();
            foreach (var listAnts in ListAntColony)
            {
                AllAnts.AddRange(listAnts);
            }

            foreach (var ant in AllAnts)
            {
                if (ant.myModifier.Contains("эпический"))
                {
                    ant.ModifierAction(heap, AllAnts, this);
                }

                if (!ant.isAlive )
                {
                    ant.myColony.RemoveAnt(ant);
                }

                if (ant.myModifier.Contains("настойчивый") && ant.hp == 0)
                {
                    ant.myColony.RemoveAnt(ant);
                }
            }

            foreach (var ant in AllAnts)
            {
                if (ant.myClass == "рабочий")
                {
                    ant.PutResource();
                }
            }
        }
    }
}