using System.Collections.Generic;
using static ColonyOfAnt.Utility;

namespace ColonyOfAnt
{
    public class Colony
    {
        private Dictionary<string, int> ResourceInColony = new()
        {
            {"веточка", 0},
            {"листик", 0},
            {"камушек", 0},
            {"росинка", 0}
        };

        private string name;
        private Queen queen;
        private int countWorkers;
        private int countWarriors;
        private int countSpecial;

        // добавить специального насекомого
        private List<Ant> Ants = new List<Ant>();

        public Colony(Queen queen, string name, int countWorkers, int countWarriors, string nameSpecial)
        {
            this.queen = queen;
            this.name = name;
            this.countWorkers = countWorkers;
            this.countWarriors = countWarriors;

            for (int i = 0; i < countWorkers; i++)
            {
                CreateAnAnt(Ants, "рабочий");
            }

            for (int i = 0; i < countWarriors; i++)
            {
                CreateAnAnt(Ants, "воин");
            }

            switch (nameSpecial)
            {
                case "стрекоза":
                    Ants.Add( new Dragonfly(this));
                    break;
                case "бабочка":
                    Ants.Add( new Butterfly(this));
                    break;
            }
            
        }

        private void CreateAnAnt(List<Ant> ants, string nameClass)
        {
            var existingAnt = new List<Ant>
            {
                new Advanced(nameClass, this), new AdvancedBrigadier(nameClass, this),
                new AdvancedExperienced(nameClass, this), new Common(nameClass, this),
                new CommonPersistent(nameClass, this), new Legendary(nameClass, this),
                new LegendarySkinny(nameClass, this), new Elite(nameClass, this)
            };

            ants.Add(existingAnt.RandomElement());
        }

        private void QuantityCountByClass()
        {
            countWarriors = 0;
            countWorkers = 0;
            countSpecial = 0;
            foreach (var item in Ants)
            {
                switch (item.myClass)
                {
                    case "воин":
                        countWarriors += 1;
                        break;
                    case "рабочий":
                        countWorkers += 1;
                        break;
                    case "особый":
                        countSpecial += 1;
                        break;
                }
            }
        }
        // TODO создать метод для дополнительного эффекта
    }
}