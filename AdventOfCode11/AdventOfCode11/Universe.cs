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

        private List<Galaxy> galaxyList = new List<Galaxy>();

        public List<GalaxyPair> AllGalaxyPairs = new List<GalaxyPair>();

        public Universe() { }
        
        public Universe(char[,] rawData)
        {
            RawData = rawData;
        }

        public void GenerateSpace()
        {
            spaceArray = new Space[RawData.GetLength(0), RawData.GetLength(1)];
            for(int row = 0; row < RawData.GetLength(0); row++)
            {
                for(int col = 0; col < RawData.GetLength(1); col++)
                {
                    spaceArray[row,col] = new Space(RawData[row,col], row, col);

                    if (spaceArray[row, col].IsEmpty == false)
                        galaxyList.Add(spaceArray[row, col].GalaxyContents);

                }
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
                    space.ResetVisited();

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
            FindLowestPairs();
        }

        private void WalkPath(Space Source, int row, int col, int steps)
        {
            if (spaceArray[row, col].Visited == false)
            {
                steps++;
                Space thisSpace = spaceArray[row, col];

                //mark visited
                thisSpace.Visited = true;

                //check for Galaxy
                if(thisSpace.IsEmpty == false)
                {
                    //add pair
                    AllGalaxyPairs.Add(new GalaxyPair(Source.GalaxyContents, thisSpace.GalaxyContents, steps));
                }

                //Walk North
                if(row > 0)
                {
                    Space nextSpace = spaceArray[row - 1, col];
                    nextSpace.Steps = steps;
                    spaceQueue.Enqueue(nextSpace);
                }
                //Walk South
                if (row < spaceArray.GetLength(0)-1)
                {
                    Space nextSpace = spaceArray[row + 1, col];
                    nextSpace.Steps = steps;
                    spaceQueue.Enqueue(nextSpace);
                }

                //Walk East
                if (col > 0)
                {
                    Space nextSpace = spaceArray[row, col - 1];
                    nextSpace.Steps = steps;
                    spaceQueue.Enqueue(nextSpace);
                }

                //Walk West
                if (col < spaceArray.GetLength(1) - 1)
                {
                    Space nextSpace = spaceArray[row, col + 1];
                    nextSpace.Steps = steps;
                }
            }
        }

        public void FindLowestPairs()
        {
            List<GalaxyPair> tempGalaxyPairs = new List<GalaxyPair>();
            AllGalaxyPairs.Sort();
            
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
