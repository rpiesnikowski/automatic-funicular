namespace AbaxService.StationService;

public class StationServiceResult
{
    public StationServiceResult(List<string> stations, List<char> nextCharacters)
    {
        Stations = stations;
        NextCharacters = nextCharacters;
    }

    public List<string> Stations { get; }
    public List<char> NextCharacters { get; }
}