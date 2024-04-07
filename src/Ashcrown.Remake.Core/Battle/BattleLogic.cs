using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle;

public class BattleLogic(
    bool aiBattle,
    ITeamFactory teamFactory,
    IBattleHistoryRecorder battleHistoryRecorder) : IBattleLogic
{
    public IBattleHistoryRecorder BattleHistoryRecorder { get; init; } = battleHistoryRecorder;
    public IList<IChampion> DiedChampions { get; init; } = new List<IChampion>();
    public DateTime StartTime { get; init; } = DateTime.UtcNow;
    public int TurnCount { get; init; }
    public bool AiBattle { get; init; } = aiBattle;
    public IBattlePlayer[] BattlePlayers { get; init; } = [];
    
    public void SetBattlePlayer(int playerNo, string[] championNames, bool aiOpponent)
    {
        BattlePlayers[playerNo - 1] = new BattlePlayer(playerNo, aiOpponent, championNames, this, teamFactory);
    }

    public IBattlePlayer GetBattlePlayer(int playerNo)
    {
        return BattlePlayers[playerNo - 1];
    }

    public IBattlePlayer GetOppositePlayer(int playerNo)
    {
        return playerNo == 1 ? BattlePlayers[1] : BattlePlayers[0];
    }

    public int GetOppositePlayerNo(int playerNo)
    {
        return playerNo == 1 ? 2 : 1;
    }

    public int GetAiOpponentPlayerNo()
    {
        return BattlePlayers[0].AiOpponent ? 1 : 2;
    }

    public IBattlePlayer GetAiOpponentPlayerInfo()
    {
        return BattlePlayers[0].AiOpponent ? BattlePlayers[0] : BattlePlayers[1];
    }

    public void ProcessDeaths()
    {
        foreach (var diedChampion in DiedChampions)
        {
            diedChampion.ChampionController.ProcessDeath();
        }

        DiedChampions.Clear();
    }

    public bool IsPlayerDead(int playerNo)
    {
        return GetBattlePlayer(playerNo).IsDead();
    }

    public bool AbilitiesUsed(int playerNo, object info, int[] spentRes)
    {
        throw new NotImplementedException();
    }

    public void InitializePlayers()
    {
        throw new NotImplementedException();
    }

    public void EndTurnProcesses(int playerNo)
    {
        throw new NotImplementedException();
    }

    public object ChangeTurnAndGetInfo()
    {
        throw new NotImplementedException();
    }

    public int GetWhoseTurnNo()
    {
        throw new NotImplementedException();
    }

    public DateTime GetBattleDuration()
    {
        throw new NotImplementedException();
    }
}