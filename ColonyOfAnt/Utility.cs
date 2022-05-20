using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;

namespace ColonyOfAnt
{
    public static class Utility
    {
        public static readonly Random rnd = new Random();
        public static int DaysBeforeDisaster = 12;
        public static int DaysHavePassed = 0;
        
        
        public static readonly List<string> existingResource = new() {"веточка", "листик", "камушек", "росинка"};
        public static readonly List<string> existingSpecial = new() {"стрекоза", "бабочка"};
        public static List<Queen> QueensDaughter = new List<Queen>();

        public static T RandomElement<T>(this List<T> list)
        {
            return list[rnd.Next(list.Count)];
        }
    }

    public struct BackpackResource
    {
        private string type;
        private int count;

        public string Type()
        {
            return type;
        }

        public int Value()
        {
            return count;
        }

        

        public void CreateBackpack(int count, string type)
        {
            this.count = count;
            this.type = type;
        }
    }
}