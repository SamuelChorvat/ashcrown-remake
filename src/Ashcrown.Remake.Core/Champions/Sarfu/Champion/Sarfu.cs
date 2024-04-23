using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champions.Sarfu.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Sarfu.Champion;

public class Sarfu : Core.Champion.Abstract.Champion
{
    public Sarfu(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, 
            SarfuConstants.Sarfu, SarfuConstants.Title, loggerFactory)
    {
        SetStartAbilities(
            new Overpower(this),
            new Charge(this),
            new Duel(this),
            new Deflect(this));
    }
}