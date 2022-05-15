using System.Collections.Generic;

namespace ColonyOfAnt
{
    public abstract class Unit
    {
        public double hp { get; protected set; }
        public double defense { get; protected set; }
        public double damage { get; protected set; }
        public string myClass { get; protected set; }
        public List<string> myModifier { get; protected set; }
        public bool isAlive = true;

        
    }
}