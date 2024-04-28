using Ashcrown.Remake.Core.Champion;

namespace Ashcrown.Remake.Api.Dtos.Outbound;

public class VersionInfo
{
    public string ServerVersion { get; init; } = AshcrownApiConstants.ServerVersion;
    public string MinimumClientVersion { get; init; } = AshcrownApiConstants.MinimumClientVersion;
    public required List<ChampionData> PlayableChampions { get; init; }
}
