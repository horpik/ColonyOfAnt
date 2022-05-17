using System;
using System.Collections.Generic;
using System.Linq;

namespace ColonyOfAnt
{
    public class Heap
    {
        private Dictionary<string, int> ResourceInHeap;

        public Heap(Dictionary<string, int> ResourceInHeap)
        {
            this.ResourceInHeap = ResourceInHeap;
        }

        public int ResourceExtraction(string nameResource)
        {
            if (!ResourceInHeap.Keys.Contains(nameResource)) return 0;

            if (ResourceInHeap[nameResource] == 0) return 0;
            ResourceInHeap[nameResource] -= 1;
            return 1;
        }

        public bool ResourcesAvailable(List<string> list)
        {
            foreach (var item in list)
            {
                if (ResourceInHeap.Keys.Contains(item) && ResourceInHeap[item] > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ResourcesAvailable()
        {
            foreach (var item in ResourceInHeap)
            {
                if (item.Value > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void DescribeItself()
        {
            foreach (var resource in ResourceInHeap)
            {
                Console.Write($"{resource.Key} = {resource.Value} ");
            }

            Console.WriteLine();
        }
    }
}