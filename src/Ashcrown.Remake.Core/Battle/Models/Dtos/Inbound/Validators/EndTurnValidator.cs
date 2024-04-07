using FluentValidation;

namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound.Validators;

public class EndTurnValidator : AbstractValidator<EndTurn>
{
    public EndTurnValidator()
    {
        RuleFor(x => x.SpentEnergy).NotNull();
        RuleFor(x => x.SpentEnergy!.Length).Equal(4);
        RuleForEach(x => x.EndTurnAbilities).SetValidator(new EndTurnAbilityValidator());
    }
}