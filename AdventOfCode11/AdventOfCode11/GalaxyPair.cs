using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode11
{
    internal class GalaxyPair : IComparable
    {
        public Galaxy GalaxyA;
        public Galaxy GalaxyB;

        public int Distance;

        public GalaxyPair(Galaxy galaxyA, Galaxy galaxyB, int distance)
        {
            GalaxyA = galaxyA;
            GalaxyB = galaxyB;
            Distance = distance;
        }

        int IComparable.CompareTo(object obj)
        {
            return Distance.CompareTo(((GalaxyPair)obj).Distance);
        }
    }
}
