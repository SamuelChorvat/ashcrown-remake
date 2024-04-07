using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Battle.Models.Dtos;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle;

public class BattlePlayer(IBattleLogic battleLogic) : IBattlePlayer
{
    public int PlayerNo { get; init; }
    public IBattleLogic BattleLogic { get; init; } = battleLogic;
    public IChampion[] Champions { get; init; } = [];
    public int[] Energy { get; init; } = [0,0,0,0];
    public bool AiOpponent { get; init; }
    
    public bool IsDead()
    {
        return Champions.All(t => !t.Alive);
    }

    public void AddEnergy(EnergyType energyType)
    {
        Energy[(int)energyType] += 1;
    }

    public bool SpendEnergy(int[] spentEnergy)
    {
        for (var i = 0; i < spentEnergy.Length; i++)
        {
            if (spentEnergy[i] > Energy[i] || spentEnergy[i] < 0) {
                return false;
            }

            Energy[i] -= spentEnergy[i];
        }
		
        return true;
    }

    public int NoOfDead()
    {
        return Champions.Count(t => !t.Alive);
    }

    public IList<IChampion> GetAliveChampions()
    {
        return Champions.Where(t => t.Alive).ToList();
    }

    public bool ChampionUseAbility(int championNo, int abilityNo, int[] targets)
    {
        var ability = Champions[championNo - 1].AbilityController.GetCurrentAbility(abilityNo);
        return Champions[championNo - 1].AbilityController.UseAbility(ability, targets);
    }

    public IAbility GetAbility(int championNo, int abilityNo)
    {
        return Champions[championNo - 1].AbilityController.GetCurrentAbility(abilityNo);
    }

    public void GenerateEnergy()
    {
        var random = new Random();
        var energyAdded = new List<int>();
        for (var i = 0; i < 3; i++)
        {
            if (!Champions[i].Alive) continue;
            var energyToAdd = random.Next(4) + 1;
            var sameEnergyAdded = energyAdded.Count( x => x == energyToAdd);

            if (sameEnergyAdded == 2) {
                var replacementEnergyToAdd = energyToAdd;
                while (replacementEnergyToAdd == energyToAdd) {
                    replacementEnergyToAdd = random.Next(4) + 1;
                }
                energyToAdd = replacementEnergyToAdd;
            } else {
                energyAdded.Add(energyToAdd);
            }

            AddEnergy((EnergyType)energyToAdd);
        }
    }

    public EnergyType LoseRandomEnergy(IChampion target, IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        if (target.ChampionController.IsIgnoringHarmful() 
            || target.ChampionController.IsInvulnerableTo(ability) 
            || target.ChampionController.IsInvulnerableTo(activeEffect:activeEffect) 
            || target.BattlePlayer.PlayerNo != PlayerNo) {
            return EnergyType.NoEnergy;
        }
		
        var available = new List<int>();
        for (var i = 0; i < Energy.Length; i++) {
            if (Energy[i] > 0) {
                available.Add(i);
            }
        }

        if (available.Count <= 0) return EnergyType.NoEnergy;
        var random = new Random();
        var lost = random.Next(available.Count);
        Energy[available[lost]] -= 1;
        return (EnergyType) available[lost];

    }

    public void GainRandomEnergy()
    {
        var random = new Random();
        AddEnergy((EnergyType)random.Next(4));
    }

    //TODO should this check it from the same character?
    public void RemoveActiveEffectFromAll(string activeEffectName)
    {
        foreach (var champion in Champions)
        {
            var activeEffect = champion.ActiveEffectController.GetActiveEffectByName(activeEffectName);
            if (activeEffect != null) {
                champion.ActiveEffectController.RemoveActiveEffect(activeEffect);
            }
        }
    }

    public IActiveEffect? CheckActiveEffectPresentOnAny(string activeEffectName, int championNo, int playerNo)
    {
        return Champions.Select(t => 
            t.ActiveEffectController.GetActiveEffectByName(activeEffectName, championNo, playerNo))
            .OfType<IActiveEffect>().FirstOrDefault();
    }

    public void TriggerStartTurnMethods()
    {
        foreach (var champion in Champions)
        {
            champion.StartTurnMethods();
        }
    }

    public void CheckResume()
    {
        foreach (var champion in Champions)
        {
            champion.ActiveEffectController.CheckResume();
        }
    }

    public void TriggerEndTurnMethods()
    {
        foreach (var champion in Champions)
        {
            champion.EndTurnMethods();
        }
    }

    public IChampion GetRandomMyChampion()
    {
        var alive = GetAliveChampions();
		
        if (alive.Count == 1) {
            return alive[0];
        }

        var random = new Random();
        var index = random.Next(alive.Count);
        return alive[index];
    }

    //TODO what about invulnerability, what if the target is invulnerable, Seth ignores invul for now
    public IChampion GetRandomEnemyChampion()
    {
        var alive = BattleLogic.GetOppositePlayer(PlayerNo).GetAliveChampions();

        switch (alive.Count)
        {
            case 0:
                return BattleLogic.GetOppositePlayer(PlayerNo).Champions[0];
            case 1:
                return alive[0];
        }

        var random = new Random();
        var index = random.Next(alive.Count);
        return alive[index];
    }

    //TODO does this behave correctly in reflect scenario?
    public IChampion[]? GetOtherChampions(IChampion champion)
    {
        IChampion[] otherChampions = champion.ChampionNo switch
        {
            1 => [Champions[1], Champions[2]],
            2 => [Champions[0], Champions[2]],
            3 => [Champions[0], Champions[1]],
            _ => []
        };

        return otherChampions[0].Alive switch
        {
            true when otherChampions[1].Alive => otherChampions,
            true => [otherChampions[0]],
            _ => otherChampions[1].Alive ? [otherChampions[1]] : null
        };
    }

    public PlayerUpdate GetPlayerUpdate()
    {
        throw new NotImplementedException();
    }

    public TargetsUpdate GetTargets(int championNo, int abilityNo)
    {
        throw new NotImplementedException();
    }

    public UsableAbilitiesUpdate GetUsableAbilities(int[] currentResources, int energyToSubtract)
    {
        throw new NotImplementedException();
    }

    public IBattlePlayer GetEnemyPlayer()
    {
        return BattleLogic.GetOppositePlayer(PlayerNo);
    }

    public int GetTotalEnergy()
    {
        return Energy.Sum();
    }

    public bool AiCanAnyoneTargetCounterAbility(IAbility ability)
    {
        return Champions.Any(champion => champion.AiCanCounterAbilityTarget(ability));
    }

    public bool AiCanAnyoneTargetReflectAbility(IAbility ability)
    {
        return Champions.Any(champion => champion.AiCanReflectAbilityTarget(ability));
    }
}