using Polly;
using Polly.CircuitBreaker;

namespace ResiliencePipelines.Strategies;

public class CircuitBreakerStrategy : IExecutionStrategy
{
    private ResiliencePipeline _pipeline;
    public CircuitBreakerStrategy()
    {
        _pipeline = new ResiliencePipelineBuilder()
            .AddCircuitBreaker(new CircuitBreakerStrategyOptions
            {
                BreakDuration = TimeSpan.FromSeconds(1),
                MinimumThroughput = 2,
                OnOpened = arguments =>
                {
                    Console.WriteLine("Circuit is open, please wait");
                    return default;
                }
            })
            .Build();
    }
    public void Execute(Func<string> operation)
    {
        _pipeline.Execute(operation);
    }
}