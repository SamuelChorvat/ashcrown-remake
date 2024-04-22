using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Ai.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Ai;

public class AiEnergyUsageController : IAiEnergyUsageController
{
    public IList<AiEnergyUsage> EnergyUsages { get; private set; } = new List<AiEnergyUsage>();
    
    private bool _ordered;

    public AiEnergyUsageController()
    {
        Reset();
    }
    
    public void Reset()
    {
        EnergyUsages.Clear();
        _ordered = false;
        for (var i = 0; i < 4; i++) {
            EnergyUsages.Add(new AiEnergyUsage {EnergyType = (EnergyType) i});
        }
    }

    public void IncrementEnergyUsage(EnergyType energyType, int incrementBy)
    {
        EnergyUsages[(int) energyType].EnergyUsageCount += incrementBy;
    }

    public void CalculateEnergyUsage(IChampion[] champions)
    {
        Reset();
        foreach (var champion in champions) {
            if (!champion.Alive) {
                continue;
            }

            foreach (var slotAbilities in champion.Abilities) {
                foreach (var ability in slotAbilities) {
                    var abilityCost = ability.GetCurrentCost();
                    for (var j = 0; j < 4; j++) {
                        IncrementEnergyUsage((EnergyType) j, abilityCost[j]);
                    }
                }
            }
        }
    }

    public EnergyType GetLeastUsedEnergyType(int[] energyLeftForSpending)
    {
        for (var i = 0; i < 4; i++) {
            if (energyLeftForSpending[GetLeastUsedEnergyIndex(i)] > 0) {
                return (EnergyType) GetLeastUsedEnergyIndex(i);
            }
        }

        throw new Exception("No energy left for spending");
    }

    private int GetLeastUsedEnergyIndex(int index) {
        if (!_ordered) {
            OrderEnergyUsage();
        }

        return (int) EnergyUsages[index].EnergyType;
    }
    
    private void OrderEnergyUsage() {
        _ordered = true;
        EnergyUsages = EnergyUsages.OrderBy(x => x.EnergyUsageCount).ToList();
    }
}