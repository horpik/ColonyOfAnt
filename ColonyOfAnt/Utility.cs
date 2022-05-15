using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;

namespace ColonyOfAnt
{
    public static class Utility
    {
        public static readonly Random rnd = new Random();

        public static readonly List<string> existingResource = new List<string>
            {"веточка", "листик", "камушек", "росинка"};
        
        public static T RandomElement<T>(this List<T> list)
        {
            return list[rnd.Next(list.Count)];
        }
        
    }

    public struct BackpackResource
    {
        private string type;
        private int count;

        public string MyType() { return type; }

        public int MyCount() { return count; }

        public void AddElement(int count) { this.count += count; }
        
        public void AddElement(int count,string type)
        {
            this.count = count;
            this.type = type;
        }
    }
}