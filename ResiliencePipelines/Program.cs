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

Func<CancellationToken, Task<string>> cacheOp = ctx => new UnpredictableService().CallSlowCache(ctx);
Dictionary<string, IExecutionStrategy> strategies = new Dictionary<string, IExecutionStrategy>()
{
    ["no resiliency"] = new NoResiliencyStrategy(),
    ["custom"] = new CustomStrategy(3),
    ["retry"] = new RetryStrategy(),
    ["timeout"] = new TimeoutStrategy(),
    ["circuit"] = new CircuitBreakerStrategy(),
    ["fallback"] = new FallbackStrategy(),
    ["hedging"] = new HedgeStrategy()
};

await strategies["hedging"].ExecuteAsync(async ctx => await cacheOp(ctx));