using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Ai.Models;

public class AiMaximizedAbility(int points, int[] targets)
{
    public int CasterNo { get; set; }
    public int AbilityNo  { get; set; }
    public int Points { get; } = Math.Max(points, 0);

    public int[] Targets { get; init; } = targets;
    public IAbility? Ability { get; set; }
    public IChampion? Champion { get; set; }
}