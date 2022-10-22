using Xunit;

namespace tests;

public class DemoTest
{
    [Fact]
    public void SimpleTest()
    {
        Assert.Equal(10, Ultra_Saver.EnergyCostAlgorithm.GasConvertkWh(10, Ultra_Saver.ApplianceType.GAS_STOVE));

    }
}