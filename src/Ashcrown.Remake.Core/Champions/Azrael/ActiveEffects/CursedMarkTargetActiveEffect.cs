using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;

namespace Ashcrown.Remake.Core.Champions.Azrael.ActiveEffects;

public class CursedMarkTargetActiveEffect : ActiveEffectBase
{
    public CursedMarkTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AzraelConstants.CursedMarkTargetActiveEffect, originAbility, championTarget)
    {
        Damage1 = originAbility.Damage1;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = true;
        Damaging = originAbility.Damaging;
        PiercingDamage = originAbility.PiercingDamage;
        MagicDamage = true;
        Infinite = true;
        Debuff = originAbility.Debuff;
        Stackable = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- This champion will receive {$"{Damage1*Stacks} {"piercing".HighlightInBold()} magic damage".HighlightInBlue()}" + GetTimeLeftAffix();
    }

    public override void OnApply()
    {
        Target.ChampionController.ReceiveActiveEffectDamage(Damage1 * Stacks, this, new AppliedAdditionalLogic());
        TickDown();
    }
}