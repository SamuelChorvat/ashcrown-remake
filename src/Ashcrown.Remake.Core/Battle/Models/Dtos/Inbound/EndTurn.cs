namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;

public class EndTurn
{
    public int[]? SpentEnergy { get; set; }
    public IList<EndTurnAbility>? EndTurnAbilities { get; set; }
}