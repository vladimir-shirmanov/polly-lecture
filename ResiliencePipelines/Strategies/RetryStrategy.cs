using System.Collections.Immutable;
using Polly;
using Polly.Retry;

namespace ResiliencePipelines.Strategies;

public class RetryStrategy : IExecutionStrategy
{
    public void Execute(Func<string> operation)
    {
        ImmutableArray<Type> networkExceptions = [typeof(HttpRequestException)];
        ImmutableArray<Type> businessLogicException = [typeof(InvalidOperationException)];
        ImmutableArray<Type> retryableExceptions = [..networkExceptions.Union(businessLogicException)];
        
        var pipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions
            {
                ShouldHandle = context =>
                {
                    var ex = context.Outcome.Exception;
                    if (ex == null)
                    {
                        return new ValueTask<bool>(false);
                    }

                    return new ValueTask<bool>(
                        retryableExceptions.Contains(ex.GetType()));
                },
                OnRetry = args =>
                {
                    Console.WriteLine("OnRetry, Attempt: {0}", args.AttemptNumber);

                    // Event handlers can be asynchronous; here, we return an empty ValueTask.
                    return default;
                },
                Delay = TimeSpan.FromSeconds(1),
                BackoffType = DelayBackoffType.Linear,
                MaxRetryAttempts = 3
            })
            .Build();
        pipeline.Execute(operation);
    }

    public Task ExecuteAsync(Func<CancellationToken, Task<string>> operation)
    {
        throw new NotImplementedException();
    }
}