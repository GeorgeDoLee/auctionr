using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Features.Auctions.Commands.Create;

internal sealed class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionCommandValidator()
    {
        RuleFor(x => x.ProductId).ValidId("Product Id");

        RuleFor(x => x.OwnerId).ValidId("Owner Id");

        RuleFor(x => x.StartingPrice)
            .NotEmpty().WithMessage("Starting price is required.")
            .GreaterThan(0).WithMessage("Starting price must be greater than 0.");

        RuleFor(x => x.MinimumBidIncrement)
            .NotEmpty().WithMessage("MinimumBidIncrement is required.")
            .GreaterThanOrEqualTo(0).WithMessage("MinimumBidIncrement must be greater than or equal to 0.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .Matches("^[A-Z]{3}$").WithMessage("Currency must be 3 uppercase letters (ISO format).");

        RuleFor(x => x.Title)
            .MaximumLength(100).WithMessage("Title can't be longer than 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description can't be longer than 2000 characters.");

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
