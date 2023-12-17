using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode10
{
    internal class Pipe
    {
        public Pipe North;
        public Pipe South;
        public Pipe East;
        public Pipe West;

        public bool isConnectedToStart { get; set; } = false;
        public bool isEnclosed { get; private set; } = false;

        public bool EnclosedNorth { get; private set; }
        public bool EnclosedSouth { get; private set; }
        public bool EnclosedEast { get; private set; }
        public bool EnclosedWest { get; private set; }


        public bool IsOpenNorth { get; private set; }
        public bool IsOpenSouth { get; private set; }
        public bool IsOpenEast { get; private set; }
        public bool IsOpenWest{ get; private set; }


        public int distToStart = int.MaxValue;
        public char PipeType;
        public int col;
        public int row;

        public Pipe() 
        {
            PipeType = '.';
            EnclosedNorth = false;
            EnclosedSouth = false;
            EnclosedEast = false;
            EnclosedWest = false;
        }

        public Pipe(char PipeType)
        {
            this.PipeType = PipeType;

            EnclosedNorth = false;
            EnclosedSouth = false;
            EnclosedEast = false;
            EnclosedWest = false;

            if (PipeType == 'S')
            {
                isConnectedToStart = true;
                distToStart = 0;
                EnclosedWest = true;

            }
            CheckIfIsOpenNorth();
            CheckIfIsOpenSouth();
            CheckIfIsOpenEast();
            CheckIfIsOpenWest();
        }

        public Pipe(char PipeType, int row, int col) : this(PipeType)
        {
            this.col = col;
            this.row = row;
        }

        public void CheckIfIsOpenNorth()
        {
            IsOpenNorth = PipeType == 'S' || PipeType == '|' || PipeType == 'L' || PipeType == 'J';
        }
        public void CheckIfIsOpenSouth()
        {
            IsOpenSouth = PipeType == 'S' || PipeType == '|' || PipeType == 'F' || PipeType == '7';
        }

        public void CheckIfIsOpenEast()
        {
            IsOpenEast = PipeType == 'S' || PipeType == '-' || PipeType == 'F' || PipeType == 'L';
        }

        public void CheckIfIsOpenWest()
        {
            IsOpenWest = PipeType == 'S' || PipeType == '-' || PipeType == 'J' || PipeType == '7';
        }

        public void UpdateEnclosedSides(Pipe prevNode)
        {
            //check to see which direction the connection is coming from
            if (prevNode == North)
            {
                //PipeType == '|' || PipeType == 'L' || PipeType == 'J';
                if(PipeType == '|')
                {
                    EnclosedEast = prevNode.EnclosedEast;
                    EnclosedWest = prevNode.EnclosedWest;
                }
                if (PipeType == 'L')
                {
                    EnclosedNorth = EnclosedEast = prevNode.EnclosedEast;
                    EnclosedSouth = EnclosedWest = prevNode.EnclosedWest;
                }
                if (PipeType == 'J')
                {
                    EnclosedNorth = EnclosedWest = prevNode.EnclosedWest;
                    EnclosedSouth = EnclosedEast = prevNode.EnclosedEast;
                    
                }

            }
            else if (prevNode == South)
            {
                //PipeType == '|' || PipeType == 'F' || PipeType == '7';
                if (PipeType == '|')
                {
                    EnclosedEast = prevNode.EnclosedEast;
                    EnclosedWest = prevNode.EnclosedWest;
                }
                if (PipeType == 'F')
                {
                    EnclosedNorth = EnclosedWest = prevNode.EnclosedWest;
                    EnclosedSouth = EnclosedEast = prevNode.EnclosedEast;
                }
                if (PipeType == '7')
                {
                    EnclosedNorth = EnclosedEast = prevNode.EnclosedEast;
                    EnclosedSouth = EnclosedWest = prevNode.EnclosedWest;
                }
            }
            else if (prevNode == East)
            {
                //PipeType == '-' || PipeType == 'F' || PipeType == 'L';
                if (PipeType == '-')
                {
                    EnclosedNorth = prevNode.EnclosedNorth;
                    EnclosedSouth = prevNode.EnclosedSouth;
                }
                if (PipeType == 'F')
                {
                    EnclosedNorth = EnclosedWest = prevNode.EnclosedNorth;
                    EnclosedSouth = EnclosedEast = prevNode.EnclosedSouth;
                }
                if (PipeType == 'L')
                {
                    EnclosedNorth = EnclosedEast = prevNode.EnclosedNorth;
                    EnclosedSouth = EnclosedWest = prevNode.EnclosedSouth;
                }
            }
            else if (prevNode == West)
            {
                //PipeType == '-' || PipeType == 'J' || PipeType == '7';
                if (PipeType == '-')
                {
                    EnclosedNorth = prevNode.EnclosedNorth;
                    EnclosedSouth = prevNode.EnclosedSouth;

                }
                if (PipeType == 'J')
                {
                    EnclosedNorth = EnclosedWest = prevNode.EnclosedNorth;
                    EnclosedSouth = EnclosedEast = prevNode.EnclosedSouth;
                }
                if (PipeType == '7')
                {
                    EnclosedNorth = EnclosedEast = prevNode.EnclosedNorth;
                    EnclosedSouth = EnclosedWest = prevNode.EnclosedSouth;
                }
            }
            
        }

        public bool IsConnectedNorth()
        {
            if(North == null) { return false; }
            return this.IsOpenNorth && North.IsOpenSouth;
        }

        public bool IsConnectedSouth()
        {
            if (North == null) { return false; }
            return this.IsOpenSouth && South.IsOpenNorth;
        }

        public bool IsConnectedEast()
        {
            if (North == null) { return false; }
            return this.IsOpenEast && East.IsOpenWest;
        }

        public bool IsConnectedWest()
        {
            if (North == null) { return false; }
            return this.IsOpenWest && West.IsOpenEast;
        }

        public bool CheckEnclosed()
        {
            
            if (isConnectedToStart)
                isEnclosed = false;

            else if (West == null)
                isEnclosed = false;
            
            else if(West.EnclosedEast || West.CheckEnclosed())            
                isEnclosed = true;
            else if (col > 20 && row > 20)
            {
                bool debug = true;
            }


            return isEnclosed;
            
        }
        public override string ToString()
        {
            return PipeType.ToString();
        }
    }
}
