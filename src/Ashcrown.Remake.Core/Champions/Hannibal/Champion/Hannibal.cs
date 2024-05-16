using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Hannibal.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Hannibal.Champion;

public class Hannibal : ChampionBase<HannibalConstants>
{
    public Hannibal(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new TasteForBlood(this),
            new HealthFunnel(this),
            new SacrificialPact(this),
            new DemonicSkin(this));
    }
}