using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lucifer.Champion;

namespace Ashcrown.Remake.Core.Champions.Lucifer.Abilities;

public class DarkChalice : AbilityBase
{
    public DarkChalice(IChampion champion) 
        : base(champion, 
            LuciferConstants.DarkChalice, 
            0,
            [1,1,0,0,0], 
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            3)
    {
        Damage1 = 35;
        
        Description = $"{LuciferConstants.Name} drinks from his chalice. {LuciferConstants.Name} will deal {$"{Damage1} magic damage".HighlightInBlue()} to all enemies when he dies. " +
                      $"This ability may only be used once and is {"invisible".HighlightInPurple()} until he dies. This ability becomes {LuciferConstants.CursedCrow.HighlightInGold()} when used.";
        Helpful = true;
        Buff = true;
        SelfCast = true;
        Invisible = true;
        MagicDamage = true;
        Damaging = true;

        ActiveEffectOwner = LuciferConstants.Name;
        ActiveEffectName = LuciferConstants.DarkChaliceActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if (!IsReady()) return;
        Active = !Owner.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LuciferConstants.DarkChaliceActiveEffect);
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetSpecialConditionPoints(AiCalculatorConstants.InfiniteNumberOfTurns);
        return totalPoints;
    }
}