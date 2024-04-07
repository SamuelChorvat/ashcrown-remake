namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;

public class EndTurnAbility
{
    public int? CasterNo { get; set; }
    public int? Order { get; set; }
    public int? AbilityNo { get; set; }
    public int[]? Targets { get; set; }
}