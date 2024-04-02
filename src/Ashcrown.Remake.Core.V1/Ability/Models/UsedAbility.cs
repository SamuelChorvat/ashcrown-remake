namespace Ashcrown.Remake.Core.V1.Ability.Models;

public class UsedAbility
{
    public int ChampionNo { get; init; }
    public int AbilityNo { get; init; }
    public int[] Targets { get; init; }
}