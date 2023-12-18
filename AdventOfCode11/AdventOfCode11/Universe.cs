using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode11
{
    internal class Universe
    {
        private char[,] RawData;

        private Space[,] spaceArray;

        private Queue<Space> spaceQueue;

//        private List<Galaxy> galaxyList = new List<Galaxy>();

        public List<GalaxyPair> AllGalaxyPairs = new List<GalaxyPair>();

        public List<int> blankRows = new List<int>();
        public List<int> blankCols = new List<int>();

        private readonly long ExpansionFactor = 999999;

        public Universe() { }
        
        public Universe(char[,] rawData)
        {
            RawData = rawData;
            GenerateSpace();
            PairGalaxies();
            //FindLowestPairs();
        }

        public void GenerateSpace()
        {
            spaceArray = new Space[RawData.GetLength(0), RawData.GetLength(1)];
            for(long row = 0; row < RawData.GetLength(0); row++)
            {
                for(long col = 0; col < RawData.GetLength(1); col++)
                {
                    spaceArray[row,col] = new Space(RawData[row,col], row, col);

//                    if (spaceArray[row, col].IsEmpty == false)
//                        galaxyList.Add(spaceArray[row, col].GalaxyContents);

                }
            }
            CalculateBlankRows();
            CalculateBlankCols();
        }

        public void CalculateBlankRows()
        {
            blankRows = new List<int>();
            // check every row
            for (long row = 0; row < spaceArray.GetLength(0); row++)
            {
                bool isRowBlank = true;
                //check all columns in that row
                for(long col = 0; col < spaceArray.GetLength(1); col++)
                {
                    if (spaceArray[row, col].IsEmpty == false)
                        isRowBlank = false;
                }

                if(isRowBlank == true) blankRows.Add((int)row);
            }
        }

        public void CalculateBlankCols()
        {
            blankCols = new List<int>();
            // check every row
            for (long col = 0; col < spaceArray.GetLength(1); col++)
            {
                bool isColBlank = true;
                //check all columns in that row
                for (long row = 0; row < spaceArray.GetLength(0); row++)
                {
                    if (spaceArray[row, col].IsEmpty == false)
                        isColBlank = false;
                }

                if (isColBlank == true) blankCols.Add((int)col);
            }
        }

        public void PairGalaxies()
        {
            //find each galaxy and run a search to find all the galaxy pairs
            foreach(Space space in spaceArray)
            {
                if (space.IsEmpty == false)
                {
                    //reset the universes spaces
                    ResetVisited();

                    space.Steps = -1;

                    //breadth first search
                    spaceQueue = new Queue<Space>();
                    spaceQueue.Enqueue(space);

                    while(spaceQueue.Count > 0)
                    {
                        Space tempSpace = spaceQueue.Dequeue();

                        WalkPath(space, tempSpace.Row, tempSpace.Col, tempSpace.Steps);

                    }
                }
            }

            //once all the galaxies have been linked find the lowest pairs to mak
            //FindLowestPairs();
        }

        private void WalkPath(Space Source, long row, long col, long steps)
        {
            if (spaceArray[row, col].Visited == false)
            {
                steps++;
                Space thisSpace = spaceArray[row, col];

                //mark visited
                thisSpace.Visited = true;

                //check for Galaxy
                if(thisSpace.IsEmpty == false
                    && Source != thisSpace)
                {
                    //add pair
                    AllGalaxyPairs.Add(new GalaxyPair(Source.GalaxyContents, thisSpace.GalaxyContents, steps));
                }

                //Walk North
                if(row > 0)
                {
                    Space nextSpace = spaceArray[row - 1, col];
  
                    nextSpace.Steps = steps;

                    //add an extra space if moving into a blank row
                    if (blankRows.IndexOf((int)row - 1) >= 0) nextSpace.Steps += ExpansionFactor;

                    spaceQueue.Enqueue(nextSpace);
                }
                //Walk South
                if (row < spaceArray.GetLength(0)-1)
                {
                    
                    Space nextSpace = spaceArray[row + 1, col];

                    nextSpace.Steps = steps;

                    //add an extra space if moving into a blank row
                    if (blankRows.IndexOf((int)row + 1) >= 0) nextSpace.Steps += ExpansionFactor;

                    spaceQueue.Enqueue(nextSpace);
                }

                //Walk East
                if (col > 0)
                {
                    Space nextSpace = spaceArray[row, col - 1];

                    nextSpace.Steps = steps;

                    //add an extra space if moving into a blank col
                    if (blankCols.IndexOf((int)col - 1) >= 0) nextSpace.Steps += ExpansionFactor;

                    spaceQueue.Enqueue(nextSpace);
                }

                //Walk West
                if (col < spaceArray.GetLength(1) - 1)
                {
                    Space nextSpace = spaceArray[row, col + 1];

                    nextSpace.Steps = steps;

                    //add an extra space if moving into a blank col
                    if (blankCols.IndexOf((int)col + 1) >= 0) nextSpace.Steps += ExpansionFactor;

                    spaceQueue.Enqueue(nextSpace);
                }
            }
        }

        //public void RemoveDuplicatePairs()
        //{
        //    List<Galaxy> processedGalaxies = new List<Galaxy>();

        //    for(long i = 0; i < AllGalaxyPairs.Count; i++)
        //    {
        //        GalaxyPair lowestPair = AllGalaxyPairs[i];
        //        Galaxy lowestA = lowestPair.GalaxyA;
        //        Galaxy lowestB = lowestPair.GalaxyB;
                
        //    }
        //}

        public long GetPairTotal()
        {
            long total = 0;

            foreach (GalaxyPair thispair in AllGalaxyPairs)
            {
                total += thispair.Distance;
            }

            return total/2;
        }

        public void ResetVisited()
        {
            foreach (Space space in spaceArray)
            {
                space.ResetVisited();
                space.Steps = 0;
            }
        }
    }
}
