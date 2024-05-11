using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;

namespace Ashcrown.Remake.Core.Champions.Cronos.ActiveEffects;

public class PulseCannonTargetActiveEffect : ActiveEffectBase
{
    public PulseCannonTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CronosConstants.PulseCannonTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        
        Description = $"- This champion will receive {$"{Damage1} affliction damage".HighlightInRed()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Damaging = originAbility.Damaging;
        AfflictionDamage = originAbility.AfflictionDamage;

        EndsOnTargetDeath = true;
        EndsOnCasterDeath = true;
        PauseOnCasterStun = true;
        PauseOnTargetInvulnerability = true;
    }

    public override void OnApply()
    {
        if (!Paused) {
            OriginAbility.Owner.ChampionController
                .DealActiveEffectDamage(Damage1, Target, this, new AppliedAdditionalLogic());
        }

        TickDown();

        if (CasterLink!.TimeLeft != TimeLeft) {
            CasterLink.TimeLeft = TimeLeft;
        }
    }
}