using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Battle.Enums;
using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound.Validators;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;
using Ashcrown.Remake.Core.Champion.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Battle;

public class BattleLogic : IBattleLogic
{
    private readonly ILogger<BattleLogic> _logger;
    private readonly ITeamFactory _teamFactory;
    private readonly IValidator<EndTurn> _endTurnValidator;
    private readonly IValidator<GetTargets> _getTargetsValidator;
    private readonly IValidator<GetUsableAbilities> _getUsableAbilitiesValidator;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IAiController _aiController;

    public BattleLogic(bool aiBattle,
        ILoggerFactory loggerFactory)
    {
        _teamFactory = new TeamFactory();
        _endTurnValidator = new EndTurnValidator();
        _getTargetsValidator = new GetTargetsValidator();
        _getUsableAbilitiesValidator = new GetUsableAbilitiesValidator();
        _loggerFactory = loggerFactory;
        _logger = loggerFactory.CreateLogger<BattleLogic>();
        BattleHistoryRecorder = new BattleHistoryRecorder(this);
        AiBattle = aiBattle;
        _aiController = new AiController(this, 
            new AiAbilitySelector(this),
            new AiEnergySelector(this, new AiEnergyUsageController()), 
            _loggerFactory.CreateLogger<AiController>());
    }
    
    public IBattleHistoryRecorder BattleHistoryRecorder { get; init; }
    public IList<IChampion> DiedChampions { get; init; } = new List<IChampion>();
    public DateTime StartTime { get; init; } = DateTime.UtcNow;
    public DateTime? EndTime { get; private set; }
    public int TurnCount { get; private set; }
    public bool AiBattle { get; init; }
    public IBattlePlayer[] BattlePlayers { get; init; } = new IBattlePlayer[2];
    public IBattlePlayer WhoseTurn { get; private set; } = null!;

    public void SetBattlePlayer(int playerNo, string playerName, string[] championNames, bool aiOpponent)
    {
        BattlePlayers[playerNo - 1] = new BattlePlayer(playerNo, playerName, aiOpponent, championNames, 
            this, _teamFactory, _loggerFactory);
        if(playerNo == 1) WhoseTurn = BattlePlayers[0];
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

    public IBattlePlayer GetAiOpponentBattlePlayer()
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

    public bool AbilitiesUsed(int playerNo, EndTurn endTurn, int[] spentEnergy)
    {
        if (EndTime is not null)
        {
            throw new Exception($"{nameof(AbilitiesUsed)} can't be called as the battle ended");
        }
        
        var validationResults = _endTurnValidator.Validate(endTurn);

        if (!validationResults.IsValid)
        {
            _logger.LogError("EndTurn validation failed -> {validationResults}",validationResults.ToString());
            return false;
        }

        var usedAbilities = new UsedAbility?[3];

        foreach (var endTurnAbility in endTurn.EndTurnAbilities!)
        {
            usedAbilities[(int) (endTurnAbility.Order - 1)!] = new UsedAbility
            {
                AbilityNo = (int) endTurnAbility.AbilityNo!,
                ChampionNo = (int) endTurnAbility.CasterNo!,
                Targets = endTurnAbility.Targets!
            };
        }

        if (!IsCostValid(playerNo, spentEnergy, usedAbilities))
        {
            _logger.LogError("Invalid cost");
            return false;
        }

        if (!AreTargetsValid(playerNo, usedAbilities))
        {
            _logger.LogError("Invalid targets problem");
            return false;
        }
        
        BattleHistoryRecorder.RecordInAbilityHistory(usedAbilities);
        
        foreach (var usedAbility in usedAbilities)
        {
            if (usedAbility == null) continue;
            if (!GetBattlePlayer(playerNo).ChampionUseAbility(usedAbility.ChampionNo, usedAbility.AbilityNo, usedAbility.Targets)) {
                _logger.LogError("Use ability problem champNo = {ChampionNo} ({ChampionName}), abilityNo = {AbilityNo} ({AbilityName})", 
                    usedAbility.ChampionNo,
                    GetBattlePlayer(playerNo).Champions[usedAbility.ChampionNo - 1].Name, 
                    usedAbility.AbilityNo, 
                    GetBattlePlayer(playerNo).Champions[usedAbility.ChampionNo - 1].AbilityController.GetCurrentAbility(usedAbility.AbilityNo).Name);
                _logger.LogError("{AbilityHistoryString}",BattleHistoryRecorder.GetAbilityHistoryString());
                return false;
            }
            ProcessDeaths();
        }

        return true;
    }

    public void InitializePlayers()
    {
        WhoseTurn.GainGoingFirstEnergy();
    }

    public void EndTurnProcesses(int playerNo)
    {
        if (EndTime is not null)
        {
            throw new Exception($"{nameof(EndTurnProcesses)} can't be called as the battle ended");
        }
        
        GetBattlePlayer(playerNo).TriggerEndTurnMethods();
        ProcessDeaths();
        GetOppositePlayer(playerNo).TriggerStartTurnMethods();
        ProcessDeaths();
        GetBattlePlayer(playerNo).CheckResume();
        GetOppositePlayer(playerNo).CheckResume();

        if ((BattlePlayers[0].IsDead() && BattlePlayers[1].IsDead()) 
            || TurnCount == BattleConstants.TurnLimit) {
            OnBattleEnded();
        } else if (BattlePlayers[0].IsDead() || BattlePlayers[1].IsDead()) {
            OnBattleEnded(BattlePlayers[0].IsDead() 
                ? BattlePlayers[1] : BattlePlayers[0]);
        } else {
            GetOppositePlayer(playerNo).GenerateEnergy();
            OnTurnChanged();
        }
    }

    public void EndBattleOnAiError(string errorMessage)
    {
        _logger.LogError("Ending battle due to AI error -> {ErrorMessage}", errorMessage);
        OnBattleEnded(GetHumanBattlePlayer());
    }

    public void EndPlayerTurn(EndTurn endTurn)
    {
        if (EndTime is not null)
        {
            throw new Exception($"{nameof(EndPlayerTurn)} can't be called as the battle ended");
        }
        
        if (WhoseTurn.PlayerNo != GetHumanBattlePlayer().PlayerNo)
        {
            _logger.LogError("Can't end turn during AI turn");
            return;
        }
        
        var validationResults = _endTurnValidator.Validate(endTurn);

        if (!validationResults.IsValid)
        {
            _logger.LogError("Validation failed -> {validationResults}",validationResults.ToString());
            return;
        }
        
        GetHumanBattlePlayer().SpendEnergy(endTurn.SpentEnergy!);
        AbilitiesUsed(GetHumanBattlePlayer().PlayerNo, endTurn, endTurn.SpentEnergy!);
        EndTurnProcesses(GetHumanBattlePlayer().PlayerNo);
    }

    public void EndAiTurn()
    {
        if (EndTime is not null)
        {
            throw new Exception($"{nameof(EndAiTurn)} can't be called as the battle ended");
        }
        
        _aiController.EndBattleTurn();
    }

    private void OnTurnChanged()
    {
        ChangeWhoseTurn();
        //TurnChanged?.Invoke(this, GetHumanBattlePlayer().GetPlayerUpdate(WhoseTurn));
    }

    public void Surrender()
    {
        OnBattleEnded(GetAiOpponentBattlePlayer());
    }
    
    public TargetsUpdate GetTargets(GetTargets getTargets)
    {
        if (WhoseTurn.PlayerNo != GetHumanBattlePlayer().PlayerNo)
        {
            throw new Exception("Can't GetTargets during AI turn");
        }
        
        var validationResults = _getTargetsValidator.Validate(getTargets);

        if (!validationResults.IsValid)
        {
            throw new Exception($"GetTargets validation failed -> {validationResults}");
        }

        return GetHumanBattlePlayer().GetTargets((int) getTargets.ChampionNo!, (int) getTargets.AbilityNo!);
    }
    
    public UsableAbilitiesUpdate GetUsableAbilities(GetUsableAbilities getUsableAbilities)
    {
        if (WhoseTurn.PlayerNo != GetHumanBattlePlayer().PlayerNo)
        {
            throw new Exception("Can't GetUsableActivities during AI turn");
        }
        
        var validationResults = _getUsableAbilitiesValidator.Validate(getUsableAbilities);

        if (!validationResults.IsValid)
        {
            throw new Exception($"GetUsableAbilities validation failed -> {validationResults}");
        }

        return GetHumanBattlePlayer().GetUsableAbilities(getUsableAbilities.CurrentEnergy!, (int) getUsableAbilities.ToSubtract!);
    }
    
    public ExchangeEnergyUpdate ExchangeEnergy(ExchangeEnergy exchangeEnergy)
    {
        if (WhoseTurn.PlayerNo != GetHumanBattlePlayer().PlayerNo)
        {
            throw new Exception("Can't ExchangeEnergy during AI turn");
        }

        var exchangeEnergyValidator = new ExchangeEnergyValidator(GetHumanBattlePlayer());
        var validationResults =exchangeEnergyValidator.Validate(exchangeEnergy);

        if (!validationResults.IsValid)
        {
            throw new Exception($"ExchangeEnergy validation failed -> {validationResults}");
        }

        if (!GetHumanBattlePlayer().SpendEnergy(exchangeEnergy.SpentEnergy!))
        {
            throw new Exception($"ExchangeEnergy failed spending energy");
        }

        for (var i = 0; i < 4; i++)
        {
            GetHumanBattlePlayer().Energy[i] += exchangeEnergy.WantedEnergy![i];
        }

        return new ExchangeEnergyUpdate
        {
            NewEnergy = GetHumanBattlePlayer().Energy,
            UsableAbilitiesUpdate = GetHumanBattlePlayer().GetUsableAbilities(GetHumanBattlePlayer().Energy, 0)
        };
    }
    
    private void OnBattleEnded(IBattlePlayer? winner = null)
    {
        EndTime = DateTime.UtcNow;
        var battleEndedUpdate = new BattleEndedUpdate();
        
        if (winner == null)
        {
            battleEndedUpdate.BattleEndedState = BattleEndedState.Tie;
        } 
        else if (winner.AiOpponent)
        {
            battleEndedUpdate.BattleEndedState = BattleEndedState.Defeat;
        }
        else
        {
            battleEndedUpdate.BattleEndedState = BattleEndedState.Victory;
        }
        
        //BattleEnded?.Invoke(this, battleEndedUpdate);
    }

    private bool IsCostValid(int playerNo, IEnumerable<int> spentEnergy, IEnumerable<UsedAbility?> usedAbilities)
    {
        var tempToSpend = spentEnergy.ToArray();
        var toSubtract = 0;
        foreach (var usedAbility in usedAbilities)
        {
            if (usedAbility == null) {
                continue;
            }
            var abilityCost = GetBattlePlayer(playerNo).GetAbility(usedAbility.ChampionNo, usedAbility.AbilityNo).GetCurrentCost();
            toSubtract += abilityCost[^1];
            for (var j = 0; j < abilityCost.Length - 1; j++) {
                if (tempToSpend[j] < abilityCost[j]) {
                    _logger.LogError("Cost problem 1");
                    return false;
                }
                tempToSpend[j] -= abilityCost[j];
            }
        }
        
        var totalTempToSpend = 0;
        foreach (var energyQuantity in tempToSpend)
        {
            if (energyQuantity < 0) {
                _logger.LogError("Cost problem 2");
                return false;
            }
            totalTempToSpend += energyQuantity;
        }

        if (totalTempToSpend < toSubtract ) {
            _logger.LogError("Cost problem 3");
            return false;
        }

        return true;
    }

    private bool AreTargetsValid(int playerNo, IEnumerable<UsedAbility?> usedAbilities)
    {
        return !(from usedAbility in usedAbilities.OfType<UsedAbility>() 
            let validTargets = GetBattlePlayer(playerNo).Champions[usedAbility.ChampionNo - 1]
                .AbilityController.GetPossibleTargetsForAbility(usedAbility.AbilityNo) 
            where !AreTargetsValidHelper(GetBattlePlayer(playerNo).Champions[usedAbility.ChampionNo - 1]
                .AbilityController.GetCurrentAbility(usedAbility.AbilityNo).Target, validTargets, usedAbility.Targets) 
            select usedAbility).Any();
    }
    
    private bool AreTargetsValidHelper(AbilityTarget target, IReadOnlyList<int> validTargets, IReadOnlyCollection<int> targets)
    {
        if (targets.Count != 6) {
            return false;
        }

        if (targets.Where((t, i) => t == 1 && validTargets[i] != 1).Any())
        {
            _logger.LogError("Invalid targets problem 1");
            return false;
        }

        if (GetTotalNumberOfTargets(targets) <= 0) {
            _logger.LogError("Invalid targets problem 2");
            return false;
        }

        switch (target) {
            case AbilityTarget.Self:
            case AbilityTarget.Ally:
            case AbilityTarget.Enemy:
            case AbilityTarget.AllyOrEnemy:
                if (GetTotalNumberOfTargets(targets) != 1) {
                    _logger.LogError("Invalid targets problem 3");
                    return false;
                }
                break;
            case AbilityTarget.Allies:
            case AbilityTarget.Enemies:
            case AbilityTarget.All:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(target), target, null);
        }

        return true;
    }

    private static int GetTotalNumberOfTargets(IEnumerable<int> targets)
    {
        return targets.Count(t => t == 1);
    }

    private void ChangeWhoseTurn()
    {
        WhoseTurn = WhoseTurn == BattlePlayers[0] ? BattlePlayers[1] : BattlePlayers[0];
        TurnCount += 1;
    }
    
    private IBattlePlayer GetHumanBattlePlayer()
    {
        return BattlePlayers[0].AiOpponent ? BattlePlayers[1] : BattlePlayers[0];
    }
}