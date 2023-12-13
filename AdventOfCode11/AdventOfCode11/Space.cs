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

        public int Row;
        public int Col;

        public bool Visited = false;
        public int Steps = 0;

        public Space() { }

        public Space(char contentsChar, int row, int col)
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
    }
}
