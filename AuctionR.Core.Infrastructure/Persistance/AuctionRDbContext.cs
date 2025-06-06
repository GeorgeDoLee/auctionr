using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuctionR.Core.Infrastructure.Persistance;

internal class AuctionRDbContext : DbContext
{
    private readonly IPublisher _publisher;

    public AuctionRDbContext(
        DbContextOptions<AuctionRDbContext> options,
        IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<Auction> Auctions { get; set; }

    public DbSet<Bid> Bids { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuctionRDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .SelectMany(e => e.DomainEvents);

        var result = await base.SaveChangesAsync(ct);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, ct);
        }

        return result;
    }
}
