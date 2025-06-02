using FluentValidation;

namespace AuctionR.Core.Application.Commands.Auctions.End;

public class EndAuctionCommandValidator : AbstractValidator<EndAuctionCommand>
{
    public EndAuctionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
