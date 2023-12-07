/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package adventofcode7;
import java.util.*;
/**
 *
 * @author Micha
 */
public class Hand implements Comparable{

    private String cards;
    private int wager;
    private int handRank; // used to determine what their hand was
    private int handValue; // the value of the parsed hex code of the hand
    
    public static final int
        HighCard = 1,
        OnePair = 2,
        TwoPair = 3,
        ThreeOAK = 4,
        FullHouse = 5,
        FourOAK = 6,
        FiveOAK = 7;
    

    public Hand(String cards, int wager) {
        this.cards = cards;
        this.wager = wager;
        
        calcHandRank();
        calcHandValue();
    }

    public String getCards() {
        return cards;
    }

    public int getWager() {
        return wager;
    }

    public int getHandRank() {
        return handRank;
    }

    public int getHandValue() {
        return handValue;
    }

    public void setCards(String cards) {
        this.cards = cards;
        calcHandRank();
        calcHandValue();
    }

    public void setWager(int wager) {
        this.wager = wager;
    }
    
    public void calcHandRank()
    {
        boolean hasPair = false;
        boolean hasTrip = false;
        List<Character> prevCheckedCards = new ArrayList<>();
        
        int jokerCount = (int)cards.chars().filter(ch -> ch == 'J').count();
        String cardsNoJ = cards.replace("J", "");
        
        handRank = calcHandRankNoJ(cardsNoJ);
        
        switch(jokerCount)
        {
            //5 jokers
            case 5->
            {
                handRank = FiveOAK;
            }
            //4 jokers
            case 4->
            {
                handRank = FiveOAK;
            }
            //3 jokers
            case 3->
            {
                //hand without jokers
                switch(handRank)
                {
                    default ->
                    {
                        handRank = FourOAK;
                    }
                    case OnePair->
                    {
                        handRank = FiveOAK;
                    }
                }                    
            }
            //2 jokers
            case 2->
            {
                //hand without jokers
                switch(handRank)
                {
                    default ->
                    {
                        handRank = ThreeOAK;
                    }
                    case OnePair->
                    {
                        handRank = FourOAK;
                    }
                    case ThreeOAK->
                    {
                        handRank = FiveOAK;
                    }
                    
                }
            }
            //1 jokers
            case 1->
            {
                //hand without jokers
                switch(handRank)
                {
                    default ->
                    {
                        handRank = OnePair;
                    }
                    case OnePair->
                    {
                        handRank = ThreeOAK;
                    }
                    case TwoPair->
                    {
                        handRank = FullHouse;
                    }
                    case ThreeOAK->
                    {
                        handRank = FourOAK;
                    }
                    case FourOAK->
                    {
                        handRank = FiveOAK;
                    }  
                }
            }
        }
    }
    
    public int calcHandRankNoJ(String cards)
    {
        boolean hasPair = false;
        boolean hasTrip = false;
        List<Character> prevCheckedCards = new ArrayList<>();
        
        //defalt hand checking
        for(char card : cards.toCharArray())
        {
            //dont check the same card twice
            if(prevCheckedCards.indexOf(card) < 0)
            {
                prevCheckedCards.add(card);

                int cardCount = (int)cards.chars().filter(ch -> ch == card).count();

                switch(cardCount)
                {
                    case 5 -> 
                    {
                        return FiveOAK;
                        
                    }
                    case 4 -> 
                    {
                        return FourOAK;
                        
                    }
                    case 3 -> 
                    {
                        // had pair, now has trip
                        if(hasPair == true)
                        {
                            return FullHouse;
                        }
                        else
                        {
                            hasTrip = true;
                        }
                    }
                    case 2 -> 
                    {
                        // had trip, now has pair
                        if(hasTrip == true)
                        {
                            return FullHouse;
                        }
                        else if(hasPair == true)
                        {
                            return  TwoPair;

                        }
                        else
                        {
                            hasPair = true;
                        }
                    }
                }
            }
        }

        if(hasTrip) return  ThreeOAK;
        else if(hasPair) return  OnePair;
        else return HighCard; 
    }
    
    public void calcHandValue()
    {
        String hexString = cards;
        
        hexString = hexString.replace("T", "a");
        hexString = hexString.replace("J", "1");
        hexString = hexString.replace("Q", "c");
        hexString = hexString.replace("K", "d");
        hexString = hexString.replace("A", "e");
        
        hexString = "0x"+hexString;
        
        handValue = Integer.decode(hexString);
    }
    
    @Override
    public int compareTo(Object o)
    {
        if( o instanceof Hand == false)
            return -1;
        
        int result;
        
        Hand otherHand = (Hand)o;
        
        //if the hand ranks are not the same, use that to evaluate
        if(handRank < otherHand.getHandRank()) result = -1;
        else if(handRank > otherHand.getHandRank()) result = 1;
        //hand has the same rank
        else
        {
            if(handValue < otherHand.handValue) result = -1;
            else if(handValue > otherHand.handValue) result = 1;
            else result = 0;
        }
        
        return result;
    }
}
