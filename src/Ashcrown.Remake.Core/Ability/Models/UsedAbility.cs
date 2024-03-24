namespace Ashcrown.Remake.Core.Ability.Models;

public class UsedAbility
{
    public required int ChampionNo { get; set; }
    public required int AbilityNo { get; set; }
    public required IEnumerable<int> Targets { get; set; }
}