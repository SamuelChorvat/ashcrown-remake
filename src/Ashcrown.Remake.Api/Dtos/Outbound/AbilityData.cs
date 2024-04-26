namespace Ashcrown.Remake.Api.Dtos.Outbound;

public class AbilityData
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int Cooldown { get; set; }
    public required string[] Classes { get; set; }
    public required int[] Cost { get; set; }
    public required int Slot { get; set; }
}