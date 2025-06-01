using FluentValidation;

namespace AuctionR.Core.Application.Commands.Auctions.Update;

public class UpdateAuctionCommandValidator : AbstractValidator<UpdateAuctionCommand>
{
    public UpdateAuctionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID is required.")
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        RuleFor(x => x.StartingPrice)
            .NotEmpty().WithMessage("Starting price is required.")
            .GreaterThan(0).WithMessage("Starting price must be greater than 0.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time is required.")
            .Must(start => start > DateTime.UtcNow)
            .WithMessage("Start time must be in the future.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required.")
            .Must((cmd, end) => end > cmd.StartTime)
            .WithMessage("End time must be after start time.");

        RuleFor(x => x.HighestBidAmount)
            .GreaterThanOrEqualTo(x => x.StartingPrice)
            .When(x => x.HighestBidAmount.HasValue)
            .WithMessage("Highest bid amount cannot be less than the starting price.");

        RuleFor(x => x.HighestBidderId)
            .GreaterThan(0)
            .When(x => x.HighestBidderId.HasValue)
            .WithMessage("Highest bidder ID must be greater than 0 if provided.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid auction status.");
    }
}

