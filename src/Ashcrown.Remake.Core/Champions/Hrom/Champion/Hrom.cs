using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Hrom.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Hrom.Champion;

public class Hrom : ChampionBase<HromConstants>
{
    public Hrom(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new LightningStrike(this),
            new LightningBarrier(this),
            new LightningStorm(this),
            new StormShield(this));
    }

    public override bool AiCanCounterAbilitySelf(IAbility ability)
    {
        return ability.Harmful
               && !ability.AbilityClassesContains(AbilityClass.Strategic)
               && ability.Owner.BattlePlayer.PlayerNo != BattlePlayer.PlayerNo
               && !AbilityController.IsStunnedToUseAbility(CurrentAbilities[1])
               && (CurrentAbilities[1].Active || CurrentAbilities[1].JustUsed);
    }

    public override bool AiCanCounterAbilityTarget(IAbility ability)
    {
        return AiCanCounterAbilitySelf(ability);
    }
}