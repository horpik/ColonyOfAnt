using System.Collections.Generic;

namespace ColonyOfAnt
{
    public class LegendarySkinny : Legendary
    {
        public LegendarySkinny(string UnitClass, Colony colony) : base(UnitClass, colony)
        {
            IHaveModifier = true;
            myModifier = new List<string>{"худой"};
        }
    }
}