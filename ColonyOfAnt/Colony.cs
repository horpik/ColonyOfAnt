using System.Collections.Generic;

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
        // добавить специального насекомого
        private List<Ant> Ants = new List<Ant>();
        
        public Colony()
        {
            
        }
    }
}