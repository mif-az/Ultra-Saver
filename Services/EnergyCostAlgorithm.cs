
namespace Ultra_Saver;

public class EnergyCostAlgorithm
{
    private const int ConvertTokWh = 3_600_000;
    private const float kWhToGas = ConvertTokWh * (float) 0.0947;

    //                              Q =   c  *  ρ  *    Δt ;  Litres -> Millilitres
    private const int WaterToBoilHeat = 4200 * 997 * (100 - 20) / 1000;
    public double TotalEnergy { get; set; } = 0;

    // powerScale is a proportion of power used for the specified time; scales 1 to 10; ex.: medium heat, 3/10 of power
    // if User's appliance power efficiancy is 80% then ApplianceEfficiency = 0.8
    public void ElectricPower (int AppliancePower, short PowerScale, int Time, int ApplianceEfficiency){
        TotalEnergy += AppliancePower * PowerScale * Time / ConvertTokWh / ApplianceEfficiency * 100;
    }

    public void GasPower (int AppliancePower, short PowerScale, int Time, int ApplianceEfficiency){
        TotalEnergy += AppliancePower * PowerScale * Time * kWhToGas / ApplianceEfficiency * 100;
    }

    public void HeatingPan (int AppliancePower, int ApplianceEfficiency){
        // If the recipe requires heating a pan
        // considering heating pan is for approximately 2 minutes
        TotalEnergy += AppliancePower * 120 / ConvertTokWh / ApplianceEfficiency * 100;
    }

    public void BoilingWater (short WaterVolume, int ApplianceEfficiency){
        TotalEnergy += WaterToBoilHeat / ConvertTokWh * WaterVolume / ApplianceEfficiency * 100;
    }
}