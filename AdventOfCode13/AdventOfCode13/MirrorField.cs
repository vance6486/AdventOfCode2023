using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode13
{
    internal class MirrorField
    {
        char[,] field = new char[1,1];

        int mirroredRow = -1;
        int mirroredCol = -1;

        public int Id = -1;

        public int FieldScore { get; private set; } = 0;

        public MirrorField(char[,] field): this (field, 999)
        {
            
        }

        public MirrorField(char[,] field, int id)
        {
            Id = id;
            this.field = field;
            FindMirrorCol();
            FindMirrorRow();

            FieldScore = 0;
            if (mirroredRow > -1)
                FieldScore += mirroredRow * 100;

            if (mirroredCol > -1)
                FieldScore += mirroredCol;
        }

        public bool DoRowsMatch(int row1, int row2)
        {
            bool allColsMatch = true;

            for(int col = 0; col < field.GetLength(1); col++)
            {
                if (field[row1, col] != field[row2, col]) allColsMatch = false;
            }

            return allColsMatch;

        }

        public bool DoColsMatch(int col1, int col2)
        {
            bool allRowsMatch = true;

            for (int row = 0; row < field.GetLength(0); row++)
            {
                if (field[row, col1] != field[row, col2]) allRowsMatch = false;
            }

            return allRowsMatch;

        }

        public void FindMirrorRow()
        {
            if(Id == 2)
            {
                bool debug = true;
            }
            //search through all rows
            for(int row = 0;row < field.GetLength(0); row++)
            {
                //if there is a next row and that row matches
                if (row + 1 < field.GetLength(0)
                    && DoRowsMatch(row, row+1))
                {
                    if(row == 0) mirroredRow = row + 1;
                    //check all the rows back to the first row to see if they all match as well
                    for (int rowOffset = 1; rowOffset<=row+1; rowOffset++)
                    {
                        //if we get to the beginning or end with all of the items matching then thats our match
                        if (row - rowOffset < 0 || row + (rowOffset + 1) >= field.GetLength(0))
                        {
                            mirroredRow = row+1;
                            break;
                        }
                        if (DoRowsMatch(row-rowOffset, row+1 +rowOffset) == false)
                            break;      
                    }
                }
            }
        }

        public void FindMirrorCol()
        {
            //search through all col
            for (int col = 0; col < field.GetLength(1); col++)
            {
                //if there is a next col and that col matches
                if (col + 1 < field.GetLength(1)
                    && DoColsMatch(col, col + 1))
                {
                    if (col == 0) mirroredCol = col + 1;
                    //check all the rows back to the first row to see if they all match as well
                    for (int colOffset = 1; colOffset <= col + 1; colOffset++)
                    {
                        
                        //if we get to the beginning or end with all of the items matching then thats our match
                        if (col - colOffset < 0 || col + (colOffset + 1) >= field.GetLength(1))
                        {
                            mirroredCol = col+1;
                            break;
                        }
                        if (DoColsMatch(col - colOffset, col + 1 + colOffset) == false)
                            break;
                    }
                }
            }
        }

        public override string ToString()
        {
            if(mirroredRow == -1 && mirroredCol == -1)
                return $"{Id}: NULL";
            else
                return $"{Id}: Row {mirroredRow,2} | Col {mirroredCol,2}";
        }
    }
}
