namespace ResiliencePipelines.Strategies;

public class NoResiliencyStrategy : IExecutionStrategy
{
    public void Execute(Func<string> operation)
    {
        operation();
    }

    public Task ExecuteAsync(Func<CancellationToken, Task<string>> operation)
    {
        throw new NotImplementedException();
    }
}