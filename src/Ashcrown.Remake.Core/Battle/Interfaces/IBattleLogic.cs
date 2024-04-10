﻿using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface IBattleLogic
{
	event EventHandler<PlayerUpdate>? TurnChanged;
	event EventHandler<BattleEndedUpdate>? BattleEnded;
	IBattleHistoryRecorder BattleHistoryRecorder { get; init; }
	IList<IChampion> DiedChampions { get; init; }
	DateTime StartTime { get; init; }
	DateTime EndTime { get; }
	int TurnCount { get; }
	bool AiBattle { get; init; }
	IBattlePlayer[] BattlePlayers { init; }
	IBattlePlayer WhoseTurn { get;}
	void SetBattlePlayer(int playerNo, string[] championNames, bool aiOpponent);
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
}