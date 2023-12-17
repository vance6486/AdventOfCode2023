using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode10
{
    internal class PipeMap
    {
        Pipe[,] pipes;
        Queue<Pipe> connectionMapper = new Queue<Pipe>();
        bool connectionsMapped = false;
        public int enclosedCount = 0;

        public enum Direction
        {
            NORTH = 1,
            SOUTH = 2,
            EAST = 3,
            WEST = 4
        }

        public PipeMap(Pipe[,] pipes)
        {
            this.pipes = pipes;
            ConnectNodes();
        }

        public void ConnectNodes()
        {
            foreach (Pipe pipe in pipes)
            {
                //check for north
                if (pipe.row > 0) pipe.North = pipes[pipe.row - 1, pipe.col];
                //check for south
                if (pipe.row < pipes.GetLength(1) - 1) pipe.South = pipes[pipe.row + 1, pipe.col];
                //check for east
                if (pipe.col < pipes.GetLength(0) - 1) pipe.East = pipes[pipe.row, pipe.col + 1];
                //check for west
                if (pipe.col > 0) pipe.West = pipes[pipe.row, pipe.col - 1];
            }
        }

        public Pipe FindStartNode()
        {
            foreach(Pipe pipe in pipes)
            {
                if (pipe.PipeType == 'S') return pipe;
            }

            return null;
        }

        public void MapConnectionsToStart()
        {
            connectionsMapped = true;

            //create a queue of squares to map the connections from start
            connectionMapper = new Queue<Pipe>();

            Pipe start = FindStartNode();

            if(start!= null)
                connectionMapper.Enqueue(start);

            while(connectionMapper.Count > 0)
            {
                Pipe pipe = connectionMapper.Dequeue();
                if(pipe.IsConnectedNorth())
                    walkPath(pipe, pipe.North);
                if (pipe.IsConnectedSouth())
                    walkPath(pipe, pipe.South);
                if (pipe.IsConnectedEast())
                    walkPath(pipe, pipe.East);
                if (pipe.IsConnectedWest())
                    walkPath(pipe, pipe.West);
            }
        }

        public void walkPath(Pipe prevNode, Pipe currentNode)
        {
            if(currentNode.distToStart > prevNode.distToStart+1)
            {
                currentNode.distToStart = prevNode.distToStart + 1;
                currentNode.isConnectedToStart = true;
                currentNode.UpdateEnclosedSides(prevNode);
                connectionMapper.Enqueue(currentNode);
            }
                
        }

        public int GetHighestConnectionToStart()
        {
            int highestConnection = 0;

            if (connectionsMapped == false)
                MapConnectionsToStart();

            foreach (Pipe pipe in pipes)
            {
                if(pipe.isConnectedToStart
                    && pipe.distToStart > highestConnection)
                    highestConnection = pipe.distToStart;
            }

            return highestConnection;
        }

        public void PrintConnectionMap()
        {
            for(int row = 0; row < pipes.GetLength(0); row++)
            {
                for(int col = 0; col < pipes.GetLength(1); col++)
                {
                    if (pipes[row, col].isConnectedToStart) Console.Write(pipes[row, col].PipeType);
                    else Console.Write('.');
                }
                Console.WriteLine();
            }
        }

        public void CalculateEnclosed()
        {
            foreach(Pipe pipe in pipes)
            {
                if(pipe.CheckEnclosed())
                    enclosedCount++;
            }
        }
    }
}
