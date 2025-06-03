namespace AuctionR.Core.Infrastructure.Jobs;

public interface IAuctionStatusJob
{
    Task StartAuctionsAsync();

    Task EndAuctionsAsync();
}
