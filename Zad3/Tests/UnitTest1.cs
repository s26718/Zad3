using Zad3.Models.ContainerTypes;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //arrange
        LiquidContainer lq = new LiquidContainer(1000, 3, 1000);
        //act
        string hazardInfo = lq.NotifyAboutHazard();
        //assert
        Assert.Equal("mhm",hazardInfo);
    }
}