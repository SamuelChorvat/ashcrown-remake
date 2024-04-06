using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Battle.Enums;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Jafali.Abilities;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;
using Ashcrown.Remake.Core.Champions.Jane.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Lucifer.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Nikto.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Samson.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Xikan.ActiveEffects;

namespace Ashcrown.Remake.Core.Champion;

public class ChampionController(IChampion owner) : IChampionController
{
    public required IChampion Owner { get; init; } = owner;
    
    public required PointsPercentageModifier TotalAllDamageDealReduce { get; set; } = new();
    public required PointsPercentageModifier TotalAllDamageDealIncrease { get; set; } = new();
    public required PointsPercentageModifier TotalAllDamageReceiveReduce { get; set; } = new();
    public required PointsPercentageModifier TotalAllDamageReceiveIncrease { get; set; } = new();
    
    public required PointsPercentageModifier TotalPhysicalDamageDealReduce { get; set; } = new();
    public required PointsPercentageModifier TotalPhysicalDamageDealIncrease { get; set; } = new();
    public required PointsPercentageModifier TotalPhysicalDamageReceiveReduce { get; set; } = new();
    public required PointsPercentageModifier TotalPhysicalDamageReceiveIncrease { get; set; } = new();
    
    public required PointsPercentageModifier TotalMagicDamageDealReduce { get; set; } = new();
    public required PointsPercentageModifier TotalMagicDamageDealIncrease { get; set; } = new();
    public required PointsPercentageModifier TotalMagicDamageReceiveReduce { get; set; } = new();
    public required PointsPercentageModifier TotalMagicDamageReceiveIncrease { get; set; } = new();
    
    public required PointsPercentageModifier TotalHealingDealReduce { get; set; } = new();
    public required PointsPercentageModifier TotalHealingDealIncrease { get; set; } = new();
    public required PointsPercentageModifier TotalHealingReceiveReduce { get; set; } = new();
    public required PointsPercentageModifier TotalHealingReceiveIncrease { get; set; } = new();
    
    public void TargetedByAbility(IAbility ability)
    {
        GoldenAuraActiveEffect.CheckIfTriggeredAndApply(Owner, ability);
    }

    public void ReceiveAbilityDamage(int amount, IAbility ability, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        ReceiveReactionsCheck(ability, secondary);

        if(!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.CustomReceiveAbilityDamageLogic, ability.Name)) {
            appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.CustomReceiveAbilityDamageLogic, ability.Name);
            if (ability.CustomReceiveAbilityDamageLogic(Owner, appliedAdditionalLogic)) {
                return;
            }
        }
		
        var newAmount = ability.ReceiveAbilityDamageModifier(Owner, amount);
		
        newAmount = ApplyToReceiveDamageModifiers(newAmount, ability);
        newAmount = EmpBurstActiveEffect.ApplyEmpBurstAfflictionDamageIncrease(Owner, newAmount, ability);

        SubtractHealth(newAmount, appliedAdditionalLogic, ability);
    }

    public void ReceiveActiveEffectDamage(int amount, IActiveEffect activeEffect, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        var newAmount = amount;

        newAmount = activeEffect.ReceiveActiveEffectDamageModifier(Owner, newAmount);
		
        newAmount = ApplyToReceiveDamageModifiers(newAmount, activeEffect:activeEffect);
        newAmount = EmpBurstActiveEffect.ApplyEmpBurstAfflictionDamageIncrease(Owner, newAmount, activeEffect:activeEffect);

        SubtractHealth(newAmount,appliedAdditionalLogic, activeEffect:activeEffect);
    }

    public void ReceiveAbilityHealing(int amount, IAbility ability, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.AdditionalReceiveAbilityHealingLogic, ability.Name)) {
            appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.AdditionalReceiveAbilityHealingLogic, ability.Name);
            ability.AdditionalReceiveAbilityHealingLogic(Owner, appliedAdditionalLogic);
        }
		
        var newAmount = amount;
		
        if(newAmount < 0) {
            newAmount = 0;
        }
		
        newAmount = ApplyToReceiveHealingModifiers(newAmount, ability);
        AddHealth(newAmount);
    }

    public void ReceiveActiveEffectHealing(int amount, IActiveEffect activeEffect, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        var newAmount = amount;
		
        if(newAmount < 0) {
            newAmount = 0;
        }
		
        newAmount = ApplyToReceiveHealingModifiers(newAmount, activeEffect:activeEffect);
        AddHealth(newAmount);
    }

    public void DealAbilityDamage(int amount, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        DealReactionsCheck(ability, secondary);
		
        var newAmount = amount;
        if(newAmount < 0) {
            newAmount = 0;
        }
        newAmount = ability.DealAbilityDamageModifier(target, newAmount, secondary);

        if (!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.AdditionalDealAbilityDamageLogic, ability.Name)) {
            appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.AdditionalDealAbilityDamageLogic, ability.Name);
            ability.AdditionalDealAbilityDamageLogic(target, appliedAdditionalLogic);
        }
        
        newAmount = ForgeSpiritActiveEffect.AbilityDamageModifier(Owner, newAmount, ability);
		
        //Do not increase self damage or special cases
        if (ability is {DoNotModifyOnDealDamage: false, AfflictionDamage: false}) {
            newAmount = ApplyToDealDamageModifiers(newAmount, ability);
        }
		
        target.ChampionController.ReceiveAbilityDamage(newAmount, ability, secondary, appliedAdditionalLogic);

        if (ability is {EnergyRemove: false, EnergySteal: false}) return;
        if (ability.Name.Equals(JafaliNames.Pride)) {
            ability.EnergyAmount = Pride.GetEnergyAmount(target);
        }

        DealAbilityEnergySteal(target, ability, true, appliedAdditionalLogic);
    }

    public void DealActiveEffectDamage(int amount, IChampion target, IActiveEffect activeEffect,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        var newAmount = amount;
		
        if(newAmount < 0) {
            newAmount = 0;
        }
        
        newAmount = ForgeSpiritActiveEffect.ActiveEffectDamageModifier(Owner, newAmount, activeEffect);
		
        //Do not increase self damage
        if (!activeEffect.OriginAbility.DoNotModifyOnDealDamage && !activeEffect.AfflictionDamage) {
            newAmount = ApplyToDealDamageModifiers(newAmount, activeEffect:activeEffect);
        }
		
        target.ChampionController.ReceiveActiveEffectDamage(newAmount, activeEffect, appliedAdditionalLogic);
    }

    public void DealDamageAndAddActiveEffect(int amount, IChampion target, IAbility ability, IActiveEffect activeEffect)
    {
        DealAbilityDamage(amount, target, ability, false, new AppliedAdditionalLogic());
        DealActiveEffect(target, ability, activeEffect, true, new AppliedAdditionalLogic());
    }

    public void DealActiveEffect(IChampion target, IAbility ability, IActiveEffect activeEffect, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        DealReactionsCheck(ability, secondary);

        if (!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.AdditionalDealActiveEffectLogic, activeEffect.Name)) {
            appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.AdditionalDealActiveEffectLogic, activeEffect.Name);
            activeEffect.AdditionalDealActiveEffectLogic(Owner, target, ability, secondary, appliedAdditionalLogic);
        }

        if (!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.CustomDealActiveEffectLogic, activeEffect.Name)) {
            appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.CustomDealActiveEffectLogic, activeEffect.Name);
            if (activeEffect.CustomDealActiveEffectLogic(Owner, target, ability, appliedAdditionalLogic)) {
                return;
            }
        }

        CounterShotActiveEffect.ReduceStunDuration(Owner, activeEffect);
        AxeOfDoomDebuffActiveEffect.ReduceStunDuration(Owner, activeEffect);
        AxeOfDoomDebuffActiveEffect.ReduceDealActiveEffectEnergyStealRemoveAmount(Owner, activeEffect);

        target.ChampionController.ReceiveActiveEffect(activeEffect, secondary, appliedAdditionalLogic);
    }

    public void ReceiveActiveEffect(IActiveEffect activeEffect, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.AdditionalReceiveActiveEffectLogic, activeEffect.Name)) {
            appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.AdditionalReceiveActiveEffectLogic, activeEffect.Name);
            activeEffect.AdditionalReceiveActiveEffectLogic(Owner, appliedAdditionalLogic);
        }
		
        ReceiveReactionsCheck(activeEffect.OriginAbility, secondary);
		
        Owner.ActiveEffectController.AddActiveEffect(activeEffect);
    }

    public void DealAbilityHealing(int amount, IChampion target, IAbility ability, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        var newAmount = amount;
		
        if(newAmount < 0) {
            newAmount = 0;
        }

        newAmount = ability.DealAbilityHealingModifier(target, newAmount);
		
        newAmount = ApplyToDealHealingModifiers(newAmount, ability);
        target.ChampionController.ReceiveAbilityHealing(newAmount, ability, appliedAdditionalLogic);
    }

    public void DealActiveEffectHealing(int amount, IChampion target, IActiveEffect activeEffect,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        var newAmount = amount;
		
        if(newAmount < 0) {
            newAmount = 0;
        }
		
        newAmount = ApplyToDealHealingModifiers(newAmount, activeEffect:activeEffect);
        target.ChampionController.ReceiveActiveEffectHealing(newAmount, activeEffect, appliedAdditionalLogic);
    }

    public void DealAbilityEnergySteal(IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        DealReactionsCheck(ability, secondary);

        var amount = ability.EnergyAmount;
        amount = AxeOfDoomDebuffActiveEffect.ReduceDealAbilityEnergyStealRemoveAmount(Owner, ability, amount);
        amount =  Math.Max(amount, 0);
		
        target.ChampionController.ReceiveAbilityEnergySteal(ability, amount, secondary, appliedAdditionalLogic);
    }

    public void DealActiveEffectEnergySteal(IChampion target, IActiveEffect activeEffect,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        var amount = Math.Max(activeEffect.EnergyAmount, 0);
		
        target.ChampionController.ReceiveActiveEffectEnergySteal(activeEffect, amount, appliedAdditionalLogic);
    }

    public void ReceiveAbilityEnergySteal(IAbility ability, int amount, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.AdditionalReceiveAbilityEnergyStealLogic, ability.Name)) {
            appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.AdditionalReceiveAbilityEnergyStealLogic, ability.Name);
            ability.AdditionalReceiveAbilityEnergyStealLogic(Owner, appliedAdditionalLogic);
        }
		
        ReceiveReactionsCheck(ability, secondary);
		
        if (ability.EnergyRemove) {
            for (var i = 0; i < amount; i ++) {
                Owner.BattlePlayer.LoseRandomEnergy(Owner, ability);
            }
        }

        if (!ability.EnergySteal) return;
        {
            for (var i = 0; i < amount; i++) {
                var stolen = Owner.BattlePlayer.LoseRandomEnergy(Owner, ability);
                if (stolen != EnergyType.NoEnergy) {
                    ability.Owner.BattlePlayer.AddEnergy(stolen);
                } else {
                    return;
                }
            }
        }
    }

    public void ReceiveActiveEffectEnergySteal(IActiveEffect activeEffect, int amount,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (activeEffect.OriginAbility.EnergyRemove) {
            for (var i = 0; i < amount; i ++) {
                Owner.BattlePlayer.LoseRandomEnergy(Owner, activeEffect:activeEffect);
            }
        }

        if (!activeEffect.OriginAbility.EnergySteal) return;
        {
            for (var i = 0; i < amount; i++) {
                var stolen = Owner.BattlePlayer.LoseRandomEnergy(Owner, activeEffect:activeEffect);
                if (stolen != EnergyType.NoEnergy) {
                    activeEffect.OriginAbility.Owner.BattlePlayer.AddEnergy(stolen);
                } else {
                    return;
                }
            }
        }
    }

    public int ApplyToDealDamageModifiers(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        var newAmount = amount;
		
        newAmount = IsIgnoringHarmful() 
            ? DealDamageModifiersIgnoreDamageReduction(newAmount, ability, activeEffect) 
            : DealDamageModifiersNoDisables(newAmount, ability, activeEffect);
		
        return newAmount;
    }

    public int ApplyToReceiveDamageModifiers(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        var newAmount = amount;
		
        if(ability is {AfflictionDamage: false} || activeEffect is {AfflictionDamage: false}) {
            if(ability is {PiercingDamage: true} || 
               activeEffect is {PiercingDamage: true} || IsIgnoringReceivedDamageReduction()) {
                newAmount = ReceiveDamageModifiersCaseCannotReduceDamage(newAmount, ability, activeEffect);
            } else {
                newAmount = ReceiveDamageModifiersCaseNoDisables(newAmount, ability, activeEffect);
            }
            newAmount = ApplyDestructibleDefense(newAmount, ability, activeEffect);
        } else {
            newAmount = ReceiveDamageModifiersCaseCannotReduceDamage(newAmount, ability, activeEffect);
        }
		
        return newAmount;
    }

    public int ApplyToDealHealingModifiers(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        var newAmount = amount;
		
        newAmount = DealHealingModifiersCaseNoDisables(newAmount, ability, activeEffect);
		
        return newAmount;
    }

    public int ApplyToReceiveHealingModifiers(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        var newAmount = amount;
		
        newAmount = IsIgnoringHarmful() 
            ? ReceiveHealingModifiersCaseIgnoreHealingReduction(newAmount, ability, activeEffect) 
            : ReceiveHealingModifiersCaseNoDisables(newAmount, ability, activeEffect);
		
		
        return newAmount;
    }

    public void SetModifiers()
    {
        if (!Owner.Alive) {
            return;
        }
		
        InitializeModifiers();
        foreach (var activeEffect in Owner.ActiveEffects)
        {
            AddActiveEffectModifiers(activeEffect);
        }
    }

    public int DealDamageModifiersNoDisables(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        var newAmount = amount;
		
		if (TotalAllDamageDealIncrease.Percentage > 0 || TotalAllDamageDealReduce.Percentage > 0) {
			if (newAmount > 0) {
				var modifyBy = (100 + TotalAllDamageDealIncrease.Percentage - TotalAllDamageDealReduce.Percentage) / (double) 100;
				if (modifyBy <= 0) {
					newAmount = 0;
				} else {
					newAmount = (int) (newAmount * modifyBy);
				}
			}
		} 
		
		if (TotalAllDamageDealIncrease.Points > TotalAllDamageDealReduce.Points) {
			newAmount += TotalAllDamageDealIncrease.Points - TotalAllDamageDealReduce.Points;
		} else if (TotalAllDamageDealIncrease.Points < TotalAllDamageDealReduce.Points) {
			if (TotalAllDamageDealReduce.Points - TotalAllDamageDealIncrease.Points > newAmount) {
				newAmount = 0;
			} else {
				newAmount -= (TotalAllDamageDealReduce.Points - TotalAllDamageDealIncrease.Points);
			}
		}
			
		
		if(ability is {MagicDamage: true} || activeEffect is {MagicDamage: true}) {
			if (TotalMagicDamageDealIncrease.Points > TotalMagicDamageDealReduce.Points) {
				newAmount += TotalMagicDamageDealIncrease.Points - TotalMagicDamageDealReduce.Points;
			} else if (TotalMagicDamageDealIncrease.Points < TotalMagicDamageDealReduce.Points) {
				if (TotalMagicDamageDealReduce.Points - TotalMagicDamageDealIncrease.Points > newAmount) {
					newAmount = 0;
				} else {
					newAmount -= (TotalMagicDamageDealReduce.Points - TotalMagicDamageDealIncrease.Points);
				}
			}
		} else if (ability is {PhysicalDamage: true} || activeEffect is {PhysicalDamage: true})  {
			if (TotalPhysicalDamageDealIncrease.Points > TotalPhysicalDamageDealReduce.Points) {
				newAmount += (TotalPhysicalDamageDealIncrease.Points - TotalPhysicalDamageDealReduce.Points);
			} else if (TotalPhysicalDamageDealIncrease.Points < TotalPhysicalDamageDealReduce.Points) {
				if (TotalPhysicalDamageDealReduce.Points > newAmount) {
					newAmount = 0;
				} else {
					newAmount -= (TotalPhysicalDamageDealReduce.Points - TotalPhysicalDamageDealIncrease.Points);
				}
			}
		}
		
		return newAmount;
    }

    public int DealDamageModifiersIgnoreDamageReduction(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        var newAmount = amount;
		
        if (TotalAllDamageDealIncrease.Percentage > 0) {
            if (newAmount > 0) {
                var modifyBy = (100 + TotalAllDamageDealIncrease.Percentage) / (double) 100;
                newAmount = (int) (newAmount * modifyBy);
            }
        } 
		
        newAmount += TotalAllDamageDealIncrease.Points;
			
		
        if(ability is {MagicDamage: true} || activeEffect is {MagicDamage: true}) {
            newAmount += TotalMagicDamageDealIncrease.Points;
        } else if (ability is {PhysicalDamage: true} || activeEffect is {PhysicalDamage: true}) {
            newAmount += TotalPhysicalDamageDealIncrease.Points;
        }
		
        return newAmount;
    }

    public int ReceiveDamageModifiersCaseNoDisables(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        int newAmount = amount;
		
		if (TotalAllDamageReceiveIncrease.Percentage > 0 || TotalAllDamageReceiveReduce.Percentage > 0) {
			if (newAmount > 0) {
				var modifyBy = (100 + TotalAllDamageReceiveIncrease.Percentage - TotalAllDamageReceiveReduce.Percentage) / (double) 100;
				if (modifyBy <= 0) {
					newAmount = 0;
				} else {
					newAmount = (int) (newAmount * modifyBy);
				}
			}
		} 
		
		if (TotalAllDamageReceiveIncrease.Points > TotalAllDamageReceiveReduce.Points) {
			TotalAllDamageReceiveIncrease.Points -= TotalAllDamageReceiveReduce.Points;
			newAmount += TotalAllDamageReceiveIncrease.Points;
			TotalAllDamageReceiveIncrease.Points = 0;
			TotalAllDamageReceiveReduce.Points = 0;
		} else if (TotalAllDamageReceiveIncrease.Points < TotalAllDamageReceiveReduce.Points) {
			TotalAllDamageReceiveReduce.Points -= TotalAllDamageReceiveIncrease.Points;
			if (TotalAllDamageReceiveReduce.Points > newAmount) {
				TotalAllDamageReceiveReduce.Points -= newAmount;
				newAmount = 0;
			} else {
				newAmount -= TotalAllDamageReceiveReduce.Points;
				TotalAllDamageReceiveReduce.Points = 0;
			}
		}
			
		
		if(ability is {MagicDamage: true} || activeEffect is {MagicDamage: true}) {
			if (TotalMagicDamageReceiveIncrease.Points > TotalMagicDamageReceiveReduce.Points) {
				TotalMagicDamageReceiveIncrease.Points -= TotalMagicDamageReceiveReduce.Points;
				newAmount += TotalMagicDamageReceiveIncrease.Points;
				TotalMagicDamageReceiveIncrease.Points = 0;
				TotalMagicDamageReceiveReduce.Points = 0;
			} else if (TotalMagicDamageReceiveIncrease.Points < TotalMagicDamageReceiveReduce.Points) {
				TotalMagicDamageReceiveReduce.Points -= TotalMagicDamageReceiveIncrease.Points;
				if (TotalMagicDamageReceiveReduce.Points > newAmount) {
					TotalMagicDamageReceiveReduce.Points -= newAmount;
					newAmount = 0;
				} else {
					newAmount -= TotalMagicDamageReceiveReduce.Points;
					TotalMagicDamageReceiveReduce.Points = 0;
				}
			}
		} else if (ability is {PhysicalDamage: true} || activeEffect is {PhysicalDamage: true}) {
			if (TotalPhysicalDamageReceiveIncrease.Points > TotalPhysicalDamageReceiveReduce.Points) {
				TotalPhysicalDamageReceiveIncrease.Points -= TotalPhysicalDamageReceiveReduce.Points;
				newAmount += TotalPhysicalDamageReceiveIncrease.Points;
				TotalPhysicalDamageReceiveIncrease.Points = 0;
				TotalPhysicalDamageReceiveReduce.Points = 0;
			} else if (TotalPhysicalDamageReceiveIncrease.Points < TotalPhysicalDamageReceiveReduce.Points) {
				TotalPhysicalDamageReceiveReduce.Points -= TotalPhysicalDamageReceiveIncrease.Points;
				if (TotalPhysicalDamageReceiveReduce.Points > newAmount) {
					TotalPhysicalDamageReceiveReduce.Points -= newAmount;
					newAmount = 0;
				} else {
					newAmount -= TotalPhysicalDamageReceiveReduce.Points;
					TotalPhysicalDamageReceiveReduce.Points = 0;
				}
			}
		}
		
		return newAmount;
    }

    public int ReceiveDamageModifiersCaseCannotReduceDamage(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        var newAmount = amount;
		
        if (TotalAllDamageReceiveIncrease.Percentage > 0) {
            if (newAmount > 0) {
                var modifyBy = (100 + TotalAllDamageReceiveIncrease.Percentage) / (double) 100;
                newAmount = (int) (newAmount * modifyBy);	
            }
        } 
		
        if (TotalAllDamageReceiveIncrease.Points > 0) {
            newAmount += TotalAllDamageReceiveIncrease.Points;
            TotalAllDamageReceiveIncrease.Points = 0;
        } 
			
		
        if(ability is {MagicDamage: true} || activeEffect is {MagicDamage: true})
        {
            if (TotalMagicDamageReceiveIncrease.Points <= 0) return newAmount;
            newAmount += TotalMagicDamageReceiveIncrease.Points;
            TotalMagicDamageReceiveIncrease.Points = 0;
        } else if (ability is {PhysicalDamage: true} || activeEffect is {PhysicalDamage: true})
        {
            if (TotalPhysicalDamageReceiveIncrease.Points <= 0) return newAmount;
            newAmount += TotalPhysicalDamageReceiveIncrease.Points;
            TotalPhysicalDamageReceiveIncrease.Points = 0;
        }
		
        return newAmount;
    }

    public int DealHealingModifiersCaseNoDisables(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        var newAmount = amount;
		
        if (TotalHealingDealIncrease.Percentage > 0 || TotalHealingDealReduce.Percentage > 0) {
            if (newAmount > 0) {
                var modifyBy = (100 + TotalHealingDealIncrease.Percentage - TotalHealingDealReduce.Percentage) / (double) 100;
                if (modifyBy <= 0) {
                    newAmount = 0;
                } else {
                    newAmount = (int) (newAmount * modifyBy);
                }
            }
        } 
		
        if (TotalHealingDealIncrease.Points > TotalHealingDealReduce.Points) {
            newAmount += TotalHealingDealIncrease.Points - TotalHealingDealReduce.Points;
        } else if (TotalHealingDealIncrease.Points < TotalHealingDealReduce.Points) {
            if (TotalHealingDealReduce.Points - TotalHealingDealIncrease.Points > newAmount) {
                newAmount = 0;
            } else {
                newAmount -= (TotalHealingDealReduce.Points - TotalHealingDealIncrease.Points);
            }
        }
		
        return newAmount;
    }

    public int ReceiveHealingModifiersCaseNoDisables(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        var newAmount = amount;
		
        if (TotalHealingReceiveIncrease.Percentage > 0 || TotalHealingReceiveReduce.Percentage > 0) {
            if (newAmount > 0) {
                var modifyBy = (100 + TotalHealingReceiveIncrease.Percentage - TotalHealingReceiveReduce.Percentage) / (double) 100;
                if (modifyBy <= 0) {
                    newAmount = 0;
                } else {
                    newAmount = (int) (newAmount * modifyBy);
                }
            }
        } 
		
        if (TotalHealingReceiveIncrease.Points > TotalHealingReceiveReduce.Points) {
            TotalHealingReceiveIncrease.Points -= TotalHealingReceiveReduce.Points;
            newAmount += TotalHealingReceiveIncrease.Points;
            TotalHealingReceiveIncrease.Points = 0;
            TotalHealingReceiveReduce.Points = 0;
        } else if (TotalHealingReceiveIncrease.Points < TotalHealingReceiveReduce.Points) {
            TotalHealingReceiveReduce.Points -= TotalHealingReceiveIncrease.Points;
            if (TotalHealingReceiveReduce.Points > newAmount) {
                TotalHealingReceiveReduce.Points -= newAmount;
                newAmount = 0;
            } else {
                newAmount -= TotalHealingReceiveReduce.Points;
                TotalHealingReceiveReduce.Points = 0;
            }
        }
		
        return newAmount;
    }

    public int ReceiveHealingModifiersCaseIgnoreHealingReduction(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        var newAmount = amount;
		
        if (TotalHealingReceiveIncrease.Percentage > 0) {
            if (newAmount > 0) {
                var modifyBy = (100 + TotalHealingReceiveIncrease.Percentage) / (double) 100;
                newAmount = (int) (newAmount * modifyBy);
            }
        } 
		
        newAmount += TotalHealingReceiveIncrease.Points;
        TotalHealingReceiveIncrease.Points = 0;
		
        return newAmount;
    }

    public int ApplyDestructibleDefense(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        var newAmount = amount;
		
        foreach (var presentActiveEffect in Owner.ActiveEffects)
        {
            if (presentActiveEffect.DestructibleDefense <= newAmount) {
                newAmount -= presentActiveEffect.DestructibleDefense;
                presentActiveEffect.DestructibleDefense = 0;
                presentActiveEffect.RemoveDestructibleDefense(ability, activeEffect);
				
            } else if(presentActiveEffect.DestructibleDefense > newAmount) {
                presentActiveEffect.DestructibleDefense -= newAmount;
                newAmount = 0;
                break;
            }
        }
		
        return newAmount;
    }

    public void SubtractHealth(int toSubtract, AppliedAdditionalLogic appliedAdditionalLogic, 
        IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        if(!Owner.Alive) {
            return;
        }

        if (ability is not {CannotBeIgnored: true}) {
            if(IsIgnoringDamage()
               || IsInvulnerableTo(ability, activeEffect)
               || IsIgnoringHarmful()) {
                return;
            }
        }
		
        var toSubtractModified = toSubtract;
        toSubtractModified = Math.Max(toSubtractModified, 0);
		
        if (activeEffect != null) {
            if (!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.AdditionalSubtractHealthLogicActiveEffect, activeEffect.Name)) {
                appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.AdditionalSubtractHealthLogicActiveEffect, activeEffect.Name);
                activeEffect.AdditionalSubtractHealthLogic(toSubtractModified, Owner, appliedAdditionalLogic);
            }
        }
		
        if (ability != null) {
            if (!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.AdditionalSubtractHealthLogicAbility, ability.Name)) {
                appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.AdditionalSubtractHealthLogicAbility, ability.Name);
                ability.AdditionalSubtractHealthLogic(toSubtractModified, Owner, appliedAdditionalLogic);
            }
            toSubtractModified = ability.SubtractHealthModifier(toSubtractModified, Owner);
        }

        toSubtractModified = HeartOfNatureActiveEffect.PreventDeath(toSubtractModified, ability, Owner);
		
        if (Owner.Health - toSubtractModified <= 0) {

            DarkChaliceActiveEffect.Trigger(Owner);
			
            OnDeath();
        } else {
            Owner.Health -= toSubtractModified;

            var currentActiveEffects = Owner.ActiveEffectController.GetCurrentActiveEffectsSeparately();
            foreach (var presentActiveEffect in currentActiveEffects) {
                presentActiveEffect.AfterSubtractHealthLogic();
            }
        }
    }

    public void AddHealth(int toAdd)
    {
        if(!Owner.Alive || Owner.Health == 0) {
            return;
        }
		
        if(IsIgnoringHealing()) {
            return;
        }
		
        if (Owner.Health + toAdd >= 100) {
            Owner.Health = 100;
        } else if(owner.Alive) {
            Owner.Health += toAdd;
        }
    }

    public void OnDeath()
    {
        if (Owner.Died) return;
        Owner.Died = true;
        Owner.BattleLogic.DiedChampions.Add(Owner);
    }

    public void ProcessDeath()
    {
        throw new NotImplementedException();
    }

    public void OnStun(IAbility ability)
    {
        throw new NotImplementedException();
    }

    public void OnInvulnerability(IAbility ability)
    {
        throw new NotImplementedException();
    }

    public bool IsIgnoringHealing()
    {
        throw new NotImplementedException();
    }

    public bool IsIgnoringStuns()
    {
        throw new NotImplementedException();
    }

    public bool IsIgnoringDamage()
    {
        throw new NotImplementedException();
    }

    public bool IsIgnoringHarmful()
    {
        throw new NotImplementedException();
    }

    public bool IsIgnoringReceivedDamageReduction()
    {
        throw new NotImplementedException();
    }

    public bool IsInvulnerabilityDisabled()
    {
        throw new NotImplementedException();
    }

    public bool IsInvulnerableToFriendlyAbility(IAbility ability)
    {
        throw new NotImplementedException();
    }

    public bool IsInvulnerableTo(IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        throw new NotImplementedException();
    }

    public bool IsClientChampionInvulnerableTo(IAbility ability)
    {
        throw new NotImplementedException();
    }

    public void EnemyDebuffMyBuff(IActiveEffect activeEffect, IAbility ability, string activeEffectOwnerName, string debuffName,
        string buffName, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        throw new NotImplementedException();
    }

    public void EnemyDamageMyBuff(IAbility ability, string abName, string activeEffectOwnerName, string buffName,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        throw new NotImplementedException();
    }

    public void DealReactionsCheck(IAbility ability, bool secondary)
    {
        throw new NotImplementedException();
    }

    public void ReceiveReactionsCheck(IAbility ability, bool secondary)
    {
        throw new NotImplementedException();
    }

    public void AddActiveEffectModifiers(IActiveEffect activeEffect)
    {
        throw new NotImplementedException();
    }

    public void RemoveActiveEffectModifiers(IActiveEffect activeEffect)
    {
        throw new NotImplementedException();
    }

    public void AddModifierOperation(PointsPercentageModifier modifierIncreaseRef, PointsPercentageModifier modifierReduceRef,
        PointsPercentageModifier abRefModifier)
    {
        throw new NotImplementedException();
    }

    public void RemoveModifierOperation(PointsPercentageModifier modifierIncreaseRef, PointsPercentageModifier modifierReduceRef,
        PointsPercentageModifier abRefModifier)
    {
        throw new NotImplementedException();
    }

    public void InitializeModifiers()
    {
        throw new NotImplementedException();
    }
}