using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode9
{
    internal class Sequence
    {
        private List<long> currentSequence = new List<long>();
        private Sequence subSequence = null;
        private bool baseSequence = false;

        public Sequence() { }

        public Sequence(List<long> baseSequence) 
        {
            this.currentSequence = baseSequence;
        }

        public void Add(long item)
        {
            currentSequence.Add(item);
        }

        public int Length()
        {
            return currentSequence.Count;
        }

        public bool isBaseSequence()
        {
            if (baseSequence == true) return true;
            
            bool isBaseSequence = true;

            // if the current sequence is all 0s it is the base sequence
            foreach (long item in currentSequence)
                isBaseSequence &= item == 0;

            baseSequence = isBaseSequence;
            return isBaseSequence;
        }

        //recursive call which populates the subsequence of the current sequence, then gets that sequences subsequence
        //until the lowest level sequence when they are all the same
        public void calculateBaseSequence()
        {
            int previousItem = 0;
            subSequence = new Sequence();
            int i = 0;
            foreach (int currentItem in currentSequence)
            {
                if(i != 0)
                {
                    subSequence.Add(currentItem - previousItem);
                }

                i++;
                previousItem = currentItem;
            }

            if (subSequence.isBaseSequence()) 
                return;
            else
                subSequence.calculateBaseSequence(); // once we have calculated a sequence, calculate all its subsequences till we get to the base
        }

        public long getLastNumber() {  return  currentSequence.Last(); }

        public long getNextNumber()
        {
            if (isBaseSequence()) return 0;

            return currentSequence[currentSequence.Count - 1] + subSequence.getNextNumber();
        }

        public long getNewFirstNumber()
        {
            if (isBaseSequence()) return 0;

            return currentSequence[0] - subSequence.getNewFirstNumber();
        }

    }
}
