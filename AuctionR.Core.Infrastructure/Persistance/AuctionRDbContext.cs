using AuctionR.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionR.Core.Infrastructure.Persistance;

internal class AuctionRDbContext : DbContext
{
    public AuctionRDbContext(DbContextOptions<AuctionRDbContext> options)
        : base(options)
    {
    }

    public DbSet<Auction> Auctions { get; set; }

    public DbSet<Bid> Bids { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuctionRDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
