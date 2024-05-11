using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Ash.Champion;

namespace Ashcrown.Remake.Core.Champions.Ash.ActiveEffects;

public class FireShieldMeActiveEffect : ActiveEffectBase
{
    public FireShieldMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AshConstants.FireShieldMeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
		
        
        Description = $"- If an enemy uses a new Physical ability on {Target.Name}, " +
                      $"they will be dealt {$"{Damage1} magic damage".HighlightInBlue()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Source = true;
        Buff = originAbility.Buff;
        Damaging = originAbility.Damaging;
        MagicDamage = originAbility.MagicDamage;
		
        EndsOnTargetDeath = true;
        PauseOnCasterStun = true;
        EndsOnCasterDeath = true;
        PauseOnTargetInvulnerability = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return Description + "\n" + GetActionControlDescription();
    }

    public override void OnApply()
    {
        OnApplyActionControlMe();
    }

    public override void ReceiveReaction(IAbility ability)
    {
        if (ability.AbilityClassesContains(AbilityClass.Physical) && ability.Harmful
                                                         && !Paused 
                                                         && ability.Owner.BattlePlayer.PlayerNo 
                                                         != Target.BattlePlayer.PlayerNo) {
            Target.ChampionController.DealActiveEffectDamage(Damage1, ability.Owner, 
                this, new AppliedAdditionalLogic());
        }
    }
}