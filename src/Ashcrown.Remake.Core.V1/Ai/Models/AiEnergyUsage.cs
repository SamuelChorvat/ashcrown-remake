using Ashcrown.Remake.Core.V1.Ability.Enums;

namespace Ashcrown.Remake.Core.V1.Ai.Models;

public class AiEnergyUsage
{
    public EnergyType EnergyType { get; init; }
    public int EnergyUsageCount { get; set; } = 0;
}