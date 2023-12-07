/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package adventofcode6;

/**
 *
 * @author Micha
 */
public class Race {
    private long raceTime = 0;
    private long raceDistance = 0;
    private long marginOfError = 0;

    public Race()
    {
        
    }
    
    public Race(long raceTime, long distance) {
        this.raceTime = raceTime;
        this.raceDistance = distance;
        
        calcMarginOfError();
    }

    public long getRaceTime() {
        return raceTime;
    }

    public void setRaceTime(long raceTime) {
        this.raceTime = raceTime;
        if(raceDistance > 0)
            calcMarginOfError();
    }

    public long getRaceDistance() {
        return raceDistance;
    }

    public void setRaceDistance(long raceDistance) {
        this.raceDistance = raceDistance;
        if(raceTime > 0)
            calcMarginOfError();
    }

    public long getMarginOfError() {
        return marginOfError;
    }
  
    
    /**
     * Calculates how far the boat will travel when the button is held down for 
     * a given amount of time
     * @param buttonHeldTime the amount of time the button is pressed
     * @return the raceDistance traveled
     */
    public long calcDistanceTraveled(long buttonHeldTime)
    {
        //if the button isnt held or is held for all the time in the race the boat wont move
        if(buttonHeldTime <=0 || buttonHeldTime >= raceTime)
            return 0;

        long speed = buttonHeldTime;
        long remainingRaceTime = raceTime - buttonHeldTime;
        
        return speed * remainingRaceTime;
    }
    
    /**
     * Calculates the minimum time required to hold down the button to beat the record
     * @return the minimum time to hold down the button 
     */
    public long calcMinWinPressTime()
    {        
        for(long i = 0; i <= (raceTime/2); i++)
        {
            if(calcDistanceTraveled(i) > raceDistance)
                return i;
        }
        
        return -1;
    }
    
    /**
     * Calculates the maximum time required to hold down the button to beat the record
     * @return the maximum time to hold down the button 
     */
    public long calcMaxWinPressTime()
    {        
        for(long i = raceTime; i >= (raceTime/2); i--)
        {
            if(calcDistanceTraveled(i) > raceDistance)
                return i;
        }
        
        return -1;
    }
    
    /**
     * Calculates the margin of error for the race by finding the difference between
     * the min time holding down the button to win and the max time and saves it to 
     * the marginOfError property
     */
    private void calcMarginOfError()
    {      
        marginOfError = calcMaxWinPressTime() - calcMinWinPressTime() +1;
    }
}
