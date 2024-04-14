using FluentValidation;

namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound.Validators;

public class EndTurnAbilityValidator : AbstractValidator<EndTurnAbility>
{
    public EndTurnAbilityValidator()
    {
        RuleFor(x => x.Order).InclusiveBetween(1, 3);
        RuleFor(x => x.CasterNo).InclusiveBetween(1, 3);
        RuleFor(x => x.AbilityNo).InclusiveBetween(1, 4);
        RuleFor(x => x.Targets).NotNull();
        RuleFor(x => x.Targets!.Length).Equal(6);
        RuleForEach(x => x.Targets).InclusiveBetween(-1, 1);
    }
}