/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package adventofcode7;
import java.io.File;
import java.io.FileNotFoundException;
import java.util.*;
/**
 *
 * @author Micha
 */
public class AdventOfCode7 {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        List<Hand> hands = new ArrayList<>();
        try {
            File myObj = new File("MyData.txt");

            Scanner myReader = new Scanner(myObj);
            
            while (myReader.hasNextLine()) {
                String data = myReader.nextLine();
                System.out.println(data);
                
                String[] dataStrArray = data.split(" ");
                hands.add(new Hand(dataStrArray[0], Integer.parseInt(dataStrArray[1])));
                
            }
            myReader.close();
            
        } catch (FileNotFoundException e) {
            System.out.println("An error occurred.");
            System.out.println(System.getProperty("user.dir"));

            e.printStackTrace();
        }
        
        Collections.sort(hands);
        int rank = 1;
        int pot = 0;
        for(Hand hand : hands)
        {
            pot += hand.getWager() * rank;
            rank ++;
        }
        
        System.out.println("The value of the pot is " + pot);
    }
    
}
