using System.Collections.Generic;
namespace ColonyOfAnt
{
    public class LocationEvening : Location
    {
        public LocationEvening(List<Colony> colonies) : base(colonies)
        {
            nameLocation = "вечер";
        }
        protected override void ActionOnHeap(Heap heap, List<List<Ant>> Ants)
        {
            var AllAnts = new List<Ant>();
            // AllAnts - список муравьёв со всех колоний
            foreach (var listAnts in Ants)
            {
                AllAnts.AddRange(listAnts);
            }

            foreach (var ant in AllAnts)
            {
                if (ant.myModifier.Contains("бригадир") && ant.myClass == "рабочий" ||
                    ant.myModifier.Contains("эпический"))
                {
                    ant.ModifierAction(heap, AllAnts);
                }
            }
        }
    }
}