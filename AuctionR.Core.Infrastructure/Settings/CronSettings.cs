namespace AuctionR.Core.Infrastructure.Settings;

public class CronSettings
{
    public string StartAuctions { get; set; } = "* * * * *";

    public string EndAuctions { get; set; } = "* * * * *";
}
