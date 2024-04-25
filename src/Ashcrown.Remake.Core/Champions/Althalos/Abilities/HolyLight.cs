using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;

namespace Ashcrown.Remake.Core.Champions.Althalos.Abilities;

public class HolyLight : Ability.Abstract.Ability
{
    public HolyLight(IChampion champion) 
        : base(champion, 
            AlthalosConstants.HolyLight, 
            0,
            [1,0,0,0,0],
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Ally, 
            AbilityType.AllyHeal,
            2)
    {
        Heal1 = 25;
        Description = $"{AlthalosConstants.Name} channels the holy energy within him " +
                      $"and heals himself or one ally for {$"{Heal1} health".HighlightInGreen()}.";
        Helpful = true;
        SelfCast = true;
        Healing = true;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        return T.GetHealingPoints(Heal1, 1, this, target);
    }
}