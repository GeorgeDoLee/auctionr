﻿using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Commands.Auctions.End;

public class EndAuctionCommandValidator : AbstractValidator<EndAuctionCommand>
{
    public EndAuctionCommandValidator()
    {
        RuleFor(x => x.AuctionId).ValidId("AuctionId");

        RuleFor(x => x.UserId).ValidId("UserId");
    }
}
