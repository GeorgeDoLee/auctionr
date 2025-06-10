using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Features.Auctions.Commands.Start;

public sealed class StartAuctionCommandValidator : AbstractValidator<StartAuctionCommand>
{
    public StartAuctionCommandValidator()
    {
        RuleFor(x => x.AuctionId).ValidId("AuctionId");

        RuleFor(x => x.UserId).ValidId("AuctionId");
    }
}
