using AuctionR.Core.Domain.Primitives;

namespace AuctionR.Core.Domain.Entities;

public class Bid : Entity
{
    public int AuctionId { get; set; }

    public int BidderId { get; set; }

    public decimal Amount { get; set; }

    public DateTime Timestamp { get; set; }


    public bool IsRetractable(int retractableSeconds)
    {
        if (DateTime.UtcNow > Timestamp.AddSeconds(retractableSeconds))
        {
            return false;
        }

        return true;
    }
}
