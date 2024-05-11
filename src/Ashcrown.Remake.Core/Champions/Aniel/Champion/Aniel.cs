using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Aniel.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Aniel.Champion;

public class Aniel : ChampionBase<AnielConstants>
{
    public Aniel(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new Condemn(this),
            new EnchantedGarlicBomb(this),
            new BladeOfGluttony(this),
            new AdrenalineRush(this));
    }
}