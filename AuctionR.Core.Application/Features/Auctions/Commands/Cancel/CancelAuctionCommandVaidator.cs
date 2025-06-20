﻿using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Features.Auctions.Commands.Cancel;

public sealed class CancelAuctionCommandVaidator : AbstractValidator<CancelAuctionCommand>
{
    public CancelAuctionCommandVaidator()
    {
        RuleFor(x => x.AuctionId).ValidId("AuctionId");

        RuleFor(x => x.UserId).ValidId("UserId");
    }
}
