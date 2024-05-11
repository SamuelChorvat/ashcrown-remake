using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;

namespace Ashcrown.Remake.Core.Champions.Cronos.ActiveEffects;

public class BattlefieldBulwarkInvulnerableActiveEffect : ActiveEffectBase
{
    public BattlefieldBulwarkInvulnerableActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CronosConstants.BattlefieldBulwarkInvulnerableActiveEffect, originAbility, championTarget)
    {
        Duration1 = 2;
        
        Description = $"- This champion is {"invulnerable".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    // Invulnerability starts the next turn
    public override void OnApply()
    {
        TickDown();
        if (TimeLeft >= Duration1) return;
        Invulnerability = true;
        TypeOfInvulnerability = [AbilityClass.All];
    }
}