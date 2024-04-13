using System.Text;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.Battle.Interfaces;

namespace Ashcrown.Remake.Core.Battle;

public class BattleHistoryRecorder(IBattleLogic battleLogic) : IBattleHistoryRecorder
{
    public IList<AbilityHistoryRecord> AbilityHistoryRecords { get; init; } = new List<AbilityHistoryRecord>();
    
    public void RecordInAbilityHistory(UsedAbility?[] used)
    {
	    foreach (var usedAbility in used)
	    {
		    if (usedAbility == null) continue;
		    var abilityRecord = new AbilityHistoryRecord()
		    {
			    TurnNo = battleLogic.TurnCount,
			    PlayerNo = battleLogic.WhoseTurn.PlayerNo,
			    PlayerName = battleLogic.WhoseTurn.PlayerName,
			    CasterName = battleLogic.WhoseTurn.Champions[usedAbility.ChampionNo - 1].Name,
			    AbilityName = battleLogic.WhoseTurn.Champions[usedAbility.ChampionNo - 1]
				    .AbilityController.GetCurrentAbility(usedAbility.AbilityNo).Name,
			    AbilityDescription = battleLogic.WhoseTurn.Champions[usedAbility.ChampionNo - 1]
				    .AbilityController.GetCurrentAbility(usedAbility.AbilityNo).Description,
			    AbilityCost = battleLogic.WhoseTurn.Champions[usedAbility.ChampionNo - 1]
				    .AbilityController.GetCurrentAbility(usedAbility.AbilityNo).GetCurrentCost(),
			    AbilityClasses = battleLogic.WhoseTurn.Champions[usedAbility.ChampionNo - 1]
				    .AbilityController.GetCurrentAbility(usedAbility.AbilityNo).AbilityClasses,
			    AbilityCooldown = battleLogic.WhoseTurn.Champions[usedAbility.ChampionNo - 1]
				    .AbilityController.GetCurrentAbility(usedAbility.AbilityNo).GetCurrentCooldown(),
			    Invisible = battleLogic.WhoseTurn.Champions[usedAbility.ChampionNo - 1]
				    .AbilityController.GetCurrentAbility(usedAbility.AbilityNo).Invisible
		    };
		    
		    for (var i = 0; i < usedAbility!.Targets.Length; i++)
		    {
			    if (usedAbility!.Targets[i] != 1) continue;
			    if (i < 3) 
			    {
				    abilityRecord.TargetNames.Add($"{battleLogic.WhoseTurn.PlayerName}" +
				                                  $"_{battleLogic.WhoseTurn.Champions[i].Name}");
			    } 
			    else 
			    {
				    abilityRecord.TargetNames.Add($"{battleLogic.WhoseTurn.PlayerName}" +
				                                  $"_{battleLogic.GetOppositePlayer(battleLogic.WhoseTurn.PlayerNo).Champions[i - 3].Name}");
			    }
		    }

		    AbilityHistoryRecords.Add(abilityRecord);
	    }
    }

    public string GetAbilityHistoryString()
    {
	    var builder = new StringBuilder();
	    builder.Append("\n----------------------------------------------------------\n");
	    foreach (var abilityHistoryRecord in AbilityHistoryRecords)
	    {
		    builder.Append("turnNo -> ").Append(abilityHistoryRecord.TurnNo).Append('\n');
		    builder.Append("playerName -> ").Append(abilityHistoryRecord.PlayerName).Append('\n');
		    builder.Append("casterName -> ").Append(abilityHistoryRecord.CasterName).Append('\n');
		    builder.Append("abilityName -> ").Append(abilityHistoryRecord.AbilityName).Append('\n');
		    builder.Append("targetNames -> ");
		    foreach (var targetName in abilityHistoryRecord.TargetNames) {
			    builder.Append(targetName).Append(' ');
		    }
		    builder.Append('\n');
		    builder.Append("----------------------------------------------------------\n");
	    }

	    return builder.ToString();
    }
}