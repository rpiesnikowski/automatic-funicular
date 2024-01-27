using AbaxService.StationService;
using AbaxService.Validation;

namespace AbaxAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        builder.Services.AddMemoryCache();
        
        builder.Services.AddLogging(config =>
        {
            config.AddDebug();
            config.AddConsole();
            // TODO: ELK or other sink
        });
        
        builder.Services.AddSingleton<IStationRepository, StationRepository>();
        builder.Services.AddSingleton<IStationService, StationService>();
        builder.Services.Decorate<IStationService, CachedStationService>();
        
        builder.Services.AddSingleton<ITrainInputValidator, TrainInputValidator>();
        builder.Services.AddSingleton<ITrainValidatorOptions, SomeDummyValidatorOptions>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapControllers();
        
        app.Run();
    }
}