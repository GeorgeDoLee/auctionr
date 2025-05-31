using FluentValidation;

namespace AuctionR.Core.Application.Commands.Auctions.Delete;

public class DeleteAuctionCommandValidator : AbstractValidator<DeleteAuctionCommand>
{
    public DeleteAuctionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
