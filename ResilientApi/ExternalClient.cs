namespace ResilientApi;

public interface IExternalClient
{
    Task<string> GetDataFromApi(CancellationToken token);
}

public class ExternalClient : IExternalClient
{
    private readonly ILogger<ExternalClient> _logger;

    public ExternalClient(ILogger<ExternalClient> logger)
    {
        _logger = logger;
    }
    
    public async Task<string> GetDataFromApi(CancellationToken token)
    {
        await Task.Delay(10_000, token);

        _logger.LogInformation("Got the result");
        
        return "Finally results";
    }
}