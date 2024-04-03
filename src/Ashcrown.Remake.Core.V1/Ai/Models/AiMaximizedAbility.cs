using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.Ai.Models;

public class AiMaximizedAbility
{
    private readonly int _points;
    
    public int CasterNo { get; set; }
    public int AbilityNo  { get; set; }
    public int Points 
    { 
        get => _points; 
        init => _points = Math.Max(value, 0);
    }
    public required int[] Targets { get; init; }
    public IAbility? Ability { get; set; }
    public IChampion? Champion { get; set; }
}