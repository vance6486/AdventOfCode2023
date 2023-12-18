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

        public long Distance;

        public GalaxyPair(Galaxy galaxyA, Galaxy galaxyB, long distance)
        {
            GalaxyA = galaxyA;
            GalaxyB = galaxyB;
            Distance = distance;
        }

        int IComparable.CompareTo(object obj)
        {
            return Distance.CompareTo(((GalaxyPair)obj).Distance);
        }

        public bool Contains(Galaxy galaxy)
        {
            return galaxy.Equals(GalaxyA) || galaxy.Equals(GalaxyB);
        }

        public override bool Equals(object obj)
        {
            if(obj == null) return false;
            if (obj is GalaxyPair pair) return pair.Contains(GalaxyA) && pair.Contains(GalaxyB);
            if (obj is Galaxy galaxy) return Contains(galaxy);
            return false;
        }

        public override string ToString()
        {
            return $"A: {GalaxyA.ToString()}| B: {GalaxyB.ToString()}";
        }
    }
}
