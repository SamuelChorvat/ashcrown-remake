using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Izrin.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Izrin.Champion;

public class Izrin : ChampionBase<IzrinConstants>
{
    public Izrin(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new Rake(this),
            new BloodyStrike(this),
            new Bite(this),
            new WillOfTheUndead(this));
    }
}