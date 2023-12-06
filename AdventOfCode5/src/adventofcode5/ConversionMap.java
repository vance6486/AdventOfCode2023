/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package adventofcode5;
import java.util.*;
/**
 *
 * @author Micha
 */
public class ConversionMap {
    private List<Conversion> conversions = new ArrayList<Conversion>();
    
    public ConversionMap()
    {
        
    }
    
    public void add(Conversion newConversion) { conversions.add(newConversion); }
    
    public long convertSourceToDest(long sourceNum)
    {
        long result = sourceNum;
        
        for(Conversion conversion : conversions)
        {
            if(conversion.isInSourceRange(sourceNum))
            {
                result = conversion.convertSourceToDest(sourceNum);
            }
        }
        
        return result;
    }
    
    public long convertDestToSource(long destNum)
    {
        long result = destNum;
        
        for(Conversion conversion : conversions)
        {
            if(conversion.isInDestRange(destNum))
            {
                result = conversion.convertDestToSource(destNum);
            }
        }
        
        return result;
    }
}