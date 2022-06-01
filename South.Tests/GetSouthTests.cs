using ElkApiDemo.Controllers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace South.Tests;
public class GetSouthTests
{
    [Fact]
    public async Task GetSouth_Returns5()
    {
        var sut = new WeatherForecastController();

        var results = await sut.GetSouth();

        Assert.Equal(5, results.Count());
    }
}