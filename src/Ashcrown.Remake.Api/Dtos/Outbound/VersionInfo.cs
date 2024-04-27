namespace Ashcrown.Remake.Api.Dtos.Outbound;

public class VersionInfo
{
    public string ServerVersion { get; init; } = AshcrownApiConstants.ServerVersion;
    public string MinimumClientVersion { get; init; } = AshcrownApiConstants.MinimumClientVersion;
}
