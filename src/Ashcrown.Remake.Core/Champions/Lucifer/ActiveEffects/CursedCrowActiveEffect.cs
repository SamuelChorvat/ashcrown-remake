using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lucifer.Champion;

namespace Ashcrown.Remake.Core.Champions.Lucifer.ActiveEffects;

public class CursedCrowActiveEffect : ActiveEffectBase
{
    public CursedCrowActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(LuciferConstants.CursedCrowActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = 10;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Hidden = true;
        Infinite = true;
        Stackable = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- {LuciferConstants.DarkChalice.HighlightInOrange()} will deal an additional {$"{Stacks*10} magic damage".HighlightInBlue()} to this champion\n"
               + $"- This ability is {"invisible".HighlightInPurple()}"
               + GetTimeLeftAffix();
    }

    public override void StartTurnChecks()
    {
        if (!OriginAbility.Owner.Alive) {
            Target.ActiveEffectController.RemoveActiveEffect(this);
        }
    }

    public override void EndTurnChecks()
    {
        if (!OriginAbility.Owner.Alive) {
            Target.ActiveEffectController.RemoveActiveEffect(this);
        }
    }
}