// See https://aka.ms/new-console-template for more information

using AdventOfCode11;

char[,] spaces;
String line;
try
{
    //Pass the file path and file name to the StreamReader constructor
    StreamReader sr = new StreamReader(@"..\..\..\MyData.txt");
    //Read the first line of text
    line = sr.ReadLine();

    spaces = new char[line.Length, line.Length];

    long row = 0;
    //Continue to read until you reach end of file
    while (line != null)
    {
        //write the line to console window
        Console.WriteLine(line);
        for (long col = 0; col < line.Length; col++)
        {

            spaces[row, col] = (char)line.ElementAt((int)col);
        }

        //Read the next line
        line = sr.ReadLine();
        row++;
    }
    //close the file
    sr.Close();

    Universe universe = new Universe(spaces);

    universe.GenerateSpace();

    Console.WriteLine(universe.GetPairTotal());

    //pipeMap.PrintConnectionMap();
    
}
catch (Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}
