using System.Collections.Generic;

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
            if (ResourceInHeap[nameResource] == 0) return 0;
            ResourceInHeap[nameResource] -= 1;
            return 1;
        }

        public bool ResourcesAvailable(List<string> list)
        {
            foreach (var item in list)
            {
                if (ResourceInHeap[item] > 0)
                {
                    return true;
                }
            }

            return false;
        }public bool ResourcesAvailable()
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
        

        public bool ResourcesExist()
        {
            foreach (var resource in ResourceInHeap)
            {
                if (resource.Value > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}