using FluentValidation;

namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound.Validators;

public class GetTargetsValidator : AbstractValidator<GetTargets>
{
    public GetTargetsValidator()
    {
        RuleFor(x => x.ChampionNo)
            .NotNull().InclusiveBetween(1,3);
        RuleFor(x => x.AbilityNo)
            .NotNull().InclusiveBetween(1,4);
    }
}