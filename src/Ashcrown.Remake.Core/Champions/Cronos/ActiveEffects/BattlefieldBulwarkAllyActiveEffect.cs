using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;

namespace Ashcrown.Remake.Core.Champions.Cronos.ActiveEffects;

public class BattlefieldBulwarkAllyActiveEffect : ActiveEffectBase
{
    public BattlefieldBulwarkAllyActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CronosConstants.BattlefieldBulwarkAllyActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = $"- If this champion is targeted by a new harmful non-Affliction ability they will become {"invulnerable".HighlightInPurple()} for 1 turn";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override void ReceiveReaction(IAbility ability)
    {
        if (!ability.AbilityClassesContains(AbilityClass.Affliction) && ability.Harmful
                                                            && ability.Owner.BattlePlayer.PlayerNo != Target.BattlePlayer.PlayerNo) {
            Target.ChampionController.ReceiveActiveEffect(
                new BattlefieldBulwarkInvulnerableActiveEffect(OriginAbility, Target),
                true, new AppliedAdditionalLogic());
        }
    }
}