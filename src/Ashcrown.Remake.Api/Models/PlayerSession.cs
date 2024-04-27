namespace Ashcrown.Remake.Api.Models;

public class PlayerSession
{
    public DateTime LastRequestDateTime { get; set; } = DateTime.UtcNow;
}