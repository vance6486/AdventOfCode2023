/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package adventofcode2;
import java.io.File;
import java.io.FileNotFoundException;
import java.util.*;
/**
 *
 * @author Creon
 */
public class AdventOfCode2 {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        //parse a file
        String line = "";
        int gameNumber = 0;
        int sumOfValid = 0;
        int sumOfPowers = 0;
        
        try {
            File myFile = new File("C:\\Users\\Creon\\Desktop\\MyData.txt");
            Scanner input = new Scanner(myFile);
            
             //Create code that scans each line of a text document
            while (input.hasNextLine()) {
                line = input.nextLine();
                
                //on each line, get the game number
                gameNumber = Integer.parseInt(line.substring(line.indexOf(" ")+1, line.indexOf(":")));
                
                //get all the games
                String allGames = line.substring(line.indexOf(":")+2);
                
                //split the string into an array
                String[] gameArray = allGames.split("(,|;)");
                
                //create a list of all the cube pulls in the game
                List<CubePull> allPulls = new ArrayList<CubePull>();
               
                //var for determining if the game was valid
                boolean isInvalidGame = false;
                
                //loop through each game (seperated by semicolon)
                for(String game : gameArray)
                {
                    //filter out strings that arent long enough 
                    if(game.length() > 4)
                    {
                        CubePull singlePull = parseCubePull(game);
                        allPulls.add(singlePull);
                        
                        isInvalidGame |= isInvalidCubePull(singlePull);
                    }
                }

                sumOfPowers += getGamePower(allPulls);
                
                //if the game is not invalid, add the game number to the total
                if(isInvalidGame == false)
                    sumOfValid += gameNumber;
                
                
            }
            input.close();
        } catch (FileNotFoundException e) {
            System.out.println("An error occurred.");
            e.printStackTrace();
        }

        System.out.println("The sum of all valid games is " + sumOfValid);
        System.out.println("The power of all games is "+ sumOfPowers);
    }
    
    // parses a string into a CubePull object
    public static CubePull parseCubePull(String stringToParse)
    {        
        CubePull result = new CubePull();
        
        int numberOfCubes = 0;
        String colorOfCubes = "";
        
        stringToParse = stringToParse.trim();
        
        //parse out the number of cubes
        numberOfCubes = Integer.parseInt(stringToParse.substring(0, stringToParse.indexOf(" ")));
        
        //parse out the color of those cubes
        colorOfCubes = stringToParse.substring(stringToParse.indexOf(" ") + 1);
        
        System.out.println("Number: " + numberOfCubes);
        System.out.println("Color: " + colorOfCubes);
        
        result.number = numberOfCubes;
        result.color = colorOfCubes;
        
        return result;
    }
    
    //returns true of the string passed in contains an invalid color number combination 
    public static boolean isInvalidCubePull(CubePull thisPull)
    {     
        
        boolean result = false;
        
        //if that number is higher than the allowable number for that game the game is invalid
        if(thisPull.color.equals("red") && thisPull.number > 12){ result = true; }
        if(thisPull.color.equals("green") && thisPull.number > 13){ result = true; }
        if(thisPull.color.equals("blue") && thisPull.number > 14){ result = true; }
        
        return result;
    }
    
    //gets the power of a game by multiplying the lowest number of red, blue, and green cubes possible
    public static int getGamePower(List<CubePull> allPulls)
    {
        int result = 0;
        int minRed = 0;
        int minGreen = 0;
        int minBlue = 0;
        
        //loop through all pulls in the game
        for(CubePull singlePull : allPulls)
        {
            //check the color and see if the number was needed higher than the previous current max
            if(singlePull.color.equals("red")){ minRed = Math.max(minRed, singlePull.number); }
            if(singlePull.color.equals("green")){ minGreen = Math.max(minGreen, singlePull.number); }
            if(singlePull.color.equals("blue")){ minBlue = Math.max(minBlue, singlePull.number); }
        }
        
        //multiply lowest numbers to get the games power
        result = minRed * minGreen * minBlue;
        
        return result;
    }
}
