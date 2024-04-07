namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;

public class MyChampionUpdate : ChampionUpdate
{
    public AbilityUpdate[] AbilityUpdates { get; set; } = new AbilityUpdate[4];
}