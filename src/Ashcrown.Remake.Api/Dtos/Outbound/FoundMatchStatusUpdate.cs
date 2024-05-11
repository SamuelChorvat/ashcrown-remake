using Ashcrown.Remake.Api.Models.Enums;

namespace Ashcrown.Remake.Api.Dtos.Outbound;

public class FoundMatchStatusUpdate
{
    public required FoundMatchStatus FoundMatchStatus { get; init; }
}