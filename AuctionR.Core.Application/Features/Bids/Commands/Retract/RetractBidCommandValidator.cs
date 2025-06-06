using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Features.Bids.Commands.Retract;

internal sealed class RetractBidCommandValidator : AbstractValidator<RetractBidCommand>
{
    public RetractBidCommandValidator()
    {
        RuleFor(x => x.BidId).ValidId("BidId");
        RuleFor(x => x.UserId).ValidId("UserId");
    }
}
