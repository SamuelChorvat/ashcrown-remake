using Ashcrown.Remake.Core.Ability.Enums;

namespace Ashcrown.Remake.Core.Ai.Models;

public class AiEnergyUsage
{
    public EnergyType EnergyType { get; init; }
    public int EnergyUsageCount { get; set; } = 0;
}