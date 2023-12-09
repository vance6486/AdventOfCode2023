/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package adventofcode8;

/**
 *
 * @author Creon
 */
public class MapNode {
    public String name;
    public char nodeType;
    public MapNode left;
    public MapNode right;
    
    public MapNode()
    {
        
    }
    
    public MapNode(String name)
    {
        this.name = name;
        nodeType = name.charAt(2);
    }

    public MapNode(String name, MapNode left, MapNode right) {
        this.name = name;
        this.left = left;
        this.right = right;
        nodeType = name.charAt(2);
    }
    
    public MapNode(String name, String left, String right) {
        this.name = name;
        this.left = new MapNode(left);
        this.right = new MapNode(right);
        nodeType = name.charAt(2);
    }
    

    @Override
    public boolean equals(Object o)
    {
        boolean result = false;
        
        if(o instanceof MapNode mapNode)
            result = name.equals(mapNode.name);
        
        if(o instanceof String s)
            result = name.equals(s);
        
        return result;
    }
    
//    public int findStepCount(int stepCount, String endName, String pattern)
//    {
//        if(name.equals(endName))
//            return stepCount;
//        
//        //find which element of the pattern we are currently on
//        int patternLength = pattern.length();
//        int currentStepIndex = (stepCount) % patternLength;
//        char nextStep = pattern.charAt(currentStepIndex);
//        
//        //add 1 to the step count for recusion
//        stepCount++;
//        
//            return left.findStepCount(stepCount, endName, pattern);
//        //right
//        if(stepCount % 25 == 0)
//            System.out.println("Step Count "+stepCount );
//        
//        //left
//        if(nextStep == 'L')
//            return left.findStepCount(stepCount, endName, pattern);
//        //right
//        if(nextStep == 'R')
//            return right.findStepCount(stepCount, endName, pattern);
//        
//        //should never get here, but for error checking
//        return -1;
//    }
}
