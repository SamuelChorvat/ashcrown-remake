using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Ai.Models;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Moroz.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Thasar.ActiveEffects;

namespace Ashcrown.Remake.Core.Ability;

public class AbilityController(
    IChampion owner,
    IActiveEffectFactory activeEffectFactory) : IAbilityController
{
    private bool _aiAbilitySelected;

    public IAbility? LastUsedAbility { get; private set; }

    public bool UsedNewAbility { get; private set; }

    public bool AiAbilitySelected
    {
        get => !owner.Alive || _aiAbilitySelected;
        set => _aiAbilitySelected = value;
    }

    public bool UseAbility(IAbility ability, int[] targets)
    {
        UseAbilityChecks(ability, targets);
        return ability.AbilityType switch
        {
            AbilityType.EnemyDebuff => UseEnemyDebuff(targets, ability),
            AbilityType.AllyHeal => UseAllyHeal(targets, ability),
            AbilityType.AlliesHeal => UseAlliesHeal(targets, ability),
            AbilityType.EnemiesDebuff => UseEnemiesDebuff(targets, ability),
            AbilityType.AllyBuff => UseAllyBuff(targets, ability),
            AbilityType.AlliesBuff => UseAlliesBuff(targets, ability),
            AbilityType.AlliesBuffEnemiesDebuff => UseAlliesBuffEnemiesDebuff(targets, ability),
            AbilityType.EnemiesDamageAndDebuff => UseEnemiesDamageAndDebuff(targets, ability),
            AbilityType.EnemyDamageAndDebuff => UseEnemyDamageAndDebuff(targets, ability),
            AbilityType.EnemyDamage => UseEnemyDamage(targets, ability),
            AbilityType.EnemiesDamage => UseEnemiesDamage(targets, ability),
            AbilityType.EnemyEnergySteal => UseEnemyEnergySteal(targets, ability),
            AbilityType.EnemyActionControl => UseEnemyActionControl(targets, ability),
            AbilityType.EnemiesActionControl => UseEnemiesActionControl(targets, ability),
            AbilityType.AllyOrEnemyActiveEffect => UseAllyOrEnemyActiveEffect(targets, ability),
            AbilityType.AllyActiveEffectOrEnemyDamage => UseAllyActiveEffectOrEnemyDamage(targets, ability),
            AbilityType.AllyOrEnemyDamage => UseAllyOrEnemyDamage(targets, ability),
            _ => throw new ArgumentOutOfRangeException(nameof(ability), "Ability type out of range")
        };
    }

    private bool UseAllyOrEnemyDamage(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }

        for (var i = 0; i < targets.Length; i++)
        {
            if (targets[i] != 1) continue;
            if (i < 3) {
                owner.BattlePlayer.Champions[i].ChampionController.TargetedByAbility(usedAbility);

                if (abilityReflected.Reflected) {
                    owner.BattlePlayer.Champions[i].ReceivedReflectedAbilities.Add(usedAbility);
                }
                var toDeal = usedAbility.Damage1;
                owner.ChampionController.DealAbilityDamage(toDeal,
                    owner.BattlePlayer.Champions[i],
                    usedAbility,
                    false,
                    new AppliedAdditionalLogic());

                usedAbility.OnUse();

                return true;
            } else {
                var toDeal = usedAbility.Damage1;
                owner.BattlePlayer.GetEnemyPlayer().Champions[i-3].ChampionController.TargetedByAbility(usedAbility);
                owner.ChampionController.DealAbilityDamage(toDeal,
                    owner.BattlePlayer.GetEnemyPlayer().Champions[i-3],
                    usedAbility,
                    false,
                    new AppliedAdditionalLogic());

                usedAbility.OnUse();

                return true;
            }
        }

        return false;
    }

    private bool UseAllyActiveEffectOrEnemyDamage(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }

        for (var i = 0; i < targets.Length; i++)
        {
            if (targets[i] != 1) continue;
            if (i < 3) {
                owner.BattlePlayer.Champions[i].ChampionController.TargetedByAbility(usedAbility);

                if (abilityReflected.Reflected) {
                    owner.BattlePlayer.Champions[i].ReceivedReflectedAbilities.Add(usedAbility);
                    var toDeal = usedAbility.Damage1;
                    owner.ChampionController.DealAbilityDamage(toDeal,
                        owner.BattlePlayer.Champions[i],
                        usedAbility,
                        false,
                        new AppliedAdditionalLogic());
                } else {
                    owner.ChampionController.DealActiveEffect(owner.BattlePlayer.Champions[i],
                        usedAbility,
                        activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!, 
                            usedAbility.ActiveEffectName!, 
                            usedAbility, owner.BattlePlayer.Champions[i]),
                        false, new AppliedAdditionalLogic());
                }

                usedAbility.OnUse();

                return true;
            }

            {
                var toDeal = usedAbility.Damage1;
                owner.BattlePlayer.GetEnemyPlayer().Champions[i-3].ChampionController.TargetedByAbility(usedAbility);
                owner.ChampionController.DealAbilityDamage(toDeal,
                    owner.BattlePlayer.GetEnemyPlayer().Champions[i-3],
                    usedAbility,
                    false,
                    new AppliedAdditionalLogic());

                usedAbility.OnUse();

                return true;
            }
        }

        return false;
    }

    private bool UseAllyOrEnemyActiveEffect(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }
		
        for (var i = 0; i < targets.Length; i++)
        {
            if (targets[i] != 1) continue;
            if (i < 3)
            {
                var activeEffect = activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!,
                    abilityReflected.Reflected
                        ? usedAbility.ActiveEffectEnemyName!
                        : usedAbility.ActiveEffectAllyName!,
                    usedAbility, owner.BattlePlayer.Champions[i]);
                activeEffect.Reflected = abilityReflected.Reflected;
                if (abilityReflected.Reflected) {
                    owner.BattlePlayer.Champions[i].ReceivedReflectedAbilities.Add(usedAbility);
                }

                owner.BattlePlayer.Champions[i].ChampionController.TargetedByAbility(usedAbility);
                owner.ChampionController.DealActiveEffect(owner.BattlePlayer.Champions[i],
                    usedAbility, 
                    activeEffect,
                    false, new AppliedAdditionalLogic());

                usedAbility.OnUse();
					
                return true;
            }

            owner.BattlePlayer.GetEnemyPlayer().Champions[i-3].ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealActiveEffect(owner.BattlePlayer.GetEnemyPlayer().Champions[i-3],
                usedAbility, 
                activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!, 
                    usedAbility.ActiveEffectEnemyName!, 
                    usedAbility, owner.BattlePlayer.GetEnemyPlayer().Champions[i-3]),
                false, new AppliedAdditionalLogic());

            usedAbility.OnUse();
					
            return true;
        }
		
        return false;
    }

    private bool UseEnemiesActionControl(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }
		
        var aeSource = activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!,
            usedAbility.ActiveEffectSourceName!, usedAbility, owner);
		
        for (var i = abilityReflected.Reflected ? 0 : 3; 
             i < (abilityReflected.Reflected ? targets.Length - 3 : targets.Length); i++)
        {
            if (targets[i] != 1) continue;
            var target = abilityReflected.Reflected ? owner.BattlePlayer.Champions[i]
                : owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
            if (abilityReflected.Reflected) {
                target.ReceivedReflectedAbilities.Add(usedAbility);
            }

            var aeTarget = activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!,
                usedAbility.ActiveEffectTargetName!, usedAbility, target);
            aeTarget.Reflected = abilityReflected.Reflected;
				
            aeTarget.CasterLink = aeSource;
            aeSource.ChildrenLinks.Add(aeTarget);

            target.ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealActiveEffect(target,
                usedAbility,
                aeTarget, 
                false, new AppliedAdditionalLogic());
        }
		
        owner.ChampionController.DealActiveEffect(owner,
            usedAbility, 
            aeSource, 
            true, new AppliedAdditionalLogic());

        usedAbility.OnUse();
		
        return true;
    }

    private bool UseEnemyActionControl(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }
		
        for (var i = abilityReflected.Reflected ? 0 : 3; 
             i < (abilityReflected.Reflected ? targets.Length - 3 : targets.Length); i++)
        {
            if (targets[i] != 1) continue;
            var target = abilityReflected.Reflected ? owner.BattlePlayer.Champions[i]
                : owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
            if (abilityReflected.Reflected) {
                target.ReceivedReflectedAbilities.Add(usedAbility);
            }

            var aeSource = activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!,
                usedAbility.ActiveEffectSourceName!, usedAbility, owner);
            var aeTarget = activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!,
                usedAbility.ActiveEffectTargetName!, usedAbility, target);
            aeTarget.Reflected = abilityReflected.Reflected;
				
            aeTarget.CasterLink = aeSource;
            aeSource.ChildrenLinks.Add(aeTarget);

            target.ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealActiveEffect(target,
                usedAbility,
                aeTarget, 
                false, new AppliedAdditionalLogic());
				
            owner.ChampionController.DealActiveEffect(owner,
                usedAbility, 
                aeSource, 
                true, new AppliedAdditionalLogic());

            usedAbility.OnUse();

            return true;
        }
		
        return false;
    }

    private bool UseEnemyEnergySteal(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }
		
        for (var i = abilityReflected.Reflected ? 0 : 3; 
             i < (abilityReflected.Reflected ? targets.Length - 3 : targets.Length); i++)
        {
            if (targets[i] != 1) continue;
            var target = abilityReflected.Reflected ? owner.BattlePlayer.Champions[i]
                : owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
            if (abilityReflected.Reflected) {
                target.ReceivedReflectedAbilities.Add(usedAbility);
            }

            target.ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealAbilityEnergySteal(
                target,
                usedAbility, 
                false, new AppliedAdditionalLogic());

            usedAbility.OnUse();

            return true;
        }

        return false;
    }

    private bool UseEnemiesDamage(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }
		
        var toDeal = usedAbility.Damage1;
		
        for (var i = abilityReflected.Reflected ? 0 : 3; 
             i < (abilityReflected.Reflected ? targets.Length - 3 : targets.Length); i++)
        {
            if (targets[i] != 1) continue;
            var target = abilityReflected.Reflected ? owner.BattlePlayer.Champions[i]
                : owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
            if (abilityReflected.Reflected) {
                target.ReceivedReflectedAbilities.Add(usedAbility);
            }

            target.ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealAbilityDamage(toDeal,
                target,
                usedAbility, 
                false, new AppliedAdditionalLogic());
        }

        usedAbility.OnUse();
		
        return true;
    }

    private bool UseEnemyDamage(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }
		
        var toDeal = usedAbility.Damage1;
		
        for (var i = abilityReflected.Reflected ? 0 : 3; 
             i < (abilityReflected.Reflected ? targets.Length - 3 : targets.Length); i++)
        {
            if (targets[i] != 1) continue;
            var target = abilityReflected.Reflected ? owner.BattlePlayer.Champions[i]
                : owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
            if (abilityReflected.Reflected) {
                target.ReceivedReflectedAbilities.Add(usedAbility);
            }

            target.ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealAbilityDamage(toDeal,
                target,
                usedAbility, 
                false, new AppliedAdditionalLogic());

            usedAbility.OnUse();
				
            return true;
        }
		
        return false;
    }

    private bool UseEnemyDamageAndDebuff(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }
		
        var toDeal = usedAbility.Damage1;
		
        for (var i = abilityReflected.Reflected ? 0 : 3; 
             i < (abilityReflected.Reflected ? targets.Length - 3 : targets.Length); i++)
        {
            if (targets[i] != 1) continue;
            var target = abilityReflected.Reflected ? owner.BattlePlayer.Champions[i]
                : owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
            if (abilityReflected.Reflected) {
                target.ReceivedReflectedAbilities.Add(usedAbility);
            }

            var activeEffect = activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!, 
                usedAbility.ActiveEffectName!, usedAbility, target);
            activeEffect.Reflected = abilityReflected.Reflected;

            target.ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealDamageAndAddActiveEffect(toDeal,
                target,
                usedAbility,
                activeEffect);

            usedAbility.OnUse();
				
            return true;
        }
		
        return false;
    }

    private bool UseEnemiesDamageAndDebuff(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }
		
        var toDeal = usedAbility.Damage1;
		
        for (var i = abilityReflected.Reflected ? 0 : 3; 
             i < (abilityReflected.Reflected ? targets.Length - 3 : targets.Length); i++)
        {
            if (targets[i] != 1) continue;
            var target = abilityReflected.Reflected ? owner.BattlePlayer.Champions[i]
                : owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
            if (abilityReflected.Reflected) {
                target.ReceivedReflectedAbilities.Add(usedAbility);
            }

            var activeEffect = activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!, 
                usedAbility.ActiveEffectName!, usedAbility, target);
            activeEffect.Reflected = abilityReflected.Reflected;

            target.ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealDamageAndAddActiveEffect(toDeal,
                target,
                usedAbility,
                activeEffect);
        }

        usedAbility.OnUse();
		
        return true;
    }

    private bool UseAlliesBuffEnemiesDebuff(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }

        for (var i = 0; i < targets.Length - 3; i++)
        {
            if (targets[i] != 1) continue;
            owner.BattlePlayer.Champions[i].ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealActiveEffect(owner.BattlePlayer.Champions[i],
                usedAbility, 
                activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!, 
                    usedAbility.ActiveEffectAlliesName!, usedAbility, 
                    owner.BattlePlayer.Champions[i]),
                false, new AppliedAdditionalLogic());
        }
		
        for (var i = abilityReflected.Reflected ? 0 : 3; 
             i < (abilityReflected.Reflected ? targets.Length - 3 : targets.Length); i++)
        {
            if (targets[i] != 1) continue;
            var target = abilityReflected.Reflected ? owner.BattlePlayer.Champions[i]
                : owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
            if (abilityReflected.Reflected) {
                target.ReceivedReflectedAbilities.Add(usedAbility);
            }

            var activeEffect = activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!,
                usedAbility.ActiveEffectEnemiesName!, usedAbility, target);
            activeEffect.Reflected = abilityReflected.Reflected;

            target.ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealActiveEffect(target,
                usedAbility,
                activeEffect,
                false, new AppliedAdditionalLogic());
        }

        usedAbility.OnUse();

        return true;
    }

    private bool UseAlliesBuff(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }
		
        for (var i = 0; i < targets.Length - 3; i++)
        {
            if (targets[i] != 1) continue;
            owner.BattlePlayer.Champions[i].ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealActiveEffect(owner.BattlePlayer.Champions[i],
                usedAbility, 
                activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!, 
                    usedAbility.ActiveEffectName!, 
                    usedAbility, owner.BattlePlayer.Champions[i]),
                false, new AppliedAdditionalLogic());
        }

        usedAbility.OnUse();
		
        return true;
    }

    private bool UseAllyBuff(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }
		
        for (var i = 0; i < targets.Length - 3; i++)
        {
            if (targets[i] != 1) continue;
            owner.BattlePlayer.Champions[i].ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealActiveEffect(owner.BattlePlayer.Champions[i],
                usedAbility, 
                activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!, 
                    usedAbility.ActiveEffectName!, usedAbility, owner.BattlePlayer.Champions[i]),
                false, new AppliedAdditionalLogic());

            usedAbility.OnUse();
				
            return true;
        }
		
        return false;
    }

    private bool UseEnemiesDebuff(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }
		
        for (var i = abilityReflected.Reflected ? 0 : 3; 
             i < (abilityReflected.Reflected ? targets.Length - 3 : targets.Length); i++)
        {
            if (targets[i] != 1) continue;
            var target = abilityReflected.Reflected ? owner.BattlePlayer.Champions[i]
                : owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
            if (abilityReflected.Reflected) {
                target.ReceivedReflectedAbilities.Add(usedAbility);
            }

            var activeEffect = activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!, 
                usedAbility.ActiveEffectName!, usedAbility, target);
            activeEffect.Reflected = abilityReflected.Reflected;

            target.ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealActiveEffect(target,
                usedAbility,
                activeEffect,
                false, new AppliedAdditionalLogic());
        }

        usedAbility.OnUse();
		
        return true;
    }

    private bool UseAlliesHeal(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var toHeal = usedAbility.Heal1;

        for (var i = 0; i < targets.Length - 3; i++)
        {
            if (targets[i] != 1) continue;
            owner.BattlePlayer.Champions[i].ChampionController.TargetedByAbility(usedAbility);

            owner.ChampionController.DealAbilityHealing(toHeal,
                owner.BattlePlayer.Champions[i],
                usedAbility, new AppliedAdditionalLogic());
        }

        usedAbility.OnUse();

        return true;
    }

    private bool UseAllyHeal(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }
		
        var toHeal = usedAbility.Heal1;
		
        for (var i = 0; i < targets.Length - 3; i++)
        {
            if (targets[i] != 1) continue;
            owner.BattlePlayer.Champions[i].ChampionController.TargetedByAbility(usedAbility);

            owner.ChampionController.DealAbilityHealing(toHeal,
                owner.BattlePlayer.Champions[i],
                usedAbility, new AppliedAdditionalLogic());

            usedAbility.OnUse();
				
            return true;
        }
		
        return false;
    }

    private bool UseEnemyDebuff(int[] targets, IAbility usedAbility)
    {
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }

        for (var i = abilityReflected.Reflected ? 0 : 3; 
             i < (abilityReflected.Reflected ? targets.Length - 3 : targets.Length); i++)
        {
            if (targets[i] != 1) continue;
            var target = abilityReflected.Reflected ? owner.BattlePlayer.Champions[i]
                : owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
            if (abilityReflected.Reflected) {
                target.ReceivedReflectedAbilities.Add(usedAbility);
            }

            var activeEffect = activeEffectFactory.CreateActiveEffect(usedAbility.ActiveEffectOwner!, 
                usedAbility.ActiveEffectName!, usedAbility, target);
            activeEffect.Reflected = abilityReflected.Reflected;

            target.ChampionController.TargetedByAbility(usedAbility);
            owner.ChampionController.DealActiveEffect(target,
                usedAbility,
                activeEffect,
                false, new AppliedAdditionalLogic());

            usedAbility.OnUse();

            return true;
        }
		
        return false;
    }

    public bool ClientCanUseAbilityChecks(IAbility ability)
    {
        if (!owner.Alive || !ability.Active) {
            return false;
        }
		
        return !IsStunnedToUseAbility(ability) && SpendEnergyClientCheck(ability);
    }

    private bool ClientCanUseAbilityChecks(IAbility ability, IReadOnlyList<int> currentEnergy, int toSubtract) {
        if (!owner.Alive || !ability.Active) {
            return false;
        }
		
        if (IsStunnedToUseAbility(ability)) {
            return false;
        }
		
        if(!SpendEnergyClientCheck(ability)) {
            return false;
        }
		
        for(var i = 0; i < 4; i++) {
            if (ability.GetCurrentCost()[i] > currentEnergy[i]) {
                return false;
            }
        }
		
        return ability.TotalCost(ability.GetCurrentCost()) + toSubtract
               <= currentEnergy.Sum();
    }

    private bool SpendEnergyClientCheck(IAbility ability)
    {
        if (owner.BattlePlayer.GetTotalEnergy() == 0 && !ability.IsFree()) {
            return false;
        }

        if (owner.BattlePlayer.GetTotalEnergy() < ability.GetTotalCurrentCost()) {
            return false;
        }

        for (var i = 0; i < 4; i++) {
            if (ability.GetCurrentCost()[i] > owner.BattlePlayer.Energy[i]) {
                return false;
            } 
        }
		
        return true;
    }

    public bool IsStunnedToUseAbility(IAbility ability)
    {
        if(!owner.Alive) {
            return true;
        }
		
        if (owner.ChampionController.IsIgnoringStuns()) {
            return false;
        }
		
        return !owner.ChampionController.IsIgnoringHarmful() && 
               owner.ActiveEffects.
                   Where((_, i) => !owner.ActiveEffects[i].Paused && ability.AbilityClasses.
                       Any(abilityClass => owner.ActiveEffects[i].StunnedContains(abilityClass))).Any();
    }

    public IAbility? GetMyAbilityByName(string abilityName)
    {
        return owner.Abilities.SelectMany(slotAbilities => slotAbilities).FirstOrDefault(ability => ability.Name.Equals(abilityName));
    }

    public IAbility GetCurrentAbility(int abilityNo)
    {
        return owner.CurrentAbilities[abilityNo - 1];
    }

    public void StartTurnFieldsReset()
    {
        UsedNewAbility = false;
        LastUsedAbility = null;
    }

    public void TickDownAbilitiesCooldowns()
    {
        if(!owner.Alive) {
            return;
        }

        foreach (var slotAbilities in owner.Abilities)
        {
            foreach (var ability in slotAbilities)
            {
                ability.TickDownCooldown();
            }
        }
    }

    public int GetNumberOfTargets(IEnumerable<int> targets)
    {
        return targets.Count(target => target == 1);
    }

    public int[] GetPossibleTargetsForAbility(int abilityNo)
    {
        var emptyTargets = new[] {0,0,0,0,0,0};
        return !owner.Alive ? emptyTargets : GetCurrentAbility(abilityNo).GetPossibleTargets();
    }

    public int[] GetUsableAbilities(int[] currentEnergy, int toSubtract)
    {
        int[] usable = [0, 0, 0, 0];
        
        for (var i = 1; i < 5; i++) {
            if (ClientCanUseAbilityChecks(GetCurrentAbility(i), currentEnergy, toSubtract) &&
                GetNumberOfTargets(GetPossibleTargetsForAbility(i)) > 0) {
                usable[i - 1] = 1;
            } else {
                usable[i - 1] = 0;
            }
        }
        
        return usable;
    }

    public void SetAiActiveAbilities(int[] currentResources, int toSubtract)
    {
        for (var i = 1; i < 5; i++) {
            if (ClientCanUseAbilityChecks(GetCurrentAbility(i), currentResources, toSubtract) &&
                GetNumberOfTargets(GetPossibleTargetsForAbility(i)) > 0) 
            {
                GetCurrentAbility(i).AiActive = true;
            } else {
                GetCurrentAbility(i).AiActive = false;
            }
        }
    }

    public AiMaximizedAbility? GetBestMaximizedAbility<TAiUtils,TAiPointsCalculator>() 
        where TAiUtils : IAiUtils 
        where TAiPointsCalculator : IAiPointsCalculator
    {
        AiMaximizedAbility? toReturn = null;

        for (var i = 1; i <= 4; i++) {
            if (!GetCurrentAbility(i).AiActive) {
                continue;
            }

            var maximizedAbility = GetCurrentAbility(i).AiMaximizeAbility<TAiPointsCalculator>();
            
            maximizedAbility.Ability = GetCurrentAbility(i);
            maximizedAbility.AbilityNo = i;
            maximizedAbility.Champion = owner;
            maximizedAbility.CasterNo = owner.ChampionNo;

            toReturn = TAiUtils.GetHigherPointsAbility(toReturn, maximizedAbility);
        }

        return toReturn;
    }
    
    private bool CounterOnEnemy(IAbility ability, int[] targets)
    {
        if (!ability.Counterable) {
            return false;
        }
		
        for (var i = 3; i < targets.Length; i++)
        {
            if (targets[i] != 1) continue;
            var targetCharacter = owner.BattlePlayer.GetEnemyPlayer().Champions[i-3];
            if (targetCharacter.ActiveEffects.Any(activeEffect => activeEffect.CounterOnEnemy(ability)))
            {
                return true;
            }
        }
		
        return false;
    }

    private AbilityReflected ReflectOnEnemy(IAbility ability, int[] targets)
    {
        if (!ability.Reflectable) {
            return new AbilityReflected(false, ability, null, targets);
        }

        for (var i = 3; i < targets.Length; i++)
        {
            if (targets[i] != 1) continue;
            var targetCharacter = owner.BattlePlayer.GetEnemyPlayer().Champions[i-3];
            foreach (var activeEffect in targetCharacter.ActiveEffects) {
                if (activeEffect.ReflectOnEnemy(ability)) {
                    return new AbilityReflected(true, ability, activeEffect, targets);
                }
            }
        }

        return new AbilityReflected(false, ability, null, targets);
    }

    private bool CounterOnMe(IAbility ability, int[] targets)
    {
        //TODO add ignore harmful check here ??
        return ability.Counterable && owner.ActiveEffects.Any(activeEffect => activeEffect.CounterOnMe(ability));
    }

    private void UseAbilityChecks(IAbility ability, int[] targets)
    {
        if(ability == null) {
            throw new Exception("Ability doesn't exist");
        }

        if (!ability.IsReady() || !ability.Active) {
            throw new Exception($"Ability not ready({ability.IsReady()}) or active({ability.Active})");
        }

        //TODO How should this work? Should champion killed by reflected ability be able to use selected ability? (Currently they are)
        if (!owner.Alive && !(owner.ReceivedReflectedAbilities.Count > 0)) {
            throw new Exception("Not alive");
        }
		
        if (ability.Target == AbilityTarget.Self && targets[owner.ChampionNo - 1] != 1
                                                 && GetNumberOfTargets(targets) == 1) {
            throw new Exception("Can only be selfcasted");
        }
		
        if (!ability.SelfCast && targets[owner.ChampionNo - 1] == 1) {
            throw new Exception("Can't be selfcasted");
        }
		
        if (IsStunnedToUseAbility(ability) && !owner.ReceivedReflectStun()) {
            throw new Exception("Stunned to use problem");
        }

        IceTombMeActiveEffect.CheckToRemove(owner);

        if (!ability.UseChecks()) {
            throw new Exception("Use checks failed");
        }
		
        ability.PutOnCooldown();
        ability.TimesUsed += 1;
        UsedNewAbility = true;
        LastUsedAbility = ability;
    }
}