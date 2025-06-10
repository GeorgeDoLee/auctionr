using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Features.Auctions.Commands.Postpone;

public sealed class PostponeAuctionCommandValidator : AbstractValidator<PostponeAuctionCommand>
{
    public PostponeAuctionCommandValidator()
    {
        RuleFor(x => x.AuctionId).ValidId("AuctionId");

        RuleFor(x => x.UserId).ValidId("UserId");

        RuleFor(x => x.StartTime)
             .NotEmpty().WithMessage("Start time is required.")
             .Must(start => start > DateTime.UtcNow)
             .WithMessage("Start time must be in the future.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required.")
            .Must((cmd, end) => end > cmd.StartTime)
            .WithMessage("End time must be after start time.");
    }
}
