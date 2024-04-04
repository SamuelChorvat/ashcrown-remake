using Ashcrown.Remake.Core.V1.Ai.Models;

namespace Ashcrown.Remake.Core.V1.Ai.Interfaces;

public interface IAiUtils
{
    static abstract AiMaximizedAbility GetHigherPointsAbility(AiMaximizedAbility? ability1, AiMaximizedAbility ability2);
}