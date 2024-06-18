using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Nazuc.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Nazuc.Champion;

public class Nazuc : ChampionBase<NazucConstants>
{
    public Nazuc(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new SpearThrow(this),
            new SpearBarrage(this),
            new HuntersMark(this),
            new SeasonedStalker(this));
    }
}