using Polly;
using Polly.Fallback;

namespace ResiliencePipelines.Strategies;

public class FallbackStrategy : IExecutionStrategy
{
    private readonly ResiliencePipeline<string> _pipeline;
    
    public FallbackStrategy()
    {
        _pipeline = new ResiliencePipelineBuilder<string>()
            .AddFallback(new FallbackStrategyOptions<string>
            {
                FallbackAction = _ => Outcome.FromResultAsValueTask("This is a fallback, main call failed")
            }).Build();
    }
    public void Execute(Func<string> operation)
    {
        string result = _pipeline.Execute(operation);
        Console.WriteLine("Here what we get as a result: {0}", result);
    }

    public Task ExecuteAsync(Func<CancellationToken, Task<string>> operation)
    {
        throw new NotImplementedException();
    }
}