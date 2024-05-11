using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dex.Champion;

namespace Ashcrown.Remake.Core.Champions.Dex.ActiveEffects;

public class NightbladeActiveEffect : ActiveEffectBase
{
    public NightbladeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(DexConstants.NightbladeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        DestructibleDefense1 = originAbility.DestructibleDefense1;
        
        Description = $"- {DexConstants.ShurikenThrow.HighlightInGold()} cost is reduced by <sprite=4>\n" +
                           $"- {DexConstants.Garrote.HighlightInGold()} affects all enemies and its cost is reduced by <sprite=4>\n" +
                           $"- This champion is {"invulnerable".HighlightInPurple()} to Physical and Strategic abilities";
        Duration = Duration1;
        TimeLeft = Duration1;
        DestructibleDefense = DestructibleDefense1;
        Invulnerability = originAbility.Invulnerability;
        TypeOfInvulnerability = originAbility.TypeOfInvulnerability;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        if (DestructibleDefense > 0) {
            return Description + "\n" 
                                    + $"- This champion has {$"{DestructibleDefense} points of destructible defense".HighlightInYellow()}" 
                                    + GetTimeLeftAffix();
        }

        return Description + GetTimeLeftAffix();
    }

    public override void OnAdd()
    {
        if (Target.AbilityController.GetMyAbilityByName(DexConstants.ShurikenThrow) != null) {
            Target.AbilityController.GetMyAbilityByName(DexConstants.ShurikenThrow)!.RandomCostModifier= -1;
        }
		
        if (Target.AbilityController.GetMyAbilityByName(DexConstants.Garrote) != null) {
            Target.AbilityController.GetMyAbilityByName(DexConstants.Garrote)!.RandomCostModifier = -1;
            Target.AbilityController.GetMyAbilityByName(DexConstants.Garrote)!.Target = AbilityTarget.Enemies;
        }
    }

    public override void OnRemove()
    {
        if (Target.AbilityController.GetMyAbilityByName(DexConstants.ShurikenThrow) != null) {
            Target.AbilityController.GetMyAbilityByName(DexConstants.ShurikenThrow)!.RemoveCostModifier(1);
        }
		
        if (Target.AbilityController.GetMyAbilityByName(DexConstants.Garrote) != null) {
            Target.AbilityController.GetMyAbilityByName(DexConstants.Garrote)!.RemoveCostModifier(1);
        }
		
        if (Target.ActiveEffectController.GetActiveEffectCountByName(DexConstants.NightbladeActiveEffect) == 1) {
            if (Target.AbilityController.GetMyAbilityByName(DexConstants.Garrote) != null) {
                Target.AbilityController.GetMyAbilityByName(DexConstants.Garrote)!.Target = AbilityTarget.Enemy;
            }
        }
    }
}