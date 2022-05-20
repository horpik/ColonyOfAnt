using System.Collections.Generic;

namespace ColonyOfAnt
{
    public class Common : Ant
    {
        public Common(string UnitClass, Colony colony)
        {
            
            myModifier = new List<string>(){"обычный"};
            InitializingParameters(1, 0, 1, UnitClass, colony, new int[2] {1, 1}, new int[2]{1,1});
        }
    }
}