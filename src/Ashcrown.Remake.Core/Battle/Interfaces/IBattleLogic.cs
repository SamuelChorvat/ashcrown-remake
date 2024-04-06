using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface IBattleLogic
{
	IList<IChampion> DiedChampions { get; init; }
	DateTime StartTime { get; init; }
	int TurnCount { get; init; }
	bool AiBattle { get; init; }
	IBattlePlayer[] PlayersBattleInfo { init; }
	void SetPlayerBattleInfo(int playerNo, string[] championNames, bool aiOpponent);
	IBattlePlayer GetPlayerBattleInfo(int playerNo);
	IBattlePlayer GetOppositePlayer(int playerNo);
	int GetOppositePlayerNo(int playerNo);
	int GetAiOpponentPlayerNo();
	IBattlePlayer GetAiOpponentPlayerInfo();
	void ProcessDeaths();
	bool IsPlayerDead(int playerNo);
	bool AbilitiesUsed(int playerNo, Object info, int[] spentRes); //TODO Model with the end turn info, also the win condition checking should go to ChangeTurnAndGetInfo
	public void InitializePlayers();
	void EndTurnProcesses(int playerNo);
	Object ChangeTurnAndGetInfo(); //TODO change this into event?, probably in IBattle?
	int GetWhoseTurnNo();
	DateTime GetBattleDuration();
}