namespace Ashcrown.Remake.Api.Dtos.Inbound;

public class PlayerRequestSelectBlindChampions : PlayerRequest
{
    public required string[] BlindChampions { get; set; }
}