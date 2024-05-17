using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Jane.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Jane.Champion;

public class Jane : ChampionBase<JaneConstants>
{
    public Jane(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new Benji(this),
            new CounterShot(this),
            new Flashbang(this),
            new Misdirection(this));
        
        Abilities[0].Add(new GoForTheThroat(this));
    }

    public override int AiApplyChampionSpecificPenalty(int totalPoints, IAbility ability)
    {
        if (ActiveEffectController.ActiveEffectPresentByActiveEffectName(JaneConstants.BenjiActiveEffect)
            && ability is { Harmful: true, AfflictionDamage: false }) {
            return totalPoints - (AiCalculatorConstants.BaseDamagePoints + 10);
        }

        return totalPoints;
    }
}