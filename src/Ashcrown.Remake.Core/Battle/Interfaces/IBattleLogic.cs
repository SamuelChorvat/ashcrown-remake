using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface IBattleLogic
{
	IBattleHistoryRecorder BattleHistoryRecorder { get; init; }
	IList<IChampion> DiedChampions { get; init; }
	DateTime StartTime { get; init; }
	int TurnCount { get; init; }
	bool AiBattle { get; init; }
	IBattlePlayer[] BattlePlayers { init; }
	void SetBattlePlayer(int playerNo, string[] championNames, bool aiOpponent);
	IBattlePlayer GetBattlePlayer(int playerNo);
	IBattlePlayer GetOppositePlayer(int playerNo);
	int GetOppositePlayerNo(int playerNo);
	int GetAiOpponentPlayerNo();
	IBattlePlayer GetAiOpponentPlayerInfo();
	void ProcessDeaths();
	bool IsPlayerDead(int playerNo);
	bool AbilitiesUsed(int playerNo, EndTurn endTurn, int[] spentRes);
	public void InitializePlayers();
	void EndTurnProcesses(int playerNo);
	PlayerUpdate ChangeTurnAndGetInfo();
	int GetWhoseTurnNo();
	DateTime GetBattleDuration();
}