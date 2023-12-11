/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package adventofcode8;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

/**
 *
 * @author Creon
 */
public class AdventOfCode8 {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        Map map = new Map();
        try {
            File myObj = new File("MyData.txt");

            Scanner myReader = new Scanner(myObj);
            
            int lineCount = 0;
            while (myReader.hasNextLine()) {
                lineCount++;
                String data = myReader.nextLine();
                System.out.println(data);
                
                //get the steps
                if(lineCount == 1)
                {
                    map.pattern = data;
                }
                else if(lineCount > 2)
                {
                    String nodeName = data.substring(0,data.indexOf(" =")).trim();
                    String leftName = data.substring(data.indexOf("(")+1,data.indexOf(", ")).trim();
                    String rightName = data.substring(data.indexOf(", ")+2,data.indexOf(")")).trim();
                    
                    map.add(new MapNode(nodeName, leftName, rightName));
                }
            }
            myReader.close();
        } catch (FileNotFoundException e) {
            System.out.println("An error occurred.");
            System.out.println(System.getProperty("user.dir"));

            e.printStackTrace();
        }
        
        Scanner input = new Scanner(System.in);
        String startNode = "AAA";
        String endNode = "ZZZ";
        int stepCount = -1;
              
        stepCount = map.findStepCount(startNode, endNode);
        
        System.out.println("The number of steps is "+ stepCount);
        
        System.out.println("Number of steps to get all A nodes to Z nodes is " + map.aNodesToZNodes());
    }
    
}
