namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;

public class ExchangeEnergyUpdate
{
    public required int[] NewEnergy { get; set; }
    public required UsableAbilitiesUpdate UsableAbilitiesUpdate { get; set; }
}