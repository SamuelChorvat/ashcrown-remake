using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;

namespace Ashcrown.Remake.Core.Champions.Moroz.ActiveEffects;

public class IceBarrierActiveEffect : ActiveEffectBase
{
    public IceBarrierActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(MorozConstants.IceBarrierActiveEffect, originAbility, championTarget)
    {
		
        Duration1 = originAbility.Duration1;
        
        Description = "- Until this champion is targeted by new enemy Strategic ability, they will <color=#FF00CD>ignore</color> all <color=#FF00CD>stun</color> effects";
        Duration = Duration1;
        TimeLeft = Duration1;
        IgnoreStuns = originAbility.IgnoreStuns;
        Unique = true;
		
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Infinite = true;
    }

    public override void ReceiveReaction(IAbility ability)
    {
        if (ability.AbilityClassesContains(AbilityClass.Strategic)
            && ability.Owner.BattlePlayer.PlayerNo != Target.BattlePlayer.PlayerNo) {
            Target.ActiveEffectController.RemoveActiveEffect(this);
        }
    }
}