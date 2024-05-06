using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Ai.Models;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Ability.Base;

public abstract class AbilityBase : IAbility
{
    private const int MaxTotalAbilityCost = 5;
    
    private int _cooldownModifier;
    private bool _cooldownChanged;
    private int _randomCostModifier;
    private bool _costChanged;
    private bool _aiActive = true;

    private readonly IAiAbilityHelper _aiAbilityHelper;

    protected AbilityBase(IChampion champion,
        string abilityName,
        int originalCooldown,
        int[] originalCost,
        AbilityClass[] abilityClasses,
        AbilityTarget abilityTarget,
        AbilityType abilityType,
        int abilitySlot)
    {
        Owner = champion;
        Name = abilityName;
        OriginalCooldown = originalCooldown;
        OriginalCost = originalCost;
        AbilityClasses = abilityClasses;
        Target = abilityTarget;
        AbilityType = abilityType;
        AbilitySlot = abilitySlot;
        _aiAbilityHelper = new AiAbilityHelper(this);
    }

    public IChampion Owner { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public int OriginalCooldown { get; set; }
    public int[] OriginalCost { get; set; }
    public AbilityClass[] AbilityClasses { get; set; }
    public AbilityTarget Target { get; set; }
    public AbilityType AbilityType { get; set; }
    public int AbilitySlot { get; set; }
    public bool Active { get; set; } = true;
    public bool IgnoreInvulnerability { get; set; }
    public bool Counterable { get; set; } = true;
    public bool Reflectable { get; set; } = true;
    public bool JustUsed { get; set; }
    public bool Harmful { get; set; }
    public bool Helpful { get; set; }
    public bool Debuff { get; set; }
    public bool Buff { get; set; }
    public bool CannotBeRemoved { get; set; }
    public bool Healing { get; set; }
    public bool Damaging { get; set; }
    public int EnergyAmount { get; set; }
    public bool UniqueActiveEffect { get; set; }
    public bool Invisible { get; set; }
    public bool CostIncrease { get; set; }
    public AbilityClass[]? CostIncreaseClasses { get; set; }
    public AbilityClass[]? CostDecreaseClasses { get; set; }
    public AbilityClass[]? CooldownDecreaseClasses { get; set; }
    public AbilityClass[]? CounterClasses { get; set; }
    public bool CannotBeIgnored { get; set; }
    public bool Invulnerability { get; set; }
    public AbilityClass[]? TypeOfInvulnerability { get; set; }
    public bool Stun { get; set; }
    public AbilityClass[]? StunType { get; set; }
    public bool DisableInvulnerability { get; set; }
    public bool DisableDamageReceiveReduction { get; set; }
    public bool IgnoreStuns { get; set; }
    public bool IgnoreHarmful { get; set; }
    public bool PiercingDamage { get; set; }
    public bool AfflictionDamage { get; set; }
    public bool MagicDamage { get; set; }
    public bool PhysicalDamage { get; set; }
    public int ToReady { get; set; }
    public int CooldownModifier
    {
        set
        {
            _cooldownModifier += value;
            _cooldownChanged = true;
        }
    }
    public int RandomCostModifier
    {
        set
        {
            _randomCostModifier += value;
            _costChanged = true;
        }
    }
    public bool SelfDisplay { get; set; }
    public bool SelfCast { get; set; }
    public int TimesUsed { get; set; }
    public bool Copied { get; set; }
    public int Damage1 { get; set; }
    public int Damage2 { get; set; }
    public int Damage3 { get; set; }
    public int BonusDamage1 { get; set; }
    public int BonusDamage2 { get; set; }
    public int Heal1 { get; set; }
    public int BonusHeal1 { get; set; }
    public int ReceiveDamageReductionPoint1 { get; set; }
    public int ReceiveDamageIncreasePoint1 { get; set; }
    public int ReceiveDamageReductionPercent1 { get; set; }
    public int DestructibleDefense1 { get; set; }
    public int DealDamageReductionPoint1 { get; set; }
    public int DealDmgIncreasePoint1 { get; set; }
    public int DealHealIncreasePoint1 { get; set; }
    public int DealHealReductionPercent1 { get; set; }
    public int ReceiveHealReductionPercent1 { get; set; }
    public int Duration1 { get; set; }
    public int Duration2 { get; set; }
    public bool EnergySteal { get; set; }
    public bool EnergyRemove { get; set; }
    public bool DoNotModifyOnDealDamage { get; set; }
    public string? ActiveEffectOwner { get; set; }
    public string? ActiveEffectName { get; set; }
    public string? ActiveEffectSourceName { get; set; }
    public string? ActiveEffectTargetName { get; set; }
    public string? ActiveEffectAlliesName { get; set; }
    public string? ActiveEffectEnemiesName { get; set; }
    public string? ActiveEffectAllyName { get; set; }
    public string? ActiveEffectEnemyName { get; set; }
    public bool AiStandardSelfInvulnerability { get; set; }
    public bool AiActive {
        get => Active && _aiActive;
        set => _aiActive = value;
    }

    public int GetCurrentCooldown()
    {
        var currentCooldown = OriginalCooldown;
		
        switch (_cooldownModifier)
        {
            case < 0 when currentCooldown + _cooldownModifier < 0:
                currentCooldown = 0;
                break;
            case < 0:
                currentCooldown += _cooldownModifier;
                break;
            case > 0 when currentCooldown + _cooldownModifier > 99:
                currentCooldown = 99;
                break;
            case > 0:
                currentCooldown += _cooldownModifier;
                break;
        }
		
        return currentCooldown;
    }

    public int[] GetCurrentCost()
    {
        var cost = OriginalCost.ToArray(); 
		
        switch (_randomCostModifier)
        {
            case < 0 when cost[(int)EnergyType.Random] + _randomCostModifier < 0:
                cost[(int)EnergyType.Random] = 0;
                break;
            case < 0:
                cost[(int)EnergyType.Random] += _randomCostModifier;
                break;
            case > 0:
            {
                var toAddLimit = MaxTotalAbilityCost - TotalCost(cost);
                if (toAddLimit > 0) {
                    var toAdd = Math.Min(toAddLimit, _randomCostModifier);
                    cost[(int)EnergyType.Random] += toAdd;
                }

                break;
            }
        }
		
        return cost;
    }

    public bool IsReady()
    {
        return ToReady == 0;
    }

    public void PutOnCooldown()
    {
        JustUsed = true;
        Active = false;
        ToReady = GetCurrentCooldown();
    }

    public void TickDownCooldown()
    {
        if(GetCurrentCooldown() == 0) {
            Active = true;
            ToReady = 0;
        }
		
        if (JustUsed) {
            JustUsed = false;
        } else if(ToReady > 0) {
            ToReady -= 1;
        } 
		
        if (ToReady == 0) {
            Active = true;
        }
    }

    public bool AbilityClassesContains(AbilityClass abilityClass)
    {
        return abilityClass == AbilityClass.All 
               || AbilityClasses.Contains(abilityClass);
    }

    public int TotalCost(IEnumerable<int> cost)
    {
        return cost.Sum();
    }

    public void RemoveCostModifier(int modifier)
    {
        if (_costChanged) {
            _randomCostModifier += modifier;
        }
    }

    public bool IsFree()
    {
        return TotalCost(GetCurrentCost()) == 0;
    }

    public int GetTotalCurrentCost()
    {
        return TotalCost(GetCurrentCost());
    }

    public void RemoveCooldownModifier(int mod)
    {
        if (_cooldownChanged) {
            _cooldownModifier += mod;
        }
    }

    public virtual int ReceiveAbilityDamageModifier(IChampion target, int amount)
    {
        return amount;
    }

    public virtual int DealAbilityHealingModifier(IChampion target, int amount)
    {
        return amount;
    }

    public virtual bool CustomReceiveAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        return false;
    }

    public virtual void AdditionalReceiveAbilityHealingLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
    }

    public virtual int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        return amount;
    }

    public virtual void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
    }

    public virtual void AdditionalReceiveAbilityEnergyStealLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
    }

    public virtual void AdditionalSubtractHealthLogic(int toSubtract, IChampion victim,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
    }

    public virtual int SubtractHealthModifier(int toSubtract, IChampion victim)
    {
        return toSubtract;
    }

    public virtual bool UseChecks()
    {
        return true;
    }

    public virtual int[] TargetsModifier(int[] targets)
    {
        return targets;
    }

    public virtual void StartTurnChecks()
    {
    }

    public virtual void EndTurnChecks()
    {
    }

    public virtual void OnUse()
    {
    }

    public AiMaximizedAbility AiMaximizeAbility<T>() where T : IAiPointsCalculator
    {
        if (AiStandardSelfInvulnerability) {
            return _aiAbilityHelper.StandardSelfInvulnerabilityMaximizer<T>();
        }

        return Target switch
        {
            AbilityTarget.Self => _aiAbilityHelper.SelfTargetAbilityMaximizer<T>(),
            AbilityTarget.Ally => _aiAbilityHelper.AllyTargetAbilityMaximizer<T>(),
            AbilityTarget.Allies => _aiAbilityHelper.AlliesTargetAbilityMaximizer<T>(),
            AbilityTarget.Enemy => _aiAbilityHelper.EnemyTargetAbilityMaximizer<T>(),
            AbilityTarget.Enemies => _aiAbilityHelper.EnemiesTargetAbilityMaximizer<T>(),
            AbilityTarget.AllyOrEnemy => _aiAbilityHelper.AllyOrEnemyTargetAbilityMaximizer<T>(),
            AbilityTarget.All => _aiAbilityHelper.AllTargetAbilityMaximizer<T>(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public int[] GetPossibleTargets()
    {
        var targetsToReturn = new[] { 0, 0, 0, 0, 0, 0 };
        switch (Target)
        {
            case AbilityTarget.Self when !Owner.ChampionController.IsInvulnerableToFriendlyAbility(this):
                targetsToReturn[Owner.ChampionNo - 1] = 1;
                break;
            case AbilityTarget.Ally:
            case AbilityTarget.Allies:
            {
                for (var j = 0; j < 3; j++) {
                    if (Owner.BattlePlayer.Champions[j].Alive 
                        && !Owner.BattlePlayer.Champions[j].ChampionController.IsInvulnerableToFriendlyAbility(this)) {
                        targetsToReturn[j] = 1;
                    }
                }

                break;
            }
            case AbilityTarget.Enemy:
            case AbilityTarget.Enemies:
            {
                for (var j = 0; j < 3; j++) {
                    if (!Owner.BattlePlayer.GetEnemyPlayer().Champions[j].ChampionController.IsClientChampionInvulnerableTo(this)) {
                        targetsToReturn[j + 3] = 1;
                    }
                }

                break;
            }
            case AbilityTarget.AllyOrEnemy:
            case AbilityTarget.All:
            {
                for (var j = 0; j < 3; j++) {
                    if (Owner.BattlePlayer.Champions[j].Alive && !Owner.BattlePlayer.Champions[j].ChampionController.IsInvulnerableToFriendlyAbility(this)) {
                        targetsToReturn[j] = 1;
                    }

                    if (!Owner.BattlePlayer.GetEnemyPlayer().Champions[j].ChampionController.IsClientChampionInvulnerableTo(this)) {
                        targetsToReturn[j + 3] = 1;
                    }
                }

                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (UniqueActiveEffect) {
            for (var j = 0; j < 3; j++) {
                if(Owner.BattlePlayer.Champions[j].ActiveEffectController.ActiveEffectPresentByOriginAbilityName(Name)) {
                    targetsToReturn[j] = 0;
                }

                if (Owner.BattlePlayer.GetEnemyPlayer().Champions[j].ActiveEffectController.ActiveEffectPresentByOriginAbilityName(Name)) {
                    targetsToReturn[j + 3] = 0;
                }
            }
        }

        targetsToReturn = TargetsModifier(targetsToReturn);

        if (SelfDisplay) {
            targetsToReturn[Owner.ChampionNo - 1] = -1;
        }

        if (!SelfCast && targetsToReturn[Owner.ChampionNo -1] == 1) {
            targetsToReturn[Owner.ChampionNo - 1] = 0;
        }

        return targetsToReturn;
    }

    public virtual int CalculateTotalPointsForTarget<T>(IChampion target) where T : IAiPointsCalculator
    {
        if (Owner.AiReady) throw new NotImplementedException();
        return 0;
    }

    public virtual int CalculateSingletonSelfEffectTotalPoints()
    {
        return 0;
    }

    public virtual AbilityUpdate GetAbilityUpdate(int abilityNo)
    {
        return new AbilityUpdate
        {
            Name = Name,
            Description = Description,
            Cooldown = GetCurrentCooldown(),
            ReadyIn = Owner.Alive ? ToReady : 0,
            Cost = GetCurrentCost(),
            CanUse = Owner.AbilityController.ClientCanUseAbilityChecks(this)
                     && Owner.AbilityController.GetNumberOfTargets(
                         Owner.AbilityController.GetPossibleTargetsForAbility(abilityNo)) > 0,
            Target = Target.ToString(),
            SelfDisplay = SelfDisplay,
            SelfCast = SelfCast,
            Classes = AbilityClasses.Select(x => x.ToString()).ToArray()
        };
    }
}