namespace Ashcrown.Remake.Api.Dtos.Inbound;

public class PlayerRequestSelectProfileIcon : PlayerRequest
{
    public required string IconName { get; set; }
}