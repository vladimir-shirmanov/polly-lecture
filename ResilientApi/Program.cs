using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.Fallback;
using Polly.Registry;
using Polly.Retry;
using Polly.Timeout;
using ResilientApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IExternalClient, ExternalClient>();

builder.Services.AddResiliencePipeline("retry", pipelineBuilder =>
    pipelineBuilder.AddRetry(new RetryStrategyOptions
    {
        Delay = TimeSpan.FromSeconds(1),
        MaxRetryAttempts = 2,
        BackoffType = DelayBackoffType.Exponential,
        UseJitter = true
    }));

builder.Services.AddResiliencePipeline<string, string>("fallback", pipelineBuilder =>
    pipelineBuilder.AddFallback(new FallbackStrategyOptions<string>
    {
        FallbackAction = _ => Outcome.FromResultAsValueTask("Fallback value")
    }).AddTimeout(new TimeoutStrategyOptions
    {
        Timeout = TimeSpan.FromSeconds(1)
    }));

builder.Services.AddHttpClient<GoogleClient>()
    .AddStandardResilienceHandler();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/test", async (CancellationToken cancellationToken, IExternalClient httpClient, ResiliencePipelineProvider<string> pipelineProvider) =>
    {
        ResiliencePipeline<string> pipeline = pipelineProvider.GetPipeline<string>("fallback");
        var result = await pipeline.ExecuteAsync(async token => await httpClient.GetDataFromApi(cancellationToken), cancellationToken);
        return result;
    })
    .WithName("test")
    .WithOpenApi();

app.Run();

public class GoogleClient
{
    private readonly HttpClient _httpClient;

    public GoogleClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}