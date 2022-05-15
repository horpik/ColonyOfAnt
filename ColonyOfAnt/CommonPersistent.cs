using System.Collections.Generic;

namespace ColonyOfAnt
{
    public class CommonPersistent : Common
    {
        // <обычный настойчивый> ВОИН(здоровье=1, защита=0, урон=1) может атаковать 1 цель за раз и
        // наносит 1 укус; всегда наносит укус, даже если был убит.

        public CommonPersistent(string UnitClass, Colony colony) : base(UnitClass, colony)
        {
            IHaveModifier = true;
            myModifier = new List<string>{"настойчивый"};
        }
        
    }
}