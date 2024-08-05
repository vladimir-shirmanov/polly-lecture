using System.Diagnostics;

namespace ResiliencePipelines.Strategies;

public class NoResiliencyStrategy : IExecutionStrategy
{
    public void Execute(Func<string> operation)
    {
        var stopwatch = Stopwatch.StartNew();
        string result = operation();
        Console.WriteLine("{0}. elapsed seconds: {1:F}", result, stopwatch.ElapsedMilliseconds/1000);
    }
}