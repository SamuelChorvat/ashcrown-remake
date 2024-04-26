namespace Ashcrown.Remake.Api.Dtos.Outbound;

public class ChampionData
{
    public required string Name { get; set; }
    public required string Title { get; set; }
    public required string Bio { get; set; }
    public required int[] Attributes { get; set; }
    public required string Artist{ get; set; }
    public List<AbilityData> Abilities = [];
}