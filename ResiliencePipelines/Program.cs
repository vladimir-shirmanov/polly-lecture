// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using ResiliencePipelines;
using ResiliencePipelines.Strategies;

Func<string> op = () =>
{
    var stopwatch = Stopwatch.StartNew();
    string result = new UnpredictableService().CallExternalApi();
    Console.WriteLine("{0}. elapsed seconds: {1:F}", result, stopwatch.ElapsedMilliseconds/1000);
    return result;
};
Dictionary<string, IExecutionStrategy> strategies = new Dictionary<string, IExecutionStrategy>()
{
    ["no resiliency"] = new NoResiliencyStrategy(),
    ["custom"] = new CustomStrategy(3),
    ["retry"] = new RetryStrategy()
};

strategies["custom"].Execute(op);