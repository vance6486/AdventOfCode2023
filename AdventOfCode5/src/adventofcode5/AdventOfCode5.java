/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package adventofcode5;

import java.io.File;  // Import the File class
import java.io.FileNotFoundException;  // Import this class to handle errors
import java.util.*;

/**
 *
 * @author Micha
 */
public class AdventOfCode5 {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        List<Seed> seeds = new ArrayList<Seed>();
        
        ConversionMap seed2soil = new ConversionMap();
        ConversionMap soil2fert = new ConversionMap();
        ConversionMap fert2water = new ConversionMap();
        ConversionMap water2light = new ConversionMap();
        ConversionMap light2temp = new ConversionMap();
        ConversionMap temp2humid = new ConversionMap();
        ConversionMap humid2loc = new ConversionMap();
        
        try {
            File myObj = new File("MyData.txt");

            Scanner myReader = new Scanner(myObj);
            
            int section = 0;

            while (myReader.hasNextLine()) {
                String data = myReader.nextLine();
                System.out.println(data);
               
                
                //check to see what section of the data we are in
                if(data.contains("seeds:"))                         { section = 1; }                     
                if(data.contains("seed-to-soil map:"))              { section = 2; }
                if(data.contains("soil-to-fertilizer map:"))        { section = 3; }
                if(data.contains("fertilizer-to-water map:"))       { section = 4; } 
                if(data.contains("water-to-light map:"))            { section = 5; }
                if(data.contains("light-to-temperature map:"))      { section = 6; }
                if(data.contains("temperature-to-humidity map:"))   { section = 7; }
                if(data.contains("humidity-to-location map:"))      { section = 8; }
                
                //read the first line to get our seeds
                if(section == 1)
                {
                    String parsedData = data.substring(data.indexOf(": ")+2);
                    
                    String[] numbersFromString = parsedData.split(" ");
                    
                    for(int i = 0; i < numbersFromString.length; i += 2)
                    {
                        String str = numbersFromString[i];
                        long start = Long.parseLong(str);
                        
                        str = numbersFromString[i+1];
                        long range = Long.parseLong(str);
                        
                        seeds.add(new Seed(start, range));
                    }
                    
                    //blank out the section so we dont try to add more seeds
                    section = 0;
                }
                
                //check to see if the data is a number line
                if(data.length() > 0
                    && Character.isDigit(data.charAt(0)))
                {
                    //parse out numbers from the string
                    String[] numbersFromString = data.split(" ");
                    long dest = Long.parseLong(numbersFromString[0]);
                    long source = Long.parseLong(numbersFromString[1]);
                    long range = Long.parseLong(numbersFromString[2]);
                    
                    
                    //add them to the current sections conversion map
                    switch(section)
                    {
                        case 2:
                            seed2soil.add(new Conversion(dest,source, range));
                            break;
                        case 3:
                            soil2fert.add(new Conversion(dest,source, range));
                            break;
                        case 4:
                            fert2water.add(new Conversion(dest,source, range));
                            break;
                        case 5:
                            water2light.add(new Conversion(dest,source, range));
                            break;
                        case 6:
                            light2temp.add(new Conversion(dest,source, range));
                            break;
                        case 7:
                            temp2humid.add(new Conversion(dest,source, range));
                            break;
                        case 8:
                            humid2loc.add(new Conversion(dest,source, range));
                            break;
                    }
                }
            }
            myReader.close();
        } catch (FileNotFoundException e) {
            System.out.println("An error occurred.");
            System.out.println(System.getProperty("user.dir"));

            e.printStackTrace();
        }
        
        long lowestLoc = Long.MAX_VALUE;
        //now that we have all of the data in the application, translate the seeds to their locations and find the lowest one
//        for(Seed seed : seeds)
//        {
//            for(long i = 0; i < seed.seedRange; i++)
//            {
//                seed.soilNo = seed2soil.convertSourceToDest(seed.seedNo);
//                seed.fertNo = soil2fert.convertSourceToDest(seed.soilNo);
//                seed.waterNo = fert2water.convertSourceToDest(seed.fertNo);
//                seed.lightNo = water2light.convertSourceToDest(seed.waterNo);
//                seed.tempNo = light2temp.convertSourceToDest(seed.lightNo);
//                seed.humidNo = temp2humid.convertSourceToDest(seed.tempNo);
//                seed.locNo = humid2loc.convertSourceToDest(seed.humidNo);
//
//                if(seed.locNo < lowestLoc) lowestLoc = seed.locNo;
//            }
//            
//        }
        
        boolean lowestFound = false;
        long locNo = 0000000;
        
        //count up to find the lowest location that matches our seed
        while(lowestFound == false)
        {

            long humidNo = humid2loc.convertDestToSource(locNo);
            long tempNo = temp2humid.convertDestToSource(humidNo);
            long lightNo = light2temp.convertDestToSource(tempNo);
            long waterNo = water2light.convertDestToSource(lightNo);
            long fertNo = fert2water.convertDestToSource(waterNo);
            long soilNo = soil2fert.convertDestToSource(fertNo);
            long seedNo = seed2soil.convertDestToSource(soilNo);
            
            for(Seed seed : seeds)
            {
                if(seed.isInSeedRange(seedNo))
                {
                    lowestFound = true;

                    System.out.println("The lowest location is "+locNo);
                }
            }
            locNo++;
            
            if(locNo % 1000000 == 0) System.out.println("LocNo: "+locNo);
        }
        
    }

}
