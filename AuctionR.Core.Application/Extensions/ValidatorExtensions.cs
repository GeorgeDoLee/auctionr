using FluentValidation;

namespace AuctionR.Core.Application.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilder<T, int> ValidId<T>(this IRuleBuilder<T, int> ruleBuilder, string identifierName = "Id")
    {
        return ruleBuilder
            .NotEmpty().WithMessage($"{identifierName} is required.")
            .GreaterThan(0).WithMessage($"{identifierName} must be greater than 0.");
    }
}
