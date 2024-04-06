namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface IAiBattle : IBattle
{
    void EndMyTurn();
    void EndAiTurn();
    void InitializeAiBattle();
}