using Ashcrown.Remake.Core.Ability;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champion.Abstract;

public abstract class Champion<T> : IChampion where T : IChampionConstants
{
    private bool[]? _energyUsage;

    protected Champion(IBattleLogic battleLogic,
        IBattlePlayer battlePlayer,
        int championNo,
        ILoggerFactory loggerFactory)
    {
        BattleLogic = battleLogic;
        BattlePlayer = battlePlayer;
        ChampionNo = championNo;
        Name = T.Name;
        Title = T.Title;
        Bio = T.Bio;
        Attributes = T.Attributes;
        Artist = T.Artist;
        ActiveEffects = new List<IActiveEffect>();
        AbilityController = new AbilityController(this, new ActiveEffectFactory());
        ActiveEffectController = new ActiveEffectController(this, loggerFactory.CreateLogger<ActiveEffectController>());
        ChampionController = new ChampionController(this, new ActiveEffectFactory());
        ChampionSpecificsController = new ChampionSpecificsController(this);
    }

    public required IBattleLogic BattleLogic { get; init; }
    public required IBattlePlayer BattlePlayer { get; init; }
    public required int ChampionNo { get; init; }
    public required string Name { get; set; }
    public required string Title { get; set; }
    public required string Bio { get; set; }
    public required int[] Attributes { get; set; }
    public required string Artist{ get; set; }
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
    public IAbility[] CurrentAbilities { get; set; } = [];
    public IList<IAbility>[] Abilities { get; set; } = [];
    public IList<IActiveEffect> ActiveEffects { get; init; }
    public IAbilityController AbilityController { get; init; }
    public IActiveEffectController ActiveEffectController { get; init; }
    public IChampionController ChampionController { get; init; }
    public IChampionSpecificsController ChampionSpecificsController { get; init; }
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

    public void SetStartAbilities(IAbility startAbility1, IAbility startAbility2, IAbility startAbility3, IAbility startAbility4)
    {
        CurrentAbilities = [startAbility1, startAbility2, startAbility3, startAbility4];
        Abilities =
        [
            new List<IAbility> { startAbility1 },
            new List<IAbility> { startAbility2 },
            new List<IAbility> { startAbility3 },
            new List<IAbility> { startAbility4 }
        ];
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