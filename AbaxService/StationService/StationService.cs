namespace AbaxService.StationService;

public class StationService : IStationService
{
    private readonly IStationRepository stationRepository;

    public StationService(IStationRepository stationRepository)
    {
        this.stationRepository = stationRepository;
    }

    public StationServiceResult Get(string input)
    {
        var a = stationRepository.GetAll();
        List<string> stations = new List<string>();
        List<char> characters = new List<char>();
        foreach (var find in a.FindAll(f => f.StartsWith(input, StringComparison.InvariantCultureIgnoreCase)))
        {
            stations.Add(find);
            if (find.Length > input.Length)
            {
               characters.Add(find[input.Length]); 
            }
            
        }
        
         characters = characters
            .Distinct()
            .ToList();

        return new StationServiceResult(stations, characters);
    }
    
}