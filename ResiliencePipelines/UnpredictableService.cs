namespace ResiliencePipelines;

public class UnpredictableService
{
    public string CallExternalApi()
    {
        Thread.Sleep(100);
        int chance = Random.Shared.Next(100);

        if (chance < 50)
        {
            throw new InvalidOperationException();
        }

        if (chance > 90)
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }

        return "Ok, it's good now, here is your result";
    }

    public async Task<string> CallSlowCache(CancellationToken token)
    {
        await Task.Delay(TimeSpan.FromSeconds(3), token);
        return "Slow Cache data returned";
    }

    public Task<string> CallFastCache()
    {
        return Task.FromResult("Fast Cache data returned");
    }
}