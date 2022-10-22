
namespace Ultra_Saver;

public struct EnergyCostAlgorithm
{
    public EnergyCostAlgorithm() { }
    public double TotalEnergy { get; set; } = 0;
    public float GasVolumeUsed { get; set; } = 0;

    //                              hours * kilo
    private const int ConvertTokWh = 3_600_000;

    //                           1 kWh = 0.0947 m3 of natural gas
    private const float kWhGasCoefficient = (float)0.0947;

    //                              Q =          ρ  *  c
    private const float GasBurnEnergy = (float)0.68 * 49_000_000;

    //                              Q =   c  *  ρ  *    Δt ;  Litres -> Millilitres (m3)
    private const int WaterToBoilHeat = 4200 * 997 * (100 - 20) / 1000;

    // powerScale is a proportion of power used for the specified time. scales 0.1 to 1
    // if User's appliance power efficiancy is 80% then ApplianceEfficiency = 0.8
    public void ElectricPower(int AppliancePower, float PowerScale, int Time, ApplianceType Type)
    {
        TotalEnergy += AppliancePower * PowerScale * Time / ConvertTokWh / ApplianceEfficiency.GetEfficiency(Type);
    }

    public static float kWhConvertGas(float TotalEnergyUsed)
    {
        return TotalEnergyUsed * kWhGasCoefficient / ApplianceEfficiency.GetEfficiency(ApplianceType.GAS_STOVE);
    }

    public static float GasConvertkWh(float TotalGasUsed, ApplianceType Type)
    {
        return TotalGasUsed * GasBurnEnergy / ConvertTokWh / ApplianceEfficiency.GetEfficiency(Type);
    }

    public void HeatingPan(int AppliancePower, ApplianceType Type)
    {
        // If the recipe requires heating a pan
        // considering heating pan is for approximately 2 minutes
        TotalEnergy += AppliancePower * 120 / ConvertTokWh / ApplianceEfficiency.GetEfficiency(Type);
    }

    public void BoilingWater(short WaterVolume, ApplianceType Type)
    {
        TotalEnergy += WaterToBoilHeat * WaterVolume / ConvertTokWh / ApplianceEfficiency.GetEfficiency(Type);
    }
}