using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AbaxService.StationService;

public class StationRepository : IStationRepository
{
    private readonly ILogger<StationRepository> logger;
    private readonly IConfiguration configuration;

    private readonly Lazy<List<string>> stations;
    
    public StationRepository(
        ILogger<StationRepository> logger, 
        IConfiguration configuration)
    {
        this.logger = logger;
        this.configuration = configuration;
        stations = new Lazy<List<string>>(GetAllPrivate, LazyThreadSafetyMode.PublicationOnly);
    }

    private List<string> GetAllPrivate()
    {
        logger.LogInformation($"Called {nameof(GetAllPrivate)}.");
        var jsonList = configuration.GetRequiredSection("Stations").Get<List<Station>>();
        logger.LogInformation("Found {0} stations", jsonList.Count);
        
        return jsonList.Select(s => s.StationName).ToList();
    }
    
    
    public List<string> GetAll()
    {
        return stations.Value;
    }
}

internal sealed class Station
{
    public required string StationName { get; set; }
}