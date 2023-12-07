/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package adventofcode6;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.*;

/**
 *
 * @author Micha
 */
public class AdventOfCode6 {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
        Race[] races = {new Race(),new Race(),new Race(),new Race()};
        
        
        try {
            File myObj = new File("MyData.txt");

            Scanner myReader = new Scanner(myObj);
            
            while (myReader.hasNextLine()) {
                String data = myReader.nextLine();
                System.out.println(data);
                
                // parse and save all the race times 
                if(data.contains("Time:"))
                {
                    data = CleanString(data);
                    String[] raceTimeStrings = data.split(" ");
                    
                    //convert string array to int array using stream
                    long[] raceTimes = Arrays.stream(raceTimeStrings)
                            .mapToLong(Long::parseLong).toArray();
                    
                    for(int i = 0; i < raceTimes.length; i++)
                    {
                        races[i].setRaceTime(raceTimes[i]);
                    }
                }
                
                // parse and save all the race distances 
                if(data.contains("Distance:"))
                {
                    data = data = CleanString(data);
                    String[] raceDistanceStrings = data.split(" ");
                    
                    //convert string array to int array using stream
                    long[] raceDistances = Arrays.stream(raceDistanceStrings)
                            .mapToLong(Long::parseLong).toArray();
                    
                    for(int i = 0; i < raceDistances.length; i++)
                    {
                        races[i].setRaceDistance(raceDistances[i]);
                    }
                }

            }
            myReader.close();
        } catch (FileNotFoundException e) {
            System.out.println("An error occurred.");
            System.out.println(System.getProperty("user.dir"));

            e.printStackTrace();
        }
        
        
        int sumMarginOfErrors = 1;
        for(Race r : races)
        {
            if(r.getMarginOfError() > 0)
                sumMarginOfErrors *= r.getMarginOfError();
        }
        
        System.out.println("The sum of the margin of errors is " + sumMarginOfErrors);
    }
    
    public static String CleanString(String str)
    {
        str = str.trim();
        str = str.replaceAll("[^0-9]", " "); // replace data that isnt a number with a space
        str = str.replaceAll(" +", " "); // replace all consecutive white space with a single space
        str = str.trim();
        return str;
    }
}
