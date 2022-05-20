using System.Collections.Generic;

namespace ColonyOfAnt
{
    public class LegendarySkinny : Legendary
    {
        public LegendarySkinny(string UnitClass, Colony colony) : base(UnitClass, colony)
        {
            myModifier.Add("худой");
        }
    }
}