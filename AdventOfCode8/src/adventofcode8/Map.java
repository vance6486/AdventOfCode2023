/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package adventofcode8;

import java.util.*;

/**
 *
 * @author Creon
 */
public class Map {

    private List<MapNode> mapNodes = new ArrayList<>();
    private List<MapNode> aNodes = new ArrayList<>();
    public String pattern = "LR";

    public void add(MapNode node) {
        // see if the left and right mapNodes already exist in the list
        MapNode leftNode = connectNode(node.left.name);
        MapNode rightNode = connectNode(node.right.name);

        //if a node of that name already exists, update that node instead of adding a new one
        MapNode isExistingNode = findNode(node);
        if (isExistingNode != null) {
            node = isExistingNode;
        } else {
            mapNodes.add(node);
            if (node.nodeType == 'A') {
                aNodes.add(node);
            }
        }

        node.left = leftNode;
        node.right = rightNode;

    }

    /**
     * Finds of the node exists in the map or returns null
     *
     * @param nodeToFind node you want to find a match for in the Map
     * @return null if node cant be found
     */
    public MapNode findNode(MapNode nodeToFind) {
        for (MapNode node : mapNodes) {
            if (node.equals(nodeToFind)) {
                return node;
            }
        }

        return null;
    }

    /**
     * Wrapper for the findNode function if they only pass in the name
     *
     * @param nodeToFind the name of the node to find
     * @return if a node with that name wasnt found return null
     */
    public MapNode findNode(String nodeToFind) {
        return findNode(new MapNode(nodeToFind));
    }

    /**
     * search through the map for a node of that name, then return it if it
     * exists
     *
     * @param nodeName the name of the node to find
     * @return the node of that name if it exists, or a new node if it doesnt
     */
    public MapNode connectNode(String nodeName) {
        MapNode result = findNode(nodeName);

        if (result == null) {
            result = new MapNode(nodeName);
            //add the new node to the list of mapNodes to connect to later
            mapNodes.add(result);
            if (result.nodeType == 'A') {
                aNodes.add(result);
            }
        }

        return result;
    }

    /**
     *
     *
     * @param startName where in the list to start
     * @param endName the name of the node to end on
     * @param pattern the pattern of steps to take to navigate the map
     * @return returns the count, or -1 if there is an error
     */
    public int findStepCount(String startName, String endName) {
        int stepCount = -1;
        MapNode startNode = findNode(startName);

        //only check for the step count if start Name was validS
        if (startNode != null) {
            MapNode temp = startNode;
            stepCount = 0;

            while (temp.equals(endName) == false) {
                //find which element of the pattern we are currently on
                int currentStepIndex = (stepCount) % pattern.length();
                char nextStep = pattern.charAt(currentStepIndex);

                //add 1 to the step count for recusion
                stepCount++;

                if (nextStep == 'L') {
                    temp = temp.left;
                } else {
                    temp = temp.right;
                }
            }
        }

        return stepCount;
    }

    public int findZCount(String startName) {
        int stepCount = -1;
        MapNode startNode = findNode(startName);

        //only check for the step count if start Name was validS
        if (startNode != null) {
            MapNode temp = startNode;
            stepCount = 0;

            while (temp.nodeType != 'Z') {
                //find which element of the pattern we are currently on
                int currentStepIndex = (stepCount) % pattern.length();
                char nextStep = pattern.charAt(currentStepIndex);

                //add 1 to the step count for recusion
                stepCount++;

                if (nextStep == 'L') {
                    temp = temp.left;
                } else {
                    temp = temp.right;
                }
            }
        }

        return stepCount;
    }

    public int findLoopCount(String startName) {
        int stepCount = -1;
        MapNode startNode = findNode(startName);

        //only check for the step count if start Name was validS
        if (startNode != null) {
            MapNode temp = startNode;
            stepCount = 0;

            while (temp.equals(startName) == false) {
                //find which element of the pattern we are currently on
                int currentStepIndex = (stepCount) % pattern.length();
                char nextStep = pattern.charAt(currentStepIndex);

                //add 1 to the step count for recusion
                stepCount++;

                if (nextStep == 'L') {
                    temp = temp.left;
                } else {
                    temp = temp.right;
                }
            }
        }

        return stepCount;
    }

    public long aNodesToZNodes() {
        long stepCount = -1;
        long[] stepsToZ = new long[aNodes.size()];

        for (int i = 0; i < aNodes.size(); i++) {
            String currentNodeA = aNodes.get(i).name;
            String currentNodeZ = currentNodeA.substring(0, 2) + 'Z';

            stepsToZ[i] = findZCount(currentNodeA);
        }



        return lcm(stepsToZ);
    }

    private static long gcd(long a, long b) {
        while (b > 0) {
            long temp = b;
            b = a % b; // % is remainder
            a = temp;
        }
        return a;
    }

    private static long lcm(long a, long b) {
        return a * (b / gcd(a, b));
    }

    private static long lcm(long[] input) {
        long result = input[0];
        for (int i = 1; i < input.length; i++) {
            result = lcm(result, input[i]);
        }
        return result;
    }
}
