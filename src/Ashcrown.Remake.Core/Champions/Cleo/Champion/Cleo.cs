using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Cleo.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Cleo.Champion;

public class Cleo : ChampionBase<CleoConstants>
{
    public Cleo(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new VenomousSting(this),
            new ChitinRegeneration(this),
            new Hive(this),
            new SwarmSummoning(this));
        
        Abilities[2].Add(new HealingSalve(this));
    }
}