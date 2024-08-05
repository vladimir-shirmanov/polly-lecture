namespace ResiliencePipelines.Strategies;

public class RetryStrategy : IExecutionStrategy
{
    public void Execute(Func<string> operation)
    {
        throw new NotImplementedException();
    }
}