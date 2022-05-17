using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static ColonyOfAnt.Utility;

namespace ColonyOfAnt
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            World world = new World();
            while (DaysBeforeDisaster > 0)
            {
                world.NextDay();
                DaysBeforeDisaster -= 1;
            }

            Console.WriteLine();
        }
    }
}