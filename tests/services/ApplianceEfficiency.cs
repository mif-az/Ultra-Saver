using System;
using Ultra_Saver;
using Xunit;

namespace tests;

public class ApplianceEfficiency
{

    [Fact]
    public void AllApliancesHaveEfficiency()
    {
        foreach (ApplianceType appliance in Enum.GetValues(typeof(ApplianceType)))
        {
            Assert.NotEqual(-1, Ultra_Saver.ApplianceEfficiency.GetEfficiency(appliance));
        }
    }

    [Fact]
    public void coverAllBranches()
    {
        Assert.Equal(-1, Ultra_Saver.ApplianceEfficiency.GetEfficiency((ApplianceType)typeof(ApplianceType).GetMembers().Length + 1));
    }
}