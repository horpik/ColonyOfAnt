using System.Collections.Generic;

namespace ColonyOfAnt
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Queen queen = new Queen();
            // string name, int countWorkers, int countWarriors, string nameSpecial
            Colony colony = new Colony(queen, "зеленые", 18,6, "стрекоза");
            
            List<Heap> heaps = new List<Heap>(){new(new Dictionary<string, int>() {{"веточка", 10},})};
            Dragonfly dragonfly = new Dragonfly(colony);
            AdvancedBrigadier advancedBrigadier = new AdvancedBrigadier("особый", colony);
            List<Ant> units = new List<Ant>();
            units.Add(dragonfly);
            units.Add(advancedBrigadier);
            List<List<Ant>> allUnits = new List<List<Ant>>();
            allUnits.Add(units);
            LocationDay locationDay = new LocationDay(heaps,allUnits);
        }
    }
}