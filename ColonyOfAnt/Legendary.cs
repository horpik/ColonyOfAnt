using System.Collections.Generic;

namespace ColonyOfAnt
{
    public class Legendary : Ant
    {
        public Legendary(string UnitClass, Colony colony)
        {
            myModifier = new List<string>(){"легендарный"};
            InitializingParameters(10, 6, 4, UnitClass, colony, new int[2] {3, 1}, new int[2]{3,3});
        }
    }
}