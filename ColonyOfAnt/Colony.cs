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

        public string name { get; private set; }
        public Queen queen { get; private set; }
        private int countWorkers;
        private int countWarriors;
        private int countSpecial;

        // добавить специального насекомого
        public List<Ant> Ants { get; private set; }

        public Colony(string name, int countWorkers, int countWarriors, string nameSpecial)
        {
            Ants = new List<Ant>();
            this.name = name;
            this.countWorkers = countWorkers;
            this.countWarriors = countWarriors;

            for (int i = 0; i < countWorkers; i++)
            {
                CreateAnAnt("рабочий");
            }

            for (int i = 0; i < countWarriors; i++)
            {
                CreateAnAnt("воин");
            }

            switch (nameSpecial)
            {
                case "стрекоза":
                    Ants.Add(new Dragonfly(this));
                    break;
                case "бабочка":
                    Ants.Add(new Butterfly(this));
                    break;
            }
        }

        public void AddQueen(Queen queen)
        {
            this.queen = queen;
        }

        public void CreateAnAnt(string nameClass)
        {
            var existingAnt = new List<Ant>
            {
                new AdvancedBrigadier(nameClass, this),
                new CommonPersistent(nameClass, this),
                new LegendarySkinny(nameClass, this)
            };
            // var existingAnt = new List<Ant>
            // {
            //     new Advanced(nameClass, this), new AdvancedBrigadier(nameClass, this),
            //     new AdvancedExperienced(nameClass, this), new Common(nameClass, this),
            //     new CommonPersistent(nameClass, this), new Legendary(nameClass, this),
            //     new LegendarySkinny(nameClass, this), new Elite(nameClass, this)
            // };

            Ants.Add(existingAnt.RandomElement());
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

        public void GetResource(Ant ant)
        {
            foreach (var resource in ant.Backpack)
            {
                ResourceInColony[resource.Type()] += resource.Value();
            }
        }

        public void RemoveAnt(Ant ant)
        {
            Ants.Remove(ant);
        }

        // TODO создать метод для дополнительного эффекта
    }
}