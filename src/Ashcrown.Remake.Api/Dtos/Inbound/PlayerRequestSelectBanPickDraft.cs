namespace Ashcrown.Remake.Api.Dtos.Inbound;

public class PlayerRequestSelectBanPickDraft : PlayerRequestMatchId
{
    public required string ChampionName { get; set; }
}