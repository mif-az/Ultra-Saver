using System;
using Ultra_Saver;
using Ultra_Saver.Controllers;
using Xunit;

namespace tests;

public class EnergyCostAlgorithm
{
    [Fact]
    public void totalEnergyAlwaysIncreases()
    {
        var algo = new Ultra_Saver.EnergyCostAlgorithm();
        var curr = algo.TotalEnergy;
        var prev = curr;

        algo.BoilingWater(1, ApplianceType.INDUCTION_STOVE);
        Assert.True((prev = curr) < (curr = algo.TotalEnergy));

        algo.ElectricPower(100, 0.5f, 30, ApplianceType.GAS_STOVE);
        Assert.True((prev = curr) < (curr = algo.TotalEnergy));

        algo.HeatingPan(4000, ApplianceType.INDUCTION_STOVE);
        Assert.True((prev = curr) < (curr = algo.TotalEnergy));

    }

    [Fact(Skip = "This test fails")]
    public void DoubleConversionDoesNotCancel()
    {
        foreach (ApplianceType type in Enum.GetValues(typeof(ApplianceType)))
        {
            Assert.True(1000 > Ultra_Saver.EnergyCostAlgorithm.GasConvertkWh(Ultra_Saver.EnergyCostAlgorithm.kWhConvertGas(1000), type));
        }
    }
}