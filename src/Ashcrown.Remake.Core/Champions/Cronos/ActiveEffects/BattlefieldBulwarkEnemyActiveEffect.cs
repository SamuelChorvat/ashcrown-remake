using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;

namespace Ashcrown.Remake.Core.Champions.Cronos.ActiveEffects;

public class BattlefieldBulwarkEnemyActiveEffect : ActiveEffectBase
{
    public BattlefieldBulwarkEnemyActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CronosConstants.BattlefieldBulwarkEnemyActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        
        Description = $"- This champion cannot {"reduce damage".HighlightInPurple()} or become {"invulnerable".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        DisableDamageReceiveReduction = originAbility.DisableDamageReceiveReduction;
        DisableInvulnerability = originAbility.DisableInvulnerability;
    }
}