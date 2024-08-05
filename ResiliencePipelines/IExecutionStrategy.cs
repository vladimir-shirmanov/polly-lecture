namespace ResiliencePipelines;

public interface IExecutionStrategy
{
    void Execute(Func<string> operation);
}