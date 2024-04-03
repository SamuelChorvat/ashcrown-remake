namespace Ashcrown.Remake.Core.V1.Battle.Interfaces;

public interface IAiBattle : IBattle
{
    void EndMyTurn();
    void EndAiTurn();
    void InitializeAiBattle();
}