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
            StartingPrice = 100,
            MinimumBidIncrement = 10,
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddDays(3),
            HighestBidAmount = 0,
            HighestBidderId = null,
            Status = AuctionStatus.Pending,
        },
        new Auction
        {
            ProductId = 2,
            StartingPrice = 150,
            MinimumBidIncrement = 15,
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddDays(7),
            HighestBidAmount = 0,
            HighestBidderId = null,
            Status = AuctionStatus.Pending,
        },
        new Auction
        {
            ProductId = 1,
            StartingPrice = 200,
            MinimumBidIncrement = 20,
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddDays(10),
            HighestBidAmount = 0,
            HighestBidderId = null,
            Status = AuctionStatus.Pending,
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