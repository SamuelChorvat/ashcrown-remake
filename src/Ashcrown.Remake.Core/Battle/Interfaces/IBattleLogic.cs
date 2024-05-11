using Ashcrown.Remake.Core.Battle.Enums;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface IBattleLogic
{
	IBattleHistoryRecorder BattleHistoryRecorder { get; init; }
	IList<IChampion> DiedChampions { get; init; }
	DateTime BattleStartTime { get; init; }
	DateTime? BattleEndTime { get; }
	DateTime TurnStartTime { get; }
	int TurnCount { get; }
	bool AiBattle { get; init; }
	IBattlePlayer[] BattlePlayers { init; }
	PlayerUpdate[] LatestPlayerUpdates { get; set; }
	BattleStatus[]? BattleEndedUpdates { get; set; }
	IBattlePlayer WhoseTurn { get;}
	void SetBattlePlayer(int playerNo, string playerName, string[] championNames, bool aiOpponent);
	IBattlePlayer GetBattlePlayer(int playerNo);
	int GetBattlePlayerNo(string playerName);
	IBattlePlayer GetOppositePlayer(int playerNo);
	int GetOppositePlayerNo(int playerNo);
	int GetAiOpponentPlayerNo();
	IBattlePlayer GetAiOpponentBattlePlayer();
	void ProcessDeaths();
	bool IsPlayerDead(int playerNo);
	bool AbilitiesUsed(int playerNo, EndTurn endTurn, int[] spentEnergy);
	public void InitializePlayers();
	void EndTurnProcesses(int playerNo);
	void EndBattleOnAiError(string errorMessage);
	void EndPlayerTurn(int playerNo, EndTurn endTurn);
	void EndAiTurn();
	void Surrender(int playerNo);
	TargetsUpdate GetTargets(int playerNo, GetTargets getTargets);
	UsableAbilitiesUpdate GetUsableAbilities(int playerNo, GetUsableAbilities getUsableAbilities);
	ExchangeEnergyUpdate ExchangeEnergy(int playerNo, ExchangeEnergy exchangeEnergy);
}