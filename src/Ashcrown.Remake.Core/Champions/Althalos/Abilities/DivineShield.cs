using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;

namespace Ashcrown.Remake.Core.Champions.Althalos.Abilities;

public class DivineShield : Ability.Abstract.Ability
{
    public DivineShield(IChampion champion) : base(champion, 
        AlthalosConstants.DivineShield, 
        4,
        [0,0,0,0,1],
        [AbilityClass.Strategic, AbilityClass.Instant], 
        AbilityTarget.Self, 
        AbilityType.AllyBuff)
    {
        Duration1 = 1;
        Description = $"This ability makes {AlthalosConstants.Althalos} " +
                      $"{"invulnerable".HighlightInPurple()} for {Duration1} turn.";
        SelfCast = true;
        Invulnerability = true;
        TypeOfInvulnerability = [AbilityClass.All];
        Helpful = true;
        Buff = true;
        ActiveEffectOwner = AlthalosConstants.Althalos;
        ActiveEffectName = AlthalosConstants.DivineShieldActiveEffect;
        AiStandardSelfInvulnerability = true;
    }
}