
namespace Ultra_Saver;

public class EnergyCostAlgorithm
{
    private const int ConvertTokWh = 3_600_000;
    private const float kWhToGas = ConvertTokWh * (float)0.0947;

    //                              Q =   c  *  ρ  *    Δt ;  Litres -> Millilitres
    private const int WaterToBoilHeat = 4200 * 997 * (100 - 20) / 1000;
    public double TotalEnergy { get; set; } = 0;

    // powerScale is a proportion of power used for the specified time; scales 1 to 10; ex.: medium heat, 3/10 of power
    // if User's appliance power efficiancy is 80% then ApplianceEfficiency = 0.8
    public void ElectricPower(int AppliancePower, short PowerScale, int Time, ApplianceType Type)
    {
        TotalEnergy += AppliancePower * PowerScale * Time / ConvertTokWh / ApplianceEfficiency.GetEfficiency(Type);
    }

    public void GasPower(int AppliancePower, short PowerScale, int Time, ApplianceType Type)
    {
        TotalEnergy += AppliancePower * PowerScale * Time * kWhToGas / ApplianceEfficiency.GetEfficiency(Type);
    }

    public void HeatingPan(int AppliancePower, ApplianceType Type)
    {
        // If the recipe requires heating a pan
        // considering heating pan is for approximately 2 minutes
        TotalEnergy += AppliancePower * 120 / ConvertTokWh / ApplianceEfficiency.GetEfficiency(Type);
    }

    public void BoilingWater(short WaterVolume, ApplianceType Type)
    {
        TotalEnergy += WaterToBoilHeat / ConvertTokWh * WaterVolume / ApplianceEfficiency.GetEfficiency(Type);
    }
}