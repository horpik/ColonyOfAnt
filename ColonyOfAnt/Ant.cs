using System.Collections.Generic;
using System.Linq;
using static ColonyOfAnt.Utility;

namespace ColonyOfAnt
{
    public abstract class Ant : Unit
    {
        // сумка с ресурсами и сколько и по сколько урона он может атаковать
        protected List<BackpackResource> Backpack;

        // первый индекс - сколько ресурсов в рюкзаке, второй - сколько из них за раз может брать 
        protected int[] ICanTakeResource = new int[2] {0, 0};

        // первый индекс - количество атак, второй - сила укуса
        protected int[] ICanAttak = new int[2] {0, 0};

        protected Colony myColony;
        public bool IHaveModifier = false;


        public void Attack(List<Ant> enemyAnts)
        {
            if (enemyAnts.Count == 0) return;
            var flag = enemyAnts.Any(ant => !ant.myModifier.Contains("неуязвимый"));
            var count = 0;
            while (count != ICanAttak[0] && flag)
            {
                var ant = enemyAnts.RandomElement();
                if (ant.myModifier.Contains("худой"))
                {
                    var ant_t = ant;
                    ant = enemyAnts.RandomElement();
                    while (ant_t == ant)
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
                if (!ant.isAlive)
                {
                    enemyAnts.Remove(ant);
                }

                count += 1;
                if (enemyAnts.Count == 0) return;
            }
        }

        public void TakeResource(Heap heap)
        {
            if (ICanTakeResource[1] == Backpack.Count)
            {
                foreach (var item in Backpack)
                {
                    item.AddElement(heap.ResourceExtraction(item.MyType()));
                }
            }
            else
            {
                var count = 0;
                var keys = Backpack.Select(item => item.MyType()).ToList();
                while (count != ICanTakeResource[1])
                {
                    // проверяем, что из кучи можно что-то взять
                    if (!heap.ResourcesAvailable(keys)) break;

                    var rndElement = rnd.Next(keys.Count);
                    if (heap.ResourceExtraction(Backpack[rndElement].MyType()) == 0) continue;
                    Backpack[rndElement].AddElement(heap.ResourceExtraction(Backpack[rndElement].MyType()));
                    count += 1;
                }
            }
        }

        public void TakeResource(Heap heap, int ICanTakeResource)
        {
            var count = 0;
            var keys = Backpack.Select(item => item.MyType()).ToList();
            while (count != ICanTakeResource)
            {
                // проверяем, что из кучи можно что-то взять
                if (!heap.ResourcesAvailable(keys)) break;

                var rndElement = rnd.Next(Backpack.Count);
                if (heap.ResourceExtraction(Backpack[rndElement].MyType()) == 0) continue;
                Backpack[rndElement].AddElement(heap.ResourceExtraction(Backpack[rndElement].MyType()));
                count += 1;
            }
        }

        public virtual void GetDamage(double incomingDamage)
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
            myModifier = new List<string> {""};
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
                        Backpack.Add(new BackpackResource());
                        Backpack[i].AddElement(0, Utility.RandomElement(existingResource));
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

        public virtual void ModifierAction(Heap heap, List<Ant> ants)
        {
        }

        public virtual void ModifierAction(Heap heap, List<Ant> ants, Location location)
        {
        }

        // TODO сделать метод "положить ресурсы в колонию"
    }
}