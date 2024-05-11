namespace Ashcrown.Remake.Api.Dtos.Inbound;

public class PlayerRequest
{
    public required string Secret { get; set; }
    public required string Name { get; set; }
}