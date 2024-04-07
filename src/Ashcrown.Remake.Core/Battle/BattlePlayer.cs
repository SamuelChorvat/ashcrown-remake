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
        var playerUpdate = new PlayerUpdate
        {
            Energy = Energy,
            LastTurn = BattleLogic.TurnCount,
            EnergyExchangeRatio = Math.Max(1, GetAliveChampions().Count),
            AbilityHistoryUpdates = GetAbilityHistoryUpdates()
        };

		for (var i = 0; i < Champions.Length; i++)
        {
            playerUpdate.MyChampions[i] = Champions[i].Name;
            playerUpdate.OpponentChampions[i] = BattleLogic.GetOppositePlayer(PlayerNo).Champions[i].Name;
		}
		
        
		for (var i = 0; i < 3; i++) {
            var myChampionsUpdate = new MyChampionUpdate();
			var champion = Champions[i];
            
            myChampionsUpdate.Health = champion.Health;
			for (var j = 0; j < 4; j++)
            {
                myChampionsUpdate.AbilityUpdates[j] = champion.CurrentAbilities[j].GetAbilityUpdate(j + 1);
            }

            foreach (var activeEffect in champion.ActiveEffects)
            {
                if(activeEffect.IsHidden(PlayerNo)) {
                    continue;
                }
                
                myChampionsUpdate.ActiveEffectUpdates.Add(activeEffect.GetActiveEffectUpdate(PlayerNo));
            }

            playerUpdate.MyChampionUpdates[i] = myChampionsUpdate;
        }
		
        var opponentChampionsUpdate = new OpponentChampionUpdate();
		for (var i = 0; i < 3; i++) {
			var champion = BattleLogic.GetOppositePlayer(PlayerNo).Champions[i];
			
			opponentChampionsUpdate.Health = champion.Health;

            foreach (var activeEffect in champion.ActiveEffects)
            {
                if(activeEffect.IsHidden(PlayerNo)) {
                    continue;
                }
                
                opponentChampionsUpdate.ActiveEffectUpdates.Add(activeEffect.GetActiveEffectUpdate(PlayerNo));
            }

            playerUpdate.OpponentChampionUpdates[i] = opponentChampionsUpdate;
        }
		
		return playerUpdate;
    }

    public TargetsUpdate GetTargets(int championNo, int abilityNo)
    {
        return new TargetsUpdate
        {
            ChampionNo = championNo,
            AbilityNo = abilityNo,
            Targets = Champions[championNo - 1].AbilityController.GetPossibleTargetsForAbility(abilityNo)
        };
    }

    public UsableAbilitiesUpdate GetUsableAbilities(int[] currentResources, int energyToSubtract)
    {
        var usableAbilitiesUpdate = new UsableAbilitiesUpdate();
        
        for(var i = 0; i < Champions.Length; i++)
        {
            usableAbilitiesUpdate.UsableAbilities[i] = Champions[i].AbilityController
                .GetUsableAbilities(currentResources, energyToSubtract);
        }

        return usableAbilitiesUpdate;
    }

    private List<AbilityHistoryUpdate> GetAbilityHistoryUpdates()
    {
        return (from abilityHistoryRecord in BattleLogic.BattleHistoryRecorder.AbilityHistoryRecords 
            where abilityHistoryRecord.PlayerNo == PlayerNo 
                  || !abilityHistoryRecord.Invisible select abilityHistoryRecord.GetAbilityHistoryUpdate()).ToList();
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