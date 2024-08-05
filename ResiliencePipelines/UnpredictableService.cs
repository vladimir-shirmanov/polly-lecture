namespace ResiliencePipelines;

public class UnpredictableService
{
    public string CallExternalApi()
    {
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
}