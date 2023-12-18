namespace AdventOfCode13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<MirrorField> mirrorFields = new List<MirrorField>();
            bool hasCurrentDataSet = false;
            char[,] mirrorFieldArray;
            List<string> dataSet = new List<string>();
            int id = 0;


            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(@"..\..\..\MyData.txt");
                //Read the first line of text
                line = sr.ReadLine();

                
                long row = 0;
                
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    
                    
                    if(line.Length > 1)
                    {
                        //new data set
                        if(hasCurrentDataSet == false) hasCurrentDataSet = true;

                        dataSet.Add(line);

                    }
                    else if(hasCurrentDataSet == true)
                    {
                        
                        //parse mirror field and add it to the list
                        mirrorFieldArray = new char[dataSet.Count, dataSet[0].Length];

                        for(int mfRow = 0; mfRow < dataSet.Count; mfRow++)
                        {
                            
                            for(int mfCol = 0; mfCol < dataSet[0].Length; mfCol++)
                            {
                                string currentLine = dataSet[mfRow];
                                mirrorFieldArray[mfRow, mfCol] = currentLine[mfCol];
                            }
                        }

                        mirrorFields.Add(new MirrorField(mirrorFieldArray, id));
                        id++;
                        Console.Write(id);
                        //reset data collector
                        dataSet = new List<string>();
                        hasCurrentDataSet = false;
                    }


                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                    row++;
                }

                if (hasCurrentDataSet == true)
                {
                    //parse mirror field and add it to the list
                    mirrorFieldArray = new char[dataSet.Count, dataSet[0].Length];

                    for (int mfRow = 0; mfRow < dataSet.Count; mfRow++)
                    {

                        for (int mfCol = 0; mfCol < dataSet[0].Length; mfCol++)
                        {
                            mirrorFieldArray[mfRow, mfCol] = dataSet[mfRow][mfCol];
                        }
                    }

                    mirrorFields.Add(new MirrorField(mirrorFieldArray, id));

                    //reset data collector
                    dataSet = new List<string>();
                    hasCurrentDataSet = false;
                }

                //close the file
                sr.Close();

                int mirrorFieldScores = 0;

                foreach(MirrorField mirrorField in mirrorFields)
                {
                    mirrorFieldScores += mirrorField.FieldScore;
                }

                Console.WriteLine(mirrorFieldScores);

                //pipeMap.PrintConnectionMap();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
