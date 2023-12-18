using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode11
{
    internal class Galaxy
    {
        public long Row { get; private set; }
        public long Col { get; private set; }
        public string Id { get; private set; }

        //public List<GalaxyPair> AllGalaxyPairs = new List<GalaxyPair>();

        public Galaxy PairedGalaxy = null;
        public bool HasPairedGalaxy {  get; private set; }

        public long StepsToPair;


        public Galaxy()
        {
            Row = 0;
            Col = 0;
        }

        public Galaxy(long row, long col)
        {
            Row = row;
            Col = col;

            Id = $"{row,3}, {col,3}";
        }

        public void PairGalaxy(Galaxy galaxy, long steps)
        {
            HasPairedGalaxy = true;
            PairedGalaxy = galaxy;
            StepsToPair = steps;
        }

        public override bool Equals(object obj)
        {
            return Id.Equals(((Galaxy)obj).Id);
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
