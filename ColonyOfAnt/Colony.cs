using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<Ant> uniqueListOfAnts = new();

        // добавить специального насекомого
        public List<Ant> Ants { get; private set; }

        public Colony(string name, int countWorkers, int countWarriors, string nameInsect)
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

            CreateSpecialInsect(nameInsect);
        }

        public void AddQueen(Queen queen)
        {
            this.queen = queen;
        }

        public void CreateAnAnt(string nameClass)
        {
            var existingAnt = new List<Ant>
            {
                new Advanced(nameClass, this), new AdvancedBrigadier(nameClass, this),
                new AdvancedExperienced(nameClass, this), new Common(nameClass, this),
                new CommonPersistent(nameClass, this), new Legendary(nameClass, this),
                new LegendarySkinny(nameClass, this), new Elite(nameClass, this)
            };

            Ants.Add(existingAnt.RandomElement());
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

        public int SumResource()
        {
            return ResourceInColony.Sum(resource => resource.Value);
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

        private void ShowInformationAboutAllAntsBriefly()
        {
            QuantityCountByClass();
            Console.WriteLine(
                $"--- Популяция {countWorkers + countWarriors + countSpecial}: рабочие - {countWorkers}, войны - {countWarriors}, особые - {countSpecial}");
        }


        private void ShowInformationAboutResource()
        {
            Console.Write("--- Ресурсы: ");
            foreach (var resource in ResourceInColony)
            {
                Console.Write($"{resource.Key} = {resource.Value} ");
            }

            Console.WriteLine();
        }

        private void DescribeQueenFull()
        {
            Console.WriteLine($"--- Королева: {queen.name}, личинок: {queen.larvae} \n" +
                              $"--- Параметры: здоровье {queen.hp}, защита {queen.defense}, урон {queen.damage}");
        }

        private void DescribeQueenBriefly()
        {
            Console.WriteLine($"--- Королева: {queen.name}, личинок: {queen.larvae}");
        }


        public void DescribeItselBriefly()
        {
            Console.WriteLine($"Колония «{name}»");
            DescribeQueenBriefly();
            ShowInformationAboutResource();
            ShowInformationAboutAllAntsBriefly();
        }

        public void DescribeItselfFull()
        {
            Console.WriteLine($"Колония «{name}»");
            DescribeQueenFull();
            ShowInformationAboutResource();
            ShowInformationAboutAllAntsFull();
        }

        public void ShowInformationAboutAllAntsFull()
        {
            if (Ants.Count == 0)
            {
                Console.WriteLine("В колонии никого нет, кроме королевы");
                return;
            }

            var countUniqueAntWorkers = new List<int>();
            var countUniqueAntWarriers = new List<int>();
            var countUniqueAntSpecial = new List<int>();


            SearchUniqueAnt();
            foreach (var t1 in uniqueListOfAnts)
            {
                countUniqueAntWorkers.Add(0);
                countUniqueAntWarriers.Add(0);
                countUniqueAntSpecial.Add(0);
                foreach (var ant in Ants.Where(t => t1.myModifier.SequenceEqual(t.myModifier)))
                {
                    switch (ant.myClass)
                    {
                        case "рабочий":
                            countUniqueAntWorkers[countUniqueAntWorkers.Count - 1] += 1;
                            break;
                        case "воин":
                            countUniqueAntWarriers[countUniqueAntWarriers.Count - 1] += 1;
                            break;
                        case "особый":
                            countUniqueAntSpecial[countUniqueAntSpecial.Count - 1] += 1;
                            break;
                    }
                }
            }


            Console.WriteLine();
            Console.WriteLine("<<<<<<<<<<<<< Рабочие >>>>>>>>>>>>>");
            for (int i = 0; i < uniqueListOfAnts.Count; i++)
            {
                if (countUniqueAntWorkers[i] == 0)
                {
                    continue;
                }

                uniqueListOfAnts[i].DescribeItselfBriefly();
                Console.WriteLine($"--- Количество: {countUniqueAntWorkers[i]}");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("<<<<<<<<<<<<<< Воины >>>>>>>>>>>>>>");
            for (int i = 0; i < uniqueListOfAnts.Count; i++)
            {
                if (countUniqueAntWarriers[i] == 0)
                {
                    continue;
                }

                uniqueListOfAnts[i].DescribeItselfBriefly();
                Console.WriteLine($"--- Количество: {countUniqueAntWarriers[i]}");
                Console.WriteLine();
            }

            var flag = countUniqueAntSpecial.Any(ant => ant != 0);

            if (!flag) return;
            Console.WriteLine("<<<<<<<<<<<<< Особые >>>>>>>>>>>>>");
            for (int i = 0; i < uniqueListOfAnts.Count; i++)
            {
                if (countUniqueAntSpecial[i] == 0)
                {
                    continue;
                }

                uniqueListOfAnts[i].DescribeItselfBriefly();
                Console.WriteLine($"--- Количество: {countUniqueAntSpecial[i]}");
                Console.WriteLine();
            }
        }


        private void SearchUniqueAnt()
        {
            uniqueListOfAnts = new List<Ant>();
            foreach (var ant in Ants)
            {
                var counter = uniqueListOfAnts.Count;
                var IsUnique = true;
                for (int j = 0; j < counter; j++)
                {
                    if (ant.myModifier.SequenceEqual(uniqueListOfAnts[j].myModifier))
                    {
                        IsUnique = false;
                        break;
                    }
                }

                if (!IsUnique) continue;
                uniqueListOfAnts.Add(ant);
            }
        }

        public void TellAboutAnt()
        {
            SearchUniqueAnt();

            for (int i = 0; i < uniqueListOfAnts.Count; i++)
            {
                Console.WriteLine($"Муравей {i + 1}:");
                uniqueListOfAnts[i].DescribeItselfBriefly();
                Console.WriteLine();
            }

            var flag = false;
            while (!flag)
            {
                Console.WriteLine("\n\n\n\nВведите 1, чтобы узнать подробнее о муравье\n" +
                                  "Введите 2, чтобы муравьи рассказали о своей королеве\n" +
                                  "Введите 3, чтобы продолжить\n\n");
                var inputNumber = Convert.ToInt16(Console.ReadLine());
                switch (inputNumber)
                {
                    case 1:
                    {
                        Console.WriteLine("Выберите номер муравья:");
                        Console.WriteLine();
                        var input = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine();
                        uniqueListOfAnts[input - 1].DescribeItselfFull();
                        break;
                    }
                    case 2:
                    {
                        Console.WriteLine("Выберите номер муравья");
                        var input = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine();
                        uniqueListOfAnts[input - 1].DescribeQueen();
                        break;
                    }
                    default:
                        flag = true;
                        break;
                }
            }
        }

        public void CreateSpecialInsect(string nameInsect)
        {
            switch (nameInsect)
            {
                case "стрекоза":
                    Ants.Add(new Dragonfly(this));
                    break;
                case "бабочка":
                    Ants.Add(new Butterfly(this));
                    break;
            }
        }
    }
}