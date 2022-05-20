using System;
using System.Collections.Generic;
using System.Linq;
using static ColonyOfAnt.Utility;

namespace ColonyOfAnt
{
    public abstract class Ant : Unit
    {
        // сумка с ресурсами и сколько и по сколько урона он может атаковать
        public List<BackpackResource> Backpack { get; protected set; }

        public List<string> myModifier { get; protected set; }

        // первый индекс - сколько ресурсов в рюкзаке, второй - сколько из них за раз может брать 
        protected int[] ICanTakeResource = new int[2] {0, 0};

        // первый индекс - количество атак, второй - сила укуса
        protected int[] ICanAttak = new int[2] {0, 0};
        public string myClass { get; protected set; }


        public void Attack(List<Ant> enemyAnts)
        {
            if (enemyAnts.Count == 0) return;
            var allNotInvulnerable = enemyAnts.Any(ant => !ant.myModifier.Contains("неуязвимый"));
            var allNotAlive = enemyAnts.Any(ant => ant.isAlive);
            var count = 0;

            while (count != ICanAttak[0] && allNotInvulnerable && allNotAlive)
            {
                List<Ant> t = enemyAnts.Where(_ant => _ant.isAlive).ToList();
                allNotInvulnerable = t.Any(ant => !ant.myModifier.Contains("неуязвимый"));
                allNotAlive = enemyAnts.Any(ant => ant.isAlive);

                var ant = enemyAnts.RandomElement();

                if (!ant.isAlive) continue;

                if (ant.myModifier.Contains("худой"))
                {
                    var ant_t = ant;
                    ant = enemyAnts.RandomElement();
                    while (ant_t == ant && enemyAnts.Count != 1)
                    {
                        ant = enemyAnts.RandomElement();
                    }

                    ant.GetDamage(damage * ICanAttak[1]);
                    count += 1;
                    continue;
                }

                if (ant.myModifier.Contains("неуязвимый"))
                {
                    continue;
                }

                ant.GetDamage(damage * ICanAttak[1]);
                allNotInvulnerable = enemyAnts.Any(ant => !ant.myModifier.Contains("неуязвимый"));
                allNotAlive = enemyAnts.Any(ant => ant.isAlive);

                count += 1;
            }
        }

        public void TakeResource(Heap heap)
        {
            if (ICanTakeResource[1] == Backpack.Count)
            {
                for (int i = 0; i < Backpack.Count; i++)
                {
                    var resource = new BackpackResource();
                    var value = Backpack[i].Value();
                    var type = Backpack[i].Type();
                    value += heap.ResourceExtraction(type);
                    resource.CreateBackpack(value, type);
                    Backpack[i] = resource;
                }
            }
            else
            {
                var count = 0;
                var keys = Backpack.Select(item => item.Type()).ToList();

                while (count != ICanTakeResource[1])
                {
                    // проверяем, что из кучи можно что-то взять
                    if (!heap.ResourcesAvailable(keys)) break;

                    var rndElement = rnd.Next(keys.Count);
                    if (heap.ResourceExtraction(Backpack[rndElement].Type()) == 0) continue;

                    var resource = new BackpackResource();
                    var value = Backpack[rndElement].Value();
                    var type = Backpack[rndElement].Type();
                    value += heap.ResourceExtraction(type);
                    resource.CreateBackpack(value, type);
                    Backpack[rndElement] = resource;

                    count += 1;
                }
            }
        }

        public void PutResource()
        {
            myColony.GetResource(this);

            for (int i = 0; i < Backpack.Count; i++)
            {
                var resource = new BackpackResource();
                var value = 0;
                var type = Backpack[i].Type();
                resource.CreateBackpack(value, type);
                Backpack[i] = resource;
            }
        }


        public void TakeResource(Heap heap, int ICanTakeResource)
        {
            var count = 0;
            var keys = Backpack.Select(item => item.Type()).ToList();
            while (count != ICanTakeResource)
            {
                // проверяем, что из кучи можно что-то взять
                if (!heap.ResourcesAvailable(keys)) break;

                var rndElement = rnd.Next(Backpack.Count);
                if (heap.ResourceExtraction(Backpack[rndElement].Type()) == 0) continue;

                var resource = new BackpackResource();
                var value = Backpack[rndElement].Value();
                var type = Backpack[rndElement].Type();
                value += heap.ResourceExtraction(type);
                resource.CreateBackpack(value, type);
                Backpack[rndElement] = resource;

                count += 1;
            }
        }

        public void GetDamage(double incomingDamage)
        {
            if (myModifier.Any(modifeir => modifeir == "неуязвимый"))
            {
                return;
            }

            if (myClass == "рабочий")
            {
                hp = 0;
                isAlive = false;
                return;
            }

            if (defense > 0)
            {
                if (defense < incomingDamage)
                {
                    hp = hp + defense - incomingDamage;
                    defense = 0;
                }
                else
                {
                    defense -= incomingDamage;
                }
            }
            else
            {
                hp -= incomingDamage;
            }

            if (hp <= 0) isAlive = false;
            if (myModifier.Contains("настойчивый") && myClass != "рабочий")
            {
                isAlive = true;
            }
        }

        protected void InitializingParameters(int hp, int defense, int damage, string myClass, Colony myColony,
            int[] ICanAttak, int[] ICanTakeResource)
        {
            this.hp = hp;
            this.defense = defense;
            this.myClass = myClass;
            this.myColony = myColony;
            switch (myClass)
            {
                case "воин":
                    this.damage = damage;
                    this.ICanAttak[0] = ICanAttak[0];
                    this.ICanAttak[1] = ICanAttak[1];
                    break;
                case "рабочий":
                {
                    this.ICanTakeResource[0] = ICanTakeResource[0];
                    Backpack = new List<BackpackResource>();
                    for (var i = 0; i < ICanTakeResource[0]; i++)
                    {
                        BackpackResource resource = new BackpackResource();

                        resource.CreateBackpack(0, existingResource.RandomElement());
                        Backpack.Add(resource);
                    }

                    break;
                }
            }
        }

        public void IncreaseParameters()
        {
            hp = 2 * hp;
            defense = 2 * defense;
        }

        public void ReduceParameters()
        {
            hp = (int) (hp / 2);
            defense = (int) (defense / 2);
        }

        public virtual void ModifierAction(Heap heap, List<Ant> ants, Location location)
        {
        }

        public void DescribeItselfFull()
        {
            DescribeItselfBriefly();

            Console.WriteLine($"--- Королева: {myColony.queen.name}");
        }

        public virtual void DescribeItselfBriefly()
        {
            Console.Write($"Тип: {string.Join(" ", myModifier)}\n" +
                          $"--- Параметры: здоровье: {hp} защита: {defense}");
            if (myClass == "воин")
            {
                Console.WriteLine($" Урон: {damage}");
            }
            else
            {
                Console.WriteLine();
            }
        }

        public void DescribeQueen()
        {
            Console.WriteLine($"Королева: {myColony.queen.name} \n" +
                              $"Личинок: {myColony.queen.larvae}\n" +
                              $"--- Параметры: здоровье {myColony.queen.hp}, защита {myColony.queen.defense}, урон {myColony.queen.damage}");
        }
    }
}