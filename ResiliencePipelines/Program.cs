// See https://aka.ms/new-console-template for more information

using ResiliencePipelines;
using ResiliencePipelines.Strategies;

Func<string> op = () => new UnpredictableService().CallExternalApi();
Dictionary<string, IExecutionStrategy> strategies = new Dictionary<string, IExecutionStrategy>()
{
    ["no resiliency"] = new NoResiliencyStrategy(),
    ["retry"] = new NoResiliencyStrategy()
};

strategies["no resiliency"].Execute(op);

