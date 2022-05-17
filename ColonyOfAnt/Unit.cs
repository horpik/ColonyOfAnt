using System.Collections.Generic;

namespace ColonyOfAnt
{
    public abstract class Unit
    {
        public double hp { get; protected set; }
        public double defense { get; protected set; }
        public double damage { get; protected set; }
        public Colony myColony { get; protected set; }
        public bool isAlive = true;
    }
}