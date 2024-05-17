using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Battle.Enums;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Hannibal.Abilities;
using Ashcrown.Remake.Core.Champions.Jafali.Abilities;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;
using Ashcrown.Remake.Core.Champions.Jane.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Lucifer.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Nikto.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Samson.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Xikan.ActiveEffects;

namespace Ashcrown.Remake.Core.Champion;

public class ChampionController(
    IChampion owner,
    IActiveEffectFactory activeEffectFactory) : IChampionController
{
    public PointsPercentageModifier TotalAllDamageDealReduce { get; set; } = new();
    public PointsPercentageModifier TotalAllDamageDealIncrease { get; set; } = new();
    public PointsPercentageModifier TotalAllDamageReceiveReduce { get; set; } = new();
    public PointsPercentageModifier TotalAllDamageReceiveIncrease { get; set; } = new();
    
    public PointsPercentageModifier TotalPhysicalDamageDealReduce { get; set; } = new();
    public PointsPercentageModifier TotalPhysicalDamageDealIncrease { get; set; } = new();
    public PointsPercentageModifier TotalPhysicalDamageReceiveReduce { get; set; } = new();
    public PointsPercentageModifier TotalPhysicalDamageReceiveIncrease { get; set; } = new();
    
    public PointsPercentageModifier TotalMagicDamageDealReduce { get; set; } = new();
    public PointsPercentageModifier TotalMagicDamageDealIncrease { get; set; } = new();
    public PointsPercentageModifier TotalMagicDamageReceiveReduce { get; set; } = new();
    public PointsPercentageModifier TotalMagicDamageReceiveIncrease { get; set; } = new();
    
    public PointsPercentageModifier TotalHealingDealReduce { get; set; } = new();
    public PointsPercentageModifier TotalHealingDealIncrease { get; set; } = new();
    public PointsPercentageModifier TotalHealingReceiveReduce { get; set; } = new();
    public PointsPercentageModifier TotalHealingReceiveIncrease { get; set; } = new();
    
    public void TargetedByAbility(IAbility ability)
    {
        GoldenAuraActiveEffect.CheckIfTriggeredAndApply(owner, ability);
    }

    public void ReceiveAbilityDamage(int amount, IAbility ability, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        ReceiveReactionsCheck(ability, secondary);

        if(!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.CustomReceiveAbilityDamageLogic, ability.Name)) {
            appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.CustomReceiveAbilityDamageLogic, ability.Name);
            if (ability.CustomReceiveAbilityDamageLogic(owner, appliedAdditionalLogic)) {
                return;
            }
        }
		
        var newAmount = ability.ReceiveAbilityDamageModifier(owner, amount);
		
        newAmount = ApplyToReceiveDamageModifiers(newAmount, ability);
        newAmount = EMPBurstActiveEffect.ApplyEMPBurstAfflictionDamageIncrease(owner, newAmount, ability);

        SubtractHealth(newAmount, appliedAdditionalLogic, ability);
    }

    public void ReceiveActiveEffectDamage(int amount, IActiveEffect activeEffect, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        var newAmount = amount;

        newAmount = activeEffect.ReceiveActiveEffectDamageModifier(owner, newAmount);
		
        newAmount = ApplyToReceiveDamageModifiers(newAmount, activeEffect:activeEffect);
        newAmount = EMPBurstActiveEffect.ApplyEMPBurstAfflictionDamageIncrease(owner, newAmount, activeEffect:activeEffect);

        SubtractHealth(newAmount,appliedAdditionalLogic, activeEffect:activeEffect);
    }

    public void ReceiveAbilityHealing(int amount, IAbility ability, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.AdditionalReceiveAbilityHealingLogic, ability.Name)) {
            appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.AdditionalReceiveAbilityHealingLogic, ability.Name);
            ability.AdditionalReceiveAbilityHealingLogic(owner, appliedAdditionalLogic);
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
        
        newAmount = ForgeSpiritActiveEffect.AbilityDamageModifier(owner, newAmount, ability);
		
        //Do not increase self damage or special cases
        if (ability is {DoNotModifyOnDealDamage: false, AfflictionDamage: false}) {
            newAmount = ApplyToDealDamageModifiers(newAmount, ability);
        }
		
        target.ChampionController.ReceiveAbilityDamage(newAmount, ability, secondary, appliedAdditionalLogic);

        if (ability is {EnergyRemove: false, EnergySteal: false}) return;
        if (ability.Name.Equals(JafaliConstants.Pride)) {
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
        
        newAmount = ForgeSpiritActiveEffect.ActiveEffectDamageModifier(owner, newAmount, activeEffect);
		
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
            activeEffect.AdditionalDealActiveEffectLogic(owner, target, ability, secondary, appliedAdditionalLogic);
        }

        if (!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.CustomDealActiveEffectLogic, activeEffect.Name)) {
            appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.CustomDealActiveEffectLogic, activeEffect.Name);
            if (activeEffect.CustomDealActiveEffectLogic(owner, target, ability, appliedAdditionalLogic)) {
                return;
            }
        }

        CounterShotActiveEffect.ReduceStunDuration(owner, activeEffect);
        AxeOfDoomDebuffActiveEffect.ReduceStunDuration(owner, activeEffect);
        AxeOfDoomDebuffActiveEffect.ReduceDealActiveEffectEnergyStealRemoveAmount(owner, activeEffect);

        target.ChampionController.ReceiveActiveEffect(activeEffect, secondary, appliedAdditionalLogic);
    }

    public void ReceiveActiveEffect(IActiveEffect activeEffect, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.AdditionalReceiveActiveEffectLogic, activeEffect.Name)) {
            appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.AdditionalReceiveActiveEffectLogic, activeEffect.Name);
            activeEffect.AdditionalReceiveActiveEffectLogic(owner, appliedAdditionalLogic);
        }
		
        ReceiveReactionsCheck(activeEffect.OriginAbility, secondary);
		
        owner.ActiveEffectController.AddActiveEffect(activeEffect);
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
        amount = AxeOfDoomDebuffActiveEffect.ReduceDealAbilityEnergyStealRemoveAmount(owner, ability, amount);
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
            ability.AdditionalReceiveAbilityEnergyStealLogic(owner, appliedAdditionalLogic);
        }
		
        ReceiveReactionsCheck(ability, secondary);
		
        if (ability.EnergyRemove) {
            for (var i = 0; i < amount; i ++) {
                owner.BattlePlayer.LoseRandomEnergy(owner, ability);
            }
        }

        if (!ability.EnergySteal) return;
        {
            for (var i = 0; i < amount; i++) {
                var stolen = owner.BattlePlayer.LoseRandomEnergy(owner, ability);
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
                owner.BattlePlayer.LoseRandomEnergy(owner, activeEffect:activeEffect);
            }
        }

        if (!activeEffect.OriginAbility.EnergySteal) return;
        {
            for (var i = 0; i < amount; i++) {
                var stolen = owner.BattlePlayer.LoseRandomEnergy(owner, activeEffect:activeEffect);
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
        if (!owner.Alive) {
            return;
        }
		
        InitializeModifiers();
        foreach (var activeEffect in owner.ActiveEffects)
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
				newAmount -= TotalAllDamageDealReduce.Points - TotalAllDamageDealIncrease.Points;
			}
		}
			
		
		if(ability is {MagicDamage: true} || activeEffect is {MagicDamage: true}) {
			if (TotalMagicDamageDealIncrease.Points > TotalMagicDamageDealReduce.Points) {
				newAmount += TotalMagicDamageDealIncrease.Points - TotalMagicDamageDealReduce.Points;
			} else if (TotalMagicDamageDealIncrease.Points < TotalMagicDamageDealReduce.Points) {
				if (TotalMagicDamageDealReduce.Points - TotalMagicDamageDealIncrease.Points > newAmount) {
					newAmount = 0;
				} else {
					newAmount -= TotalMagicDamageDealReduce.Points - TotalMagicDamageDealIncrease.Points;
				}
			}
		} else if (ability is {PhysicalDamage: true} || activeEffect is {PhysicalDamage: true})  {
			if (TotalPhysicalDamageDealIncrease.Points > TotalPhysicalDamageDealReduce.Points) {
				newAmount += TotalPhysicalDamageDealIncrease.Points - TotalPhysicalDamageDealReduce.Points;
			} else if (TotalPhysicalDamageDealIncrease.Points < TotalPhysicalDamageDealReduce.Points) {
				if (TotalPhysicalDamageDealReduce.Points > newAmount) {
					newAmount = 0;
				} else {
					newAmount -= TotalPhysicalDamageDealReduce.Points - TotalPhysicalDamageDealIncrease.Points;
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
        var newAmount = amount;
		
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
                newAmount -= TotalHealingDealReduce.Points - TotalHealingDealIncrease.Points;
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

        for (var i = 0; i < owner.ActiveEffects.Count; i++)
        {
            var presentActiveEffect = owner.ActiveEffects[i];
            if (presentActiveEffect.DestructibleDefense <= newAmount)
            {
                newAmount -= presentActiveEffect.DestructibleDefense;
                presentActiveEffect.DestructibleDefense = 0;
                presentActiveEffect.RemoveDestructibleDefense(ability, activeEffect);
            }
            else if (presentActiveEffect.DestructibleDefense > newAmount)
            {
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
        if(!owner.Alive) {
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
                activeEffect.AdditionalSubtractHealthLogic(toSubtractModified, owner, appliedAdditionalLogic);
            }
        }
		
        if (ability != null) {
            if (!appliedAdditionalLogic.CheckIfAlreadyApplied(AdditionalLogic.AdditionalSubtractHealthLogicAbility, ability.Name)) {
                appliedAdditionalLogic.AddAppliedAdditionalLogic(AdditionalLogic.AdditionalSubtractHealthLogicAbility, ability.Name);
                ability.AdditionalSubtractHealthLogic(toSubtractModified, owner, appliedAdditionalLogic);
            }
            toSubtractModified = ability.SubtractHealthModifier(toSubtractModified, owner);
        }

        toSubtractModified = HeartOfNatureActiveEffect.PreventDeath(toSubtractModified, ability, owner);
		
        if (owner.Health - toSubtractModified <= 0) {

            DarkChaliceActiveEffect.Trigger(owner);
			
            OnDeath();
        } else {
            owner.Health -= toSubtractModified;

            var currentActiveEffects = owner.ActiveEffectController.GetCurrentActiveEffectsSeparately();
            foreach (var presentActiveEffect in currentActiveEffects) {
                presentActiveEffect.AfterSubtractHealthLogic();
            }
        }
    }

    public void AddHealth(int toAdd)
    {
        if(!owner.Alive || owner.Health == 0) {
            return;
        }
		
        if(IsIgnoringHealing()) {
            return;
        }
		
        if (owner.Health + toAdd >= 100) {
            owner.Health = 100;
        } else if(owner.Alive) {
            owner.Health += toAdd;
        }
    }

    public void OnDeath()
    {
        if (owner.Died) return;
        owner.Died = true;
        owner.BattleLogic.DiedChampions.Add(owner);
    }

    public void ProcessDeath()
    {
        if (!owner.Died) return;
        owner.Health = 0;
        owner.Alive = false;
        owner.ActiveEffectController.RemoveMyActiveEffectsOnDeathFromAll();

        foreach (var activeEffect in owner.ActiveEffects) {
            activeEffect.AdditionalProcessDeathLogic();
        }
			
        owner.ActiveEffectController.ClearActiveEffects();

        SacrificialPact.RebornHannibal(owner);
    }

    public void OnStun(IAbility ability)
    {
        if (IsIgnoringStuns()) return;
        owner.ActiveEffectController.RemoveMyActiveEffectsOnStunFromAll();
        owner.ActiveEffectController.PauseMyActiveEffectsOnStunFromAll();
    }

    public void OnInvulnerability(IAbility ability)
    {
        if (IsInvulnerabilityDisabled()) return;
        owner.ActiveEffectController.RemoveActiveEffectsOnInvulnerability();
        owner.ActiveEffectController.PauseActiveEffectsOnInvulnerability();
    }

    public bool IsIgnoringHealing()
    {
        return !IsIgnoringHarmful() && owner.ActiveEffects.Where(t => !t.Paused).Any(t => t.IgnoreHealing);
    }

    public bool IsIgnoringStuns()
    {
        return IsIgnoringHarmful() || owner.ActiveEffects.Where(t => !t.Paused).Any(t => t.IgnoreStuns);
    }

    public bool IsIgnoringDamage()
    {
        return IsIgnoringHarmful() || owner.ActiveEffects.Where(t => !t.Paused).Any(t => t.IgnoreDamage);
    }

    public bool IsIgnoringHarmful()
    {
        return owner.ActiveEffects.Any(t => t.IgnoreHarmful);
    }

    public bool IsIgnoringReceivedDamageReduction()
    {
        return !IsIgnoringHarmful() 
               && owner.ActiveEffects.Where(t => !t.Paused).Any(t => t.DisableDamageReceiveReduction);
    }

    public bool IsInvulnerabilityDisabled()
    {
        return !owner.ChampionController.IsIgnoringHarmful() 
               && owner.ActiveEffects.Where(t => !t.Paused).Any(t => t.DisableInvulnerability);
    }

    public bool IsInvulnerableToFriendlyAbility(IAbility? ability)
    {
        if (ability == null
            || owner.ChampionController.IsIgnoringHarmful()
            || ability.Owner.BattlePlayer.PlayerNo != owner.BattlePlayer.PlayerNo
            || !ability.Helpful) {
            return false;
        }

        return owner.ActiveEffects.Where(t => !t.Paused).Any(t => t.InvulnerableToFriendlyAbilities);
    }

    public bool IsInvulnerableTo(IAbility? ability = null, IActiveEffect? activeEffect = null, AbilityClass[]? abilityClasses = null)
    {
        if(!owner.Alive) {
            return true;
        }
		
        switch (ability)
        {
            case null when activeEffect == null:
                return false;
            case {Owner: not null} when IsInvulnerableToFriendlyAbility(ability):
                return true;
        }

        if (IsInvulnerabilityDisabled()) {
            return false;
        }
		
        if (ability is {IgnoreInvulnerability: true}) {
            return false;
        }
		
        if (activeEffect is {IgnoreInvulnerability: true}) {
            return false;
        }

        AbilityClass[] classes = [];
		
        if (ability != null) {
            classes = ability.AbilityClasses;
        } else if (activeEffect != null)
        {
            classes = activeEffect.ActiveEffectClasses ?? activeEffect.OriginAbility.AbilityClasses;
        }
        else if (abilityClasses != null)
        {
            classes = abilityClasses;
        }

        return owner.ActiveEffects.Any(t => classes.Any(t.InvulnerabilityContains));
    }

    public bool IsClientChampionInvulnerableTo(IAbility? ability)
    {
        if(!owner.Alive) {
            return true;
        }
		
        if (ability == null) {
            return false;
        }
		
        if (IsInvulnerabilityDisabled()) {
            return false;
        }
		
        return !ability.IgnoreInvulnerability 
               && owner.ActiveEffects.Where(activeEffect => !activeEffect.Hidden)
                   .Any(activeEffect => ability.AbilityClasses.Any(activeEffect.InvulnerabilityContains));
    }

    public void EnemyDebuffMyBuff(IActiveEffect activeEffect, IAbility ability, string activeEffectOwnerName, string debuffName,
        string buffName, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (!activeEffect.Name.Equals(debuffName)) return;
        if (!owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(buffName) ||
            !owner.ActiveEffectController.GetLastActiveEffectByName(buffName)!.Fresh) {
            DealActiveEffect(owner, ability, 
                activeEffectFactory.CreateActiveEffect(activeEffectOwnerName, buffName, ability, owner), 
                true, appliedAdditionalLogic);
        }
    }

    public void EnemyDamageMyBuff(IAbility ability, string abilityName, string activeEffectOwnerName, string buffName,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (!ability.Name.Equals(abilityName)) return;
        if (!owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(buffName) ||
            !owner.ActiveEffectController.GetLastActiveEffectByName(buffName)!.Fresh) {
            DealActiveEffect(owner, ability, 
                activeEffectFactory.CreateActiveEffect(activeEffectOwnerName, buffName, ability, owner), 
                true, appliedAdditionalLogic);
        }
    }

    public void DealReactionsCheck(IAbility ability, bool secondary)
    {
        if (secondary) {
            return;
        }

        var currentActiveEffects = owner.ActiveEffectController.GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            activeEffect.DealReaction(ability);
        }
    }

    public void ReceiveReactionsCheck(IAbility ability, bool secondary)
    {
        if (secondary) {
            return;
        }

        var currentActiveEffects = owner.ActiveEffectController.GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            activeEffect.ReceiveReaction(ability);
        }
    }

    public void AddActiveEffectModifiers(IActiveEffect activeEffect)
    {
        // Deal Damage
        AddModifierOperation(TotalAllDamageDealIncrease, TotalAllDamageDealReduce, activeEffect.AllDamageDealModifier);
        AddModifierOperation(TotalPhysicalDamageDealIncrease, TotalPhysicalDamageDealReduce, activeEffect.PhysicalDamageDealModifier);
        AddModifierOperation(TotalMagicDamageDealIncrease, TotalMagicDamageDealReduce, activeEffect.MagicDamageDealModifier);
		
        // Receive Damage
        AddModifierOperation(TotalAllDamageReceiveIncrease, TotalAllDamageReceiveReduce, activeEffect.AllDamageReceiveModifier);
        AddModifierOperation(TotalPhysicalDamageReceiveIncrease, TotalPhysicalDamageReceiveReduce, activeEffect.PhysicalDamageReceiveModifier);
        AddModifierOperation(TotalMagicDamageReceiveIncrease, TotalMagicDamageReceiveIncrease, activeEffect.MagicDamageReceiveModifier);
		
        // Deal Healing
        AddModifierOperation(TotalHealingDealIncrease, TotalHealingDealReduce, activeEffect.HealingDealModifier);
		
        // Receive Healing
        AddModifierOperation(TotalHealingReceiveIncrease, TotalHealingReceiveReduce, activeEffect.HealingReceiveModifier);
    }

    public void RemoveActiveEffectModifiers(IActiveEffect activeEffect)
    {
        //Deal Damage
        RemoveModifierOperation(TotalAllDamageDealIncrease, TotalAllDamageDealReduce, activeEffect.AllDamageDealModifier);
        RemoveModifierOperation(TotalPhysicalDamageDealIncrease, TotalPhysicalDamageDealReduce, activeEffect.PhysicalDamageDealModifier);
        RemoveModifierOperation(TotalMagicDamageDealIncrease, TotalMagicDamageDealReduce, activeEffect.MagicDamageDealModifier);
		
        // Receive Damage
        RemoveModifierOperation(TotalAllDamageReceiveIncrease, TotalAllDamageReceiveReduce, activeEffect.AllDamageReceiveModifier);
        RemoveModifierOperation(TotalPhysicalDamageReceiveIncrease, TotalPhysicalDamageReceiveReduce, activeEffect.PhysicalDamageReceiveModifier);
        RemoveModifierOperation(TotalMagicDamageReceiveIncrease, TotalMagicDamageReceiveReduce, activeEffect.MagicDamageReceiveModifier);
		
        // Deal Healing
        RemoveModifierOperation(TotalHealingDealIncrease, TotalHealingDealReduce, activeEffect.HealingDealModifier);
		
        // Receive Healing
        RemoveModifierOperation(TotalHealingReceiveIncrease, TotalHealingReceiveReduce, activeEffect.HealingReceiveModifier);
    }

    public void AddModifierOperation(PointsPercentageModifier modifierIncrease, PointsPercentageModifier modifierReduce,
        PointsPercentageModifier modifier)
    {
        switch (modifier.Points)
        {
            case > 0:
                modifierIncrease.Points += modifier.Points;
                break;
            case < 0:
                modifierReduce.Points -= modifier.Points;
                break;
        }

        switch (modifier.Percentage)
        {
            case > 0:
                modifierIncrease.Percentage += modifier.Percentage;
                break;
            case < 0:
                modifierReduce.Percentage -= modifier.Percentage;
                break;
        }
    }

    public void RemoveModifierOperation(PointsPercentageModifier modifierIncrease, PointsPercentageModifier modifierReduce,
        PointsPercentageModifier modifier)
    {
        switch (modifier.Points)
        {
            case > 0 when modifierIncrease.Points >= modifier.Points:
                modifierIncrease.Points -= modifier.Points;
                break;
            case > 0:
                modifierIncrease.Points = 0;
                break;
            case < 0 when modifierReduce.Points >= -modifier.Points:
                modifierReduce.Points -= -modifier.Points;
                break;
            case < 0:
                modifierReduce.Points = 0;
                break;
        }

        switch (modifier.Percentage)
        {
            case > 0 when modifierIncrease.Percentage >= modifier.Percentage:
                modifierIncrease.Percentage -= modifier.Percentage;
                break;
            case > 0:
                modifierIncrease.Percentage = 0;
                break;
            case < 0 when modifierReduce.Percentage >= -modifier.Percentage:
                modifierReduce.Percentage -= -modifier.Percentage;
                break;
            case < 0:
                modifierReduce.Percentage = 0;
                break;
        }
    }

    public void InitializeModifiers()
    {
        TotalAllDamageDealReduce = new PointsPercentageModifier();
        TotalAllDamageDealIncrease = new PointsPercentageModifier();
        TotalAllDamageReceiveReduce = new PointsPercentageModifier();
        TotalAllDamageReceiveIncrease = new PointsPercentageModifier();

        TotalPhysicalDamageDealReduce = new PointsPercentageModifier();
        TotalPhysicalDamageDealIncrease = new PointsPercentageModifier();
        TotalPhysicalDamageReceiveReduce = new PointsPercentageModifier();
        TotalPhysicalDamageReceiveIncrease = new PointsPercentageModifier();

        TotalMagicDamageDealReduce = new PointsPercentageModifier();
        TotalMagicDamageDealIncrease = new PointsPercentageModifier();
        TotalMagicDamageReceiveReduce = new PointsPercentageModifier();
        TotalMagicDamageReceiveIncrease = new PointsPercentageModifier();

        TotalHealingDealReduce = new PointsPercentageModifier();
        TotalHealingDealIncrease = new PointsPercentageModifier();
        TotalHealingReceiveReduce = new PointsPercentageModifier();
        TotalHealingReceiveIncrease = new PointsPercentageModifier();
    }
}