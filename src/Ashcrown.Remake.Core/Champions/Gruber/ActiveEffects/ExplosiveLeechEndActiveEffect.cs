using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;

namespace Ashcrown.Remake.Core.Champions.Gruber.ActiveEffects;

public class ExplosiveLeechEndActiveEffect : ActiveEffectBase
{
    public ExplosiveLeechEndActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(GruberConstants.ExplosiveLeechEndActiveEffect, originAbility, championTarget)
    {
        Duration1 = 1;

        Description = $"- {GruberConstants.ExplosiveLeech.HighlightInGold()} has ended\n";
        Duration = Duration1;
        TimeLeft = Duration1;
        NoTick = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return Description + $"{"ENDS THIS TURN".HighlightInGold()}";
    }
}