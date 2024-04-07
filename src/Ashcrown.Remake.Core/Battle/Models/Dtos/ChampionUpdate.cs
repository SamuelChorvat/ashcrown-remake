namespace Ashcrown.Remake.Core.Battle.Models.Dtos;

public abstract class ChampionUpdate
{
    public int Health { get; set; }
    public IList<ActiveEffectUpdate> ActiveEffectUpdates { get; set; } = new List<ActiveEffectUpdate>();
}