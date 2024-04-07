namespace Ashcrown.Remake.Core.Ability.Models;

public class UsedAbility
{
    public required int ChampionNo { get; init; }
    public required int AbilityNo { get; init; }
    public required int[] Targets { get; init; }
}