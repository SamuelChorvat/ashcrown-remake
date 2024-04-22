using FluentValidation;

namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound.Validators;

public class GetUsableAbilitiesValidator : AbstractValidator<GetUsableAbilities>
{
    public GetUsableAbilitiesValidator()
    {
        RuleFor(x => x.CurrentEnergy).NotNull();
        RuleFor(x => x.CurrentEnergy!.Length).Equal(4);
        RuleForEach(x => x.CurrentEnergy).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ToSubtract)
            .NotNull().InclusiveBetween(0,25);
    }
}