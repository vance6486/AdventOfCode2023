using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode11
{
    internal class Space
    {
        public bool IsEmpty { get; private set; } = true;
        public Galaxy GalaxyContents { get; private set; } = null;

        public long Row;
        public long Col;

        public bool Visited = false;
        public long Steps = 0;

        public Space() { }

        public Space(char contentsChar, long row, long col)
        {
            Row = row; 
            Col = col;

            if (contentsChar == '#' )
            {
                IsEmpty = false;
                GalaxyContents = new Galaxy(row, col);
            }
        }

        public void ResetVisited() { Visited = false; }

        public override bool Equals(object? obj)
        {
            if(obj is Space otherSpace) return Row == otherSpace.Row && Col == otherSpace.Col;
            return false;
        }
    }
}
