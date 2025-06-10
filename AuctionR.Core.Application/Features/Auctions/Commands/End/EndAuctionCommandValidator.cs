using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Features.Auctions.Commands.End;

public sealed class EndAuctionCommandValidator : AbstractValidator<EndAuctionCommand>
{
    public EndAuctionCommandValidator()
    {
        RuleFor(x => x.AuctionId).ValidId("AuctionId");

        RuleFor(x => x.UserId).ValidId("UserId");
    }
}
