using System;
using Ultra_Saver;
using Xunit;

namespace tests;

public class DemoTest
{
    [Fact]
    public void SimpleTest()
    {
        Assert.Equal(0.9f, Ultra_Saver.ApplianceEfficiency.GetEfficiency(ApplianceType.OVEN));

    }

    [Fact]
    public void AllApliancesHaveEfficiency()
    {
        foreach (ApplianceType appliance in Enum.GetValues(typeof(ApplianceType)))
        {
            Assert.NotEqual(-1, ApplianceEfficiency.GetEfficiency(appliance));
        }
    }
}