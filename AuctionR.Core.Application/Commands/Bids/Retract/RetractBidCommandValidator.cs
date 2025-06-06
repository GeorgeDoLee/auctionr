using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Commands.Bids.Retract;

public class RetractBidCommandValidator : AbstractValidator<RetractBidCommand>
{
    public RetractBidCommandValidator()
    {
        RuleFor(x => x.BidId).ValidId("BidId");
        RuleFor(x => x.UserId).ValidId("UserId");
    }
}
