using FluentValidation;

namespace AuctionR.Core.Application.Commands.Auctions.Create;

public class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.")
            .GreaterThan(0).WithMessage("ProductId must be greater than 0.");

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
    }
}
