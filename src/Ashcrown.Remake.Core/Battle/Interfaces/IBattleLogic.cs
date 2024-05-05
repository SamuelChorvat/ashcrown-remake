using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface IBattleLogic
{
	IBattleHistoryRecorder BattleHistoryRecorder { get; init; }
	IList<IChampion> DiedChampions { get; init; }
	DateTime StartTime { get; init; }
	DateTime? EndTime { get; }
	int TurnCount { get; }
	bool AiBattle { get; init; }
	IBattlePlayer[] BattlePlayers { init; }
	IBattlePlayer WhoseTurn { get;}
	void SetBattlePlayer(int playerNo, string playerName, string[] championNames, bool aiOpponent);
	IBattlePlayer GetBattlePlayer(int playerNo);
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
	void EndPlayerTurn(EndTurn endTurn);
	void EndAiTurn();
	void Surrender();
	TargetsUpdate GetTargets(GetTargets getTargets);
	UsableAbilitiesUpdate GetUsableAbilities(GetUsableAbilities getUsableAbilities);
	ExchangeEnergyUpdate ExchangeEnergy(ExchangeEnergy exchangeEnergy);
}