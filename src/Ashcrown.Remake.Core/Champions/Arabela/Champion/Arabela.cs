using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Arabela.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Arabela.Champion;

public class Arabela : ChampionBase<ArabelaConstants>
{
    public Arabela(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new DivineTrap(this),
            new FlashHeal(this),
            new PrayerOfHealing(this),
            new HolyShield(this));
    }
}