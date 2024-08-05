using Polly;
using Polly.Retry;
using Polly.Timeout;

namespace ResiliencePipelines.Strategies;

public class TimeoutStrategy : IExecutionStrategy
{
    public void Execute(Func<string> operation)
    {
        var pipeline = new ResiliencePipelineBuilder()
            .AddTimeout(new TimeoutStrategyOptions
            {
                Timeout = TimeSpan.FromSeconds(3),
                OnTimeout = arguments =>
                {
                    Console.WriteLine("Timeout limit has been exceeded");
                    return default;
                }
            })
            .AddRetry(new RetryStrategyOptions
            {
                Delay = TimeSpan.FromSeconds(2),
                OnRetry = arguments =>
                {
                    Console.WriteLine("Attempt: {0}", arguments.AttemptNumber);
                    return default;
                }
            }) // this is an inner strategy
            .Build();

        // will do retries and if operation takes > 3 sec throws timeout
        pipeline.Execute(operation);
    }
}