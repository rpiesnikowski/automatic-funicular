using System.Text;
using AbaxService.StationService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace AbaxServiceTests;

public class StationRepositoryTests
{
    [Test]
    public void StationRepository_GetAll()
    {
        string raw = """
                     {
                       "Stations":[
                         {
                           "stationCode":"ABW",
                           "stationName":"Abbey Wood"
                         },
                         {
                           "stationCode":"ABE",
                           "stationName":"Aber"
                         }
                       ]
                     }

                     """;

        var builder = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(raw))).Build();
        var repository = new StationRepository(new Mock<ILogger<StationRepository>>().Object, builder);
        var result = repository.GetAll();
        Assert.That(2, Is.EqualTo(result.Count));
    }
    
    [Test]
    public void StationRepository_EmptyJson()
    {
        string raw = """
                     {
                      
                     }

                     """;
        var builder = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(raw))).Build();
        var repository = new StationRepository(new Mock<ILogger<StationRepository>>().Object, builder);
        Assert.Throws<InvalidOperationException>(() => repository.GetAll());
    }
    
    [Test]
    public void StationRepository_NullStationsJson()
    {
        string raw = """
                     {
                      "Stations": {}
                     }

                     """;
        var builder = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(raw))).Build();
        var repository = new StationRepository(new Mock<ILogger<StationRepository>>().Object, builder);
        Assert.Throws<InvalidOperationException>(() => repository.GetAll());
    }
    
}