using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Arabela.Champion;

namespace Ashcrown.Remake.Core.Champions.Arabela.ActiveEffects;

public class DivineTrapEndActiveEffect : ActiveEffectBase
{
    public DivineTrapEndActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(ArabelaConstants.DivineTrapEndActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Description = $"- {ArabelaConstants.DivineTrap.HighlightInGold()} ended on this champion\n";
        Duration = Duration1;
        TimeLeft = Duration1;
        NoTick = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"{Description}{"ENDS THIS TURN".HighlightInGold()}";
    }

    public override void StartTurnChecks()
    {
        Target.ActiveEffectController.RemoveActiveEffect(this);
    }
}