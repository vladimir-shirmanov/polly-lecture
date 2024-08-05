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
                operation();
            }
            catch (Exception e)
            {
                retryCount++;
            }
        } while (retryCount < _numOfRetries);
    }
}