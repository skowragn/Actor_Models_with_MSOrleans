namespace CarsManager.Orleans.Infrastructure;

public class OrleansClusterClientHostedService : IHostedService
{
    private readonly ILogger<OrleansClusterClientHostedService> _logger;
    private readonly IConfiguration _configuration;
    public IClusterClient Client { get; set; }

    public OrleansClusterClientHostedService(ILogger<OrleansClusterClientHostedService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;


    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Connecting...");
    

        _logger.LogInformation("Orleans Client Connected {Initialized}");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}