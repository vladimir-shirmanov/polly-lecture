namespace ResiliencePipelines.Strategies;

public class NoResiliencyStrategy : IExecutionStrategy
{
    public void Execute(Func<string> operation)
    {
        operation();
    }
}