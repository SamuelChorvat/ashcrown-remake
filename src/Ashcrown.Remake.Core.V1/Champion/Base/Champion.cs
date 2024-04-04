using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Battle.Interfaces;
using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.Champion.Base;

public abstract class Champion(
    IBattleLogic battleLogic,
    IBattlePlayer battlePlayer,
    int championNo,
    string championName,
    IAbility startAbility1,
    IAbility startAbility2,
    IAbility startAbility3,
    IAbility startAbility4) : IChampion
{
    private bool[]? _energyUsage;
    
    public required IBattleLogic BattleLogic { get; init; } = battleLogic;
    public required IBattlePlayer BattlePlayer { get; init; } = battlePlayer;
    public required int ChampionNo { get; init; } = championNo;
    public required string Name { get; set; } = championName;
    public int Health { get; set; } = ChampionConstants.ChampionMaxHealth;
    public bool Alive { get; set; } = true;
    public bool Died { get; set; }
    public required IList<IAbility> ReceivedReflectedAbilities { get; init; } = new List<IAbility>();
    
    public bool[]? EnergyUsage
    {
        get
        {
            _energyUsage = [false, false, false, false, false];
            foreach (var abilitiesInSlot in Abilities)
            {
                GetEnergyUsageHelper(abilitiesInSlot);
            }

            return _energyUsage;
        }
    }

    public bool AiReady { get; set; } = true;
    public bool RobotSuitUsed { get; set; }
    public bool SacrificialPactTriggered { get; set; }
    public bool AiLethal { get; set; }
    public int AiTotalDestructibleDefenseLeft { get; set; }
    public int AiTotalDamageToReceiveAfterDestructible { get; set; }
    public int AiTotalHealingToReceive { get; set; }
    public required IAbility[] CurrentAbilities { get; init; } = 
        [startAbility1, startAbility2, startAbility3, startAbility4];
    public required IList<IAbility>[] Abilities { get; init; } =
    [
        new List<IAbility> { startAbility1 },
        new List<IAbility> { startAbility2 },
        new List<IAbility> { startAbility3 },
        new List<IAbility> { startAbility4 }
    ];
    public required IList<IActiveEffect> ActiveEffects { get; init; } = new List<IActiveEffect>();
    public required IAbilityController AbilityController { get; init; } //TODO
    public required IActiveEffectController ActiveEffectController { get; init; } //TODO
    public required IChampionController ChampionController { get; init; } //TODO
    public required IChampionSpecificsController ChampionSpecificsController { get; init; } //TODO
    public void StartTurnMethods()
    {
        ReceivedReflectedAbilities.Clear();
        ActiveEffectController.ApplyActiveEffects();
        ActiveEffectController.ClearMarkedActiveEffects();
        AbilityController.TickDownAbilitiesCooldowns();
        AbilityController.StartTurnFieldsReset();
        ChampionController.SetModifiers();
        ChampionSpecificsController.StartTurnChampionSpecificsChecks();
    }

    public void EndTurnMethods()
    {
        ChampionController.SetModifiers();
        ChampionSpecificsController.EndTurnChampionSpecificsChecks();
    }

    public virtual bool AiCanCounterAbilitySelf(IAbility ability)
    {
        return false;
    }

    public virtual bool AiCanCounterAbilityTarget(IAbility ability)
    {
        return false;
    }

    public virtual bool AiCanReflectAbilitySelf(IAbility ability)
    {
        return false;
    }

    public virtual bool AiCanReflectAbilityTarget(IAbility ability)
    {
        return false;
    }

    public int GetTargetNo(IChampion championTargeting)
    {
        if (championTargeting.BattlePlayer.PlayerNo == BattlePlayer.PlayerNo) {
            return ChampionNo - 1;
        }

        return 3 + ChampionNo - 1;
    }

    public virtual int AiApplyChampionSpecificPenalty(int totalPoints, IAbility ability)
    {
        return totalPoints;
    }

    public bool ReceivedReflectStun()
    {
        return ReceivedReflectedAbilities.Any(ability => ability.Stun);
    }
    
    private void GetEnergyUsageHelper(IEnumerable<IAbility> abilities) {
        foreach (var ability in abilities) {
            for (var i = 0; i < ability.OriginalCost.Length; i++) {
                if (!EnergyUsage![i]) {
                    EnergyUsage[i] = ability.OriginalCost[i] > 0;
                }
            }
        }
    }
}