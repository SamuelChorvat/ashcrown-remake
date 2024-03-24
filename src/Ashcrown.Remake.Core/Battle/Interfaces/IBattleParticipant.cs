using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface IBattleParticipant
{
    IEnumerable<IChampion> Champions { get; init; }
    IEnumerable<int> Energy { get; init; }
    bool IsAiControlled { get; init; }

    bool IsDead();
    int GetNumberOfDead();
    IEnumerable<IChampion> GetAlive();
    void AddEnergy(EnergyType energyType);
    void SpendEnergy(IEnumerable<int> spentEnergy);
    void GenerateEnergy();
    int LoseRandomEnergy(IChampion champion, IAbility ability, IActiveEffect activeEffect);
    void GainRandomEnergy();
    int GetTotalEnergy();
    IChampion GetRandomAliveChampion();
    IEnumerable<IChampion> GetOtherChampions(IChampion champion);
    void UseAbilities(IEnumerable<UsedAbility> usedAbility, IEnumerable<int> usedEnergy);
    void UseAbility(int championNo, int abilityNo, IEnumerable<int> targets);
}