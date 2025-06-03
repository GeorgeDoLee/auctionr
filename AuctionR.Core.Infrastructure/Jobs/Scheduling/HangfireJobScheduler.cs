using Hangfire;

namespace AuctionR.Core.Infrastructure.Jobs.Scheduling;

public class HangfireJobScheduler : IJobScheduler
{
    public void ConfigureRecurringJobs()
    {
        RecurringJob.AddOrUpdate<IAuctionStatusJob>(
            "EndAuctionsJob",
            job => job.EndAuctionsAsync(),
            Cron.Minutely
        );

        RecurringJob.AddOrUpdate<IAuctionStatusJob>(
            "StartAuctionsJob",
            job => job.StartAuctionsAsync(),
            Cron.Minutely
        );
    }
}
