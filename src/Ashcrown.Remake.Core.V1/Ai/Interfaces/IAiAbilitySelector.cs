using Ashcrown.Remake.Core.V1.Ai.Models;

namespace Ashcrown.Remake.Core.V1.Ai.Interfaces;

public interface IAiAbilitySelector
{
    IAiController AiController { get; init; }
    IList<AiMaximizedAbility> SelectAbilities();
}