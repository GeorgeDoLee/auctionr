using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Enums;

namespace AuctionR.Core.Infrastructure.Seeders;

internal static class DummyData
{
    internal static List<Auction> GetAuctions()
    {
        return
        [
            new()
            {
                ProductId = 1,
                OwnerId = 10,
                Title = "Messi Jersey",
                Description = "Authentic signed Messi jersey.",
                StartingPrice = 100,
                MinimumBidIncrement = 10,
                Currency = "USD",
                StartTime = DateTime.UtcNow.AddDays(1),
                EndTime = DateTime.UtcNow.AddDays(5),
                CreatedAt = DateTime.UtcNow,
                Status = AuctionStatus.Active
            },
            new()
            {
                ProductId = 2,
                OwnerId = 11,
                Title = "Diamond Ring",
                Description = "Elegant diamond engagement ring.",
                StartingPrice = 150,
                MinimumBidIncrement = 15,
                Currency = "USD",
                StartTime = DateTime.UtcNow.AddDays(2),
                EndTime = DateTime.UtcNow.AddDays(6),
                CreatedAt = DateTime.UtcNow,
                Status = AuctionStatus.Pending
            },
            new()
            {
                ProductId = 3,
                OwnerId = 12,
                Title = "World Cup Ball",
                Description = "2026 World Cup official match ball.",
                StartingPrice = 200,
                MinimumBidIncrement = 20,
                Currency = "USD",
                StartTime = DateTime.UtcNow.AddDays(3),
                EndTime = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                Status = AuctionStatus.Active
            },
            new()
            {
                ProductId = 4,
                OwnerId = 13,
                Title = "Vintage Rolex",
                Description = "Swiss-made vintage Rolex watch.",
                StartingPrice = 500,
                MinimumBidIncrement = 25,
                Currency = "USD",
                StartTime = DateTime.UtcNow.AddDays(1),
                EndTime = DateTime.UtcNow.AddDays(4),
                CreatedAt = DateTime.UtcNow,
                Status = AuctionStatus.Pending
            },
            new()
            {
                ProductId = 5,
                OwnerId = 14,
                Title = "PS5 Console",
                Description = "PlayStation 5 disc edition, sealed.",
                StartingPrice = 300,
                MinimumBidIncrement = 20,
                Currency = "USD",
                StartTime = DateTime.UtcNow.AddDays(2),
                EndTime = DateTime.UtcNow.AddDays(6),
                CreatedAt = DateTime.UtcNow,
                Status = AuctionStatus.Active
            },
            new()
            {
                ProductId = 6,
                OwnerId = 15,
                Title = "MacBook Pro 2023",
                Description = "Latest M3 Pro MacBook Pro, 16 inch.",
                StartingPrice = 1200,
                MinimumBidIncrement = 50,
                Currency = "USD",
                StartTime = DateTime.UtcNow.AddDays(4),
                EndTime = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow,
                Status = AuctionStatus.Active
            },
            new()
            {
                ProductId = 7,
                OwnerId = 16,
                Title = "Rare Comic Book",
                Description = "First edition Spider-Man comic.",
                StartingPrice = 1000,
                MinimumBidIncrement = 100,
                Currency = "USD",
                StartTime = DateTime.UtcNow.AddDays(5),
                EndTime = DateTime.UtcNow.AddDays(8),
                CreatedAt = DateTime.UtcNow,
                Status = AuctionStatus.Pending
            },
        ];
    }

    internal static List<Bid> GetBids(List<Auction> auctions)
    {
        return
        [
            new() { AuctionId = auctions[0].Id, BidderId = 2, Amount = 110, Timestamp = DateTime.UtcNow.AddDays(-1) },
            new() { AuctionId = auctions[0].Id, BidderId = 3, Amount = 120, Timestamp = DateTime.UtcNow.AddDays(-1).AddHours(2) },
            new() { AuctionId = auctions[1].Id, BidderId = 4, Amount = 160, Timestamp = DateTime.UtcNow.AddDays(-2) },
            new() { AuctionId = auctions[2].Id, BidderId = 1, Amount = 220, Timestamp = DateTime.UtcNow.AddDays(-1) },
            new() { AuctionId = auctions[2].Id, BidderId = 2, Amount = 240, Timestamp = DateTime.UtcNow.AddDays(-1).AddHours(3) },
            new() { AuctionId = auctions[2].Id, BidderId = 1, Amount = 260, Timestamp = DateTime.UtcNow },
            new() { AuctionId = auctions[3].Id, BidderId = 5, Amount = 525, Timestamp = DateTime.UtcNow },
            new() { AuctionId = auctions[3].Id, BidderId = 6, Amount = 550, Timestamp = DateTime.UtcNow.AddHours(1) },
            new() { AuctionId = auctions[4].Id, BidderId = 7, Amount = 320, Timestamp = DateTime.UtcNow.AddDays(-1) },
            new() { AuctionId = auctions[4].Id, BidderId = 8, Amount = 340, Timestamp = DateTime.UtcNow },
            new() { AuctionId = auctions[5].Id, BidderId = 9, Amount = 1250, Timestamp = DateTime.UtcNow },
            new() { AuctionId = auctions[6].Id, BidderId = 10, Amount = 1100, Timestamp = DateTime.UtcNow },
            new() { AuctionId = auctions[6].Id, BidderId = 11, Amount = 1200, Timestamp = DateTime.UtcNow.AddHours(2) },
        ];
    }
}
