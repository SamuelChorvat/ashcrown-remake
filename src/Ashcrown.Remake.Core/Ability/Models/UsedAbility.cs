namespace Ashcrown.Remake.Core.Ability.Models;

public class UsedAbility
{
    public int ChampionNo { get; init; }
    public int AbilityNo { get; init; }
    public int[] Targets { get; init; }
}