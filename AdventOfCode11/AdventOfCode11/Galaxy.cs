using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode11
{
    internal class Galaxy
    {
        public int Row { get; private set; }
        public int Col { get; private set; }
        public int Id { get; private set; }

        //public List<GalaxyPair> AllGalaxyPairs = new List<GalaxyPair>();

        public Galaxy PairedGalaxy = null;
        public bool HasPairedGalaxy {  get; private set; }


        public Galaxy()
        {
            Row = 0;
            Col = 0;
        }

        public Galaxy(int row, int col)
        {
            Row = row;
            Col = col;

            Id = (row*1000) + col;
        }

        //public void AddPair(Galaxy otherGalaxy, int distance)
        //{
        //    if(this != otherGalaxy)
        //    {
        //        AllGalaxyPairs.Add(new GalaxyPair(this, otherGalaxy, distance));
        //        AllGalaxyPairs.Sort();
        //    }
                
        //}

        //public bool hasGalaxyPair(Galaxy otherGalaxy)
        //{
        //    bool hasPair = false;

        //    foreach (GalaxyPair pair in AllGalaxyPairs)
        //    {
        //        if(pair.GalaxyB == otherGalaxy)
        //        {
        //            hasPair = true;
        //        }
        //    }

        //    return hasPair;
        //}

        public override bool Equals(object obj)
        {
            return Id.Equals(((Galaxy)obj).Id);
        }

    }
}
