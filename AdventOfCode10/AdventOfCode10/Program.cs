namespace AdventOfCode10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pipe[,] pipes;

            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(@"..\..\..\MyData.txt");
                //Read the first line of text
                line = sr.ReadLine();

                pipes = new Pipe[line.Length, line.Length];

                int row = 0;
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);
                    for(int col = 0; col<line.Length; col++)
                    {
                        
                        pipes[row, col] = new Pipe((char)line.ElementAt(col), row, col);
                    }

                    //Read the next line
                    line = sr.ReadLine();
                    row++;
                }
                //close the file
                sr.Close();

                PipeMap pipeMap = new PipeMap(pipes);

                pipeMap.MapConnectionsToStart();

                Console.WriteLine(pipeMap.GetHighestConnectionToStart());

                //pipeMap.PrintConnectionMap();
                pipeMap.CalculateEnclosed();
                Console.WriteLine(pipeMap.enclosedCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }
    }
}