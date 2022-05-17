using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using static ColonyOfAnt.Utility;

namespace ColonyOfAnt
{
    public class World
    {
        private List<Heap> heaps;
        private List<Colony> colonies;
        private int DaysBeforeDisaster = 13;

        public World()
        {
            Colony colony_green = new Colony("зеленые", 18, 6, "стрекоза");
            Queen queen_green = new Queen("Ольга", 20, 5, 15, rnd.Next(2, 3), rnd.Next(3, 4), false, colony_green);
            colony_green.AddQueen(queen_green);

            Colony colony_redheads = new Colony("рыжие", 10, 5, "бабочка");
            Queen queen_redheads =
                new Queen("Екатерина", 28, 9, 18, rnd.Next(3, 4), rnd.Next(2, 3), false, colony_green);
            colony_redheads.AddQueen(queen_redheads);

            Heap heap1 = new Heap(new Dictionary<string, int>()
            {
                {"веточка", 47},
                {"листик", 32},
            });
            Heap heap2 = new Heap(new Dictionary<string, int>()
            {
                {"веточка", 29},
                {"листик", 18},
                {"камушек", 22},
                {"росинка", 47}
            });
            Heap heap3 = new Heap(new Dictionary<string, int>()
            {
                {"веточка", 17},
                {"листик", 45},
                {"камушек", 39},
                {"росинка", 23}
            });
            Heap heap4 = new Heap(new Dictionary<string, int>()
            {
                {"веточка", 21},
                {"камушек", 47},
                {"росинка", 19}
            });
            Heap heap5 = new Heap(new Dictionary<string, int>()
            {
                {"веточка", 44},
                {"листик", 28},
                {"камушек", 20},
                {"росинка", 36}
            });

            heaps = new List<Heap> {heap1, heap2, heap3, heap4, heap5};
            colonies = new List<Colony> {colony_green, colony_redheads};
        }

        private List<Heap> heaps_time;
        private List<List<Ant>> ants_time;

        public void NextDay()
        {
            foreach (var colony in colonies)
            {
                colony.queen.CreateAnt();
            }

            heaps_time = new List<Heap>();
            ants_time = new List<List<Ant>>();
            foreach (var heap in heaps.Where(heap => heap.ResourcesAvailable()))
            {
                heaps_time.Add(heap);
            }

            if (rnd.Next(2) == 0)
            {
                heaps_time.Reverse();
            }

            foreach (var colony in colonies.Where(colony => colony.Ants.Count > 0))
            {
                ants_time.Add(colony.Ants);
            }

            DaysBeforeDisaster -= 1;

            LocationMorning locationMorning = new LocationMorning(heaps_time, ants_time);
            LocationDay locationDay = new LocationDay(heaps_time, ants_time);
            LocationEvening locationEvening = new LocationEvening(heaps_time, ants_time);
        }

        public void TellAboutWorld()
        {
            Console.WriteLine();
            Console.WriteLine($"День {DaysHavePassed} (до засухи {DaysBeforeDisaster} дней)\n" +
                              $"-------------------");

            foreach (var colony in colonies)
            {
                colony.DescribeItselBriefly();
                Console.WriteLine();
            }

            for (int i = 0; i < heaps.Count; i++)
            {
                Console.Write($"Куча {i + 1}: ");
                heaps[i].DescribeItself();
            }

            Console.WriteLine();
        }

        public void TellAboutColony()
        {
            Console.WriteLine("Введите номер колонии");
            for (int i = 0; i < colonies.Count; i++)
            {
                Console.WriteLine($"Колония {i + 1}:");
                colonies[i].DescribeItselBriefly();
                Console.WriteLine();
            }

            var colonySelection= Convert.ToInt16(Console.ReadLine());
            Console.WriteLine();
            colonies[colonySelection-1].DescribeItselfFull();
        }
    }
}