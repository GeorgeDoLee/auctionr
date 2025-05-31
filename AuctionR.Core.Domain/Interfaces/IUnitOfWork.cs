namespace AuctionR.Core.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBidRepository Bids { get; }

    IAuctionRepository Auctions { get; }
    
    Task Complete(CancellationToken ct = default);
}
