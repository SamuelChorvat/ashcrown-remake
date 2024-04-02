namespace Ashcrown.Remake.Core.V1.Battle.Interfaces;

public interface IBattle
{
    void EndTurn();
    void ExchangeEnergy();
    void GetTargets();
    void GetUsableAbilities();
    void Surrender();
}