/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package adventofcode1;

import java.util.*;

import java.io.File;  // Import the File class
import java.io.FileNotFoundException;  // Import this class to handle errors

/**
 *
 * @author Creon
 */
public class AdventOfCode1 {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        String line = "";
        int linesNumber = 0;
        int sum = 0;
        
        try {
            File myFile = new File("C:\\Users\\Creon\\Desktop\\MyData.txt");
            Scanner input = new Scanner(myFile);
            
             //Create code that scans each line of a text document
            while (input.hasNextLine()) {
                line = input.nextLine();
                // on each line, return the first and last number as a 2 digit number
                linesNumber = decypherNumber(line);
                // add the sum of all the 2 digit numbers together
                
                sum += linesNumber;
                
                System.out.println(linesNumber);
            }
            input.close();
        } catch (FileNotFoundException e) {
            System.out.println("An error occurred.");
            e.printStackTrace();
        }

        System.out.println("The number is: " + sum);
       
       
    }

    public static int decypherNumber(String line) {
        int result = 0;
        int firstNum = 0;
        int lastNum = 0;
        List<Integer> numbers = new ArrayList<Integer>();

        //loop through each element of the list as a char
        for (int i = 0; i < line.length(); i++) {
            //run is digit
            char currentChar = line.charAt(i);
            if (Character.isDigit(currentChar)) {
                //save all the digits in a list
                numbers.add(Integer.parseInt(String.valueOf(currentChar)));
            }
            //else if otsfne
            else if(currentChar == 'o'
                    || currentChar == 't'
                    || currentChar == 's'
                    || currentChar == 'f'
                    || currentChar == 'n'
                    || currentChar == 'e')
            {
                int parsedNum = parseNumberText(line.substring(i));
                //save all the digits in a list
                if(parsedNum != -1){numbers.add(parsedNum);}
            }
            
        }
        
        //get the first and last numbers from the list
        firstNum = numbers.get(0);
        lastNum = numbers.get(numbers.size() - 1);

        //combine the first and last numbers 
        result = firstNum * 10 + lastNum;

        return result;
    }
    
    //determines if the section passed in contains a number as text
    public static int parseNumberText(String section)
    {
        int result = -1;
        String threeLetters = "";
        String fourLetters = "";
        String fiveLetters = "";
        
        //only parse if the string is long enough
        if(section.length()>= 3){ threeLetters = section.substring(0,3); }
        if(section.length()>= 4){ fourLetters = section.substring(0,4); }
        if(section.length()>= 5){ fiveLetters = section.substring(0,5); }
        
        //check the first 3 letters to see if it spells one, two, or six
        if(threeLetters.equals("one")){ result = 1;}
        if(threeLetters.equals("two")){ result = 2;}
        if(threeLetters.equals("six")){ result = 6;}
        
        //check the first 4 letters to see if it spells four, five, nine
        if(fourLetters.equals("four")){ result = 4;}
        if(fourLetters.equals("five")){ result = 5;}
        if(fourLetters.equals("nine")){ result = 9;}
        
        //check the first 5 letters to see if it spells three, seven, or eight
        if(fiveLetters.equals("three")){ result = 3;}
        if(fiveLetters.equals("seven")){ result = 7;}
        if(fiveLetters.equals("eight")){ result = 8;}
        
        return result;
    }
}
