using AuctionR.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionR.Core.Infrastructure.Persistance.Configurations;

internal class AuctionConfiguration : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
        builder.ToTable("Auctions");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.ProductId)
            .IsRequired();
         
        builder.Property(a => a.StartingPrice) 
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(a => a.MinimumBidIncrement)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(a => a.StartTime)
            .IsRequired();

        builder.Property(a => a.EndTime)
            .IsRequired();

        builder.Property(a => a.HighestBidAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(a => a.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasMany(a => a.Bids)
            .WithOne()
            .HasForeignKey(b => b.AuctionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
