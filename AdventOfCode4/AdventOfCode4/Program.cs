using System.Numerics;

namespace AdventOfCode4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ScratchCard> cards = new List<ScratchCard>();
            int lineCount = 0;
            //Parse Text File
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(@"..\..\..\MyData.txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    

                    //Identify Card Name
                    string cardName = line.Substring(0, line.IndexOf(":"));
                    cardName.Trim();

                    //Identify all winning numbers (10)
                    List<int> winningNumbersList = new List<int>();

                    string winningNumbersString = line.Substring(line.IndexOf(":")+1, (line.IndexOf("|") - line.IndexOf(":") + 1));
                    string[] WNStrArray = winningNumbersString.Split(" ");
                    foreach (string str in WNStrArray)
                    {
                        if(int.TryParse(str, out int number))
                        {
                            winningNumbersList.Add(number);
                        }
                    }


                    //Identify all game numbers(25)
                    List<int> gameNumbersList = new List<int>();

                    string gameNumbersString = line.Substring(line.IndexOf("|"));
                    string[] GNStrArray = gameNumbersString.Split(" ");
                    foreach (string str in GNStrArray)
                    {
                        if (int.TryParse(str, out int number))
                        {
                            gameNumbersList.Add(number);
                        }
                    }

                    //load all these into a list of ScratchCards
                    cards.Add(new ScratchCard(cardName, winningNumbersList.ToArray(), gameNumbersList.ToArray()));

                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

            //loop through the list of scratch cards and sum of their points
            int totalPoints;
            totalPoints = 0;
            int totalCards = 0;
            for(int i = 0; i < cards.Count; i++)
            {
                ScratchCard card = cards[i];
                for(int j = 0; j < card.CardAmount;  j++)
                {
                    //print out total points
                    totalPoints += card.getCardScore();

                    //calculate copies of subsequent cards
                    int numOfMatchingNumbers = card.getNumMatchingNumbers();
                    for (int k = 1; k <= numOfMatchingNumbers; k++)
                    {
                        if(i+k < cards.Count)
                        {
                            cards[i + k].CardAmount++;
                        }
                        
                    }
                    totalCards ++;
                }
            }

            Console.WriteLine("The total number of points is " + totalPoints);
            Console.WriteLine("The total number of cards is " + totalCards);
        }
    }
}