using Ashcrown.Remake.Core.V1.Ability.Enums;
using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.Ability.Models;
using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Battle.Models;
using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.ActiveEffect.Abstract;

public abstract class ActiveEffect(
    IAbility originAbility,
    string activeEffectName,
    IChampion championTarget,
    int duration,
    int timeLeft,
    string description) : IActiveEffect
{
    public required IAbility OriginAbility { get; set; } = originAbility;
    public required IChampion Target { get; set; } = championTarget;
    public required string Name { get; set; } = activeEffectName;
    public required string Description { get; set; } = description;
    public required int Duration { get; set; } = duration;
    public required int TimeLeft { get; set; } = timeLeft;
    public AbilityClass[]? ActiveEffectClasses { get; set; }
    public int DestructibleDefense { get; set; }
    public bool Hidden { get; set; }
    public int Stacks { get; set; }
    public bool Infinite { get; set; }
    public IActiveEffect? CasterLink { get; set; }
    public required IList<IActiveEffect> ChildrenLinks { get; set; } = new List<IActiveEffect>();
    public bool Fresh { get; set; } = true;
    public bool Paused { get; set; }
    public bool Source { get; set; }
    public bool Unique { get; set; }
    public bool Stackable { get; set; }
    public bool NoTick { get; set; }
    public bool Reflected { get; set; }
    public bool IgnoreInvulnerability { get; set; }
    public bool Harmful { get; set; }
    public bool Helpful { get; set; }
    public bool Debuff { get; set; }
    public bool Buff { get; set; }
    public bool CannotBeRemoved { get; set; }
    public bool Healing { get; set; }
    public bool Damaging { get; set; }
    public bool Invulnerability { get; set; }
    public AbilityClass[]? TypeOfInvulnerability { get; set; }
    public bool Stun { get; set; }
    public AbilityClass[]? StunType { get; set; }
    public bool IgnoreHealing { get; set; }
    public bool IgnoreDamage { get; set; }
    public bool DisableInvulnerability { get; set; }
    public bool DisableDamageReceiveReduction { get; set; }
    public bool IgnoreStuns { get; set; }
    public bool IgnoreHarmful { get; set; }
    public bool PiercingDamage { get; set; }
    public bool AfflictionDamage { get; set; }
    public bool MagicDamage { get; set; }
    public bool PhysicalDamage { get; set; }
    public bool InvulnerableToFriendlyAbilities { get; set; }
    public bool EndsOnCasterStun { get; set; }
    public bool PauseOnCasterStun { get; set; }
    public bool PauseOnTargetInvulnerability { get; set; }
    public bool EndsOnCasterDeath { get; set; }
    public bool EndsOnTargetInvulnerability { get; set; }
    public bool EndsOnTargetDeath { get; set; }
    public int EnergyAmount { get; set; }
    public bool EnergySteal { get; set; }
    public bool EnergyRemove { get; set; }
    public int Damage1 { get; set; }
    public int Damage2 { get; set; }
    public int BonusDamage1 { get; set; }
    public int Heal1 { get; set; }
    public int ReceiveDamageReductionPoint1 { get; set; }
    public int ReceiveDmgIncreasePoint1 { get; set; }
    public int ReceiveDamageReductionPercent1 { get; set; }
    public int DestructibleDefense1 { get; set; }
    public int DealDamageReductionPoint1 { get; set; }
    public int DealDmgIncreasePoint1 { get; set; }
    public int DealHealIncreasePoint1 { get; set; }
    public int DealHealReductionPercent1 { get; set; }
    public int ReceiveHealReductionPercent1 { get; set; }
    public int Duration1 { get; set; }
    public int Duration2 { get; set; }
    public PointsPercentageModifier? AllDamageDealModifier { get; set; } = new();
    public PointsPercentageModifier? PhysicalDamageDealModifier { get; set; } = new();
    public PointsPercentageModifier? MagicDamageDealModifier { get; set; } = new();
    public PointsPercentageModifier? AllDamageReceiveModifier { get; set; } = new();
    public PointsPercentageModifier? PhysicalDamageReceiveModifier { get; set; } = new();
    public PointsPercentageModifier? MagicDamageReceiveModifier { get; set; } = new();
    public PointsPercentageModifier? HealingDealModifier { get; set; } = new();
    public PointsPercentageModifier? HealingReceiveModifier { get; set; } = new();
    public bool RemoveIt { get; set; }

    public virtual string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return Description + GetTimeLeftAffix();
    }

    public virtual string GetTimeLeftAffix(int? customTurnsLeft = null)
    {
        var turnsLeft = customTurnsLeft ?? TimeLeft;
        return turnsLeft switch
        {
            > 1 when !Infinite => $"\n<color=#EAB65B>{turnsLeft} TURNS LEFT</color>",
            1 when !Infinite => 
                Target.BattlePlayer.PlayerNo == Target.BattleLogic.GetWhoseTurnNo()
                ? "\n<color=#EAB65B>1 TURN LEFT</color>"
                : "\n<color=#EAB65B>ENDS THIS TURN</color>",
            _ => Infinite ? "\n<color=#EAB65B>INFINITE</color>" : "\n<color=red>AFFIX ERROR</color>"
        };
    }

    public string GetTimeLeftAffixActionControl()
    {
        switch (TimeLeft)
        {
            case > 1 when !Infinite:
                return "<color=#EAB65B>" + TimeLeft + " TURNS LEFT ON THE TARGET(S)</color>";
            case 1 when !Infinite:
            {
                return Target.BattlePlayer.PlayerNo != Target.BattleLogic.GetWhoseTurnNo() 
                    ? "<color=#EAB65B>1 TURN LEFT ON THE TARGET(S)</color>" 
                    : "<color=#EAB65B>ENDS THIS TURN ON THE TARGET(S)</color>";
            }
            default:
            {
                if (Infinite){
                    return "<color=#EAB65B>INFINITE ON THE TARGET(S)</color>";
                }

                break;
            }
        }

        return "<color=red>AFFIX ERROR ACTION/CONTROL</color>";
    }

    public string GetActionControlDescription()
    {
        return $"- This champion is using <color=#EAB65B>{OriginAbility.Name}</color>" +
               $"\n{(Paused ? 
                   "<color=red>PAUSED</color>" 
                   : "<color=green>ACTIVE</color>")} - {GetTimeLeftAffixActionControl()}";
    }

    public void TickDown()
    {
        Fresh = false;
		
        if (NoTick) {
            return;
        }
			
        if (TimeLeft == 1 && !Infinite) {
            RemoveIt = true;
        } else if (!Infinite) {
            TimeLeft -= 1;
        }
    }

    public bool WasCastedByMe(int playerNo)
    {
        return OriginAbility.Owner.BattlePlayer.PlayerNo == playerNo;
    }

    public virtual string GetAbilityName()
    {
        return OriginAbility.Name;
    }

    public bool IsHidden(int playerNo)
    {
        if (Hidden)
        {
            return OriginAbility.Owner.BattlePlayer.PlayerNo != playerNo;
        }
		
        return false;
    }

    public bool InvulnerabilityContains(AbilityClass abilityClass)
    {
        if (TypeOfInvulnerability == null) {
            return false;
        }
		
        return TypeOfInvulnerability.Contains(AbilityClass.All) 
               || TypeOfInvulnerability.Contains(abilityClass);
    }

    public bool StunnedContains(AbilityClass abilityClass)
    {
        if (StunType == null) {
            return false;
        }
        
        return StunType.Contains(AbilityClass.All) 
               || StunType.Contains(abilityClass);
    }

    public virtual void AddStack(IActiveEffect activeEffect)
    {
        StandardStack(activeEffect);
    }

    public void StandardStack(IActiveEffect activeEffect)
    {
        Stacks += 1;
    }

    public virtual void DestructibleDefenseStack(IActiveEffect activeEffect)
    {
        DestructibleDefense += activeEffect.DestructibleDefense1;
    }

    public virtual void OnAdd()
    {
    }

    public virtual void OnRemove()
    {
    }

    public virtual void OnApply()
    {
        TickDown();
    }

    public void OnApplyDot()
    {
        Target.ChampionController.ReceiveActiveEffectDamage(Damage1, this, new AppliedAdditionalLogic());
        TickDown();
    }

    public void OnApplyActionControlMe()
    {
        if (ChildrenLinks?.Count == 0) {
            RemoveIt = true;
        }
    }

    public void OnApplyActionControlTargetDamage()
    {
        if (!Paused) {
            OriginAbility.Owner.ChampionController.DealActiveEffectDamage(Damage1, 
                Target, this, new AppliedAdditionalLogic());
        }
		
        TickDown();
        
        if (CasterLink != null && CasterLink.TimeLeft != TimeLeft) {
            CasterLink.TimeLeft = TimeLeft;
        }
    }

    public virtual int ReceiveActiveEffectDamageModifier(IChampion target, int amount)
    {
        return amount;
    }

    public virtual bool CustomDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        return false;
    }

    public virtual void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
    }

    public virtual void AdditionalReceiveActiveEffectLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
    }

    public virtual void RemoveDestructibleDefense(IAbility ability, IActiveEffect activeEffect)
    {
    }

    public virtual void AdditionalSubtractHealthLogic(int toSubtract, IChampion victim, AppliedAdditionalLogic appliedAdditionalLogic)
    {
    }

    public virtual void AfterSubtractHealthLogic()
    {
    }

    public virtual void AdditionalProcessDeathLogic()
    {
    }

    public virtual void DealReaction(IAbility ability)
    {
    }

    public virtual void ReceiveReaction(IAbility ability)
    {
    }

    public virtual bool CounterOnEnemy(IAbility ability)
    {
        return false;
    }

    public virtual bool ReflectOnEnemy(IAbility ability)
    {
        return false;
    }

    public virtual bool CounterOnMe(IAbility ability)
    {
        return false;
    }

    public virtual bool ActiveEffectChecks()
    {
        return true;
    }

    public virtual void StartTurnChecks()
    {
    }

    public virtual void EndTurnChecks()
    {
    }

    public void ModifyTargetsOnReflect(IAbility reflectedAbility, int[] targets)
    {
        switch (reflectedAbility.AbilityType) {
            case AbilityType.EnemiesDebuff:
            case AbilityType.AlliesBuffEnemiesDebuff:
            case AbilityType.EnemiesDamageAndDebuff:
            case AbilityType.EnemiesDamage:
            case AbilityType.EnemiesActionControl:
                for (var i = 0; i < targets.Length; i++) {
                    targets[i] = i < 3 ? 1 : 0;
                }
                break;
            case AbilityType.EnemyDamageAndDebuff:
            case AbilityType.EnemyEnergySteal:
            case AbilityType.EnemyActionControl:
            case AbilityType.EnemyDamage:
            case AbilityType.EnemyDebuff:
            case AbilityType.AllyOrEnemyActiveEffect:
            case AbilityType.AlliesBuff:
            case AbilityType.AllyHeal:
            case AbilityType.AlliesHeal:
            case AbilityType.AllyBuff:
            case AbilityType.AllyOrEnemyDamage:
            case AbilityType.AllyActiveEffectOrEnemyDamage:
                break;
            default:
                throw new ArgumentOutOfRangeException(paramName:nameof(reflectedAbility), 
                    message:"Reflected ability type out of range");
        }
    }
}