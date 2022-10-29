using System.Collections.Generic;
using Xunit;

namespace tests;

public class Model
{
    [Fact]
    public void NoModelIsEqualToNull()
    {
        Assert.False(new MockModel1().Equals(null));
    }

    [Theory]
    [MemberData(nameof(ModelsAreEqualIfSignaturesAreCases))]
    public void ModelsAreEqualIfSignaturesAre(Ultra_Saver.Model<string> model1, Ultra_Saver.Model<string> model2)
    {
        Assert.True(model1.GetSignature().Equals(model2.GetSignature()) ? model1.Equals(model2) : !model1.Equals(model2));
    }

    public static IEnumerable<object[]> ModelsAreEqualIfSignaturesAreCases => new List<object[]> {
        new object[] {new MockModel1() {Signature = "sig1"}, new MockModel1() {Signature = "sig1"}},
        new object[] {new MockModel1() {Signature = "sig1"}, new MockModel1() {Signature = "NOT sig1"}},
        new object[] {new MockModel1() {Signature = "sig2"}, new MockModel2() {Something = "sig2"}},
        new object[] {new MockModel1() {Signature = "NOT sig2"}, new MockModel2() {Something = "sig2"}},
    };

}

class MockModel1 : Ultra_Saver.Model<string>
{
    public string Signature { get; set; } = "";
    public override string GetSignature()
    {
        return Signature;
    }
}

class MockModel2 : Ultra_Saver.Model<string>
{
    public string Something { get; set; } = "";
    public override string GetSignature()
    {
        return Something;
    }
}