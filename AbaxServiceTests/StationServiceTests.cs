using AbaxService.StationService;
using Moq;

namespace AbaxServiceTests;

public class StationServiceTests
{
    private Mock<IStationRepository> repository;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        repository = new Mock<IStationRepository>();
    }

    public static List<StationServiceTestCase> cases = new()
    {
        new StationServiceTestCase("DART case")
        {
            Input = "DART",
            Stations = new List<string>()
            {
                "DARTFORD", "DARTON", "TOWER HILL", "DERBY"
            },
            ExpectedStations = new List<string>(){"DARTFORD", "DARTON"},
            NextCharacters = new List<char>(){'F','O'}
        },
        new StationServiceTestCase("LIVERPOOL case")
        {
            Input = "LIVERPOOL",
            Stations = new List<string>()
            {
                "LIVERPOOL", "LIVERPOOL LIME STREET", "PADDINGTON"
            },
            ExpectedStations = new List<string>(){"LIVERPOOL", "LIVERPOOL LIME STREET"},
            NextCharacters = new List<char>(){' '}
        },
        new StationServiceTestCase("KINGS CROSS case")
        {
            Input = "KINGS CROSS",
            Stations = new List<string>()
            {
                "EUSTON", "LONDON BRIDGE", "VICTORIA"
            },
            ExpectedStations = new List<string>(){},
            NextCharacters = new List<char>(){}
        }
    };

    [TestCaseSource(nameof(cases))]
    public void StationService_Get(StationServiceTestCase tc)
    {
        repository.Setup(s => s.GetAll()).Returns(tc.Stations);
        var service = new StationService(repository.Object);
        
        var result = service.Get(tc.Input);
        
        Assert.That(result.Stations, Is.EqualTo(tc.ExpectedStations));
        Assert.That(result.NextCharacters, Is.EqualTo(tc.NextCharacters));
    }

    public class StationServiceTestCase : TestCase
    {
        public string Input { get; set; }
        public List<string> Stations { get; set; }
        public List<string> ExpectedStations { get; set; }
        public List<char> NextCharacters { get; set; }


        public StationServiceTestCase(string title) : base(title)
        {
        }
    }
}