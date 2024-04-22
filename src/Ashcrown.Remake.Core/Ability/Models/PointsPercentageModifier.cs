namespace Ashcrown.Remake.Core.Ability.Models;

public class PointsPercentageModifier(int points = 0, int percentage = 0)
{
    public int Points { get; set; } = points;
    public int Percentage { get; set; } = percentage;
}