using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode4
{
    internal class ScratchCard
    {
        private string cardNo;
        private int[] winningNumbers;
        private int[] gameNumbers;
        private int cardAmount;

        private int numMatchingNum = 0;

        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }

        public int[] WinningNumbers
        {
            get { return winningNumbers; }
            set { winningNumbers = value; }
        }

        public int[] GameNumbers
        {
            get { return gameNumbers; }
            set { gameNumbers = value; }
        }

        public int CardAmount
        {
            get { return cardAmount; }
            set { cardAmount = value; }
        }

        public ScratchCard()
        {
            cardAmount = 1;
        }
        
        /**
         * Parametered Constructor
         */
        public ScratchCard(string cardNo, int[] winningNumbers, int[] gameNumbers)
        {
            this.cardNo = cardNo;
            this.winningNumbers = winningNumbers;
            this.gameNumbers = gameNumbers;
            cardAmount = 1;
        }

        public int getNumMatchingNumbers()
        {
            if(numMatchingNum == 0)
            {
                //loop through to see if any of the game numbers match the winning numbers
                foreach (int number in gameNumbers)
                {
                    foreach (int winningNumber in winningNumbers)
                    {
                        if (number == winningNumber)
                            numMatchingNum++;
                    }
                }
            }
            return numMatchingNum;
        }

        public int getCardScore()
        {
            int result = 0;
            int numMatching = getNumMatchingNumbers();
            result = (int)Math.Pow(2, (numMatching - 1));
            return result;
        }
    }
}
