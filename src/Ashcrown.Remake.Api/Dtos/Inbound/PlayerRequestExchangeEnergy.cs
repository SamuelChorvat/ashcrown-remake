using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;

namespace Ashcrown.Remake.Api.Dtos.Inbound;

public class PlayerRequestExchangeEnergy : PlayerRequestMatchId
{
    public required ExchangeEnergy ExchangeEnergy { get; init; }
}