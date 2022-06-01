using ElkApiDemo.Controllers;
using System.Linq;
using Xunit;

namespace North.Tests;
public class GetNorthTests
{
    [Fact]
    public void GetNorth_Returns5()
    {
        var sut = new WeatherForecastController();

        var results = sut.GetNorth();

        Assert.Equal(5, results.Count());
    }
}