using System.Collections.Generic;

namespace ColonyOfAnt
{
    public class Elite : Ant
    {
        public Elite(string UnitClass, Colony colony)
        {
            myModifier = new List<string>(){"элитный"};
            InitializingParameters(8, 4, 3, UnitClass, colony, new int[2] {2, 2}, new int[2] {2, 2});
        }
    }
}