namespace Ashcrown.Remake.Core.Battle.Models.Dtos;

public class MyChampionUpdate : ChampionUpdate
{
    public AbilityUpdate[] AbilityUpdates { get; set; } = new AbilityUpdate[4];
}