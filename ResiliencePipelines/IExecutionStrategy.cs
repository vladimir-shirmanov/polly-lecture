namespace ResiliencePipelines;

public interface IExecutionStrategy
{
    void Execute(Func<string> operation);
    Task ExecuteAsync(Func<CancellationToken, Task<string>> operation);
}