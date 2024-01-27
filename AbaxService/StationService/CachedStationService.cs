using Microsoft.Extensions.Caching.Memory;

namespace AbaxService.StationService;

public class CachedStationService : IStationService
{
    private readonly IMemoryCache memoryCache;
    private readonly IStationService service;

    public CachedStationService(
        IMemoryCache memoryCache, 
        IStationService service)
    {
        this.memoryCache = memoryCache;
        this.service = service;
    }

    public StationServiceResult Get(string input)
    {
        var item = memoryCache.Get<StationServiceResult>(input);
        if (item == null)
        {
            item = service.Get(input);
            var options = new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(5) };
            memoryCache.Set(input, item, options);
        }

        return item;
    }
}