
namespace Ultra_Saver;

public class EnergyCostAlgorithm
{
    public double TotalEnergy { get; set; } = 0;

    // powerScale is a proportion of power used for the specified time; scales 1 to 10; ex.: medium heat, 3/10 of power
    // if User's appliance power efficiancy is 80% then ApplianceEfficiency = 0.8
    public void ElectricPower (int AppliancePower, short PowerScale, short TimeInMin, float ApplianceEfficiency){
        TotalEnergy += AppliancePower * PowerScale * TimeInMin / 600_000 / ApplianceEfficiency;
    }

    public void GasPower (int AppliancePower, short PowerScale, short TimeInMin, float ApplianceEfficiency){
        TotalEnergy += AppliancePower * (float) 0.0947 / ApplianceEfficiency;
    }

    public void HeatingPan (int AppliancePower, float ApplianceEfficiency){
        // considering heating pan is for approximately 2 minutes
        TotalEnergy += AppliancePower / 108_000_000 / ApplianceEfficiency;
    }

    public void BoilingWater (short WaterVolume, float ApplianceEfficiency){
        TotalEnergy += (float) 0.336 / 3600 * WaterVolume / ApplianceEfficiency;
    }
}