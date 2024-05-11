using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Akio.Champion;

namespace Ashcrown.Remake.Core.Champions.Akio.ActiveEffects;

public class MasterfulSliceActiveEffect : ActiveEffectBase
{
    public MasterfulSliceActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AkioConstants.MasterfulSliceActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        Damage2 = originAbility.Damage2;
        Description = $"- This champion will receive {$"{Damage1} physical damage".HighlightInOrange()}\n"
                      + $"- If this champion uses a new non-Strategic ability they will receive {$"{Damage2} physical damage".HighlightInOrange()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Damaging = originAbility.Damaging;
        PhysicalDamage = originAbility.PhysicalDamage;
    }

    public override void OnApply()
    {
        OnApplyDot();
    }

    public override void EndTurnChecks()
    {
        if(Target.AbilityController.LastUsedAbility != null 
           && !Target.AbilityController.LastUsedAbility.AbilityClassesContains(AbilityClass.Strategic)) {
            Target.ChampionController.ReceiveActiveEffectDamage(Damage2, this, new AppliedAdditionalLogic());
        }
    }
}