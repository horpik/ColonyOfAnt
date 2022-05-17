using System;
using System.Collections.Generic;
using static ColonyOfAnt.Utility;

namespace ColonyOfAnt
{
    public class Queen : Unit
    {
        private int GrowthCycle;
        private int CanCreateQueens;
        public int larvae { get; private set; }
        public string name { get; private set; }
        private List<string> ExistingClasses = new List<string> {"рабочий", "воин"};

        public Queen(string name, double hp, double defense, double damage, int GrowthCycle, int CanCreateQueens,
            bool isDaughter, Colony colony)
        {
            this.name = name;
            this.hp = hp;
            this.defense = defense;
            this.damage = damage;
            this.GrowthCycle = GrowthCycle;
            this.CanCreateQueens = CanCreateQueens;
            myColony = colony;
        }

        public void CreateAnt()
        {
            if (DaysBeforeDisaster % GrowthCycle == (GrowthCycle - 1))
            {
                larvae = rnd.Next(4);
            }

            if (DaysBeforeDisaster % GrowthCycle == 0)
            {
                for (int i = 0; i < larvae; i++)
                {
                    if (rnd.Next(5) == 0 && CanCreateQueens > 0)
                    {
                        string name = "дочь " + this.name;
                        var queen = new Queen(name, hp, defense, damage, GrowthCycle, 0, true,
                            new Colony(myColony.name, 0, 0, existingSpecial.RandomElement()));
                        QueensDaughter.Add(queen);
                        CanCreateQueens -= 1;
                        continue;
                    }

                    myColony.CreateAnAnt(ExistingClasses.RandomElement());
                }
            }
        }
    }
}