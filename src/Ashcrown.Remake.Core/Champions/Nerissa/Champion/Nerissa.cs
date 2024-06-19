using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Nerissa.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Nerissa.Champion;

public class Nerissa : ChampionBase<NerissaConstants>
{
    public Nerissa(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new Overflow(this),
            new Drown(this),
            new AncientSpirits(this),
            new MesmerizingWater(this));
    }
}