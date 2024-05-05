using Ashcrown.Remake.Api.Models.Enums;

namespace Ashcrown.Remake.Api.Dtos.Outbound;

public class AcceptedMatchStatusUpdate
{
    public required AcceptedMatchStatus AcceptedMatchStatus { get; init; }
}