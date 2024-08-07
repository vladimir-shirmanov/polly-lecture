using Polly;
using Polly.Hedging;

namespace ResiliencePipelines.Strategies;

public class HedgeStrategy : IExecutionStrategy
{
    private readonly ResiliencePipeline<string> _pipeline;
    public HedgeStrategy()
    {
        _pipeline = new ResiliencePipelineBuilder<string>()
            .AddHedging(new HedgingStrategyOptions<string>
            {
                Delay = TimeSpan.FromSeconds(2),
                ActionGenerator = arguments =>
                {
                    var result = new UnpredictableService().CallFastCache();
                    return async () => Outcome.FromResult(await result);
                }
            }).Build();
    }
    public void Execute(Func<string> operation)
    {
        string result = _pipeline.Execute(operation);
        Console.WriteLine("Got this result: {0}", result);
    }

    public async Task ExecuteAsync(Func<CancellationToken, Task<string>> operation)
    {
        string result = await _pipeline.ExecuteAsync(async token => await operation(token));
        Console.WriteLine("Got this result: {0}", result);
    }
}