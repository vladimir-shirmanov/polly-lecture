namespace ResiliencePipelines.Strategies;

public class CustomStrategy : IExecutionStrategy
{
    private int _numOfRetries;
    
    public CustomStrategy(int numOfRetries)
    {
        _numOfRetries = numOfRetries;
    }
    
    public void Execute(Func<string> operation)
    {
        int retryCount = 0;
        do
        {
            try
            {
                Console.WriteLine("Attempt: {0}", retryCount+1);
                operation();
                break;
            }
            catch (Exception e)
            {
                retryCount++;
            }
        } while (retryCount < _numOfRetries);
    }

    public Task ExecuteAsync(Func<CancellationToken, Task<string>> operation)
    {
        throw new NotImplementedException();
    }
}