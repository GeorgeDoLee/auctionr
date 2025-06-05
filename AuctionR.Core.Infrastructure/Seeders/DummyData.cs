using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Enums;

namespace AuctionR.Core.Infrastructure.Seeders;

internal static class DummyData
{
    internal static IEnumerable<Auction> Auctions =>
    [
        new Auction
        {
            ProductId = 1,
            OwnerId = 10,
            Title = "Jersey",
            Description = "Jersey worn by messi.",
            StartingPrice = 100,
            MinimumBidIncrement = 10,
            Currency = "USD",
            StartTime = DateTime.UtcNow.AddDays(15),
            EndTime = DateTime.UtcNow.AddDays(20),
            CreatedAt = DateTime.UtcNow,
            HighestBidAmount = 0,
            HighestBidderId = null,
            Status = AuctionStatus.Active,
        },
        new Auction
        {
            ProductId = 2,
            OwnerId = 11,
            Title = "Ring",
            Description = "Diamond engagement ring.",
            StartingPrice = 150,
            MinimumBidIncrement = 15,
            Currency = "USD",
            StartTime = DateTime.UtcNow.AddDays(10),
            EndTime = DateTime.UtcNow.AddDays(15),
            CreatedAt = DateTime.UtcNow,
            HighestBidAmount = 0,
            HighestBidderId = null,
            Status = AuctionStatus.Pending,
        },
        new Auction
        {
            ProductId = 1,
            OwnerId = 10,
            Title = "Ball",
            Description = "World cup 2026 original ball.",
            StartingPrice = 200,
            MinimumBidIncrement = 20,
            Currency = "USD",
            StartTime = DateTime.UtcNow.AddDays(25),
            EndTime = DateTime.UtcNow.AddDays(30),
            CreatedAt = DateTime.UtcNow,
            HighestBidAmount = 0,
            HighestBidderId = null,
            Status = AuctionStatus.Active,
        },
    ];

    internal static IEnumerable<Bid> Bids =>
    [
        new Bid
        {
            AuctionId = 3,
            BidderId = 1,
            Amount = 200,
            Timestamp = new DateTime(2025, 5, 29),
        },
        new Bid
        {
            AuctionId = 3,
            BidderId = 2,
            Amount = 125,
            Timestamp = new DateTime(2025, 5, 30),
        },
        new Bid
        {
            AuctionId = 3,
            BidderId = 1,
            Amount = 250,
            Timestamp = new DateTime(2025, 6, 1),
        },
        new Bid
        {
            AuctionId = 1,
            BidderId = 3,
            Amount = 115,
            Timestamp = new DateTime(2025, 5, 31),
        },
    ];
}