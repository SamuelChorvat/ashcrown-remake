using Ashcrown.Remake.Core.Battle.Enums;

namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;

public class BattleEndedUpdate : EventArgs
{
    public BattleEndedState BattleEndedState { get; set; }
}