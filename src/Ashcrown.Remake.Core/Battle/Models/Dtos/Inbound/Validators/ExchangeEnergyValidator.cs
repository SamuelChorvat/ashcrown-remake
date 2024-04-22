using Ashcrown.Remake.Core.Battle.Interfaces;
using FluentValidation;

namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound.Validators;

public class ExchangeEnergyValidator : AbstractValidator<ExchangeEnergy>
{
    public ExchangeEnergyValidator(IBattlePlayer battlePlayer)
    {
        RuleFor(x => x.SpentEnergy).NotNull();
        RuleFor(x => x.SpentEnergy!.Length).Equal(4);
        RuleForEach(x => x.SpentEnergy).GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.WantedEnergy).NotNull();
        RuleFor(x => x.WantedEnergy!.Length).Equal(4);
        RuleForEach(x => x.WantedEnergy).GreaterThanOrEqualTo(0);

        RuleFor(x => x.WantedEnergy!.Sum() * Math.Max(1, battlePlayer.GetAliveChampions().Count))
            .Equal(x => x.SpentEnergy!.Sum());
    }
}