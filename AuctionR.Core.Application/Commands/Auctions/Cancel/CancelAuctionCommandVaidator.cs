using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Commands.Auctions.Cancel;

public class CancelAuctionCommandVaidator : AbstractValidator<CancelAuctionCommand>
{
    public CancelAuctionCommandVaidator()
    {
        RuleFor(x => x.Id).ValidId();
    }
}
