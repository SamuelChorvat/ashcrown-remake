using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Evanore.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Evanore.Champion;

public class Evanore : ChampionBase<EvanoreConstants>
{
    public Evanore(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new MonstrousBear(this),
            new MurderOfCrows(this),
            new UmbraWolf(this),
            new HoundDefense(this));
    }
}