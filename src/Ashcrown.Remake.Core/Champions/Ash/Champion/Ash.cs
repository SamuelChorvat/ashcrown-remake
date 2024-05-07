using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Ash.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Ash.Champion;

public class Ash : ChampionBase<AshConstants>
{
    public Ash(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new FireShield(this),
            new Fireball(this),
            new PhoenixFlames(this),
            new FireBlock(this));
        
        Abilities[1].Add(new FireWhirl(this));
        Abilities[1].Add(new Inferno(this));
    }

    public override int AiApplyChampionSpecificPenalty(int totalPoints, IAbility ability)
    {
        if (ActiveEffectController.ActiveEffectPresentByActiveEffectName(AshConstants.FireShieldMeActiveEffect)
            && ability.Harmful && ability.AbilityClassesContains(AbilityClass.Physical)) {
            return totalPoints - (AiCalculatorConstants.BaseDamagePoints + 10);
        }

        return totalPoints;
    }
}