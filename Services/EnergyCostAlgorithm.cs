
namespace Ultra_Saver;

public class EnergyCostAlgorithm
{
    public float TotalEnergy { get; set; } = 0;

    // powerScale is a proportion of power used for the specified time; scales 1 to 10; ex.: medium heat, 3/10 of power
    // if User's appliance power efficiancy is 80% then ApplianceEfficiency = 0.8
    public void ElectricPower (short AppliancePower, byte PowerScale, short TimeInMin, float ApplianceEfficiency){
        TotalEnergy += AppliancePower * PowerScale * TimeInMin / 600_000 / ApplianceEfficiency;
    }

    public void GasPower (short AppliancePower, byte PowerScale, short TimeInMin, float ApplianceEfficiency){
        // small stove - ~1200w , medium - ~1500 - 1800, large - up to 3k
        // 1 kWh = 0.0947 m3 gas (100% efficient combustion)
        //TotalEnergy +=  / ApplianceEfficiency
    }

    public void BoilingWater (short WaterVolume, float ApplianceEfficiency){
        // If 100% efficiency
        TotalEnergy += 4200 * WaterVolume * 1000 * 80 / ApplianceEfficiency;
    }

    public void HeatingPan (short AppliancePower, float ApplianceEfficiency){
        // considering heating pan is for approximately 2 minutes
        TotalEnergy += AppliancePower / 30 / ApplianceEfficiency;
    }
}

// Research clearly shows that induction cooktops are more energy efficient:
// gas cooktops are about 40 percent efficient;
// electric-coil and standard smooth-top electric cooktops are about 74 percent efficient,
// and induction cooktops are 84 percent efficient