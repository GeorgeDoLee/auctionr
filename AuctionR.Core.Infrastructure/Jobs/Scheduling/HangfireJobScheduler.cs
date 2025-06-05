using AuctionR.Core.Infrastructure.Settings;
using Hangfire;
using Microsoft.Extensions.Options;

namespace AuctionR.Core.Infrastructure.Jobs.Scheduling;

public class HangfireJobScheduler : IJobScheduler
{
    private readonly CronSettings _cronSettings;

    public HangfireJobScheduler(IOptions<CronSettings> cronSettingsOptions)
    {
        _cronSettings = cronSettingsOptions.Value;
    }

    public void ConfigureRecurringJobs()
    {
        RecurringJob.AddOrUpdate<IAuctionStatusJob>(
            "EndAuctionsJob",
            job => job.EndAuctionsAsync(),
            _cronSettings.StartAuctions
        );

        RecurringJob.AddOrUpdate<IAuctionStatusJob>(
            "StartAuctionsJob",
            job => job.StartAuctionsAsync(),
            _cronSettings.EndAuctions
        );
    }
}
