namespace Ashcrown.Remake.Core.V1.Battle.Interfaces;

public interface IAiBattle
{
    void InitializeAiBattle();
    void EndMyTurn();
    void EndAiTurn();
    void ExchangeEnergy();
    void GetTargets();
    void GetUsableAbilities();
    void Surrender();
}