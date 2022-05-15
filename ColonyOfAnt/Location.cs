using System.Collections.Generic;
using System.Linq;
using static ColonyOfAnt.Utility;

namespace ColonyOfAnt
{
    // делим на локации:
    // утро - активация всех специальных модификаторов на куче.
    // день - сражение за ресурсы, взятие ресурсов.
    // вечер - положить ресурсы в колонию и освободить рюкзаки.
    public abstract class Location
    {
        protected List<List<Ant>> AntOnHeapFromColony;
        protected List<List<List<Ant>>> AntOnHeap;
        public string nameLocation { get; protected set; }
        public bool flagEpic = false;

        public Location(List<Heap> heaps, List<List<Ant>> AllAnt)
        {
            var startIndex = new int[heaps.Count];
            var endIndex = new int[heaps.Count];
            startIndex[0] = 0;

            if (heaps.Count == 1 && !heaps[0].ResourcesAvailable())
            {
                ActionOnHeap(heaps[0], AllAnt);
                return;
            }

            foreach (var listAnt in AllAnt)
            {
                AntOnHeapFromColony = new List<List<Ant>>();
                endIndex[0] = rnd.Next(startIndex[0] + 1, listAnt.Count);
                for (int i = 1; i < heaps.Count; i++)
                {
                    if (endIndex[i - 1] == listAnt.Count - 1) break;
                    if (i == heaps.Count - 1)
                    {
                        startIndex[i] = endIndex[i - 1] + 1;
                        endIndex[i] = listAnt.Count - 1;
                        break;
                    }

                    startIndex[i] = endIndex[i - 1] + 1;
                    endIndex[i] = rnd.Next(startIndex[i], listAnt.Count);
                }

                for (int i = 0; i < heaps.Count; i++)
                {
                    if (startIndex[i] == 0 && endIndex[i] == 0)
                    {
                        break;
                    }

                    AntOnHeapFromColony.Add(listAnt.GetRange(startIndex[i], endIndex[i] - startIndex[i] + 1));
                }

                if (AntOnHeap != null) AntOnHeap.Add(AntOnHeapFromColony);
            }

            for (var i = 0; i < heaps.Count; i++)
            {
                List<List<Ant>> listColony =
                    (from ListAnt in AntOnHeap where ListAnt[i] != null select ListAnt[i]).ToList();

                ActionOnHeap(heaps[i], listColony);
            }
        }

        public Location(List<Colony> colonies)
        {
        }

        protected virtual void ActionOnHeap(Heap heap, List<List<Ant>> ListColony)
        {
        }
    }
}