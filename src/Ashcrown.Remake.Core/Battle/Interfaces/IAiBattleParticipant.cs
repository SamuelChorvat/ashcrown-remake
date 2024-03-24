using Ashcrown.Remake.Core.Ability.Interfaces;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface IAiBattleParticipant : IBattleParticipant
{
    bool AiCanAnyoneTargetCounterAbility(IAbility ability);
    bool AiCanAnyoneTargetReflectAbility(IAbility ability);
}