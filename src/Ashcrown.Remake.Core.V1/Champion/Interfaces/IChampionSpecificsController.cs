namespace Ashcrown.Remake.Core.V1.Champion.Interfaces;

public interface IChampionSpecificsController
{
    IChampion Owner { get; init; }
    void StartTurnChampionSpecificsChecks();
    void EndTurnChampionSpecificsChecks();

}