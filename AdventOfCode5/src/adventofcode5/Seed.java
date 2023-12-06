/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package adventofcode5;

/**
 *
 * @author Micha
 */
public class Seed {
    public long seedNo = 0;
    public long seedRange = 0;
    
    public long soilNo = 0;
    public long fertNo = 0;
    public long waterNo = 0;
    public long lightNo = 0;
    public long tempNo = 0;
    public long humidNo = 0;
    public long locNo = 0;
    
    public Seed(long seedNo)
    {
        this.seedNo = seedNo;
    }
    
    public Seed(long seedNo, long seedRange)
    {
        this.seedNo = seedNo;
        this.seedRange = seedRange;
    }
    
    public boolean isInSeedRange(long num)
    {
        return (num >= seedNo && num <= seedNo+seedRange);
    }
}
