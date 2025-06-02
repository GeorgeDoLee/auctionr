using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Commands.Auctions.Update;

public class UpdateAuctionCommandValidator : AbstractValidator<UpdateAuctionCommand>
{
    public UpdateAuctionCommandValidator()
    {
        RuleFor(x => x.Id).ValidId();

        RuleFor(x => x.ProductId).ValidId("Product Id");

        RuleFor(x => x.StartingPrice)
            .NotEmpty().WithMessage("Starting price is required.")
            .GreaterThan(0).WithMessage("Starting price must be greater than 0.");

        RuleFor(x => x.MinimumBidIncrement)
            .NotEmpty().WithMessage("MinimumBidIncrement is required.")
            .GreaterThanOrEqualTo(0).WithMessage("MinimumBidIncrement must be greater than or equal to 0.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time is required.")
            .Must(start => start > DateTime.UtcNow)
            .WithMessage("Start time must be in the future.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required.")
            .Must((cmd, end) => end > cmd.StartTime)
            .WithMessage("End time must be after start time.");

        RuleFor(x => x.HighestBidderId)
            .GreaterThan(0)
            .When(x => x.HighestBidderId.HasValue)
            .WithMessage("Highest bidder ID must be greater than 0 if provided.");

        RuleFor(x => x.HighestBidAmount)
            .GreaterThanOrEqualTo(x => x.StartingPrice)
            .When(x => x.HighestBidderId.HasValue)
            .WithMessage("Highest bid amount cannot be less than the starting price.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid auction status.");
    }
}

