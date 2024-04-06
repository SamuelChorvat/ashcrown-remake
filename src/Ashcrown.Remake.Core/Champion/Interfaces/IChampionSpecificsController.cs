namespace Ashcrown.Remake.Core.Champion.Interfaces;

public interface IChampionSpecificsController
{
    IChampion Owner { get; init; }
    void StartTurnChampionSpecificsChecks();
    void EndTurnChampionSpecificsChecks();

}