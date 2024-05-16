using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hannibal.Champion;

namespace Ashcrown.Remake.Core.Champions.Hannibal.Abilities;

public class SacrificialPact : AbilityBase
{
    public SacrificialPact(IChampion champion) 
        : base(champion, 
            HannibalConstants.SacrificialPact, 
            0,
            [1,1,0,0,0], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Ally, 
            AbilityType.AllyBuff, 
            3)
    {
        Description = $"{HannibalConstants.Name} targets one ally, if their health drops to {"25 health".HighlightInGreen()} or below while {HannibalConstants.Name} is dead, " +
                      $"that character will heal to {"75 health".HighlightInGreen()}, will have all friendly and harmful effects {"removed".HighlightInPurple()} from them " +
                      $"and they will have all their abilities replaced by {HannibalConstants.Name}'s. This ability is {"invisible".HighlightInPurple()} and cannot be used while active.";
        Helpful = true;
        Buff = true;
        Invisible = true;

        ActiveEffectOwner = HannibalConstants.Name;
        ActiveEffectName = HannibalConstants.SacrificialPactActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if (IsReady())
        {
            Active = Owner.BattlePlayer.CheckActiveEffectPresentOnAny(HannibalConstants.SacrificialPactActiveEffect, 
                Owner.ChampionNo, Owner.BattlePlayer.PlayerNo) == null;
        }
    }

    //TODO Refactor this
    public static void RebornHannibal(IChampion victim)
    {
        if (!victim.SacrificialPactTriggered) {
            return;
        }

        victim.Alive = true;
        victim.Died = false;
        victim.Health = 75;
        victim.SacrificialPactTriggered = false;

        victim.Name = HannibalConstants.Name;

        victim.Abilities[0].Clear();
        victim.Abilities[1].Clear();
        victim.Abilities[2].Clear();
        victim.Abilities[3].Clear();

        victim.Abilities[0].Add(new TasteForBlood(victim));
        victim.Abilities[1].Add(new HealthFunnel(victim));
        victim.Abilities[2].Add(new SacrificialPact(victim));
        victim.Abilities[3].Add(new DemonicSkin(victim));

        victim.CurrentAbilities[0] = victim.Abilities[0][0];
        victim.CurrentAbilities[1] = victim.Abilities[1][0];
        victim.CurrentAbilities[2] = victim.Abilities[2][0];
        victim.CurrentAbilities[3] = victim.Abilities[3][0];
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetSpecialConditionPoints(AiCalculatorConstants.InfiniteNumberOfTurns);
        totalPoints = (int) Math.Round(totalPoints * ((float)(100 - Owner.Health)/100) * ((float) target.Health/100));
        return totalPoints;
    }
}