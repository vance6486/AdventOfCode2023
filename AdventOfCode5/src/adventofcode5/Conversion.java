/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package adventofcode5;

/**
 *
 * @author Micha
 */
public class Conversion {
    private long sourceStart;
    private long sourceEnd;
    
    private long destStart;
    private long destEnd;
    
    private long range;
    private long conversion;
    
    public Conversion(long destStart, long sourceStart, long range)
    {
        this.sourceStart = sourceStart;
        this.sourceEnd = sourceStart + range;
        
        this.destStart = destStart;
        this.destEnd = destStart + range;
        
        this.range = range;
        
        this.conversion = destStart - sourceStart;
    }

    public long getSourceStart() {
        return sourceStart;
    }

    public long getSourceEnd() {
        return sourceEnd;
    }

    public long getDestStart() {
        return destStart;
    }

    public long getDestEnd() {
        return destEnd;
    }

    public long getRange() {
        return range;
    }

    public boolean isInSourceRange(long sourceNum)
    {
        return (sourceNum >= sourceStart && sourceNum <= sourceEnd);
    }
    
    public boolean isInDestRange(long destNum)
    {
        return (destNum >= destStart && destNum <= destEnd);
    }
    
    public long convertSourceToDest(long sourceNum)
    {
        return sourceNum + conversion;
    }
    
    public long convertDestToSource(long destNum)
    {
        return destNum + (conversion * -1);
    }
}
